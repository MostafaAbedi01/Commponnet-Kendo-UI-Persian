using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class MustMatchValidator : DataAnnotationsModelValidator<MustMatchAttribute>
    {
        public MustMatchValidator(ModelMetadata metadata, ControllerContext context, MustMatchAttribute attribute)
            : base(metadata, context, attribute) { }
        public override System.Collections.Generic.IEnumerable<ModelValidationResult> Validate(object container)
        {
            var propertyToMatch = Metadata.ContainerType.GetProperty(Attribute.PropertyToMatch); if (propertyToMatch != null)
            {
                var valueToMatch = propertyToMatch.GetValue(container, null); var value = Metadata.Model;
                bool valid = Equals(value, valueToMatch); if (!valid)
                {
                    yield return new ModelValidationResult { Message = ErrorMessage };
                }
            }
            // we're not calling base.Validate here so that the attribute IsValid method doesn't get called       
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            ModelClientValidationRule rule = new ModelClientValidationRule() { ErrorMessage = ErrorMessage, ValidationType = "mustMatch" };
            rule.ValidationParameters.Add("fieldToMatch", base.Attribute.PropertyToMatch);
            return new[] { rule };
        }
    }
    //public class PropertiesMustMatchValidator : DataAnnotationsModelValidator<PropertiesMustMatchAttribute>
    //{
    //    public PropertiesMustMatchValidator(ModelMetadata metadata, ControllerContext context, PropertiesMustMatchAttribute attribute)
    //        : base(metadata, context, attribute)
    //    {
    //    }

    //    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
    //    {
    //        ModelClientValidationRule rule = new ModelClientValidationRule() { ErrorMessage = null, ValidationType = "mustMatch" };
    //        rule.ValidationParameters.Add("true", base.Attribute.ConfirmProperty);
    //        return new[] { rule };
    //    }
    //}
}
