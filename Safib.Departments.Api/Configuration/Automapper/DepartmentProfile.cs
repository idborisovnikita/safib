namespace Safib.Departments.Api.Configuration.Automapper;

public class DepartmentProfile : Profile {
    public DepartmentProfile() {
        CreateMap<DepartmentImportDto, Department>();
    }
}