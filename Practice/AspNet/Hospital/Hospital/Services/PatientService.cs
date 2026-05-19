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
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id_to_get)
        {
            return await _context.Patients.FirstOrDefaultAsync(pat => pat.Id == id_to_get);
        }

        public async Task CreatePatientAsync(PatientCreateDto dto)
        {
            Patient pat = new Patient();

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.Patients.AddAsync(pat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePatientAsync(PatientUpdateDto dto, int patient_to_update_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_update_id);

            if (pat == null)
            {
                return false;
            }

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePatientAsync(int patient_to_delete_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_delete_id);

            if (pat == null)
            {
                return false;
            }

            _context.Patients.Remove(pat);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
