using JobFinder.Models;
using JobFinder.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFCompanyRepository : EFBaseRepository<Company>, ICompanyRepository
    {
    }
}
