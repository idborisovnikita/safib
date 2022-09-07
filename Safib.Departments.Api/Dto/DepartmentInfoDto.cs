namespace Safib.Departments.Api.Dto;

public class DepartmentInfoDto {
    public Guid DepartmentId { get; set; }

    public Guid? ParentDepartmentId { get; set; }

    public string Name { get; set; }

    public bool Enabled { get; set; }
}