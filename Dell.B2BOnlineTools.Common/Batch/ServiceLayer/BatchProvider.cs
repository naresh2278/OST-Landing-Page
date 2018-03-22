
using Dell.B2BOnlineTools.Common.Batch.Models;
using Dell.B2BOnlineTools.Common.Batch.Repositories;
using Dell.B2BOnlineTools.Common.EF.Context;
using Dell.B2BOnlineTools.Common.EF.Repository;
using Dell.B2BOnlineTools.Common.EF.ServiceLayer;

namespace Dell.B2BOnlineTools.Common.Batch.ServiceLayer
{
    public class BatchProvider : Store
    {
        public BatchProvider(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        private BatchRegistryRepository _batchRegistryRepository;
        private BatchEmailAuditRepository _batchEmailAuditRepository;
        public BatchRegistryRepository Registry
        {
            get
            {
                if(_batchRegistryRepository == null)
                    lock (syncLock)
                        if (_batchRegistryRepository == null)
                            _batchRegistryRepository = new BatchRegistryRepository(_applicationDbContext);
                return _batchRegistryRepository;
            }
        }
        public BatchEmailAuditRepository EmailAudit
        {
            get
            {
                if(_batchEmailAuditRepository == null)
                    lock (syncLock)
                        if (_batchEmailAuditRepository == null)
                            _batchEmailAuditRepository = new BatchEmailAuditRepository(_applicationDbContext);
                return _batchEmailAuditRepository;
            }
        }
    }
}
