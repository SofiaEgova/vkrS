using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using vkrS.Models;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        VKRDbContext db = new VKRDbContext();

        [TestMethod]
        public void TestMethod1()
        {
            int n = 100;
            string str = "12345";
            Guid[] ids = new Guid[n];
            var image = new Image { ImageId = Guid.NewGuid(), Link = "sofiakul/v8" };
            db.Images.Add(image);

            for(int i = 0; i < n; i++)
            {
                ids[i] = Guid.NewGuid();
                db.TimeSeries.Add(new TimeSeries { TimeSeriesId = ids[i], AmountOfElements = 5, Title = i + "", Elements = str });
                start(image.ImageId + "", ids[i] + "");
            }

            var results = db.Results.Where(r => r.ImageId == image.ImageId).Count();
            Assert.AreEqual(n, results);
        }

        public void start(string imageInput, string ts)
        {
            if (imageInput != null && ts != null)
            {
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

                    process.Start();

                    process.WaitForExit();
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

                    string result = srIncoming.ReadToEnd();
                    startTime.Stop();
                    var resultTime = startTime.Elapsed;
                    process.Close();
                    var res = new Result { ResultId = Guid.NewGuid(), ImageId = image.ImageId, TimeSeriesId = timeseries.TimeSeriesId, Accuracy = result, Time = resultTime };
                    db.Results.Add(res);
                    db.SaveChanges();
                }
            }
        }
    }
}
