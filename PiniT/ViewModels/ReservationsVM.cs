using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiniT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PiniT.ViewModels
{

    public class CreateReservationsVM
    {
        public Reservation Reservation { get; set; }
        public Table Table { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public bool HasAcceptedTerms { get; set; }

        [Range(0,90,ErrorMessage = "Value must be 90 or lower!")]
        [Required]
        [DisplayName("You will arrive in the next:")]
        public int EstimatedTime { get; set; }
    }
}