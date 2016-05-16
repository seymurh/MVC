using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace testApp.Filters
{
    public class CustomExceptionLogger:Attribute,IExceptionLogger,IExceptionHandler
    {
        public System.Threading.Tasks.Task LogAsync(ExceptionLoggerContext context, System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleAsync(ExceptionHandlerContext context, System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}