using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testApp.Controllers
{
    public class HomeController : BlogBaseController 
    {
        [Authorize]
        public ActionResult Index()
        {

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return  View("Index",new { Name = "Ilqar", Salary = "1890" });
        }

        public FileResult About()
        {
            ViewBag.Message = "Your app description page.";

            return File("", "");
        }
        
        public ActionResult Contact(HttpFileCollection col)
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
