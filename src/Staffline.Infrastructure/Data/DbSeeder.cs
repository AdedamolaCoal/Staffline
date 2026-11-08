using Microsoft.EntityFrameworkCore;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Domain.Entities.Employees;
using Staffline.Domain.Enums;
using Staffline.Infrastructure.Persistence;

namespace Staffline.Infrastructure.Data;

public static class DbSeeder
{
    private const string DevPassword = "Password@1";

    public static async Task SeedAsync(
        AppDbContext db,
        IPasswordService passwords,
        CancellationToken ct = default
    )
    {
        // IT SHOULD RETURN IF DATA EXISTS, TO ENSURE IT'S SAFE ON STARTUP.
        if (await db.Employees.AnyAsync(ct)) return;

        var engineering = new Department { Name = "Engineering" };
        var humanResources = new Department { Name = "Human Resources" };
        var finance = new Department { Name = "Finance" };

        var g = Enumerable.Range(1, 5)
            .Select(i => new Grade
            {
                Code = $"G{i}",
                Level = i
            }).ToArray();
        db.Grades.AddRange(g);

        Employee Create(
            string number,
            string name,
            string email,
            Department dept,
            Grade grd,
            Employee? manager,
            params RoleName[] roles
        )
        {
            var e = new Employee
            {
                EmployeeNumber = number,
                Email = email.ToLowerInvariant(),
                Fullname = name,
                PasswordHash = string.Empty,
                Department = dept,
                Grade = grd,
                Manager = manager
            };
            e.PasswordHash = passwords.Hash(e, DevPassword);
            foreach (var role in roles)
                e.Roles.Add(
                    new EmployeeRole
                    {
                        EmployeeId = e.Id,
                        Role = role
                    });
            db.Employees.Add(e);
            return e;
        }

        var director = Create(
            "EMP-0001",
            "Munirudeen Akanbi",
            "director@staffline.com",
            engineering, g[4], null,
            RoleName.Manager, RoleName.Staff
        );
        Create(
            "EMP-0002",
            "Amaka Obi",
            "admin@staffline.com",
            engineering, g[3], director,
            RoleName.Admin, RoleName.Staff
        );
        Create(
            "EMP-0003",
            "Ngozi Eze",
            "hr@staffline.com",
            humanResources, g[3], director,
            RoleName.Hr, RoleName.Staff
        );
        Create(
            "EMP-0004",
            "Ibrahim Musa",
            "finance@staffline.com",
            finance, g[3], director,
            RoleName.Finance, RoleName.Staff
        );

        var managerEng = Create(
            "EMP-0005",
            "Chiamaka Okafor",
            "manager@staffline.com",
            engineering, g[3], director,
            RoleName.Manager, RoleName.Staff
        );
        var managerFin = Create(
            "EMP-0006",
            "Bola Johnson",
            "manager2@staffline.com",
            finance, g[3], director,
            RoleName.Manager, RoleName.Staff
        );

        Create(
            "EMP-0007",
            "Segun Balogun",
            "staff1@staffline.com",
            engineering, g[1], managerEng,
            RoleName.Staff
        );
        Create(
            "EMP-0008",
            "Fatima Bello",
            "staff2@staffline.com",
            engineering, g[2], managerEng,
            RoleName.Staff
        );
        Create(
            "EMP-0009",
            "Emeka Nwosu",
            "staff3@staffline.com",
            finance, g[1], managerFin,
            RoleName.Staff
        );
    }
}