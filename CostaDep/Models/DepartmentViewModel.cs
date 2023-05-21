using Costa.Entities;

namespace Costa.Models
{
    public class DepartmentViewModel
    {
        public Guid DepartmentViewModelId { get; set; }

        public Guid? ParentDepartmentId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? EmployeesCount { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<DepartmentViewModel> ChildDepartments { get; set; }
    }
}
