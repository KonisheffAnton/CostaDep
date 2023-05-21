using Costa.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Costa.Models
{
    public class DepartmentsDto
    {
        public Guid Id { get; set; }

        public Guid? ParentDepartmentId { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public bool Activity { get; set; } = false;

        public Department ParentDepartment { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Department> ChildDepartments { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
