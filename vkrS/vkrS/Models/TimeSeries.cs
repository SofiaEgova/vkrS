using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace vkrS.Models
{
    [DataContract]
    public class TimeSeries
    {
        [Required]
        [DataMember]
        public Guid TimeSeriesId { get; set; }

        [Required]
        [DataMember]
        public bool IsDouble { get; set; }

        [Required]
        [DataMember]
        public string Elements { get; set; }
    }
}