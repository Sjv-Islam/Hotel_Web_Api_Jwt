using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Web_Api_Jwt.Models
{
    public class Guest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string GuestName { get; set; }

        public string Address { get; set; }
        public string ContactNo { get; set; }
        public bool LoyaltyMember { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }


        public ICollection<Reservation> reservations { get; set; }
    }
}