using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;

namespace Hospital.Services
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync(string? specialization, DoctorSortBy? sort_by, string? part_of_name, int pageSize = 20, int pages = 1);
        Task<Doctor?> GetDoctorByIdAsync(int id_to_get);

        Task CreateDoctorAsync(DoctorDto dto);
        Task CreateDoctorListAsync(List<DoctorDto> dto_list);
        Task<bool> UpdateDoctorAsync(DoctorDto dto, int doctor_to_update_id);
        Task<bool> DeleteDoctorAsync(int doctor_to_delete_id);
    }
}
