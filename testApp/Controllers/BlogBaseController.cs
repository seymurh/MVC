using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;
using testApp.Repositories;


namespace testApp.Controllers
{
    public class BlogBaseController : Controller
    {
        public List<Category> Categories
        {
            get
            {

                if (Session != null && Session["categories"] == null)
                {

                    CategoryRepository rep = new CategoryRepository();
                    List<Category> categories = rep.Find();
                    Session["categories"] = categories;
                }
                return Session["categories"] as List<Category>;
            }
            set
            {
                Session["categories"] = value;
            }
        }
        
         

       
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.categories=this.Categories;
           
            base.OnActionExecuting(filterContext);
        }


    }
}
