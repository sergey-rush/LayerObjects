using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LOB.Core;
using LOB.Data;

namespace LOB.BLL
{
    public class Logs: BaseData
    {
        #region Logs
        public static Log GetLogByLogId(int logId)
        {
            Log log = null;
            string key = "Logs_GetLogByLogId_" + logId;

            if (Cache[key] != null)
            {
                log = (Log)Cache[key];
            }
            else
            {
                log = DataAccess.Logs.GetLogByLogId(logId);
                CacheData(key, log);
            }
            return log;
        }
        
        public static List<Log> GetPagedLogs(int pageIndex, int pageSize, Log log)
        {
            return DataAccess.Logs.GetPagedLogs(pageIndex, pageSize, log);
        }

        public static int CountLogs(Log log)
        {
            return DataAccess.Logs.CountLogs(log);
        }

        public static void InsertLog(Log log)
        {
            ThreadPool.QueueUserWorkItem(DataAccess.Logs.InsertLog, log);
        }

        #endregion

        #region Requests

        public static Request GetRequestByRequestId(int requestId)
        {
            Request request = null;
            string key = "Requests_GetRequestByRequestId_" + requestId;

            if (Cache[key] != null)
            {
                request = (Request)Cache[key];
            }
            else
            {
                request = DataAccess.Logs.GetRequestByRequestId(requestId);
                CacheData(key, request);
            }
            return request;
        }

        public static List<Request> GetPagedRequests(int pageIndex, int pageSize)
        {
            return DataAccess.Logs.GetPagedRequests(pageIndex, pageSize);
        }

        public static int CountRequests()
        {
            return DataAccess.Logs.CountRequests();
        }

        public static int InsertRequest(Request request)
        {
            return DataAccess.Logs.InsertRequest(request);
        }

        #endregion
    }
}
