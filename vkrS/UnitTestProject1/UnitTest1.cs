using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vkrS.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string array = "12345"; 

        [TestMethod]
        public void TestMethod1()
        {
            // генерировать много рядов и проходить на них много раз, посмотреть как изменится статистика

            var db = new VKRDbContext();

            Guid[] ids = new Guid[100];
            for(int i = 0; i < 100; i++)
            {
                ids[i] = Guid.NewGuid();
                db.TimeSeries.
            }
        }
    }
}
