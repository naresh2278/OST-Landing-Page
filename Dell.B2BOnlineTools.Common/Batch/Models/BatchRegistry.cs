

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dell.B2BOnlineTools.Common.Batch.Models
{
    [Table("batch_registry")]
    public class BatchRegistry
    {
        [Key, Column("queue_name", Order = 0)]
        public string QueueName { get; set; }
        [Key, Column("batch_id", Order = 1)]
        public string BatchId { get; set; }
        [Column("current_batch_index", Order = 2)]
        public int BatchIndex { get; set; }
        [Column("last_update", Order = 3)]
        public DateTime UpdatedAt { get; set; }
        [Column("active", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Active { get; set; }
    }
}
