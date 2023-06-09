﻿using FluentValidation;

namespace Costa.Models.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(employee => employee.SurName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(employee => employee.Patronymic).NotNull().NotEmpty().MaximumLength(50);

            RuleFor(employee => employee.DateOfBirth)
    .InclusiveBetween(
        DateTime.Now.AddYears(-65), DateTime.Now.AddYears(-18));

            RuleFor(employee => employee.Position).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(employee => employee.DocNumber).NotNull().NotEmpty().MaximumLength(6);
            RuleFor(employee => employee.DocSeries).NotNull().NotEmpty().MaximumLength(4);
            RuleFor(employee => employee.DepartmentId).NotNull().NotEmpty();
        }
    }
}
