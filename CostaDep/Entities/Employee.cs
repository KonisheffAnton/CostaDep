using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Costa.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(4)]
        public string DocSeries { get; set; }

        [Required]
        [MaxLength(6)]
        public string DocNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        public Department Department { get; set; }

    }
}
