using System.ComponentModel.DataAnnotations;

namespace Hospital.Dto
{
    public class AppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        [MinLength(1)]
        public string? Reason { get; set; }
        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }
        [Range(1, int.MaxValue)]
        public int DoctorId { get; set; }
    }
}
