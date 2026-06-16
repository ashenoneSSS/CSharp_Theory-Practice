using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Hospital.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync(string? diagnosis, PatientSortBy? sort_by, string? part_of_name, int pageSize = 20, int pages = 1);
        Task<Patient?> GetPatientByIdAsync(int id_to_get);
        Task CreatePatientAsync(PatientDto dto);
        Task CreatePatientListAsync(List<PatientDto> dto_list);
        Task<bool> UpdatePatientAsync(PatientDto dto, int patient_to_update_id);
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


        public async Task<List<Patient>> GetAllPatientsAsync(
            string? diagnosis,
            PatientSortBy? sort_by,
            string? part_of_name,
            int pageSize = 20,
            int pages = 1)
        {
            IQueryable<Patient> query = _context.Patients.AsNoTracking();


            if (!string.IsNullOrWhiteSpace(diagnosis))
            {
                query = query
                    .Where(pat => pat.Diagnosis == diagnosis);
            }
            if (!string.IsNullOrWhiteSpace(part_of_name))
            {
                query = query
                    .Where(pat => pat.FullName
                    .Contains(part_of_name));
            }
            if (sort_by != null)
            {
                switch (sort_by)
                {
                    case PatientSortBy.Enum_Id:
                        query = query
                            .OrderBy(pat => pat.Id);
                        break;

                    case PatientSortBy.Enum_Age:
                        query = query
                            .OrderBy(pat => pat.Age);
                        break;

                    case PatientSortBy.Enum_Name:
                        query = query
                            .OrderBy(pat => pat.FullName);
                        break;
                }
            }


            // Pagination

            if (pages < 1)
            {
                pages = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 20;
            }
            if (pageSize > 100)
            {
                pageSize = 100;
            }

            int skip_value = ((pages - 1) * pageSize);
            int take_value = pageSize;

            query = query
                    .Skip(skip_value)
                    .Take(take_value);



            return await query.ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id_to_get)
        {
            return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(pat => pat.Id == id_to_get);
        }


        public async Task CreatePatientAsync(PatientDto dto)
        {
            Patient pat = new Patient();

            pat.Age = dto.Age;
            pat.FullName = dto.FullName;
            pat.Diagnosis = dto.Diagnosis;

            await _context.Patients.AddAsync(pat);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Patient with id:{LogerPatientId} Created Successefuly", pat.Id);
        }

        public async Task CreatePatientListAsync(List<PatientDto> dto_list)
        {
            List<Patient> pats = new List<Patient>();

            for (int i = 0; i < dto_list.Count; i++)
            {
                Patient pat = new Patient();

                pat.Age = dto_list[i].Age;
                pat.FullName = dto_list[i].FullName;
                pat.Diagnosis = dto_list[i].Diagnosis;

                pats.Add(pat);
            }

            await _context.Patients.AddRangeAsync(pats);
            await _context.SaveChangesAsync();

            foreach (Patient pat in pats)
            {
                _logger.LogInformation("Patient with id:{PatientId} Created Successefuly", pat.Id);
            }
        }


        public async Task<bool> UpdatePatientAsync(PatientDto dto, int patient_to_update_id)
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
