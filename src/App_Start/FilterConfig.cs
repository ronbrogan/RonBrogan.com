using System.Web;
using System.Web.Mvc;
using Core.Filters;

namespace RonBrogan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
#if !DEBUG            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
#endif
            filters.Add(new CurrentContextFilter());
        }
    }
}
