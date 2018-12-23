using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LOB.Core;

namespace LOB.BLL
{
    public class DataMonitor
    {
        public static bool IsRun = false;

        public static event MonitorDelegate OnOrderEvent;

        protected static void OrderInvoked(DataEventArgs e)
        {
            if (OnOrderEvent != null)
                OnOrderEvent(EventArgs.Empty, e);
        }

        public static event MonitorDelegate OnPickEvent;

        protected static void PickInvoked(DataEventArgs e)
        {
            if (OnPickEvent != null)
            { OnPickEvent(EventArgs.Empty, e);}
        }

        public static event MonitorDelegate OnPackEvent;

        protected static void PackInvoked(DataEventArgs e)
        {
            if (OnPackEvent != null)
            { OnPackEvent(EventArgs.Empty, e);}
        }

        public static event MonitorDelegate OnRouteEvent;

        protected static void RouteInvoked(DataEventArgs e)
        {
            if (OnRouteEvent != null)
            { OnRouteEvent(EventArgs.Empty, e); }
        }

        public static void Start()
        {
            if (!IsRun)
            {
                Logs.InsertLog(new Log("Сервис запущен"));
                ThreadPool.QueueUserWorkItem(new WaitCallback(RunScanner), null);
                IsRun = true;
            }
        }

        private static void RunScanner(object state)
        {
            int counter = 0;
            while (IsRun)
            {
                int timeout = Timeout;
                Thread.Sleep(timeout);

                counter += (timeout/1000);

                if ((counter % 100) == 0)
                {
                    //Resets.ResetData();
                }

                if ((counter % 15) == 0) // runs over every 30 seconds 
                {
                    OrderInvoked(new DataEventArgs(counter.ToString()));
                }

                if ((counter % 30) == 0) // runs over every 45 seconds
                {
                    PickInvoked(new DataEventArgs(counter.ToString()));
                }

                if ((counter % 45) == 0) // runs over every 1 minute
                {
                    PackInvoked(new DataEventArgs(counter.ToString()));
                }

                if ((counter % 10) == 0) // runs over every 1 minute
                {
                    RouteInvoked(new DataEventArgs(counter.ToString()));
                }
            }
        }

        public static void Stop()
        {
            IsRun = false;
            Logs.InsertLog(new Log("Сервис остановлен"));
        }

        private static int Timeout
        {
            get
            {
                // 3600000 = hour
                DateTime dt = DateTime.Now;
                int timeout = 0;
                switch (dt.Hour)
                {
                    case 1:
                        timeout = 3600000;
                        break;
                    case 2:
                        timeout = 3600000;
                        break;
                    case 3:
                        timeout = 3600000;
                        break;
                    case 4:
                        timeout = 3600000;
                        break;
                    case 5:
                        timeout = 3600000;
                        break;
                    case 6:
                        timeout = 3600000;
                        break;
                    case 7:
                        timeout = 3600000;
                        break;
                    case 8:
                        timeout = 5000;
                        break;
                    case 9:
                        timeout = 5000;
                        break;
                    case 10:
                        timeout = 5000;
                        break;
                    case 11:
                        timeout = 5000;
                        break;
                    case 12:
                        timeout = 5000;
                        break;
                    case 13:
                        timeout = 5000;
                        break;
                    case 14:
                        timeout = 5000;
                        break;
                    case 15:
                        timeout = 5000;
                        break;
                    case 16:
                        timeout = 5000;
                        break;
                    case 17:
                        timeout = 5000;
                        break;
                    case 18:
                        timeout = 5000;
                        break;
                    case 19:
                        timeout = 5000;
                        break;
                    case 20:
                        timeout = 5000;
                        break;
                    case 21:
                        timeout = 3600000;
                        break;
                    case 22:
                        timeout = 3600000;
                        break;
                    case 23:
                        timeout = 3600000;
                        break;
                    default:
                        timeout = 3600000;
                        break;
                }
                return timeout;
            }
        }
    }
}
