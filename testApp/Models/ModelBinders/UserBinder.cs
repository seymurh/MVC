using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testApp.Models.ModelBinders
{
    public class UserBinder:IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            User user = new User();
            string name= controllerContext.HttpContext.Request["Name"];
            user.Email = controllerContext.HttpContext.Request["email"];
            user.Name = "BizimModel";
            user.Surname = "Soy adı";
            return user;
        }
    }
}