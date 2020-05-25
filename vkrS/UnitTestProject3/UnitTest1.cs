using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using vkrS.Models;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        db datb = new db();

        [TestMethod]
        public void TestMethod1()
        {
            datb.TimeSeries.AddRange(gen(10));
        }

        [TestMethod]
        public void TestMethod2()
        {
            datb.TimeSeries.AddRange(gen(100));
        }

        [TestMethod]
        public void TestMethod3()
        {
            datb.TimeSeries.AddRange(gen(1000));
        }

        public TimeSeries[] gen(int count)
        {
            TimeSeries[] ids = new TimeSeries[count];
            string str = "12345";
            for(int i = 0; i < count; i++)
            {
                ids[i] = new TimeSeries { TimeSeriesId = Guid.NewGuid(), AmountOfElements = 5, Elements = str };
            }
            return ids;
        }
    }
}
