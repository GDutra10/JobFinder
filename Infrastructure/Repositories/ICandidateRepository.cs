using JobFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public interface ICandidateRepository
    {
        Candidate Get(int pIdUser, int pIdJob);
        List<Candidate> GetAll(int pIdJob);
        void Accept(Candidate pCandidate);
        void Refuse(Candidate pCandidate);
    }
}
