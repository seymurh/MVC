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
}