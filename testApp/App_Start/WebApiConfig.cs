using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.ModelBinding;
using MongoDB.Bson;
using testApp.Filters;
using testApp.Models.ModelBinders;

namespace testApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            // config.BindParameter(typeof(ObjectId), new ObjectIdModelBinder());
            var provider = new System.Web.Http.ModelBinding.Binders.SimpleModelBinderProvider(typeof(ObjectId), new ObjectIdApiModelBinder());
            config.Services.Insert(typeof(System.Web.Http.ModelBinding.ModelBinderProvider), 0, provider);
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionLogger());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}