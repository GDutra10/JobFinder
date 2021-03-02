using JobFinder.Infrastructure.DataSeeders;
using JobFinder.Infrastructure.Repositories;
using JobFinder.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Managers
{
    public static class RepositoryManager
    {
        public static void Configure(string pORMToUse)
        {
            switch (pORMToUse)
            {
                case "EntityFramework":
                    RepositorySingleton.Instantiate(
                        new EFUserBaseRepository(),
                        new EFJobRepository(),
                        new EFCompanyRepository(),
                        new EFUserTokenRepository(),
                        new EFRoleRepository(),
                        new EFCustomerRepository(),
                        new EFCandidateRepository());
                    new EFDataSeeder().SeedData();
                    break;
                case "Test":
                    RepositorySingleton.Instantiate(null, null, null, null, null, null, null);
                    break;
                default:
                    throw new Exception($"ORM '{pORMToUse}' not configured!");
            }
        }


    }
}
