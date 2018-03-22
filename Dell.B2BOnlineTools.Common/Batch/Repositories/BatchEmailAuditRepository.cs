

using Dell.B2BOnlineTools.Common.Batch.Models;
using Dell.B2BOnlineTools.Common.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dell.B2BOnlineTools.Common.Batch.Repositories
{
    public class BatchEmailAuditRepository : Repository<BatchEmailAudit>
    {
        public BatchEmailAuditRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
