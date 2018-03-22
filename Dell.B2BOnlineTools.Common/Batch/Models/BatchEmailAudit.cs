
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dell.B2BOnlineTools.Common.Batch.Models
{
    [Table("batch_email_audit")]
    public class BatchEmailAudit
    {
        [Key, Column("queue_name", Order = 0)]
        public string QueueName { get; set; }
        [Key, Column("batch_id", Order = 1)]
        public string BatchId { get; set; }
        [Key, Column("batch_index", Order = 2)]
        public int BatchIndex { get; set; }
        [Column("sent_object", Order = 3)]
        public string SentObject { get; set; }
        [Column("sent_at", Order = 4)]
        public DateTime SentAt { get; set; }
        [Column("sent_status", Order = 5)]
        public string SentStatus { get; set; }
    }
}
