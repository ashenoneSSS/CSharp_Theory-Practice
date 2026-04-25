namespace Hospital.Dto
{
    public class PatientCreateDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public string Diagnosis { get; set; } = null!;
    }
}
