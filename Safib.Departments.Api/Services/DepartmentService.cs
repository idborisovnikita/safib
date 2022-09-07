namespace Safib.Departments.Api.Services;

public class DepartmentService : IDepartmentService {

    private readonly DepartmentRepository _repository;

    public DepartmentService(DepartmentRepository repository) {
        _repository = repository;
    }

    public IEnumerable<DepartmentInfoDto> GetAllDepartments() {
        var depts = _repository.GetQuery()
            .Select(dept => new DepartmentInfoDto { 
                DepartmentId = dept.DepartmentId,
                ParentDepartmentId = dept.ParentDepartmentId,
                Name = dept.Name,
                Enabled = dept.Enabled
            })
            .OrderBy(d => d.Name);

        return depts;        
    }

    public void Import(IEnumerable<DepartmentImportDto> dto) {
        object locker = new();

        lock (locker) {
            foreach (var importDto in dto) {
                var dbEntity = _repository.GetQuery().Where(d => d.Name == importDto.Name).FirstOrDefault();

                if (dbEntity != null) {
                    dbEntity.Enabled = importDto.Enabled;
                    dbEntity.ParentDepartmentId = null;

                    _repository.Update(dbEntity);
                }
                else {
                    dbEntity = new Department {
                        Enabled = importDto.Enabled,
                        Name = importDto.Name
                    };

                    _repository.Add(dbEntity);                    
                }

                if (importDto.Departments.Any()) {
                    UpdateChildDepartments(importDto, dbEntity);
                }
            }

            _repository.SaveChanges();
        }        
    }

    private void UpdateChildDepartments(DepartmentImportDto parent, Department parentDbEntity) {
        foreach (var importDto in parent.Departments) {
            var currentDbEntity = _repository
                .GetQuery()
                .Where(d => d.Name == importDto.Name)
                .FirstOrDefault();

            if (currentDbEntity != null) {
                currentDbEntity.Enabled = importDto.Enabled;
                currentDbEntity.ParentDepartmentId = parentDbEntity.DepartmentId;

                _repository.Update(currentDbEntity);                
            }
            else {
                currentDbEntity = new Department {
                    Enabled = importDto.Enabled,
                    Name = importDto.Name,
                    ParentDepartment = parentDbEntity,
                };

                _repository.Add(currentDbEntity);
            }

            if (importDto.Departments.Any()) {
                UpdateChildDepartments(importDto, currentDbEntity);
            }
        }
    }
}