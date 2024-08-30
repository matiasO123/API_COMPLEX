using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.wrapper
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string message = null)
        {
            succeeded = true;
            message = message ?? string.Empty;
            this.data = data;
        }

        public bool succeeded { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }

        public T data { get; set; }
       
    }
}
