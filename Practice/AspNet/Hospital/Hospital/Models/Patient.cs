namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public string Diagnosis { get; set; } = null!;
    }
}
