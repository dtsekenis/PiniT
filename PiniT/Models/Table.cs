using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class Table
    {
        public Table()
        {
            Reservations = new HashSet<Reservation>();
        }
        [Key]
        public int TableId { get; set; }
        [Required]
        [Range(1,200)]
        public int Size { get; set; }
        public bool IsBooked { get; set; }
        public string Name { get; set; }

        [ForeignKey("Restaurant")]
        public string RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}