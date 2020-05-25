using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vkrS.Models;
using System.Data.Entity;

namespace vkrS.Controllers
{
    public class StatisticController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        // GET: Statistic
        public ActionResult GetStatistic()
        {
            ViewBag.Results = db.Results.Include(r=>r.Image);
            ViewBag.TS = db.TimeSeries.Where(t=>t.Results.Count>0).OrderByDescending(t=>t.Results.Count);
            return View();
        }
    }
}