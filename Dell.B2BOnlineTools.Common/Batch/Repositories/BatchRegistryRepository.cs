
using Dell.B2BOnlineTools.Common.Batch.Models;
using Dell.B2BOnlineTools.Common.EF.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dell.B2BOnlineTools.Common.Batch.Repositories
{
    public class BatchRegistryRepository : Repository<BatchRegistry>
    {
        public BatchRegistryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetIndex(string queueName, string batchId)
        {
            int nextIndex = 1;
            var count = await base.Count(x => x.QueueName == queueName && x.BatchId == batchId);
            if (count <= 0)
                await base.Add(new BatchRegistry()
                {
                    BatchId = batchId,
                    BatchIndex = nextIndex,
                    QueueName = queueName,
                    UpdatedAt = DateTime.Now
                });
            else
            {
                nextIndex = await base.Get(x => x.QueueName == queueName && x.BatchId == batchId).MaxAsync(x => x.BatchIndex);
                await base.Update(new BatchRegistry()
                {
                    BatchId = batchId,
                    BatchIndex = ++nextIndex,
                    QueueName = queueName,
                    UpdatedAt = DateTime.Now
                });
            }                
            return nextIndex;
        }
    }
}
