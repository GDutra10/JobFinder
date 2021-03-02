using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Job
    {
        [Key]
        public int IdJob { get; set; }
        public string DeTitle { get; set; }
        public String DeDescription { get; set; }
        public float? VlSalaryMin { get; set; }
        public float? VlSalaryMax { get; set; }
        public bool IsActive { get; set; }
        
        [ForeignKey("Company")]
        [Column("IdUser")]
        public int IdCompany { get; set; }
        public Company Company { get; set; }

        [ForeignKey("Role")]
        [Column("IdRole")]
        public int IdRole { get; set; }
        public Role Role { get; set; }

        public DateTime DtRegister { get; set; }

    }
}
