using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HotelBooking.Models
{
    public class BookingHotelContext : DbContext
    {
        public BookingHotelContext() : base("ConnectDatabase")
        {

        }
        public DbSet<NewsModel> News { get; set; }
    }
}