namespace Hospital.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<Doctor> Doctors { get; set; } = new();
    }
}
