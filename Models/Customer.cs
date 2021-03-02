using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Customer : UserBase
    {
        [Required]
        [ForeignKey("Role")]
        [Column("IdRole")]
        public int IdRole { get; set; }
        public Role Role { get; set; }

    }
}
