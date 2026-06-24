using Hospital.Dto;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments(
            [FromQuery] string? name,
            [FromQuery] bool sort_by_name = false,
            [FromQuery] int page_size = 20,
            [FromQuery] int pages = 1)
        {
            List<Department> deps = await _service.GetAllDepartmentAsync(name, sort_by_name, page_size, pages);

            return Ok(deps);
        }


        [HttpGet("{id_of_department}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id_of_department)
        {
            Department? dep = await _service.GetDepartmentByIdAsync(id_of_department);

            if (dep == null)
            {
                return NotFound();
            }

            return Ok(dep);
        }


        [HttpPost]
        public async Task<ActionResult> CreateDepartment([FromBody] DepartmentDto dto)
        {
            await _service.CreateDepartmentAsync(dto);

            return NoContent();
        }


        [HttpPost("ListAdding")]
        public async Task<ActionResult> CreateListOfDepartments([FromBody] List<DepartmentDto> dto_list)
        {
            await _service.CreateDepartmentListAsync(dto_list);

            return NoContent();
        }


        [HttpPut("{id_to_update}")]
        public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentDto dto, int id_to_update)
        {
            bool is_dep_exists = await _service.UpdateDepartmentAsync(dto, id_to_update);

            if (!is_dep_exists)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id_to_delete}")]
        public async Task<ActionResult> DeleteDepartment(int id_to_delete)
        {
            bool is_dep_exists = await _service.DeleteDepartmentAsync(id_to_delete);

            if (!is_dep_exists)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
