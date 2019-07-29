using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class AccountWallet
    {
        [ForeignKey("User")]
        public string Id { get; set; }

        [Range(0,Double.MaxValue)]
        public decimal Credits { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}