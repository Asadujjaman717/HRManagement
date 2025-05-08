using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class Employee
    {
        [Key]
        public Guid EmpId { get; set; }  // PK

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; }  // FK

        [Required(ErrorMessage = "Employee Code is required.")]
        public string EmpCode { get; set; }  // Must be unique

        [Required(ErrorMessage = "Employee Name is required.")]
        public string EmpName { get; set; }

        [Required]
        [ForeignKey(nameof(Shift))]
        public Guid ShiftId { get; set; }  // FK under company

        [Required]
        [ForeignKey(nameof(Department))]
        public Guid DeptId { get; set; }   // FK under company

        [Required]
        [ForeignKey(nameof(Designation))]
        public Guid DesigId { get; set; }  // FK under company

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } // Text version ("Male", "Female", etc.)

        [Required(ErrorMessage = "Gross Salary is required.")]
        public double Gross { get; set; }  // User input

        public double Basic { get; set; }   // Auto calculated: Gross * Company Basic %
        public double HRent { get; set; }   // Auto calculated
        public double Medical { get; set; } // Auto calculated
        public double Others { get; set; }  // Others part (Gross - Basic - HRent - Medical)

        [Required(ErrorMessage = "Joining Date is required.")]
        [DataType(DataType.Date)]
        public DateTime dtJoin { get; set; } // Joining date

        // Navigation Properties
        public Company? Company { get; set; }
        public Shift? Shift { get; set; }
        public Department? Department { get; set; }
        public Designation? Designation { get; set; }
    }
}
