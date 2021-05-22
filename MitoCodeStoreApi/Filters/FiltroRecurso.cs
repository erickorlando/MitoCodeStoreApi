using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MitoCodeStoreApi.Filters
{
    public class FiltroRecurso : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }
    }
}