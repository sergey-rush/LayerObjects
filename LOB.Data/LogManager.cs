using System;
using System.Collections.Generic;
using System.Data;
using LOB.Core;

namespace LOB.Data
{
    public abstract class LogManager: DataAccess
    {
        private static LogManager _instance;

        public static LogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogProvider();
                }
                return _instance;
            }
        }

        #region Logs

        public abstract Log GetLogByLogId(int logId);
        public abstract List<Log> GetPagedLogs(int pageIndex, int pageSize, Log log);
        public abstract int CountLogs(Log log);
        public abstract void InsertLog(object obj);

        protected virtual Log GetLogFromReader(IDataReader reader)
        {
            Log log = new Log()
            {
                Id = (int)reader["id"],
                Created = (DateTime)reader["created"]
            };

            if (reader["product_id"] != DBNull.Value)
            {
                log.ProductId = (int) reader["product_id"];
            }
            if (reader["shop_id"] != DBNull.Value)
            {
                log.ShopId = (int)reader["shop_id"];
            }
            if (reader["order_id"] != DBNull.Value)
            {
                log.OrderId = (int)reader["order_id"];
            }
            if (reader["order_item_id"] != DBNull.Value)
            {
                log.OrderItemId = (int)reader["order_item_id"];
            }
            if (reader["pick_id"] != DBNull.Value)
            {
                log.PickId = (int)reader["pick_id"];
            }
            if (reader["pick_item_id"] != DBNull.Value)
            {
                log.PickItemId = (int)reader["pick_item_id"];
            }
            if (reader["pack_id"] != DBNull.Value)
            {
                log.PackId = (int)reader["pack_id"];
            }
            if (reader["pack_item_id"] != DBNull.Value)
            {
                log.PackItemId = (int)reader["pack_item_id"];
            }
            if (reader["quantity"] != DBNull.Value)
            {
                log.Quantity = (int)reader["quantity"];
            }
            if (reader["prev_state_id"] != DBNull.Value)
            {
                log.PrevState = (ItemState)reader["prev_state_id"];
            }
            if (reader["new_state_id"] != DBNull.Value)
            {
                log.NewState = (ItemState)reader["new_state_id"];
            }
            if (reader["user_id"] != DBNull.Value)
            {
                log.UserId = (int)reader["user_id"];
            }
            if (reader["info"] != DBNull.Value)
            {
                log.Info = reader["info"].ToString();
            }
            return log;
        }

        protected virtual List<Log> GetLogCollectionFromReader(IDataReader reader)
        {
            List<Log> items = new List<Log>();
            while (reader.Read())
                items.Add(GetLogFromReader(reader));
            return items;
        }

        #endregion

        #region Requests

        public abstract Request GetRequestByRequestId(int requestId);
        public abstract List<Request> GetPagedRequests(int pageIndex, int pageSize);
        public abstract int CountRequests();
        public abstract int InsertRequest(Request request);

        protected virtual Request GetRequestFromReader(IDataReader reader)
        {// id, ip, user_agent, user_name, uri, input_data, http_method, url_referrer, url_referrer_host, created
            Request request = new Request()
            {
                Id = (int)reader["id"],
                Created = (DateTime)reader["created"]
            };

            if (reader["ip"] != DBNull.Value)
            {
                request.Ip = reader["ip"].ToString();
            }
            if (reader["user_agent"] != DBNull.Value)
            {
                request.UserAgent = reader["user_agent"].ToString();
            }
            if (reader["user_name"] != DBNull.Value)
            {
                request.UserName = reader["user_name"].ToString();
            }
            if (reader["uri"] != DBNull.Value)
            {
                request.Uri = reader["uri"].ToString();
            }
            if (reader["http_method"] != DBNull.Value)
            {
                request.HttpMethod = reader["http_method"].ToString();
            }
            if (reader["url_referrer"] != DBNull.Value)
            {
                request.UrlReferrer = reader["url_referrer"].ToString();
            }
            if (reader["url_referrer_host"] != DBNull.Value)
            {
                request.UrlReferrerHost = reader["url_referrer_host"].ToString();
            }
            return request;
        }

        protected virtual List<Request> GetRequestCollectionFromReader(IDataReader reader)
        {
            List<Request> items = new List<Request>();
            while (reader.Read())
                items.Add(GetRequestFromReader(reader));
            return items;
        }

        #endregion
    }
}