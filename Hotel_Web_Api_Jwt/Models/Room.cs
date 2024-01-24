using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Web_Api_Jwt.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string Room_Type { get; set; }
    }
}