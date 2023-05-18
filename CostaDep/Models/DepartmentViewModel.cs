using Costa.Entities;
using System.Collections.Generic;

namespace Costa.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentViewModelId { get; set; }

        public int? ParentDepartmentId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? EmployeesCount { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<DepartmentViewModel> ChildDepartments { get; set; }
    }
}
