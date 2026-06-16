namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Specialization { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
        public List<Appointment> Appointments { get; set; } = new();
    }
}
