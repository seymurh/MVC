using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        UserRepository rep = new UserRepository();
        public ActionResult Index()
        {
            List<User> users = new List<User>();
            users.Add(new User {Name="Xanlar",Surname="Beylerov",Email="aa@gmail"});
            users.Add(new User { Name = "Turkan", Surname = "Zeynalova", Email = "gasd@gmail" });
            users.Add(new User { Name = "Selim", Surname = "Sultanov", Email = "bbb@gmail" });
            users.Add(new User { Name = "Zahid", Surname = "Qurbanov", Email = "bbvca@gmail" });
            return View(users);
        }
        [HttpGet]
        public ActionResult AddUser() 
        {
            User user = new User();
            user.Name = "Qafar";
            user.Surname = "Akifov";
            user.Email = "q.a@gmail.com";
            return View(user);
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            user.Name = "Suleyman";
            return View(user);
        }
        public ActionResult adf()
        {
            return View();
        }
        public ActionResult Delete(ObjectId id,string returnUrl)
        {
            if (id != ObjectId.Empty) 
            {
                rep.Delete(id);
              // return RedirectToAction("Users", "Admin");
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "User Id set olunmayıb!");
                return Redirect(returnUrl);
            }
            
        }
        [HttpGet]
        public JsonResult Find(ObjectId id)
        {
            User user = rep.FindById(id);
            return Json(user,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            rep.Update(user);
            return RedirectToAction("Users","Admin");
        }
    }
}
