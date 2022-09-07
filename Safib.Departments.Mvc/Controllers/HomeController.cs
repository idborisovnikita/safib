namespace Safib.Departments.Mvc.Controllers;

public class HomeController : Controller {

    private readonly ILogger<HomeController> _logger;
    private readonly IDepartmentsService _service;

    public HomeController(ILogger<HomeController> logger, IDepartmentsService service) {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index() {
        return View();
    }

    [HttpGet("GetDepartments")]
    public async Task<IActionResult> GetDepartmentsAsync() {
        try {
            var departments = await _service.GetDepartmentsTreeViewModel();

            return Ok(departments);
        }
        catch (Exception e) {
            _logger.LogError($"Message: {e.Message}\nStackTrace: {e.StackTrace}");
            return BadRequest();
        }
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file) {
        var result = new ImportResultViewModel();

        if (file.Length > 2097152) {
            result.Success = false;
            result.Message = "Размер файла не должен превышать 2 Мб";

            return PartialView("_ImportResult", result);
        }

        try {
            result.Success = await _service.Import(file);
        }
        catch (Exception e) {
            result.Success = false;
            _logger.LogError($"Message: {e.Message}\nStackTrace: {e.StackTrace}");
        }

        return PartialView("_ImportResult", result);
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}