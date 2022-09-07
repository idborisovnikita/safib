namespace Safib.Departments.Api.DataAccess;

public class DepartmentRepository {

    private readonly DepartmentContext _context;

    public DepartmentRepository(DepartmentContext context) {
        _context = context;
    }

    public IQueryable<Department> GetQuery() {
        return _context.Departments;
    }

    public Department Add(Department entity) {
        if (entity == null) {
            throw new ArgumentNullException(nameof(entity));
        }

        return _context.Departments.Add(entity).Entity;
    }

    public Department Update(Department entity) {
        if (entity == null) {
            throw new ArgumentNullException(nameof(entity));
        }

        return _context.Departments.Update(entity).Entity;
    }
    
    public void SaveChanges() => _context.SaveChanges();
}