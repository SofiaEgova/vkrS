using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vkrS.Models;
using System.Data.Entity;

namespace vkrS.Controllers
{
    public class ResultController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        // GET: Result
        public ActionResult GetResult(Guid id)
        {
            var result = db.Results.FirstOrDefault(r => r.ResultId == id);
            ViewBag.Result = result;

            var ts = db.TimeSeries.FirstOrDefault(t => t.TimeSeriesId == result.TimeSeriesId);
            var allResults = db.Results.Include(x => x.Image).Where(r => r.TimeSeriesId == ts.TimeSeriesId);
            ViewBag.AllResults = allResults;
            
            return View();
        }
    }
}