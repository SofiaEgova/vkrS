using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using vkrS.Models;

namespace UnitTestProject3
{
    public class db : DbContext
    {
        public virtual DbSet<TimeSeries> TimeSeries { get; set; }
    }
}
