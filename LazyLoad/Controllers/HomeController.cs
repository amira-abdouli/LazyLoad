using LazyLoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace LazyLoad.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db1 = new ApplicationDbContext();
            var db2 = new ApplicationDbContext(false);
            var customerr1 = db1.Customers.Find(1);
            var customerr2 = db2.Customers.Find(1);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}