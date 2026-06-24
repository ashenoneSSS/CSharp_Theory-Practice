using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAppointments(
            [FromQuery] int? doctor_id,
            [FromQuery] int? patient_id,
            [FromQuery] AppointmentSortBy? sort_by,
            [FromQuery] int page_size = 20,
            [FromQuery] int pages = 1)
        {
            List<Appointment> apps = await _service.GetAllAppointmentsAsync(doctor_id, patient_id, sort_by, page_size, pages);

            return Ok(apps);
        }


        [HttpGet("{id_of_appointment}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id_of_appointment)
        {
            Appointment? app = await _service.GetAppointmentByIdAsync(id_of_appointment);

            if (app == null)
            {
                return NotFound();
            }

            return Ok(app);
        }


        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            await _service.CreateAppointmentAsync(dto);

            return NoContent();
        }


        [HttpPost("ListAdding")]
        public async Task<ActionResult> CreateListOfAppointments([FromBody] List<AppointmentDto> dto_list)
        {
            await _service.CreateAppointmentListAsync(dto_list);

            return NoContent();
        }


        [HttpPut("{id_to_update}")]
        public async Task<ActionResult> UpdateAppointment([FromBody] AppointmentDto dto, int id_to_update)
        {
            bool is_app_exists = await _service.UpdateAppointmentAsync(dto, id_to_update);

            if (!is_app_exists)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id_to_delete}")]
        public async Task<ActionResult> DeleteAppointment(int id_to_delete)
        {
            bool is_app_exists = await _service.DeleteAppointmentAsync(id_to_delete);

            if (!is_app_exists)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
