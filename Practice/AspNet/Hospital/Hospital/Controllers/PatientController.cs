using Microsoft.AspNetCore.Mvc;
using Hospital.Services;
using Hospital.Models;
using Hospital.Dto;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _service;

        public PatientController(PatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            List<Patient> pats = await _service.GetAllPatientsAsync();

            return Ok(pats);
        }

        [HttpGet("{id_of_patient}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id_of_patient)
        {
            Patient? pat = await _service.GetPatientsByIdAsync(id_of_patient);

            if(pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatient([FromBody] PatientCreateDto dto)
        {
            await _service.CreatePersonAsync(dto);

            return Ok();
        }

        [HttpPut("{id_to_update}")]
        public async Task<ActionResult> UpdatePatient([FromBody] PatientUpdateDto dto, int id_to_update)
        {
            Patient? pat = await _service.GetPatientsByIdAsync(id_to_update);

            if (pat == null)
            {
                return NotFound();
            }



            await _service.UpdatePatientAsync(dto, id_to_update);

            return Ok();
        }

        [HttpDelete("{id_to_delete}")]
        public async Task<ActionResult> DeletePatient(int id_to_delete)
        {
            Patient? pat = await _service.GetPatientsByIdAsync(id_to_delete);

            if (pat == null)
            {
                return NotFound();
            }



            await _service.DeletePatientAsync(id_to_delete);

            return Ok();
        }

    }
}
