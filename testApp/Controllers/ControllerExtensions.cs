using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Controllers
{
    public static class ControllerExtensions
    {
        public static User CurrentUser(this Controller controller)
        {
            string username= controller.User.Identity.Name;
            UserRepository userRepository=new UserRepository();
            User user = userRepository.FindUser(username);
            return user;
        }
    }
}