using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Web_Api_Jwt.Models
{
    public class AccessibleInfo
    {
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}