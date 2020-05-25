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
    public class OpenImageController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        // GET: OpenImage
        public ActionResult GetOpenImage(Guid id)
        {
            var image = db.Images.FirstOrDefault(i => i.ImageId == id);
            if (image != null)
            {
                ViewBag.Link = image.Link;
                ViewBag.Id = image.ImageId;
                ViewBag.TimeSeries = db.TimeSeries;
                ViewBag.res = db.Results;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Start(string imageInput, string ts)
        {
            if (imageInput != null && ts != null)
            {
                int total = (int)GC.GetTotalMemory(true);
                Guid imId = Guid.Parse(imageInput);
                Guid tsId = Guid.Parse(ts);
                var image = db.Images.FirstOrDefault(i => i.ImageId == imId);
                var timeseries = db.TimeSeries.FirstOrDefault(t => t.TimeSeriesId == tsId);
                if (image != null && timeseries != null)
                {
                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    Process process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            Arguments = "/c docker pull " + image.Link
                        }
                    };

                    //process.Start();
                    //process.WaitForExit();

                    float cpu;
                    string result;
                    using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", "_Total"))
                    {
                        cpu = pcProcess.NextValue();

                        int indexOfChar = image.Link.IndexOf('/') + 1;
                        string im = image.Link.Remove(0, indexOfChar);
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            Arguments = "/c docker run -e ARRAY=" + timeseries.Elements.Replace(Environment.NewLine, "") + " " + im
                        };
                        process.Start();
                        StreamReader srIncoming = process.StandardOutput;

                        result = srIncoming.ReadToEnd();
                        startTime.Stop();
                        process.WaitForExit();
                        cpu = pcProcess.NextValue() / (float)Environment.ProcessorCount;
                    }

                    var c = (int)cpu;

                    process.Close();
                    var resultTime = startTime.Elapsed;

                    total = (int)GC.GetTotalMemory(true) - total;

                    total = ((int)(total / (float)Environment.ProcessorCount))/1024;
                    
                    var res = new Result { ResultId = Guid.NewGuid(), ImageId = image.ImageId, TimeSeriesId = timeseries.TimeSeriesId, Res = result, Time = resultTime , Memory=total.ToString(), CPU=c+"%"};
                    db.Results.Add(res);
                    db.SaveChanges();
                    return Redirect("~/Result/GetResult/" + res.ResultId);
                }
            }
            return Content("error");
        }
    }
}