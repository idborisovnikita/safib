namespace Safib.Departments.Api.DataAccess;

public class DepartmentContext : DbContext {

    public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Department>()
            .HasOne(d => d.ParentDepartment)
            .WithMany(d => d.Departments);

        base.OnModelCreating(modelBuilder);
    }
}