using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Models.QMsg
{
    public interface IQMsgHandler
    {
        event QMsgExceptionHandler ExceptionHandler;
        QueueMessage QMsg { get; }
        QInfo QInfo { get; }

        bool Execute();
        void OnException(Service service, QMsgExceptionEventArgs args, Exception e);
    }

    public delegate void QMsgExceptionHandler(Service service, QMsgExceptionEventArgs args, Exception e);
    public class QMsgExceptionEventArgs : EventArgs
    {
        public QInfo qInfo { get; set; }        
        public object[] value { get; set; }
    }

    public class QInfo
    {
        public QInfo(string qName, string batchId, int batchIndex)
        {
            QName = qName;
            BatchIndex = batchIndex;
            BatchId = batchId;
            QueueReadAt = DateTime.Now;
        }
        public string QName { get; protected set; }
        public string BatchId { get; protected set; }
        public int BatchIndex { get; protected set; }
        public DateTime QueueReadAt { get; private set; }
    }
}
