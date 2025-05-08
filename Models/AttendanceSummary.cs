using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class AttendanceSummary
    {
        [Key]
        public Guid Id { get; set; } // Primary Key

        [Required]
        [ForeignKey(nameof(Employee))]
        public Guid EmpId { get; set; } // FK to Employee

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; } // FK to Company

        [Required]
        public int dtYear { get; set; } // e.g., 2024

        [Required]
        public int dtMonth { get; set; } // e.g., 9 for September

        [Required]
        public int Present { get; set; } // No of Present Days

        [Required]
        public int Late { get; set; } // No of Late Days

        [Required]
        public int Absent { get; set; } // No of Absent Days

        // Navigation properties
        public Employee Employee { get; set; }
        public Company Company { get; set; }
    }
}
