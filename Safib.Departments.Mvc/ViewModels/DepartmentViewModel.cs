namespace Safib.Departments.Mvc.ViewModels;

public class DepartmentViewModel {
    public Guid DepartmentId { get; set; }

    public Guid? ParentDepartmentId { get; set; }

    public DepartmentViewModel ParentDepartment { get; set; }

    public string Name { get; set; }

    public bool Enabled { get; set; }

    public List<DepartmentViewModel> Departments { get; set; } = new List<DepartmentViewModel>();
}