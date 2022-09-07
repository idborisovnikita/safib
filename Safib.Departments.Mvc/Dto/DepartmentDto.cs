namespace Safib.Departments.Mvc.Dto;

public class DepartmentDto {
    public Guid DepartmentId { get; set; }

    public Guid? ParentDepartmentId { get; set; }

    public string Name { get; set; }

    public bool Enabled { get; set; }
}