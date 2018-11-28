using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public sealed class PhoneAttribute : RegularExpressionAttribute
    {
        public PhoneAttribute()
            : base(@"^(\0|\+)?[0-9 -()]{7,12}$")
        {
            ErrorMessage = "فرمت تلفن وارد شده صحیح نیست.";
        }
    }
}
