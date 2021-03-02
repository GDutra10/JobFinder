using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Candidate
    {
        [Key]
        public int IdCandidate { get; set; }
        
        public DateTime DtRegister { get; set; }

        [Required]
        [ForeignKey("Job")]
        [Column("IdJob")]
        public int IdJob { get; set; }
        public Job Job { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [Column("IdUser")]
        public int IdCustomer { get; set; }
        public Customer Customer { get; set; }

        public bool WasAccept { get; set; }
        public bool WasReject { get; set; }

    }
}
