using Employee.BLL.BOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Validators
{
    public class UpdateEmployeeBOValidator : AbstractValidator<UpdateEmployeeBO>
    {
        public UpdateEmployeeBOValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
            RuleFor(x => x.HireDate)
                .Must(date => date <= DateTime.UtcNow).WithMessage("Hire date cannot be in the future.");
        }
    }
}
