using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models.API.Requests
{
    public class AcceptRejectCandidateRequest
    {
        public int IdCandidate { get; set; }
        public int IdJob { get; set; }

    }
}
