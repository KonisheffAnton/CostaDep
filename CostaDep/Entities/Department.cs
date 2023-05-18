using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Costa.Models;

namespace Costa.Entities
{
    public class Department
    {
        [Required]
        public int Id { get; set; }

        public int? ParentDepartmentId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Department ParentDepartment { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Department> ChildDepartments { get; set; }

        public Department()
        {
            Employees = new Collection<Employee>();
            ChildDepartments = new Collection<Department>();
        }
    }
}
