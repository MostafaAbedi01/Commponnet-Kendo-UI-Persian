using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class DataTypeValidator : DataAnnotationsModelValidator<DataTypeAttribute>
    {
        public DataTypeValidator(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute)
            : base(metadata, context, attribute)
        {
            this.message = attribute.ErrorMessage;
        }

        string message;

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            List<ModelClientValidationRule> rules = new List<ModelClientValidationRule>();

            ModelClientValidationRule rule;
            switch (Attribute.DataType)
            {
                case DataType.EmailAddress:
                    rule = new ModelClientValidationRule() { ErrorMessage = message, ValidationType = "email" };
                    rule.ValidationParameters.Add("true", "true");
                    rules.Add(rule);
                    break;
                case DataType.Url:
                    rule = new ModelClientValidationRule() { ErrorMessage = message, ValidationType = "url" };
                    rule.ValidationParameters.Add("true", "true");
                    rules.Add(rule);
                    break;
                case DataType.Date:
                    rule = new ModelClientValidationRule() { ErrorMessage = message, ValidationType = "dpDate" };
                    rule.ValidationParameters.Add("true", "true");
                    rules.Add(rule);
                    break;
            }

            return rules;
        }
    }
}
