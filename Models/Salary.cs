using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class Salary
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public Guid EmpId { get; set; }

        [Required]
        public int dtYear { get; set; }

        [Required]
        public int dtMonth { get; set; }

        [Required]
        public double Gross { get; set; }

        [Required]
        public double Basic { get; set; }

        [Required]
        public double Hrent { get; set; }

        [Required]
        public double Medical { get; set; }

        [Required]
        public double AbsentAmount { get; set; } // (Basic / 30) * Absent

        [Required]
        public double PayableAmount { get; set; } // Gross - AbsentAmount

        public bool IsPaid { get; set; }

        public double PaidAmount { get; set; }

        // Navigation
        public Company Company { get; set; }
        public Employee Employee { get; set; }
    }
}
