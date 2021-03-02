using JobFinder.Models;
using JobFinder.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFCandidateRepository : EFBaseRepository<Candidate>, ICandidateRepository
    {
        public void Accept(Candidate pCandidate)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                pCandidate.WasAccept = true;
                this.Update(pCandidate);
            }
        }

        public void Refuse(Candidate pCandidate)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                pCandidate.WasReject = true;
                this.Update(pCandidate);
            }
        }

        public Candidate Get(int pIdUser, int pIdJob)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                return context.Candidates.FirstOrDefault(c => c.IdCustomer == pIdUser && c.IdJob == pIdJob);
            }
        }

        public List<Candidate> GetAll(int pIdJob)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                return context.
                    Candidates
                    .Include(ca => ca.Customer)
                    .Include(ca => ca.Job)
                    .Where(c => c.IdJob == pIdJob).ToList();
            }
        }
    }
}
