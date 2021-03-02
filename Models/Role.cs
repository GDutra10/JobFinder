using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        [Required]
        public string NmRole { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
