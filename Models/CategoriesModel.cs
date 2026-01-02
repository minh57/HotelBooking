using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    [Table("tb_NewsCategories")]
    public class CategoriesModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isPublic { get; set; }

    }
}


  