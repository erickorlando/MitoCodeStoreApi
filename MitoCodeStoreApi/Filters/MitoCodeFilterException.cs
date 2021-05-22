using Microsoft.AspNetCore.Mvc.Filters;

namespace MitoCodeStoreApi.Filters
{
    public class MitoCodeFilterException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MitcodeException)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}