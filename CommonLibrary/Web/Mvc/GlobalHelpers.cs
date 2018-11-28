using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using CommonLibrary.Web.Mvc.Validation;

namespace Mehr.Web.Mvc
{
    public static class GlobalHelpers
    {
        public static void EnableCustomBindings()
        {
            ModelBinders.Binders[typeof(long)] = new PersianDateTimeDefaultModelBinder() { };
            ModelBinders.Binders[typeof(long?)] = new PersianDateTimeDefaultModelBinder() { };
            ModelBinders.Binders[typeof(bool)] = new IntToBoolBinder() { };
        }

        public const string ScriptVirtualPath = "/js/";
        public const string DynamicScriptVirtualPath = "/js/f/";
        [Obsolete]
        public const string DynamicSciprtVirtualPath = DynamicScriptVirtualPath;

        public static void EnableCustomClientValidations()
        {
            ModelValidatorProviders.Providers.RemoveAt(2);//Remove data type validator
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DataTypeAttribute), typeof(DataTypeValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredAttribute), typeof(RequiredValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NonZeroRequiredAttribute), typeof(RequiredValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(StringLengthAttribute), typeof(StringLengthValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(PersianDateAttribute), typeof(PersianDateValidator));
            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(Mehr.Web.Mvc.Validation.PhoneAttribute), typeof(RegularExpressionAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CellPhoneAttribute), typeof(RegularExpressionAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MustMatchAttribute), typeof(MustMatchValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RangeAttribute), typeof(RangeValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NationalCodeAttribute), typeof(NationalCodeValidator));
        }

        public static void EnableDynamicValidationSeperation()
        {
            string folderName = HttpContext.Current.Server.MapPath(DynamicSciprtVirtualPath);
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }

    }
}
