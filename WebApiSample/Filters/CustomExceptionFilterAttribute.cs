using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiSample.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(actionExecutedContext.Response.StatusCode)
            {
                Content = new StringContent(actionExecutedContext.Response.ReasonPhrase, Encoding.UTF8, "text/javascript")
            };
        }
    }
}