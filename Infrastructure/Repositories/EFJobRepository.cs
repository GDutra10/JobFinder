using JobFinder.Models;
using JobFinder.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFJobRepository : EFBaseRepository<Job>, IJobRepository
    {
        public void Close(Job pJob)
        {
            pJob.IsActive = false;
            this.Update(pJob);
        }

        public List<Job> GetJobsByCompany(int pIdCompany)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                return context
                    .Jobs
                    .Include(j => j.Role)
                    .Include(j => j.Company)
                    .Where(c => c.IdCompany == pIdCompany)
                    .ToList();
            }
        }

        public List<Job> GetJobsByRole(int pIdRole)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                return context
                    .Jobs
                    .Include(j => j.Role)
                    .Include(j => j.Company)
                    .Where(c => c.IdRole == pIdRole)
                    .ToList();
            }
        }
    }
}
