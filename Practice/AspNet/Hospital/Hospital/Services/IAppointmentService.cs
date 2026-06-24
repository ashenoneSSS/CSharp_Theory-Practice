using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;

namespace Hospital.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync(int? doctor_id, int? patient_id, AppointmentSortBy? sort_by, int pageSize = 20, int pages = 1);
        Task<Appointment?> GetAppointmentByIdAsync(int id_to_get);

        Task CreateAppointmentAsync(AppointmentDto dto);
        Task CreateAppointmentListAsync(List<AppointmentDto> dto_list);
        Task<bool> UpdateAppointmentAsync(AppointmentDto dto, int appointment_to_update_id);
        Task<bool> DeleteAppointmentAsync(int appointment_to_delete_id);
    }
}
