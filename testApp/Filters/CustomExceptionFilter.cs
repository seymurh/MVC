using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Controllers;


namespace testApp.Filters
{
    public class CustomExceptionFilter:FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            
            //Yani exception davam eləməsin
            filterContext.ExceptionHandled = true;
            filterContext.RouteData.Values["action"]= "Error";
            var controller=new EmployeesController();
            controller.ViewBag.error = filterContext.Exception.Message;
            filterContext.Result = controller.Error();
            var route= filterContext.RouteData;
        }
    }
}