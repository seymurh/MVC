using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using testApp.Services;
using testApp.Models;
using testApp.Repositories;
using MongoDB.Bson;
using testApp.Filters;
using System.IO;

namespace testApp.Controllers
{

    public class AdminController : BlogBaseController
    {
       

        RoleRepository rep = new RoleRepository();
        UserRepository userRep = new UserRepository();
        // GET: /Admin/
        RoleProvider provider = new CustomRoleProvider();
        
        public ActionResult Index()
        {
           // User user = this.CurrentUser();
            List<Role> roles = rep.GetRoles();
            //List<User> users = userRep.GetUsers();
            //ViewBag.users = users;
            return View(roles);
        }
        public JsonResult GetUsers()
        {
         
            List<User> users = userRep.GetUsers();
            return Json(users);
        }
        [HttpGet]
        public ActionResult Update(ObjectId id)
        {
           
           
            Role role = rep.FindRoleById(id);
            return View(role);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var mvcPath = "/Images/Users/" + fileName;
                        var path = Server.MapPath("~" + mvcPath);
                        file.SaveAs(path);
                        user.ProfileImageUrl = mvcPath;
                    }
                }
                userRep.AddUser(user);
                return RedirectToAction("Users");
            }
            else
            {
                return View();
            }
           
        }
       
        [HttpPost]
        public ActionResult Update(Role role)
        {
            if (ModelState.IsValid)
            {
                rep.Update(role);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddRole() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(Role role) 
        {
            rep.AddRole(role);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(ObjectId id)
        {
            rep.DeleteRole(id);
            return RedirectToAction("Index");
        }

        public ActionResult Users()
        {
            List<User> users = userRep.GetUsers();
            return View(users);
        }
        [HttpGet]
        public ActionResult AddRoleToUser(ObjectId id)
        {
            User user = userRep.FindById(id);
            ViewBag.Roles = rep.GetRoles();
            return View(user);
        }
        [HttpPost]
        public ActionResult AddRoleToUser(ObjectId userid, ObjectId roleId)
        {
            Role role = rep.FindRoleById(roleId);
            User user = userRep.FindById(userid);
            if (user.Roles == null)
            {
                user.Roles = new List<Role>();
            }
            user.Roles.Add(role);
            userRep.Update(user);
            return RedirectToAction("Users");
        }



    }
}
