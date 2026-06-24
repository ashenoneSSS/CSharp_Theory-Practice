using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients(
            [FromQuery] string? diagnosis,
            [FromQuery] PatientSortBy? sort_by,
            [FromQuery] string? part_of_name,
            [FromQuery] int page_size = 20,
            [FromQuery] int pages = 1)
        {
            List<Patient> pats = await _service.GetAllPatientsAsync(diagnosis, sort_by, part_of_name, page_size, pages);

            return Ok(pats);
        }


        [HttpGet("{id_of_patient}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id_of_patient)
        {
            Patient? pat = await _service.GetPatientByIdAsync(id_of_patient);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }


        [HttpPost]
        public async Task<ActionResult> CreatePatient([FromBody] PatientDto dto)
        {
            await _service.CreatePatientAsync(dto);

            return NoContent();
        }


        [HttpPost("ListAdding")]
        public async Task<ActionResult> CreateListOfPatients([FromBody] List<PatientDto> dto_list)
        {
            await _service.CreatePatientListAsync(dto_list);

            return NoContent();
        }


        [HttpPut("{id_to_update}")]
        public async Task<ActionResult> UpdatePatient([FromBody] PatientDto dto, int id_to_update)
        {
            bool is_pat_exists = await _service.UpdatePatientAsync(dto, id_to_update);

            if (!is_pat_exists)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id_to_delete}")]
        public async Task<ActionResult> DeletePatient(int id_to_delete)
        {
            bool is_pat_exists = await _service.DeletePatientAsync(id_to_delete);

            if (!is_pat_exists)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
