using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class Designation
    {
        [Key]
        public Guid DesigId { get; set; }

        [Required(ErrorMessage = "Designation Name is required.")]
        public string DesigName { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; }  // Foreign key for Company

        [Required]
        [ForeignKey(nameof(Department))]
        public Guid DeptId { get; set; }  // ✅ New: Foreign key for Department

        // Navigation properties
        public Company Company { get; set; }
        public Department Department { get; set; }
    }
}
