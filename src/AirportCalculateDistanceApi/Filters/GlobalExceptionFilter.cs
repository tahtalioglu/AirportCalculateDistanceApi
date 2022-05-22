using System.Net;
using AirportCalculateDistanceApi.Infrastructure.ExceptionHandling;
using AirportCalculateDistanceApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AirportCalculateDistanceApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(DomainException))
            {
                var globalResult = new BaseResponse<object>
                {
                    Errors = (((DomainException) context.Exception).ErrorList)
                };
                
                context.Result = new ObjectResult(globalResult);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }
            else
            {
                _logger.LogError($"{context.Exception.StackTrace}, {context.Exception},{context.Exception.Message}");
                
                var globalResult = new BaseResponse<object>
                {
                    Errors = new List<Error>
                        {
                            new Error
                            {
                                ErrorMessage = context.Exception.Message
                            }
                        }
                };

                context.Result = new ObjectResult(globalResult);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}