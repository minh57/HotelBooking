using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    [Table("tb_News")]
    public class NewsModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Keywords { get; set; }
        public bool isPublic { get; set; }
        public int? ID_Category { get; set; }
        public string Name { get; set; }
    }
}


  