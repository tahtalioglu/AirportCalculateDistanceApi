using AirportCalculateDistanceApi.Models.Requests;
using FluentValidation;
using AirportCalculateDistanceApi.Infrastructure.Constant;

namespace AirportCalculateDistanceApi.Infrastructure.Validators
{

    public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
    {
        public CalculateRequestValidator()
        {
            RuleFor(p => p.FirstAirport).Must(q=>!string.IsNullOrEmpty(q)).WithMessage(ErrorCodeConstants.AirportReqired);
            RuleFor(p => p.SecondAirport).Must(q => !string.IsNullOrEmpty(q)).WithMessage(ErrorCodeConstants.AirportReqired);
            RuleFor(p => p.FirstAirport).Must(q => q.Length ==3).When(r=>r.FirstAirport != null).WithMessage(ErrorCodeConstants.WrongIATAFormat);
            RuleFor(p => p.SecondAirport).Must(q => q.Length == 3).When(r => r.FirstAirport != null).WithMessage(ErrorCodeConstants.WrongIATAFormat);
        }
    }
}
