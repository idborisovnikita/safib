namespace Safib.Departments.Mvc.Services.Interfaces;

public interface IDepartmentsService {
    Task<IEnumerable<DepartmentViewModel>> GetDepartmentsTreeViewModel();
    Task<bool> Import(IFormFile file);
}