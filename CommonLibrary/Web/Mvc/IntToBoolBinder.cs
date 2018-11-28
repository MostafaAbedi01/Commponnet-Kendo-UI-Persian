using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mehr.Web.Mvc
{
    public class IntToBoolBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(bool))
            {
                ValueProviderResult valueResult = null;

                valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueResult == null)
                    return base.BindModel(controllerContext, bindingContext); ;

                int intValue;
                if (int.TryParse(valueResult.AttemptedValue, out intValue))
                    return intValue != 0;
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
