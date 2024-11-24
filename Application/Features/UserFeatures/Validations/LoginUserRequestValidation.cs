using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using FluentValidation;

namespace Application.Features.UserFeatures.Validations
{

    public class LoginUserParamsValidation : AbstractValidator<LoginUser>
    {
        public LoginUserParamsValidation()
        {
            RuleFor(eu => eu.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");

            RuleFor(eu => eu.UserEmail)
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").WithMessage("Email must be in Email@something.com format");
        }
    }

    public class LoginUserRequestValidation : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidation()
        {
            RuleFor(request => request.UserCredentials)
                .SetValidator(new LoginUserParamsValidation());
        }
    }
}
