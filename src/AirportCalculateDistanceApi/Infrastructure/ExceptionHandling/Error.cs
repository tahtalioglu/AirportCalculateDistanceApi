namespace AirportCalculateDistanceApi.Infrastructure.ExceptionHandling
{
    public class Error
    {
        public Error()
        {
            
        }
        public Error(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public Error(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}