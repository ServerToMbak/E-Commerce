using System.Collections.Generic;

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