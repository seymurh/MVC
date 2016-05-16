using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MongoDB.Bson;
using testApp.Filters;
using testApp.Models;
using testApp.Models.ModelBinders;

namespace testApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(config =>
            //{
            //    config.MapHttpAttributeRoutes();
            // // config.BindParameter(typeof(ObjectId), new ObjectIdModelBinder());
            // var provider = new SimpleModelBinderProvider(typeof(ObjectId), new ObjectIdApiModelBinder());
            //    config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            //    config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionLogger());
            //    config.Routes.MapHttpRoute(
            //        name: "DefaultApi",
            //        routeTemplate: "api/{controller}/{id}",
            //        defaults: new { id = RouteParameter.Optional }
            //    );
            //});
            GlobalConfiguration.Configure(WebApiConfig.Register);
           
            //    WebApiConfig.Register(GlobalConfiguration.Configuration);
          
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Bununla biz ModelBinders dictionary-sinə düzəltdiyimiz yeni Binderi əlavə edirik
            //User tipi tələb olunanda artıq həmin Binder işləyəcək
          //  ModelBinders.Binders.Add(typeof(User),new UserBinder());
            ModelBinders.Binders.Add(typeof(ObjectId), new ObjectIdModelBinder());
            AuthConfig.RegisterAuth();
            
        }
        
    }
}