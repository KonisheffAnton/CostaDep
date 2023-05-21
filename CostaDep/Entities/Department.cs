using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Costa.Entities
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentDepartmentId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Department? ParentDepartment { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Department> ChildDepartments { get; set; }

        public Department()
        {
            Employees = new Collection<Employee>();
            ChildDepartments = new Collection<Department>();
        }
    }
}
