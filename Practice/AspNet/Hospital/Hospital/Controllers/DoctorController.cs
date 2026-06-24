using Hospital.Dto;
using Hospital.Enums;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors(
            [FromQuery] string? specialization,
            [FromQuery] DoctorSortBy? sort_by,
            [FromQuery] string? part_of_name,
            [FromQuery] int pages_size = 20,
            [FromQuery] int pages = 1)
        {
            List<Doctor> docs = await _service.GetAllDoctorsAsync(specialization, sort_by, part_of_name, pages_size, pages);

            return Ok(docs);
        }


        [HttpGet("{id_of_doctor}")]
        public async Task<ActionResult<Doctor?>> GetDoctorById(int id_of_doctor)
        {
            Doctor? doc = await _service.GetDoctorByIdAsync(id_of_doctor);

            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }


        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            await _service.CreateDoctorAsync(dto);

            return NoContent();
        }


        [HttpPost("ListAdding")]
        public async Task<ActionResult> CreateListOfDoctors([FromBody] List<DoctorDto> dto_list)
        {
            await _service.CreateDoctorListAsync(dto_list);

            return NoContent();
        }


        [HttpPut("{id_to_update}")]
        public async Task<ActionResult> UpdateDoctor([FromBody] DoctorDto dto, int id_to_update)
        {
            bool is_doc_exists = await _service.UpdateDoctorAsync(dto, id_to_update);

            if (!is_doc_exists)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id_to_delete}")]
        public async Task<ActionResult> DeleteDoctor(int id_to_delete)
        {
            bool is_doc_exists = await _service.DeleteDoctorAsync(id_to_delete);

            if (!is_doc_exists)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
