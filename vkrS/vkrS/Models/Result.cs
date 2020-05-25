using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace vkrS.Models
{
    [DataContract]
    public class Result
    {
        [Required]
        [DataMember]
        public Guid ResultId { get; set; }

        [Required]
        [DataMember]
        public Guid ImageId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeSeriesId { get; set; }
        
        [DataMember]
        public string Res { get; set; }

        [Required]
        [DataMember]
        public TimeSpan Time { get; set; }
        
        [Required]
        [DataMember]
        public string Memory { get; set; }

        [Required]
        [DataMember]
        public string CPU { get; set; }

        public virtual Image Image { get; set; }
        
        public virtual TimeSeries TimeSeries { get; set; }
    }
}