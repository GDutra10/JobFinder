using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models.API.Requests
{
    public class JobAddRequest
    {
        public string DeTitle { get; set; }
        public String DeDescription { get; set; }
        public float? VlSalaryMin { get; set; }
        public float? VlSalaryMax { get; set; }
        public int IdRole { get; set; }

    }
}
