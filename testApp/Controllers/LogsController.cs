using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Controllers
{
    public class LogsController : Controller
    {
        //
        // GET: /Logs/
        LogRepository rep = new LogRepository();
        
        public ActionResult Index()
        {
            string[] users = "seymur,sahib,zahir".Split(',');
            
            List<Log> logs = rep.Get(); 
            return View(logs);
        }

    }
   
}
