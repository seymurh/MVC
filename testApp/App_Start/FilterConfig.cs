using System.Web;
using System.Web.Mvc;
using testApp.Filters;

namespace testApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionFilter());
           // filters.Add(new LogActionFilter());
            //filters.Add(new CustomExceptionFilter());
        }
        
    }
}