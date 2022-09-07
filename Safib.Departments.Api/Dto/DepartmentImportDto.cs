namespace Safib.Departments.Api.Dto;

public class DepartmentImportDto {
    public string Name { get; set; }

    public Guid Id { get; set; }

    public bool Enabled { get; set; }

    public IEnumerable<DepartmentImportDto> Departments { get; set; }

    public override string ToString() => Name;
}