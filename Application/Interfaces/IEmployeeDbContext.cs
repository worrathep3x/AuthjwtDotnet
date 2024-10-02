using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IEmployeeDbContext
{
    DbSet<Employee> Employees { get; set; }

    DbSet<JobDepartment> JobDepartments { get; set; }

    DbSet<Leave> Leaves { get; set; }

    DbSet<Payroll> Payrolls { get; set; }

    DbSet<Qualification> Qualifications { get; set; }

    DbSet<Salary> Salaries { get; set; }

    DbSet<User> Users { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
