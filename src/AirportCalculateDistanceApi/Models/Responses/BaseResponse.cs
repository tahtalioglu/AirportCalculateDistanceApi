using AirportCalculateDistanceApi.Infrastructure.ExceptionHandling;

namespace AirportCalculateDistanceApi.Models.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse() => Errors = new List<Error>();

        public bool HasError => Errors.Any();
        public List<Error> Errors { get; set; }
        public T Result { get; set; }
    }
}
