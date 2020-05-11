using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [DataMember]
        public string Link { get; set; }
    }
}