using AirportCalculateDistanceApi.Infrastructure.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AirportCalculateDistanceApi.Filters
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorCodes = context.ModelState.Values.Select(value => value.Errors.Where(a => !string.IsNullOrWhiteSpace(a.ErrorMessage))
                        .Select(a => a.ErrorMessage)
                        .FirstOrDefault())
                    .Where(errorCode => !string.IsNullOrWhiteSpace(errorCode))
                    .ToList();

                throw new DomainException(errorCodes.Select(a => new Error(a)).ToList());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}