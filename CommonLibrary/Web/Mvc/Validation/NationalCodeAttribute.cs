using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class NationalCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            string stringValue = Convert.ToString(value);
            return NationalCodeLogic.IsValid(stringValue);
        }
    }
}
