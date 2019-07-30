using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string Title { get; set; }

        public bool isProfileImage { get; set; }

        [Required]
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [ForeignKey("Restaurant")]
        public string RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}