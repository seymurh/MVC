using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;
using testApp.Services;
using testApp.Filters;
using MongoDB.Bson;

namespace testApp.Controllers
{
  //  [CustomExceptionFilter]
   
    public class EmployeesController:Controller
    {

       
        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.Request["id"] == "3")
        //    {
        //        filterContext.Result = RedirectToAction("GetEmployees");
        //    }
        //    else
        //    {
        //        base.OnAuthorization(filterContext);
        //    }
        //}

        public EmployeeService service = new EmployeeService();

       
        [CustomAuthorize]
      //  [Authorize(Roles="user")]
        [HttpGet]
        public ActionResult GetEmployees() 
        {
            throw new Exception("custom exception");
            List<Employee> employees = service.GetEmployees();
            return View(employees);
        }
     //   [CustomExceptionFilter]
        [CustomAuthorizeAttribute]
        [HttpGet]
        public ActionResult AddEmployee() 
        {
         //   throw new Exception("hey it's exception");
            ViewBag.Action = "AddEmployee";
            return View(new Employee());
        }
      
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            
            service.AddEmployee(employee);
            return RedirectToAction("GetEmployees");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UpdateEmployee(ObjectId id)
        {
            ViewBag.Action = "UpdateEmployee";
            Employee employee = service.GetEmployeeById(id);
            return View("_EmployeePartial",employee);
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            service.UpdateEmployee(employee);
            return  RedirectToAction("GetEmployees");
        }
        [HttpPost]
        public JsonResult GetEmployees(string k)
        {
            return Json(new { });
        }

        public ActionResult Error() 
        {
            return View();
        }
    }
}