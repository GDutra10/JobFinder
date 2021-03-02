using JobFinder.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models.API.Requests
{
    public class LoginRequest
    {
        public string DeEmail { get; set; }
        public string DePassword { get; set; }
        public int UserType { get; set; }
    }
}
