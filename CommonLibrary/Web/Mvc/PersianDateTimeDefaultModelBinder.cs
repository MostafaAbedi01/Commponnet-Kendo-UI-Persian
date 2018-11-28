using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mehr.Web.Mvc
{
    public class PersianDateTimeDefaultModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelName.EndsWith("Date") &&
                (bindingContext.ModelType == typeof(long) || bindingContext.ModelType == typeof(long?)))
            {
                ValueProviderResult valueResult = null;

                valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueResult == null)
                    return null;

                string stringValue = (string)valueResult.ConvertTo(typeof(string));
                PersianDateTime persianDateTime;
				if (PersianDateTime.TryParse(stringValue, out persianDateTime))
					return persianDateTime.ToLong();
				else
				{
					long value = 0;
					if(long.TryParse(stringValue,out value))
					{
                        return value;
                        //return PersianDateTime.FromLong(value);
					}
				}
                return null;
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
