using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiValdationErrorResponse : ApiResponse
    {
        public ApiValdationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors {get; set;}
    }
}