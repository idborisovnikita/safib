namespace Safib.Departments.Api;

public static class SeedData {

    public static void EnsurePopulated(this IApplicationBuilder builder) {
        var context = builder.ApplicationServices.GetRequiredService<DepartmentContext>();

        context.Database.Migrate();
        if (!context.Departments.Any()) {

            var DepartmentId1 = new Guid("4b6a6b9a-2303-402a-9970-6e71f4a47151");
            var DepartmentId2 = new Guid("c72e5cb5-d6b4-4c0c-9992-d7ae1c53a820");
            var DepartmentId3 = new Guid("7de3299b-2796-4982-a85b-2d6d1326396e");
            var DepartmentId4 = new Guid("0a58955e-342f-4095-88c6-1109d0f70583");
            var DepartmentId5 = new Guid("50454d55-a73c-4cbc-be25-3c5729dcb82b");
            var DepartmentId6 = new Guid("e54002e4-301f-41d2-97a5-a790db14415f");
            var DepartmentId7 = new Guid("58c2f448-8e76-4244-a7bd-cd35a9f21f18");
            var DepartmentId8 = new Guid("08e08d8f-afb8-454f-ab19-74bf9667c426");
            var DepartmentId9 = new Guid("dcdd0abf-687c-41c5-ac1d-561cd79b84f2");

            context.Departments.AddRange(
                new Department {
                    DepartmentId = DepartmentId1,
                    Name = "Department #1",
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId2,
                    Name = "Department #2",
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId3,
                    Name = "Department #3",
                    ParentDepartmentId = DepartmentId2,
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId4,
                    Name = "Department #4",
                    ParentDepartmentId = DepartmentId2,
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId5,
                    Name = "Department #5",
                    ParentDepartmentId = DepartmentId3,
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId6,
                    Name = "Department #6",
                    ParentDepartmentId = DepartmentId3,
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId7,
                    Name = "Department #7",
                    ParentDepartmentId = DepartmentId6,
                    Enabled = true
                },
                new Department {
                    DepartmentId = DepartmentId8,
                    Name = "Department #8",
                    ParentDepartmentId = DepartmentId6,
                    Enabled = false
                },
                new Department {
                    DepartmentId = DepartmentId9,
                    Name = "Department #9",
                    ParentDepartmentId = DepartmentId2,
                    Enabled = false
                }
            );

            context.SaveChanges();
        }
    }
}