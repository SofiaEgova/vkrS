using System;
using System.Collections.Generic;
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

            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            //проверка на наличие в бд
            return Redirect("~/TimeSeries/GetTimeSeriesAdmin");
        }
    }
}
