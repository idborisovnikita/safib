using System.Net;

namespace Safib.Departments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase {

    private readonly ILogger<DepartmentsController> _logger;
    private readonly IDepartmentService _service;

    public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentService service) {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DepartmentInfoDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public ActionResult<IEnumerable<DepartmentInfoDto>> GetAll() {
        try {
            var result = _service.GetAllDepartments();

            if (!result.Any()) {
                return NotFound();
            }

            return Ok(result);
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public ActionResult Import(IEnumerable<DepartmentImportDto> dto) {
        try {
            _service.Import(dto);

            return Ok();
        }
        catch {
            return BadRequest();
        }
    }
}