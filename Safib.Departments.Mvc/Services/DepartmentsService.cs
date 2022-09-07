namespace Safib.Departments.Mvc.Services;

public class DepartmentsService : IDepartmentsService {

    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly IHubContext<ApplicationHub> _hubContext;

    public DepartmentsService(HttpClient httpClient, IMapper mapper, IHubContext<ApplicationHub> hubContext) {
        _httpClient = httpClient;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task<IEnumerable<DepartmentViewModel>> GetDepartmentsTreeViewModel() {
        var departmentsDto = await GetDepartmentAsync();
        var treeVm = _mapper
            .Map<IEnumerable<DepartmentViewModel>>(departmentsDto)
            .BuildDepartmentsTree();

        return treeVm;
    }

    public async Task<bool> Import(IFormFile file) {
        var sb = new StringBuilder();
        using (var reader = new StreamReader(file.OpenReadStream())) {
            while (reader.Peek() >= 0) {
                sb.AppendLine(reader.ReadLine());
            }
        }
        var httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
        var httpResponse = await _httpClient.PostAsync("api/departments", httpContent);

        if (httpResponse.IsSuccessStatusCode) {
            await _hubContext.Clients.All.SendAsync("update");
        }

        return httpResponse.IsSuccessStatusCode;
    }

    private async Task<IEnumerable<DepartmentDto>> GetDepartmentAsync() {
        var responseString = await _httpClient.GetStringAsync("api/departments");
        var departments = JsonSerializer
            .Deserialize<IEnumerable<DepartmentDto>>(responseString, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

        return departments;
    }
}