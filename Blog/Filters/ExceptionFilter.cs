using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

using Blog.Utils;

namespace Blog.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        LogUtil logUtil = new LogUtil();
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                logUtil.addLog("error", "Endpoint not found: " + context.Request.RequestUri, context.Exception);
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}