using Costa.Entities;
using Costa.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Costa.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            var departments = new List<Department>();
            var employees = new List<Employee>();

            if (!context.Departments.Any())
            {
                for (int i = 1; i <= 9; i++)
                {
                    var department = new Department
                    {
                        Name = $"Department {i}",
                        Code = $"Code {i}"
                    };

                    if (i > 1)
                    {
                        int randomParentIndex = new Random().Next(1, i);
                        department.ParentDepartment = departments[randomParentIndex - 1];
                    }

                    departments.Add(department);
                    context.Departments.Add(department);
                    context.SaveChanges();
                }
            }

            if (!context.Employees.Any())
            {
                for (int i = 0; i <= 9; i++)
                {
                    var employee = new Employee
                    {
                        Department = departments[new Random().Next(0, 8)],
                        FirstName = $"Employee {i}",
                        SurName = $"SurName {i}",
                        Patronymic = $"Patronymic {i}",
                        DateOfBirth = RandomBirthday(),
                        DocSeries = $"800{i}",
                        DocNumber = $"43514{i}",
                        Position = $"Position {i}",
                    };

                    employees.Add(employee);
                }
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

        }

        public static DateTime RandomBirthday()
        {
            var minDate = DateTime.Today.AddYears(-65);
            var maxDate = DateTime.Today.AddYears(-18);
            var range = (maxDate - minDate).Days;
            var random = new Random();
            var randomDays = random.Next(range);

            return minDate.AddDays(randomDays);
        }
    }
}