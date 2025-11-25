using ApiSplit.Requests;
using FluentValidation;

namespace ApiSplit.Validators;


public class PerfumeRequestValidator : AbstractValidator<PerfumeRequest>
{
    public PerfumeRequestValidator()
    {
        RuleFor(p => p.Volume).NotNull().GreaterThanOrEqualTo(50U);
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
    }
}