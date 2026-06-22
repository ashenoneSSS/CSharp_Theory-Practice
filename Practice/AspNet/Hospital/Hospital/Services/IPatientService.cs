using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;

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
}
