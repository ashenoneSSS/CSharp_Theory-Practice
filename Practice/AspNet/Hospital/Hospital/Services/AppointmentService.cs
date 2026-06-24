using Hospital.Dto;
using Hospital.Data;
using Hospital.Models;
using Hospital.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HospitalDbContext _context;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HospitalDbContext dbcontext, ILogger<AppointmentService> logger)
        {
            _context = dbcontext;
            _logger = logger;
        }


        public async Task<List<Appointment>> GetAllAppointmentsAsync(
            int? doctor_id,
            int? patient_id,
            AppointmentSortBy? sort_by,
            int pageSize = 20,
            int pages = 1)
        {
            IQueryable<Appointment> query = _context.Appointments.AsNoTracking();

            if (doctor_id != null)
            {
                query = query.Where(app => app.DoctorId == doctor_id);
            }
            if (patient_id != null)
            {
                query = query.Where(app => app.PatientId == patient_id);
            }
            if (sort_by != null)
            {
                switch (sort_by)
                {
                    case AppointmentSortBy.Enum_Id:
                        query = query.OrderBy(app => app.Id);
                        break;

                    case AppointmentSortBy.Enum_Date:
                        query = query.OrderBy(app => app.AppointmentDate);
                        break;
                }
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


        public async Task<Appointment?> GetAppointmentByIdAsync(int id_to_get)
        {
            return await _context.Appointments.AsNoTracking().FirstOrDefaultAsync(app => app.Id == id_to_get);
        }


        public async Task CreateAppointmentAsync(AppointmentDto dto)
        {
            Appointment app = new Appointment();

            app.AppointmentDate = dto.AppointmentDate;
            app.Reason = dto.Reason;
            app.PatientId = dto.PatientId;
            app.DoctorId = dto.DoctorId;

            await _context.Appointments.AddAsync(app);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Appointment with id:{LogerAppointmentId} Created Successefuly", app.Id);
        }


        public async Task CreateAppointmentListAsync(List<AppointmentDto> dto_list)
        {
            List<Appointment> apps = new List<Appointment>();

            for (int i = 0; i < dto_list.Count; i++)
            {
                Appointment app = new Appointment();

                app.AppointmentDate = dto_list[i].AppointmentDate;
                app.Reason = dto_list[i].Reason;
                app.PatientId = dto_list[i].PatientId;
                app.DoctorId = dto_list[i].DoctorId;

                apps.Add(app);
            }

            await _context.Appointments.AddRangeAsync(apps);
            await _context.SaveChangesAsync();

            foreach (Appointment app in apps)
            {
                _logger.LogInformation("Appointment with id:{AppointmentId} Created Successefuly", app.Id);
            }
        }


        public async Task<bool> UpdateAppointmentAsync(AppointmentDto dto, int appointment_to_update_id)
        {
            Appointment? app = await _context.Appointments.FirstOrDefaultAsync(app => app.Id == appointment_to_update_id);

            if (app == null)
            {
                _logger.LogWarning("Appointment with id:{LogerAppointmentId} doesn`t exists", appointment_to_update_id);
                return false;
            }

            app.AppointmentDate = dto.AppointmentDate;
            app.Reason = dto.Reason;
            app.PatientId = dto.PatientId;
            app.DoctorId = dto.DoctorId;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Appointment with id:{LogerAppointmentId} Updated Successefuly", app.Id);

            return true;
        }


        public async Task<bool> DeleteAppointmentAsync(int appointment_to_delete_id)
        {
            Appointment? app = await _context.Appointments.FirstOrDefaultAsync(app => app.Id == appointment_to_delete_id);

            if (app == null)
            {
                _logger.LogWarning("Appointment with id:{LogerAppointmentId} doesn`t exists", appointment_to_delete_id);
                return false;
            }

            _context.Appointments.Remove(app);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Appointment with id:{LogerAppointmentId} Deleted Successefuly", app.Id);

            return true;
        }
    }
}
