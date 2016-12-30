using System.Web;
using System.Web.Mvc;
using Core.Filters;

namespace RonBrogan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CurrentContextFilter());
        }
    }
}
