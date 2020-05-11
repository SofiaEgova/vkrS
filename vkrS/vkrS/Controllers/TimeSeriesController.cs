using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
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
            return View();
        }

        [HttpPost]
        public void AddTimeSeries()
        {
            HttpPostedFileBase file = Request.Files["fileInput"];
            string elements = "";
            using (StreamReader sr = new StreamReader(file.InputStream))
            {
                elements+=sr.ReadToEnd();
            }
            // считать первое число, если оно дабл то изменить на дабл!!!
            db.TimeSeries.Add(new TimeSeries { TimeSeriesId = Guid.NewGuid(), IsDouble = true, Elements = elements });
            db.SaveChanges();
        }

        //// GET: api/TimeSeries/5
        //[ResponseType(typeof(TimeSeries))]
        //public IHttpActionResult GetTimeSeries(Guid id)
        //{
        //    TimeSeries timeSeries = db.TimeSeries.Find(id);
        //    if (timeSeries == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(timeSeries);
        //}

        //// POST: api/TimeSeries
        //[ResponseType(typeof(TimeSeries))]
        //public IHttpActionResult PostTimeSeries(TimeSeries timeSeries)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.TimeSeries.Add(timeSeries);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TimeSeriesExists(timeSeries.TimeSeriesId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = timeSeries.TimeSeriesId }, timeSeries);
        //}
    }
}