namespace Safib.Departments.Mvc.Configuration.Automapper;

public class DepartmentProfile : Profile {
    public DepartmentProfile() {
        CreateMap<DepartmentDto, DepartmentViewModel>();
    }
}