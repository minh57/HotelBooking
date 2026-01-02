using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBooking.Models;
using System.Web.Mvc;


namespace HotelBooking.Models
{
    public class NewsCreateVM
    {
        public NewsModel News { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}