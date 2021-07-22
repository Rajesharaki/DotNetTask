using FluentValidation;
using FluentValidationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationProject.Validation
{
    public class CustomerValidator:AbstractValidator<NewCustomer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.EmailAddress).NotNull();
            RuleFor(x => x.CustomerName).NotNull().WithMessage("Customer Name Should not be null");
        }
    }
}
