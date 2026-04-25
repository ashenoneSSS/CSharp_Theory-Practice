namespace Hospital.Dto
{
    public class PatientUpdateDto
    {
        public string FullName { get; set; } = null!;
        public int Age { get; set; }
        public string Diagnosis { get; set; } = null!;
    }
}
