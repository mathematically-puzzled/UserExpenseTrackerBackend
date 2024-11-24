using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using FluentValidation;

namespace Application.Features.User_Features.Validations
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        public NewUserValidator()
        {
            RuleFor(nu => nu.Name)
                .NotEmpty().WithMessage("Name field cannot be empty")
                .MaximumLength(20).WithMessage("Name field cannot be greater than 40 characters");

            RuleFor(nu => nu.Currency)
                .NotEmpty().WithMessage("Currency field cannot be empty");

            RuleFor(nu => nu.BankBalance)
                .GreaterThanOrEqualTo(0).WithMessage("Bank balance must be defined or be 0");

            RuleFor(nu => nu.MobileNumber)
                .NotEmpty().WithMessage("Please enter Mobile No.")
                .Must(MobileNumber => MobileNumber.ToString().Length > 8 && MobileNumber.ToString().Length < 15)
                .WithMessage("Enter Mobile Number in correct format");

            RuleFor(nu => nu.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");

            RuleFor(nu => nu.EmailId)
                .NotEmpty().WithMessage("Email cannot be empty")
                .Matches("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").WithMessage("Email must be in Email@something.com format");
        }
    }
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidation()
        {
            RuleFor(request => request.UserRequest)
                .SetValidator(new NewUserValidator());
        }
    }
}
