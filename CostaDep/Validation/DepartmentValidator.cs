using Costa.Entities;
using FluentValidation;
using System;

namespace Costa.Models.Validation
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(department => department.Name).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(department => department.Code).NotEmpty().NotNull().MaximumLength(10);
        }
    }
}
