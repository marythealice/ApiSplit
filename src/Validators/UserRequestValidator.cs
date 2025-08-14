using ApiSplit.Requests;
using FluentValidation;

namespace ApiSplit.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(u => u.Name).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u => u.Address.RecipientName).NotEmpty();
        RuleFor(u => u.Address.StreetName).NotEmpty();
        RuleFor(u => u.Address.StreetNumber).NotEmpty();
        RuleFor(u => u.Address.ApartmentNumber).NotNull();
        RuleFor(u => u.Address.State).NotEmpty();
        RuleFor(u => u.Address.City).NotEmpty();
        RuleFor(u => u.Address.ZipCode)
        .NotEmpty()
        .MinimumLength(8);
    }
}