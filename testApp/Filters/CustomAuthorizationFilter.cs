using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace testApp.Filters
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
          
            string id=httpContext.Request["id"] ;
            if (id== "1")
            {
                return true;
            }
            else if (id == "2")
            {
                return false;
            }
            else
            {
                return base.AuthorizeCore(httpContext);
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            base.OnAuthorization(filterContext);
        }

    }
}