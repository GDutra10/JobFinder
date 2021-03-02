using JobFinder.Models;
using JobFinder.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.DataSeeders
{
    public class EFDataSeeder : IDataSeeder
    {
        JobFinderDbContext Context;

        public void SeedData()
        {
            using (Context = new JobFinderDbContext())
            {
                SeedRoles();
                SeedUsers();
            }
        }

        public void SeedRoles()
        {
            if (!Context.Roles.Any())
            {
                Context.Roles.Add(new Role { NmRole = "Software Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "Front-End Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "Back-End Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "C# Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "Java Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "Java Script Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "React Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "Phyton Developer", IsActive = true });
                Context.Roles.Add(new Role { NmRole = "C++ Developer", IsActive = true });
            }

            Context.SaveChanges();
        }

        public void SeedUsers()
        {
            // Customers
            if (!Context.Customers.Any())
            {
                Context.Customers.Add(new Customer
                {
                    DeEmail = "email@email.com",
                    DePassword = "1",
                    IdRole = 1,
                    NmUser = "Customer Test",
                    DtRegister = DateTime.Now
                });
            }

            // Companies
            if (!Context.Companies.Any())
            {
                Context.Companies.Add(new Company
                {
                    DeEmail = "email@email.com",
                    DePassword = "1",
                    NmUser = "Company Test",
                    DtRegister = DateTime.Now
                });
            }

            Context.SaveChanges();
        }

    }
}
