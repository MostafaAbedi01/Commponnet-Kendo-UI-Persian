using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CommonLibrary.Web.Mvc.Validation
{
    public class NationalCodeValidator : DataAnnotationsModelValidator<NationalCodeAttribute>
    {
        public NationalCodeValidator(ModelMetadata metadata, ControllerContext context, NationalCodeAttribute attribute)
            : base(metadata, context, attribute)
        {
        }
    }
}
