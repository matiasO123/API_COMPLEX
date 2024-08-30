using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.dto
{
    public class RefreshToken
    {
        public int id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now >= Expires;
        public DateTime Created     { get; set; }
        public string createdByIp {  get; set; }    
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string replacedBytoken { get; set; }
        public bool isActive => Revoked == null && !IsExpired;

        
    }
}
