using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LOB.Core;

namespace LOB.Data
{
    public class LogProvider: LogManager
    {
        #region Logs

        public override Log GetLogByLogId(int logId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.get_log_by_log_id", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_log_id", SqlDbType.Int).Value = logId;
                        using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                                return GetLogFromReader(reader);
                            else
                                return null;
                        }
                  
                }
            }
        }

        public override List<Log> GetPagedLogs(int pageIndex, int pageSize, Log log)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.get_paged_logs", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_page_index", SqlDbType.Int).Value = pageIndex;
                        cmd.Parameters.Add("p_page_size", SqlDbType.Int).Value = pageSize;
                        cmd.Parameters.Add("p_product_id", SqlDbType.Int).Value = log.ProductId;
                        cmd.Parameters.Add("p_order_id", SqlDbType.Int).Value = log.OrderId;
                        cmd.Parameters.Add("p_order_item_id", SqlDbType.Int).Value = log.OrderItemId;
                        cmd.Parameters.Add("p_pick_id", SqlDbType.Int).Value = log.PickId;
                        cmd.Parameters.Add("p_pick_item_id", SqlDbType.Int).Value = log.PickItemId;
                        cmd.Parameters.Add("p_pack_id", SqlDbType.Int).Value = log.PackId;
                        cmd.Parameters.Add("p_pack_item_id", SqlDbType.Int).Value = log.PackItemId;
                        cmd.Parameters.Add("p_prev_state_id", SqlDbType.Int).Value = log.PrevState;
                        cmd.Parameters.Add("p_new_state_id", SqlDbType.Int).Value = log.NewState;
                        cmd.Parameters.Add("p_user_id", SqlDbType.Int).Value = log.UserId;
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            return GetLogCollectionFromReader(reader);
                        }
                    
                }
            }
        }

        public override int CountLogs(Log log)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.count_logs", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_product_id", SqlDbType.Int).Value = log.ProductId;
                        cmd.Parameters.Add("p_order_id", SqlDbType.Int).Value = log.OrderId;
                        cmd.Parameters.Add("p_order_item_id", SqlDbType.Int).Value = log.OrderItemId;
                        cmd.Parameters.Add("p_pick_id", SqlDbType.Int).Value = log.PickId;
                        cmd.Parameters.Add("p_pick_item_id", SqlDbType.Int).Value = log.PickItemId;
                        cmd.Parameters.Add("p_pack_id", SqlDbType.Int).Value = log.PackId;
                        cmd.Parameters.Add("p_pack_item_id", SqlDbType.Int).Value = log.PackItemId;
                        cmd.Parameters.Add("p_prev_state_id", SqlDbType.Int).Value = log.PrevState;
                        cmd.Parameters.Add("p_new_state_id", SqlDbType.Int).Value = log.NewState;
                        cmd.Parameters.Add("p_user_id", SqlDbType.Int).Value = log.UserId;
                        cmd.Parameters.Add("p_log_items_count", SqlDbType.Int).Direction = ParameterDirection.Output;
                       
                        int ret = ExecuteNonQuery(cmd);
                        int count = (int)cmd.Parameters["p_log_items_count"].Value;
                        return count;
                    }
               
            }
        }

        public override void InsertLog(object obj)
        {
            Log log = (Log) obj;

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.insert_log", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_product_id", SqlDbType.Int).Value = log.ProductId;
                        cmd.Parameters.Add("p_order_id", SqlDbType.Int).Value = log.OrderId;
                        cmd.Parameters.Add("p_order_item_id", SqlDbType.Int).Value = log.OrderItemId;
                        cmd.Parameters.Add("p_pick_id", SqlDbType.Int).Value = log.PickId;
                        cmd.Parameters.Add("p_pick_item_id", SqlDbType.Int).Value = log.PickItemId;
                        cmd.Parameters.Add("p_pack_id", SqlDbType.Int).Value = log.PackId;
                        cmd.Parameters.Add("p_pack_item_id", SqlDbType.Int).Value = log.PackItemId;
                        cmd.Parameters.Add("p_quantity", SqlDbType.Int).Value = log.Quantity;
                        cmd.Parameters.Add("p_prev_state_id", SqlDbType.Int).Value = log.PrevState;
                        cmd.Parameters.Add("p_new_state_id", SqlDbType.Int).Value = log.NewState;
                        cmd.Parameters.Add("p_user_id", SqlDbType.Int).Value = log.UserId;
                        cmd.Parameters.Add("p_info", SqlDbType.NVarChar).Value = log.Info;
                        cmd.Parameters.Add("p_log_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        
                        ExecuteNonQuery(cmd);
                        int id = (int)cmd.Parameters["p_log_id"].Value;
                    }
               
            }
        }

        #endregion

        #region Requests

        public override Request GetRequestByRequestId(int requestId)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.get_request_by_request_id", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_request_id", SqlDbType.Int).Value = requestId;
                        using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                                return GetRequestFromReader(reader);
                            else
                                return null;
                        }
                    }
                
            }
        }

        public override List<Request> GetPagedRequests(int pageIndex, int pageSize)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
               
                    using (SqlCommand cmd = new SqlCommand("public.get_paged_requests", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_page_index", SqlDbType.Int).Value = pageIndex;
                        cmd.Parameters.Add("p_page_size", SqlDbType.Int).Value = pageSize;
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            return GetRequestCollectionFromReader(reader);
                        }
                    }
                
            }
        }

        public override int CountRequests()
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.count_requests", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_request_items_count", SqlDbType.Int).Direction = ParameterDirection.Output;
                       
                        int ret = ExecuteNonQuery(cmd);
                        int count = (int)cmd.Parameters["p_request_items_count"].Value;
                        return count;
                    }
                
            }
        }

        public override int InsertRequest(Request request)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                
                    using (SqlCommand cmd = new SqlCommand("public.insert_request", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_ip", SqlDbType.NVarChar).Value = request.Ip;
                        cmd.Parameters.Add("p_user_agent", SqlDbType.NVarChar).Value = request.UserAgent;
                        cmd.Parameters.Add("p_user_name", SqlDbType.NVarChar).Value = request.UserName;
                        cmd.Parameters.Add("p_uri", SqlDbType.NVarChar).Value = request.Uri;
                        cmd.Parameters.Add("p_http_method", SqlDbType.NVarChar).Value = request.HttpMethod;
                        cmd.Parameters.Add("p_url_referrer", SqlDbType.NVarChar).Value = request.UrlReferrer;
                        cmd.Parameters.Add("p_url_referrer_host", SqlDbType.NVarChar).Value = request.UrlReferrerHost;
                        cmd.Parameters.Add("p_request_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                       
                        ExecuteNonQuery(cmd);
                        int id = (int)cmd.Parameters["p_request_id"].Value;
                        return id;
                    }
                
            }
        }

        #endregion
    }
}