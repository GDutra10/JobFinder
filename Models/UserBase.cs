using JobFinder.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class UserBase
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public string NmUser { get; set; }
        [Required]
        public string DeEmail { get; set; }
        [Required]
        public string DePassword { get; set; }

        public string NuTelephone { get; set; }

        public DateTime DtRegister { get; set; }

        [NotMapped]
        public string DeToken { get; set; }

        [NotMapped]
        public UserType UserType { get; set; }
    }
}
