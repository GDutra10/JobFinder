using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.DataSeeders
{
    public interface IDataSeeder
    {
        void SeedData();
        void SeedRoles();
        void SeedUsers();

    }
}
