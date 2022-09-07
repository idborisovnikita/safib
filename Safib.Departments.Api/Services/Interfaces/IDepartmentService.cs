namespace Safib.Departments.Api.Services.Interfaces;
public interface IDepartmentService {
    IEnumerable<DepartmentInfoDto> GetAllDepartments();
    void Import(IEnumerable<DepartmentImportDto> dto);
}