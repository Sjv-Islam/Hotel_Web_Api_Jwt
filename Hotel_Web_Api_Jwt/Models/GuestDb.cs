using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotel_Web_Api_Jwt.Models
{
    public class GuestDb: DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        public DbSet<AccessibleInfo> Users { get; set; }
        public GuestDb() : base("connection")
        {

        }
    }
}