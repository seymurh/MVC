using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Repositories;
using testApp.Models;
using MongoDB.Bson;

namespace testApp.Controllers
{
    public class CategoryController : BlogBaseController
    {
        //
        // GET: /Category/
        CategoryRepository rep = new CategoryRepository();
        public ActionResult Index()
        {
            List<Category> categories=rep.Find();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                rep.Add(category);
                //((List<Category>)Session["categories"]).Add(category);
                Categories = rep.Find();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }



    }
}
