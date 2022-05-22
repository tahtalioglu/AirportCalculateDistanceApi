using AirportCalculateDistanceApi.Models.Requests;
using AirportCalculateDistanceApi.Models.Responses;
namespace AirportCalculateDistanceApi.Services
{
    public interface ICalculateService
    {
        Task<BaseResponse<double>> Calculate(CalculateRequest request);
        
    }
}
