using Application.Features.User_Features.Commands;
using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .NotEmpty().WithMessage("Please enter Mobile No.");

            RuleFor(nu => nu.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");
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
