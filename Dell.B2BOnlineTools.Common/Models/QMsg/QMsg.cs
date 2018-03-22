
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dell.B2BOnlineTools.Common.Models.QMsg
{
    public class QueueMessage
    {
        public QueueMessage()
        {
            Header = new Header();
            Operation = new Operation();
            Body = new Body();
        }
        [Required]
        public Header Header { get; set; }
        [Required]
        public Operation Operation { get; set; }
        [Required]
        public Body Body { get; set; }

    }

    public class Operation
    {
        public Operation()
        {
            Services = new List<Service>();
        }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Service> Services { get; set; }
    }
}
