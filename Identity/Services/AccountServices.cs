using Application.dto;
using Application.Enums;
using Application.exception;
using Application.Interfaces;
using Application.wrapper;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AccountServices : IAccountServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly jwt _jwt;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDateTimeService _dateTimeService;

        public AccountServices(UserManager<ApplicationUser> userManager, IOptions<jwt> jwt, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticationAsync(AuthenticationRequest request, string ipAdress)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null) throw new ApiException("Email not registered");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.password, false, lockoutOnFailure: false);

            if (!result.Succeeded) throw new ApiException("user name or password not valid");

            JwtSecurityToken jwt_st = await GenerateJWT_Token(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwt_st);
            response.email = user.Email;
            response.UserName = user.UserName;
            var rolsList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolsList.ToList();
            response.isVerified = user.EmailConfirmed;

            var refreshtoken = GenerateRefreshToken(ipAdress);
            response.RefleshToken = refreshtoken.Token;
            return new Response<AuthenticationResponse>(response, "User authenticated");
        }
        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var user_with_same_user_name = await _userManager.FindByNameAsync(request.userName);
            if (user_with_same_user_name != null)
            {
                throw new ApiException("user name has already been taken");
            }

            var user = new ApplicationUser
            {
                Email = request.email,
                name = request.name,
                UserName = request.userName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user_with_same_mail = await _userManager.FindByEmailAsync(request.email);
            if (user_with_same_mail != null) throw new ApiException("this email already has an account");
            else
            {
                var result = await _userManager.CreateAsync(user, request.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Rols.Basic.ToString());
                    return new Response<string>(user.Id, message: "User registered!");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }


        private async Task<JwtSecurityToken> GenerateJWT_Token(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAdress = IpHelper.GetIpAdress();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAdress),
            }.Union(userClaims).Union(roleClaims);


            var simmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(simmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                createdByIp = ipAddress,
                Created = DateTime.Now
            };
        }

        private string RandomTokenString()
        {
            /*using var rngCripto_sp = new RSACryptoServiceProvider();
            var randomBytes = new Byte[40];
            rngCripto_sp.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-","");*/
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[40];
            rng.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
