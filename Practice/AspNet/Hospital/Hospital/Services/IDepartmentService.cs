using Hospital.Dto;
using Hospital.Models;

namespace Hospital.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentAsync(string? name, bool sort_by_name = false, int pageSize = 20, int pages = 1);
        Task<Department?> GetDepartmentByIdAsync(int id_to_get);

        Task CreateDepartmentAsync(DepartmentDto dto);
        Task CreateDepartmentListAsync(List<DepartmentDto> dto_list);
        Task<bool> UpdateDepartmentAsync(DepartmentDto dto, int department_to_update_id);
        Task<bool> DeleteDepartmentAsync(int id_to_delete);
    }
}
