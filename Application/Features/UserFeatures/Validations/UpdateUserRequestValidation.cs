using Application.Features.UserFeatures.Commands;
using Application.Models.Users;
using FluentValidation;

namespace Application.Features.User_Features.Validations
{
    public class UpdateUserValidataion : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidataion()
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
        }
    }

    public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidation()
        {
            RuleFor(request => request.UserRequest)
                .SetValidator(new UpdateUserValidataion());
        }
    }
}
