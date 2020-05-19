using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vkrS.Models;

namespace vkrS.Controllers
{
    public class HomeController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            SelectList images = new SelectList(db.Images, "ImageId", "Link");
            ViewBag.Images = images;
            SelectList ts = new SelectList(db.TimeSeries, "TimeSeriesId", "Elements");
            ViewBag.TimeSeries = ts;

            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                return Json(new { status = "error", message = "no user" });
            }
            user.IsActive = true;
            db.SaveChanges();
            return Redirect("~/TimeSeries/GetTimeSeriesAdmin");
        }

        
    }
}
