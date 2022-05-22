namespace AirportCalculateDistanceApi.Infrastructure.ExceptionHandling
{
    public class DomainException : Exception
    {
        public DomainException(int errorCode)
        {
            ErrorList = new List<Error>
            {
                new Error
                {
                    ErrorCode = errorCode
                }
            };
        }

        public DomainException(List<int> errorCodes)
        {
            if (errorCodes != null && errorCodes.Any())
            {
                ErrorList = new List<Error>(errorCodes.Select(code => new Error
                {
                    ErrorCode = code
                }));
            }
        }
        
        public DomainException(List<Error> errorList)
        {
            ErrorList = new List<Error>(errorList);
        }
        
        public DomainException(Error error)
        {
            ErrorList = new List<Error> {error};
        }

        public List<Error> ErrorList { get; }
    }
}