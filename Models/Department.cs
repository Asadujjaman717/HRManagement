using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HrManagement.Models;

namespace HrManagement.Models
{
    public class Department
    {
        [Key]
        public Guid DeptId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public Guid ComId { get; set; }

        [Required]
        public string DeptName { get; set; }

        public Company? Company { get; set; }
    }
}
