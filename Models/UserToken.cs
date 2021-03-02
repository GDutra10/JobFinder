using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class UserToken
    {
        [Key]
        public int IdUserToken { get; set; }

        public string DeToken { get; set; }

        public DateTime DtExpire { get; set; }

        [ForeignKey("Customer")]
        [Column("IdCustomer")]
        public int? IdCustomer { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Company")]
        [Column("IdCompany")]
        public int? IdCompany { get; set; }
        public Company? Company { get; set; }

        public bool IsActive { get; set; }

    }
}
