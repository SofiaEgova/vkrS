using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using Newtonsoft.Json;
using vkrS.Models;

namespace vkrS.Controllers
{
    public class TimeSeriesController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        // GET: TimeSeries
        public JsonResult GetTimeSeries()
        {
            return Json(db.TimeSeries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimeSeriesAdmin()
        {
            var ts = db.TimeSeries;
            ViewBag.TimeSeries = ts;
            ViewBag.TimeSeriesCount = db.TimeSeries.Count();
            return View();
        }

        [HttpPost]
        public ActionResult AddTimeSeries(HttpPostedFileBase fileInput, string title, string description)
        {
            if (fileInput == null) return null;
            int c = 0;
            string elements = "";
            using (StreamReader sr = new StreamReader(fileInput.InputStream))
            {
                string l = "";
                while ((l = sr.ReadLine()) != null)
                {
                    c++;
                    elements += l;

                }
            }
            int[] arr = new int[c];
            for (int i = 0; i < c; i++)
            {
                arr[i] = Convert.ToInt32(elements[i]);
            }
            var el = elements;
            elements = elements.Replace(Environment.NewLine, "");
            if (title == string.Empty) title = fileInput.FileName;
            if (description == string.Empty) description = "";
            var ts = new TimeSeries { TimeSeriesId = Guid.NewGuid(), AmountOfElements = c, Title = title, Elements = elements, Description = description };
            db.TimeSeries.Add(ts);
            db.SaveChanges();
            ViewBag.Title = title;

            var chartData = new StringBuilder();
            chartData.Append("[");
            for (int i = 0; i < c; i++)
            {
                if (i == ts.AmountOfElements - 1)
                {
                    chartData.Append(string.Format("{0}", ts.Elements[i]));
                    break;
                }
                chartData.Append(string.Format("{0},", elements[i]));
            }
            chartData.Append("]");
            ViewBag.chartData = chartData;
            ViewBag.Title = ts.Title;

            return View("TimeSeriesVisual");
        }

        public ActionResult VisualTimeSeries(string id)
        {
            var idts = Guid.Parse(id);
            var ts = db.TimeSeries.FirstOrDefault(t => t.TimeSeriesId == idts);

            var chartData = new StringBuilder();
            chartData.Append("[");
            for (int i = 0; i < ts.AmountOfElements; i++)
            {
                if(i== ts.AmountOfElements - 1)
                {
                    chartData.Append(string.Format("{0}", ts.Elements[i]));
                    break;
                }
                chartData.Append(string.Format("{0},", ts.Elements[i]));
            }
            chartData.Append("]");
            ViewBag.chartData = chartData;
            ViewBag.Title = ts.Title;

            return View("TimeSeriesVisual");
        }
    }
}