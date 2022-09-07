namespace Safib.Departments.Api.Models;

[Index(nameof(ParentDepartmentId))]
[Index(nameof(Name), IsUnique = true)]
public class Department {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DepartmentId { get; set; }

    public Guid? ParentDepartmentId { get; set; }

    public virtual Department ParentDepartment { get; set; }

    public virtual ICollection<Department> Departments { get; set; }

    [Required]
    public string Name { get; set; }

    public bool Enabled { get; set; }

    public override string ToString() => Name;
}