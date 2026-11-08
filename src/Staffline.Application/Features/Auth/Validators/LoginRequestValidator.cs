using FluentValidation;
using Staffline.Application.Features.Auth.Contracts;

namespace Staffline.Application.Features.Auth.Validators;

public sealed class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(128);
    }
}