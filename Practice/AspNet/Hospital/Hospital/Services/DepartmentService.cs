using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Hospital.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HospitalDbContext _context;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(HospitalDbContext context, ILogger<DepartmentService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<List<Department>> GetAllDepartmentAsync(
            string? name,
            bool sort_by_name = false,
            int pageSize = 20,
            int pages = 1)
        {
            IQueryable<Department> query = _context.Departments.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(dep => dep.Name == name);
            }

            if (!sort_by_name)
            {
                query = query.OrderBy(dep => dep.Id);
            }
            else
            {
                query = query.OrderBy(dep => dep.Name);
            }

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

            int skip_value = (pages - 1) * pageSize;
            int take_value = pageSize;

            query = query
                .Skip(skip_value)
                .Take(take_value);


            return await query.ToListAsync();
        }


        public async Task<Department?> GetDepartmentByIdAsync(int id_to_get)
        {
            return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(dep => dep.Id == id_to_get);
        }


        public async Task CreateDepartmentAsync(DepartmentDto dto)
        {
            Department dep = new Department();

            dep.Name = dto.Name;

            await _context.Departments.AddAsync(dep);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Department with id:{LogerDepartmentId} Created Successefuly", dep.Id);
        }


        public async Task CreateDepartmentListAsync(List<DepartmentDto> dto_list)
        {
            List<Department> deps = new List<Department>();

            for (int i = 0; i < dto_list.Count; i++)
            {
                Department dep = new Department();

                dep.Name = dto_list[i].Name;

                deps.Add(dep);
            }

            await _context.Departments.AddRangeAsync(deps);
            await _context.SaveChangesAsync();

            foreach (Department dep in deps)
            {
                _logger.LogInformation("Department with id:{DepartmentId} Created Successefuly", dep.Id);
            }
        }


        public async Task<bool> UpdateDepartmentAsync(DepartmentDto dto, int department_to_update_id)
        {
            Department? dep = await _context.Departments.FirstOrDefaultAsync(dep => dep.Id == department_to_update_id);

            if (dep == null)
            {
                _logger.LogWarning("Department with id:{LogerDepartmentId} doesn`t exists", department_to_update_id);
                return false;
            }

            dep.Name = dto.Name;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Department with id:{LogerDepartmentId} Updated Successefuly", dep.Id);

            return true;
        }


        public async Task<bool> DeleteDepartmentAsync(int id_to_delete)
        {
            Department? dep = await _context.Departments.FirstOrDefaultAsync(dep => dep.Id == id_to_delete);

            if (dep == null)
            {
                _logger.LogWarning("Department with id:{LogerDepartmentId} doesn`t exists", id_to_delete);
                return false;
            }

            _context.Departments.Remove(dep);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Department with id:{LogerDepartmentId} Deleted Successefuly", dep.Id);

            return true;
        }
    }
}
