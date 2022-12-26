namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string mesage = null,string details = null) : base(statusCode, mesage)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}