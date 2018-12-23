using System;

namespace LOB.Core
{
    public delegate void MonitorDelegate(object sender, DataEventArgs e);

    public class DataEventArgs: EventArgs
    {
        private string _message;

        public string Message
        {
            get { return _message; }
        }

        public DataEventArgs(string message)
        {
            _message = message;
        }
    }
}