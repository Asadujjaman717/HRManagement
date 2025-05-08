using System.ComponentModel.DataAnnotations;

namespace HrManagement.Models
{
    public class Company
    {
        [Key]
        public Guid ComId { get; set; }
        [Required]
        public string ComName { get; set; }
        [Range(0, 1)]
        public double Basic {  get; set; }
        [Range(0, 1)]
        public double Hrent {  get; set; }
        [Range(0, 1)]
        public double? Medical { get; set; }
        public bool IsInactive { get; set; }

    }
}

