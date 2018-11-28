using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class StringLengthValidator : StringLengthAttributeAdapter
    {
        public StringLengthValidator(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var maxLenghtValidator = new ModelClientValidationStringLengthRule(null, 0, Attribute.MaximumLength);
            if (Attribute.MinimumLength != 0)
            {
                ModelClientValidationRule rule = new ModelClientValidationRule() { ErrorMessage = null, ValidationType = "stringMinLength" };
                rule.ValidationParameters.Add("minimumLength", Attribute.MinimumLength);
                return new[] { maxLenghtValidator, rule };
            }
            return new[] { maxLenghtValidator };
        }
    }
}
