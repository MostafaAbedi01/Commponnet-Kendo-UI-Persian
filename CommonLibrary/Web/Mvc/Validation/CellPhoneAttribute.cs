using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public sealed class CellPhoneAttribute : RegularExpressionAttribute
    {
        public CellPhoneAttribute(string regex = @"^(\0|\+)?[0-9 -()]{9,17}$")
            : base(regex)
        {
            ErrorMessage = "فرمت تلفن همراه وارد شده صحیح نیست.";
        }
    }
}
