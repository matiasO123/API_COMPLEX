using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.dto
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public List<string> Roles { get; set; }
        public bool isVerified { get; set; }
        public string JWToken { get; set; }

        [JsonIgnore]
        public string RefleshToken { get; set; }
    }
}
