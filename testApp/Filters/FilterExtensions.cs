using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Filters
{
    public static class FilterExtensions
    {
        public static User CurrentUser(this HttpContextBase httpContext)
        {
            string username = httpContext.User.Identity.Name;
            UserRepository userRepository = new UserRepository();
            User user = userRepository.FindUser(username);
            return user;
        }
    }
}