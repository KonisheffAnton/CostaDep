using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Costa.Entities
{
    [Table("Empoyee")]
    public class Employee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? Id { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? Patronymic { get; set; }

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
