using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;

namespace testApp.Models.ModelBinders
{
    public class ObjectIdModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider
                .GetValue(key);

            if (valueProviderResult == null ||
                string.IsNullOrEmpty(valueProviderResult
                    .AttemptedValue))
            {
                return ObjectId.Empty;
            }
            else
            {
                return new ObjectId(valueProviderResult.AttemptedValue);
            }
        }
    }
    public class ObjectIdApiModelBinder : System.Web.Http.ModelBinding.IModelBinder
    {

        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider
                .GetValue(key);

            if (valueProviderResult == null ||
                string.IsNullOrEmpty(valueProviderResult
                    .AttemptedValue))
            {
                bindingContext.Model = ObjectId.Empty;
                return true;
            }
            else
            {
                bindingContext.Model = new ObjectId(valueProviderResult.AttemptedValue);
                return true;
            }
        }
    }
}