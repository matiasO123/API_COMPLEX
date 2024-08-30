using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.dto
{
    public class ClientDTO
    {
        public int Id {  get; set; }
        public string? name { get; set; }
        public DateTime? birthDate { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? adress { get; set; }

        //public int age;
    }
}
