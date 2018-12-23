using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using LOB.Core;

namespace LOB.BLL
{
    public abstract class BaseData
    {
        //public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected static ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        /// <summary>
        /// Remove from cache all items whose key starts with the input prefix
        /// </summary>
        protected static void RemoveFromCache(string key)
        {
            key = key.ToLower();
            List<string> items = new List<string>();

            items = Cache.Select(kvp => kvp.Key).ToList();

            foreach (string item in items)
            {
                if (item.ToLower().StartsWith(key))
                {
                    Cache.Remove(item);
                }
            }
        }

        /// <summary>
        /// A better and more flexible solution
        /// TODO: http://stackoverflow.com/questions/4183270/how-to-clear-the-net-4-memorycache/22388943#comment34789210_22388943
        /// </summary>
        public static void ClearCache()
        {
            var allKeys = Cache.Select(o => o.Key);
            foreach (string item in allKeys)
            {
                Cache.Remove(item);
            }
            //Parallel.ForEach(allKeys, key => Cache.Remove(key));
        }

        /// <summary>
        /// Cache the input data
        /// </summary>
        protected static void CacheData(string key, object data)
        {
            if (data != null)
            {
                Cache.Add(key, data, DateTime.Now.AddDays(1), null);
            }
        }

        protected static int GetPageIndex(int pageIndex, int pageSize)
        {
            if (pageSize <= 0)
                return 0;
            else
                return (int)Math.Floor((double)pageIndex / (double)pageSize);
        }



        protected static bool BarCodeIsValid(string barCode)
        {
            bool result = false;
            if (barCode.Length == 12)
            {
                char[] codes = barCode.ToCharArray();
                foreach (char code in codes)
                {
                    int number;
                    if (!Int32.TryParse(code.ToString(), out number))
                    {
                        barCode = String.Empty;
                        break;
                    }
                }

                result = true;
            }
            else
            {
                barCode = String.Empty;
            }
            return result;
        }

        /// <summary>
        /// Generates BarCode in Int64 value
        /// The legacy system requires BarCode as Int64
        /// Remove upon get upgraded  
        /// </summary>
        /// <returns>BarCode in Int64 value</returns>
        protected static long GetBarCode()
        {
            long result = 0;
            Random random = new Random();
            while (result <= 0)
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                result = BitConverter.ToInt64(buf, 0);
            }
            return result;
        }


        public static string GetStatus(ItemState state)
        {
            return EnumName.GetStringValue(state);
        }

        public static string GetSpanClass(ItemState state)
        {
            string output = String.Empty;
            string name = GetStatus(state);
            switch (state)
            {
                case ItemState.None:
                    output = String.Format("<span class=\"badge badge-none\">{0}</span>", name);
                    break;
                case ItemState.Created:
                    output = String.Format("<span class=\"badge badge-created\">{0}</span>", name);
                    break;
                //case ItemState.Assigned:
                //    output = String.Format("<span class=\"badge badge-assigned\">{0}</span>", name);
                //    break;
                //case ItemState.Accepted:
                //    output = String.Format("<span class=\"badge badge-accepted\">{0}</span>", name);
                //    break;
                //case ItemState.Added:
                //    output = String.Format("<span class=\"badge badge-added\">{0}</span>", name);
                //    break;
                //case ItemState.NotOnSale:
                //    output = String.Format("<span class=\"badge badge-missing\">{0}</span>", name);
                //    break;
                //case ItemState.Submitted:
                //    output = String.Format("<span class=\"badge badge-submitted\">{0}</span>", name);
                //    break;
                //case ItemState.Confirmed:
                //    output = String.Format("<span class=\"badge badge-confirmed\">{0}</span>", name);
                //    break;
                //case ItemState.Completed:
                //    output = String.Format("<span class=\"badge badge-completed\">{0}</span>", name);
                //    break;
                //case ItemState.Purchased:
                //    output = String.Format("<span class=\"badge badge-purchased\">{0}</span>", name);
                //    break;
                //case ItemState.Picking:
                //    output = String.Format("<span class=\"badge badge-picking\">{0}</span>", name);
                //    break;
                //case ItemState.Picked:
                //    output = String.Format("<span class=\"badge badge-picked\">{0}</span>", name);
                //    break;
                //case ItemState.Packing:
                //    output = String.Format("<span class=\"badge badge-packing\">{0}</span>", name);
                //    break;
                //case ItemState.Packed:
                //    output = String.Format("<span class=\"badge badge-packed\">{0}</span>", name);
                //    break;
                //case ItemState.Delivering:
                //    output = String.Format("<span class=\"badge badge-delivering\">{0}</span>", name);
                //    break;
                //case ItemState.Delivered:
                //    output = String.Format("<span class=\"badge badge-delivered\">{0}</span>", name);
                //    break;
                //case ItemState.Paid:
                //    output = String.Format("<span class=\"badge badge-paid\">{0}</span>", name);
                //    break;
                //case ItemState.Enabled:
                //    output = String.Format("<span class=\"badge badge-enabled\">{0}</span>", name);
                //    break;
                //case ItemState.Disabled:
                //    output = String.Format("<span class=\"badge badge-disabled\">{0}</span>", name);
                //    break;
                //case ItemState.Rejected:
                //    output = String.Format("<span class=\"badge badge-rejected\">{0}</span>", name);
                //    break;
                //case ItemState.Return:
                //    output = String.Format("<span class=\"badge badge-return\">{0}</span>", name);
                //    break;
                //case ItemState.Cancelled:
                //    output = String.Format("<span class=\"badge badge-cancelled\">{0}</span>", name);
                //    break;
                //case ItemState.Deleted:
                //    output = String.Format("<span class=\"badge badge-deleted\">{0}</span>", name);
                //    break;
                default:
                    throw new ArgumentException("ItemState");
            }
            return output;
        }





        public static string GetWeekDay(DateTime dt)
        {
            DayOfWeek dow = dt.DayOfWeek;
            switch (dow)
            {
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресенье";

            }
            return string.Empty;
        }

    }
}