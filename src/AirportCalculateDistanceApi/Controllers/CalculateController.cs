using AirportCalculateDistanceApi.Services;
using AirportCalculateDistanceApi.Models.Responses;
using AirportCalculateDistanceApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AirportCalculateDistanceApi.Controllers
{
    [Route("v1/calculates")]
    [Produces("application/json")]
    public class CalculateController : ControllerBase
    {
        private readonly ICalculateService _calculateService;
        private readonly ILogger<CalculateController> _logger;

        public CalculateController(ILogger<CalculateController> logger, ICalculateService calculateService)
        {
            _logger = logger;
            _calculateService = calculateService;
        }
         
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BaseResponse<double>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No value found for requested filter.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Request not accepted.")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "Access not allowed.")]
        [HttpGet]
        public async Task<IActionResult>  Get(CalculateRequest request)
        {
            var response = await _calculateService.Calculate(request);


            if (response == null)
            {
                return NotFound(response);
            }

            if (!response.HasError && response != null)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}