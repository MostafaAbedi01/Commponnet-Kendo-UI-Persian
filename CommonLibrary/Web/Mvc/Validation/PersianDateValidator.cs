using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CommonLibrary.Web.Mvc.Validation;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class PersianDateValidator : DataAnnotationsModelValidator<PersianDateAttribute>
    {
        public PersianDateValidator(ModelMetadata metadata, ControllerContext context, PersianDateAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule() { ErrorMessage = null, ValidationType = "cpDate" };

            if (Attribute.MaxOffsetToNow.HasValue)
                yield return new ModelClientValidationRule()
                {
                    ErrorMessage = null,
                    ValidationType = "cpMaxDate",
                    ValidationParameters = { { "maxDate", Attribute.MaxOffsetToNow.Value.Days } }
                };


            if (Attribute.MinOffsetToNow.HasValue)
                yield return new ModelClientValidationRule()
                {
                    ErrorMessage = null,
                    ValidationType = "cpMinDate",
                    ValidationParameters = { { "minDate", Attribute.MinOffsetToNow.Value.Days } }
                };
        }
    }
}
