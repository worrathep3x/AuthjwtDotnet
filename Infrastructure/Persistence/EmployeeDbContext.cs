using System;
using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class EmployeeDbContext : DbContext , IEmployeeDbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<JobDepartment> JobDepartments { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).HasColumnName("emp_ID");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("emp_email");
            entity.Property(e => e.EmpPass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("emp_pass");
            entity.Property(e => e.Fname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<JobDepartment>(entity =>
        {
            entity.HasKey(e => e.JobId);

            entity.ToTable("JobDepartment");

            entity.Property(e => e.JobId).HasColumnName("job_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.JobDept)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("job_dept");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SalaryRange)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("salary_range");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.LeaveId);
            entity.ToTable("Leave");

            entity.Property(e => e.LeaveId).HasColumnName("leave_ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmpId).HasColumnName("emp_ID");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId);
            entity.ToTable("Payroll");

            entity.Property(e => e.PayrollId).HasColumnName("payroll_ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmpId).HasColumnName("emp_ID");
            entity.Property(e => e.JobId).HasColumnName("job_ID");
            entity.Property(e => e.LeaveId).HasColumnName("leave_ID");
            entity.Property(e => e.Report)
                .HasColumnType("text")
                .HasColumnName("report");
            entity.Property(e => e.SalaryId).HasColumnName("salary_ID");
            entity.Property(e => e.TotalAmount).HasColumnName("total amount");
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.QualId);

            entity.ToTable("Qualification");

            entity.Property(e => e.QualId).HasColumnName("qual_ID");
            entity.Property(e => e.DateIn).HasColumnName("date_in");
            entity.Property(e => e.EmpId).HasColumnName("emp_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Requirements)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("requirements");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId);
            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId).HasColumnName("salary_ID");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.JobId).HasColumnName("job_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.AdminId);

            entity.Property(e => e.AdminId).HasColumnName("admin_ID");
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("admin_email");
            entity.Property(e => e.AdminPass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("admin_pass");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Fname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lname");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
