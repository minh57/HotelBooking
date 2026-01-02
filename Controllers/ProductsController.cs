using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers.Products
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LendingRoom()
        {
            return View();
        }
        public ActionResult LendingCar()
        {
            return View();
        }
        public ActionResult PopularPlace()
        {
            return View();
        }
    }
}