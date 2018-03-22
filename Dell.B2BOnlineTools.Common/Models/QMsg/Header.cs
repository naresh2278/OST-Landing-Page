
using Dell.B2BOnlineTools.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dell.B2BOnlineTools.Common.Models.QMsg
{
    public class Header
    {
        [Required]
        public Source Source { get; set; }
        public Destination Destination { get; set; }

        public Channel Channel { get; set; }
        public string TimeStamp { get; set; } = DateTime.Now.ToGMTOffsetString();
    }

    public class Source
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        public object Others { get; set; }
    }
    public class Destination
    {
        [Required]
        public string[] Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Service> Services { get; set; }
    }
    public class Channel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public string Type { get; set; }
        public Url Url { get; set; }
    }

}
