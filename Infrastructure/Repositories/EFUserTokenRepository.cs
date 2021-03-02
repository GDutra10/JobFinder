using JobFinder.Models;
using JobFinder.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFUserTokenRepository : EFBaseRepository<UserToken>, IUserTokenRepository
    {
        public UserToken Get(string pDeToken)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                return context.UserTokens.FirstOrDefault(c => c.DeToken == pDeToken);
            }
        }
    }
}
