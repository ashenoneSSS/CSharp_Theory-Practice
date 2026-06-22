using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Hospital.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _context;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(HospitalDbContext dbcontext, ILogger<DoctorService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }


        public async Task<List<Doctor>> GetAllDoctorsAsync(
            string? specialization,
            DoctorSortBy? sort_by,
            string? part_of_name,
            int pageSize = 20,
            int pages = 1)
        {
            IQueryable<Doctor> query = _context.Doctors.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(specialization))
            {
                query = query
                    .Where(doc => doc.Specialization == specialization);
            }
            if (!string.IsNullOrWhiteSpace(part_of_name))
            {
                query = query
                    .Where(doc => doc.FullName
                    .Contains(part_of_name));
            }
            if (sort_by != null)
            {
                switch (sort_by)
                {
                    case DoctorSortBy.Enum_Id:
                        query = query
                            .OrderBy(doc => doc.Id);
                        break;

                    case DoctorSortBy.Enum_Name:
                        query = query
                            .OrderBy(doc => doc.FullName);
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


        public async Task<Doctor?> GetDoctorByIdAsync(int id_to_get)
        {
            return await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(doc => doc.Id == id_to_get);
        }
        

        public async Task CreateDoctorAsync(DoctorDto dto)
        {
            Doctor doc = new Doctor();

            doc.FullName = dto.FullName;
            doc.Specialization = dto.Specialization;
            doc.DepartmentId = dto.DepartmentId;

            await _context.Doctors.AddAsync(doc);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Doctor with id:{LogerDoctorId} Created Successefuly", doc.Id);
        }

        public async Task CreateDoctorListAsync(List<DoctorDto> dto_list)
        {
            List<Doctor> docs = new List<Doctor>();

            for (int i = 0; i < dto_list.Count; i++)
            {
                Doctor doc = new Doctor();

                doc.FullName = dto_list[i].FullName;
                doc.Specialization = dto_list[i].Specialization;
                doc.DepartmentId = dto_list[i].DepartmentId;

                docs.Add(doc);
            }

            await _context.Doctors.AddRangeAsync(docs);
            await _context.SaveChangesAsync();

            foreach (Doctor doc in docs)
            {
                _logger.LogInformation("Doctor with id:{DoctorId} Created Successefuly", doc.Id);
            }
        }


        public async Task<bool> UpdateDoctorAsync(DoctorDto dto, int doctor_to_update_id)
        {
            Doctor? doc = await _context.Doctors.FirstOrDefaultAsync(doc => doc.Id == doctor_to_update_id);

            if (doc == null)
            {
                _logger.LogWarning("Doctor with id:{LogerDoctorId} doesn`t exists", doctor_to_update_id);
                return false;
            }

            doc.FullName = dto.FullName;
            doc.Specialization = dto.Specialization;
            doc.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Doctor with id:{LogerDoctorId} Updated Successefuly", doc.Id);

            return true;
        }


        public async Task<bool> DeleteDoctorAsync(int doctor_to_delete_id)
        {
            Doctor? doc = await _context.Doctors.FirstOrDefaultAsync(p => p.Id == doctor_to_delete_id);

            if (doc == null)
            {
                _logger.LogWarning("Doctor with id:{LogerDoctorId} doesn`t exists", doctor_to_delete_id);
                return false;
            }

            _context.Doctors.Remove(doc);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Doctor with id:{LogerDoctorId} Deleted Successefuly", doc.Id);

            return true;
        }
    }
}
