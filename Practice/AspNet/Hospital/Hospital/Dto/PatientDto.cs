using System.ComponentModel.DataAnnotations;

namespace Hospital.Dto
{
    public class PatientDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Range(0, 120)]
        public int Age { get; set; }

        [MinLength(1)]
        public string Diagnosis { get; set; } = null!;
    }
}
