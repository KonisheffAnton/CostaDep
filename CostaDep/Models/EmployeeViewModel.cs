using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Costa.Validation;

namespace Costa.Models
{
    public class EmployeeViewModel
    {
        [HiddenInput]
        public int? EmployeeViewModelId { get; set; }

        public Guid? DepartmentId { get; set; }

        [MaxLength(50)]
        public string SurName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? Patronymic { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [BirthTimeCastom]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(4)]
        public string DocSeries { get; set; }

        [MaxLength(6)]
        public string DocNumber { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        public SelectList? DepartmentList { get; set; }

        public int SelectedDepartment { get; set; }
    }
}
