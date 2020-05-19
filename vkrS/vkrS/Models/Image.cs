using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace vkrS.Models
{
    [DataContract]
    public class Image
    {
        [Required]
        [DataMember]
        public Guid ImageId { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [Required]
        [DataMember]
        public string Link { get; set; }

        [ForeignKey("ImageId")]
        public virtual List<Result> Results { get; set; }
    }
}