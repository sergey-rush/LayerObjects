using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LOB.Http
{
    public class HttpModule : IHttpModule
    {
        void IHttpModule.Init(HttpApplication app)
        {
            app.AddOnPreRequestHandlerExecuteAsync(new BeginEventHandler(OnBegin), new EndEventHandler(OnEnd));
        }

        private IAsyncResult OnBegin(object sender, EventArgs e, AsyncCallback cb, object state)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            AsyncOperation asyncOperation = new AsyncOperation(cb, context, state);
            asyncOperation.StartAsyncTask();
            return asyncOperation;
        }

        private void OnEnd(IAsyncResult asyncResult)
        {

        }

        void IHttpModule.Dispose()
        {
        }
    }
}
