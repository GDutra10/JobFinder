using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models.API.Requests
{
    public class RegisterUpdateUserRequest
    {
        public string NmUser { get; set; }
        public string DeEmail { get; set; }
        public string DePassword { get; set; }
        public string DePasswordConfirm { get; set; }
        public string NuTelephone { get; set; }
        public int UserType { get; set; }
        public int IdRole { get; set; }
    }
}
