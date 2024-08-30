using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.dto
{
    public class RegisterRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }    
        public string confirmPassword   { get; set; }

    }
}
