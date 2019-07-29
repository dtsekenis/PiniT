using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class Reservation
    {
        public Reservation()
        {
            BookDate = DateTime.Now;
            BookingFee = 10.00m;
        }

        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }

        [Required]
        public DateTime BookDate { get; set; }
        public string Comment { get; set; }
        public virtual PiniTCustomer Customer { get; set; }
        public virtual Table Table { get; set; }

        [DisplayName("Fee")]
        public decimal BookingFee { get; set; }
    }
}