using System.ComponentModel.DataAnnotations;

namespace Hospital.Dto
{
    public class DepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
