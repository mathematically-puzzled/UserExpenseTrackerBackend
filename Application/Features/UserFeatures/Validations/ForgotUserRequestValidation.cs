using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using FluentValidation;

namespace Application.Features.UserFeatures.Validations
{
    public class ForgotUserValidation : AbstractValidator<ForgotUser>
    {
        public ForgotUserValidation()
        {
            RuleFor(fu => fu.MobileNumber)
                .NotEmpty().WithMessage("Please enter Mobile No.")
                .Must(MobileNumber => MobileNumber.ToString().Length > 8 && MobileNumber.ToString().Length < 15)
                .WithMessage("Enter Mobile Number in correct format");

            RuleFor(fu => fu.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");

            RuleFor(fu => fu.EmailId)
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").WithMessage("Email must be in Email@something.com format");
        }
    }
    public class ForgotUserRequestValidation : AbstractValidator<ForgotUserPasswordRequest>
    {
        public ForgotUserRequestValidation()
        {
            RuleFor(request => request.UserCredentials)
                .SetValidator(new ForgotUserValidation());
        }
    }
}
