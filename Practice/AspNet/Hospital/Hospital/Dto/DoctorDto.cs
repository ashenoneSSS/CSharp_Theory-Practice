using System.ComponentModel.DataAnnotations;

namespace Hospital.Dto
{
    public class DoctorDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [MinLength(1)]
        public string? Specialization { get; set; }
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
    }
}
