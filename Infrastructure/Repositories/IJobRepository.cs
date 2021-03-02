using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public interface IJobRepository
    {
        List<Job> GetJobsByRole(int pIdRole);
        List<Job> GetJobsByCompany(int pIdCompany);
        void Close(Job pJob);
        
    }
}
