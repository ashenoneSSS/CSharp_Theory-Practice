using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class PatientService
    {
        private readonly HospitalDbContext _context;
        public PatientService(HospitalDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientsByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(pat => pat.Id == id);
        }

        public async Task CreatePersonAsync(PatientCreateDto dto)
        {
            Patient pat = new Patient();

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.Patients.AddAsync(pat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(PatientUpdateDto dto, int patient_to_update_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_update_id);

            if (pat == null)
            {
                return;
            }

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int patient_to_delete_id)
        {
            Patient? pat = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient_to_delete_id);

            if (pat == null)
            {
                return;
            }

            _context.Patients.Remove(pat);

            await _context.SaveChangesAsync();
        }

    }
}
