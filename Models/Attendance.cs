using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class Attendance
    {
        [Key]
        public Guid Id { get; set; }   // ✅ Guid as PK

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; }  // Company Id

        [Required]
        [ForeignKey(nameof(Employee))]
        public Guid EmpId { get; set; }  // Employee Id

        //[Required(ErrorMessage = "Attendance Date is required.")]
        //[DataType(DataType.Date)]
        //public DateTime dtDate { get; set; }  // Date (yyyy-MM-dd)

        [Required(ErrorMessage = "Attendance Date is required.")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime dtDate { get; set; }  // Date (yyyy-MM-dd)

        [Required(ErrorMessage = "Attendance Status is required.")]
        [StringLength(1)] // Only one character: P, A, or L
        public string AttStatus { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? InTime { get; set; }   // ✅ Nullable because Absent can be empty

        [DataType(DataType.Time)]
        public TimeSpan? OutTime { get; set; }  // ✅ Nullable because Absent can be empty

        // Navigation Properties
        //public string CompanyId { get; set; }
        public Company? Company { get; set; }
        //public string EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}