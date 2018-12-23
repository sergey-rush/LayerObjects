using System;
using System.Threading;
using System.Web;

namespace LOB.Http
{
    internal class AsyncOperation : IAsyncResult
    {
        private bool _completed;
        private readonly object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;

        bool IAsyncResult.IsCompleted
        {
            get { return _completed; }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return null; }
        }

        object IAsyncResult.AsyncState
        {
            get { return _state; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }

        internal AsyncOperation(AsyncCallback callback, HttpContext context, object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        internal void StartAsyncTask()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartRequest), null);
            _completed = true;
            _callback(this);
        }

        private void StartRequest(object workItemState)
        {
            LogRequest logRequest = new LogRequest();
            logRequest.ProcessRequest(_context);
        }
    }
}