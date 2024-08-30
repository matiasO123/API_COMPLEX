using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client: AuditableBaseEntity
    {    
        //public int Id { get; set; }
        public string? name { get; set; }
        public DateTime birthDate { get; set; } 
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? adress { get; set; }

        public int age;

        public int Age
        {
            get
            {
                if (this.age <= 0)
                    this.age = new DateTime(DateTime.Now.Subtract(this.birthDate).Ticks).Year - 1;
                return this.age;
            }
        }
    }
}
