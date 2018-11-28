using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class RangeValidator : DataAnnotationsModelValidator<RangeAttribute>
    {
        public RangeValidator(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRangeRule(null, Attribute.Minimum, Attribute.Maximum) };
        }
    }
}
