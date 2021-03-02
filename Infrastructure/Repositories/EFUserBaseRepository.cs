using JobFinder.Enums;
using JobFinder.Models;
using JobFinder.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFUserBaseRepository : EFBaseRepository<UserBase>, IUserBaseRepository
    {

        public UserBase Get(string pDeEmail, string pDePassword, UserType pUserType)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                switch (pUserType)
                {
                    case UserType.Company:
                        return context.Companies
                            .Where(c => c.DeEmail == pDeEmail &&
                                        c.DePassword == pDePassword)
                            .FirstOrDefault();
                    case UserType.Customer:
                        return context.Customers
                            .Where(c => c.DeEmail == pDeEmail &&
                                        c.DePassword == pDePassword)
                            .FirstOrDefault();
                    default:
                        return null;
                }
            }

        }

        public UserBase Get(string pDeEmail, UserType pUserType)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {

                if (UserType.Customer == pUserType)
                {
                    Customer customer = context.Customers
                            .Where(c => c.DeEmail == pDeEmail)
                            .FirstOrDefault();

                    return customer;
                }
                else if (UserType.Company == pUserType)
                {
                    Company company = context.Companies
                           .Where(c => c.DeEmail == pDeEmail)
                           .FirstOrDefault();

                    return company;
                }

                return null;
               
            }
        }
    }
}
