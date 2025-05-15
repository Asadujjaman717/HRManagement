
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Models
{
    public class Shift
    {
        [Key]
        public Guid ShiftId { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public Guid ComId { get; set; }

        [Required]
        public string ShiftName { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan InTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan OutTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan LateTime { get; set; }

        public Company Company { get; set; }
    }
}
