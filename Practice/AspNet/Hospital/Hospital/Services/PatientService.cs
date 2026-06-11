using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id_to_get);
        Task CreatePatientAsync(PatientCreateDto dto);
        Task<bool> UpdatePatientAsync(PatientUpdateDto dto, int patient_to_update_id);
        Task<bool> DeletePatientAsync(int patient_to_delete_id);
    }


    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _context;
        private readonly ILogger<PatientService> _logger;

        public PatientService(HospitalDbContext dbcontext, ILogger<PatientService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            IQueryable<Patient> query = _context.Patients;

            return await query.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id_to_get)
        {
            return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(pat => pat.Id == id_to_get);
        }

        public async Task CreatePatientAsync(PatientCreateDto dto)
        {
            Patient pat = new Patient();

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.Patients.AddAsync(pat);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient with id:{LogerPatientId} Created Successefuly", pat.Id);
        }

        public async Task<bool> UpdatePatientAsync(PatientUpdateDto dto, int patient_to_update_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_update_id);

            if (pat == null)
            {
                _logger.LogWarning("Patient with id:{LogerPatientId} doesn`t exists", patient_to_update_id);
                return false;
            }

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient with id:{LogerPatientId} Updated Successefuly", pat.Id);

            return true;
        }

        public async Task<bool> DeletePatientAsync(int patient_to_delete_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_delete_id);

            if (pat == null)
            {
                _logger.LogWarning("Patient with id:{LogerPatientId} doesn`t exists", patient_to_delete_id);
                return false;
            }

            _context.Patients.Remove(pat);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient with id:{LogerPatientId} Deleted Successefuly", pat.Id);

            return true;
        }

    }
}
