namespace Safib.Departments.Mvc.Extensions;

public static class Extensions {
    public static IEnumerable<DepartmentViewModel> BuildDepartmentsTree(this IEnumerable<DepartmentViewModel> departments) {
        var lookup = new Dictionary<Guid, DepartmentViewModel>();

        foreach (var dept in departments) {
            lookup.Add(dept.DepartmentId, dept);
        }

        foreach (var item in lookup.Values) {
            if (item.ParentDepartmentId.HasValue && lookup.TryGetValue(item.ParentDepartmentId.Value, out DepartmentViewModel parent)) {
                item.ParentDepartment = parent;
                parent.Departments.Add(item);
            }
        }

        return lookup.Values.Where(d => d.ParentDepartment == null);
    }
}