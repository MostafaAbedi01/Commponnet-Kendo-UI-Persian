using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Mehr.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Class |  AttributeTargets.Property)]
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer Serializer = new JavaScriptSerializer();
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(stringified))
                {
                    var regex = new Regex(@"^(\[.*\]|{.*})$", RegexOptions.Compiled);
                    var allKeys = controllerContext.HttpContext.Request.Params.AllKeys.OrderBy(s => s);
                    var values = allKeys.Select(s => controllerContext.HttpContext.Request.Params[s]).ToList();
                    stringified = values.FirstOrDefault(regex.IsMatch);

                    if (string.IsNullOrEmpty(stringified)) return null;
                }
                return Serializer.Deserialize(stringified, bindingContext.ModelType);
            }
        }
    }

}
