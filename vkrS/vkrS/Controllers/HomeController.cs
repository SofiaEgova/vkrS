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
            //проверка на наличие в бд
            return Redirect("~/TimeSeries/GetTimeSeriesAdmin");
        }

        [HttpPost]
        public void AddImage(string imageInput)
        {
            if (imageInput != null)
            {
                db.Images.Add(new Image { ImageId = Guid.NewGuid(), Link = imageInput });
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Start(string imageInput, string ts)
        {
            if (imageInput != null && ts!=null)
            {
                Guid imId = Guid.Parse(imageInput);
                Guid tsId = Guid.Parse(ts);
                var image = db.Images.FirstOrDefault(i => i.ImageId == imId);
                var timeseries = db.TimeSeries.FirstOrDefault(t => t.TimeSeriesId == tsId);
                if (image != null && timeseries != null)
                {
                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    Process process = new Process { StartInfo = new ProcessStartInfo { FileName = "cmd.exe", RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, Arguments= "/c docker pull " + image.Link } };

                    process.Start();
                    
                    process.WaitForExit();
                    int indexOfChar = image.Link.IndexOf('/')+1;
                    string im = image.Link.Remove(0, indexOfChar);
                    process.StartInfo=new ProcessStartInfo {  FileName = "cmd.exe", RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, Arguments = "/c docker run -e ARRAY=" + timeseries.Elements.Replace(Environment.NewLine, "") + " "+im } ;
                    process.Start();
                    StreamReader srIncoming = process.StandardOutput;

                    string result = srIncoming.ReadToEnd();
                    startTime.Stop();
                    var resultTime = startTime.Elapsed;

                    db.Results.Add(new Result { ResultId = Guid.NewGuid(), ImageId = image.ImageId, TimeSeriesId = timeseries.TimeSeriesId, Accuracy = "1", Time = resultTime });
                    db.SaveChanges();
                }
            }
        }
    }
}
