using MiniORM.App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniORM.App.Data
{
    public class SoftUniDbContextClass : DbContext
    {
        public SoftUniDbContextClass(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Department> Department { get; }

        public DbSet<Employee> Employee { get; }

        public DbSet<Project> Project { get; }

        public DbSet<EmployeeProject> EmployeeProjects { get; }
    }
}
