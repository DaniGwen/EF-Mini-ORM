using MiniORM.App.Data;
using System;
using System.Linq;

namespace MiniORM.App
{
    class StartUp
    {
        static void Main()
        {

            var context = new SoftUniDbContextClass(connection);

            context.Employee.Add(new Data.Entities.Employee
            {
                FirstName = "John",
                LastName = "Snow",
                DepartmentId = context.Department.First().Id,
                IsEmployed = true
            });

            var employee = context.Employee.Last();
            employee.FirstName = "Ben";

            context.SaveChanges();
        }
    }
}


