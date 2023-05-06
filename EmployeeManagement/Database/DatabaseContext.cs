using EmployeeManagement.Mapping;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentEmployee> DepartmentsEmployees { get; set; }
        public DbSet<DepartmentManager> DepartmentsManagers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamEmployee> TeamsEmployees { get; set; }





        public DatabaseContext(DbContextOptions databaseContextOptions) : base(databaseContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DepartmentBuildingConfiguration());

        }
    }
}
