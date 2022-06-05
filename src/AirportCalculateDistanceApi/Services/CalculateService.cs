using AirportCalculateDistanceApi.Models.Responses;
using AirportCalculateDistanceApi.Models.Requests;
using System.Text.Json;
using AirportCalculateDistanceApi.Infrastructure.Constant;
using GeoCoordinatePortable;
using AirportCalculateDistanceApi.Infrastructure.ExceptionHandling;

namespace AirportCalculateDistanceApi.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public CalculateService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        public async Task<BaseResponse<double>> Calculate(CalculateRequest request)
        {
            ControlInputs(request);
            var response = new BaseResponse<double>();

            Task<ApiResponse> firstTask = GetAirport(request.FirstAirport);
            Task<ApiResponse> secondTask = GetAirport(request.SecondAirport);

             await Task.WhenAll(firstTask, secondTask);

            if (firstTask is null)
            {
                response.Errors.Add(new Infrastructure.ExceptionHandling.Error() { ErrorMessage = String.Format(ErrorCodeConstants.AirportNotFound,request.FirstAirport) });
                return response;
            }
            if (secondTask is null)
            {
                response.Errors.Add(new Infrastructure.ExceptionHandling.Error() { ErrorMessage = String.Format(ErrorCodeConstants.AirportNotFound, request.SecondAirport) });
                return response;
            }

            response.Result= GetDistance(firstTask.Result.Location, secondTask.Result.Location);
            
            return response;
        }

        private void ControlInputs(CalculateRequest request)
        {
            if (string.IsNullOrEmpty(request.FirstAirport))
            {
                throw new DomainException(new Error(ErrorCodeConstants.AirportReqired));
            }
            if (request.FirstAirport.Length !=3)
            {
                throw new DomainException(new Error(ErrorCodeConstants.WrongIATAFormat));
            }
            if (string.IsNullOrEmpty(request.SecondAirport))
            {
                throw new DomainException(new Error(ErrorCodeConstants.AirportReqired));
            }
            if (request.SecondAirport.Length != 3)
            {
                throw new DomainException(new Error(ErrorCodeConstants.WrongIATAFormat));
            }
        }

        public async Task<ApiResponse> GetAirport(string iata)
        {
            var httpResponseMessage = await _client.GetAsync($"{Constants.UrlOfAirport}{iata}");
            var httpResponseStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ApiResponse>(httpResponseStream, _options);
        }
        static double GetDistance(Location location1, Location location2)
        {
            double lat1 = location1.Lat;
            double lon1 = location1.Lon;
            double lat2 = location2.Lat;
            double lon2 = location2.Lon;
            
            var coord1 = new GeoCoordinate(lat1, lon1);
            var coord2 = new GeoCoordinate(lat2, lon2);

            var distance = coord1.GetDistanceTo(coord2)/ Constants.MilePerKm;
            return distance;
        }
    }
}
