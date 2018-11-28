using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;

namespace CommonLibrary.Web.Mvc.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class MustMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "باید با {0} یکسان باشد.";
        
        private readonly object _typeId = new object();
        
        public MustMatchAttribute(string propertyToMatch) :
            base(DefaultErrorMessage) { PropertyToMatch = propertyToMatch; }

        public string PropertyToMatch { get; private set; }
        
        public override object TypeId { get { return _typeId; } }
        
        public override string FormatErrorMessage(string name) { return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, PropertyToMatch); }
        
        public override bool IsValid(object value)
        {            // we don't have enough information here to be able to validate against another field            
            // we need the DataAnnotationsMustMatchValidator adapter to be registered           
            throw new Exception("MustMatchAttribute requires the DataAnnotationsMustMatchValidator adapter to be registered"); // TODO – make this a typed exception :-)  
        }
    }

    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    //public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    //{
    //    private const string _defaultErrorMessage = "'{0}' و '{1}' یکسان نیستند.";
    //    private readonly object _typeId = new object();

    //    public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
    //        : base(_defaultErrorMessage)
    //    {
    //        OriginalProperty = originalProperty;
    //        ConfirmProperty = confirmProperty;
    //    }

    //    public string ConfirmProperty { get; private set; }
    //    public string OriginalProperty { get; private set; }

    //    public override object TypeId
    //    {
    //        get
    //        {
    //            return _typeId;
    //        }
    //    }

    //    public override string FormatErrorMessage(string name)
    //    {
    //        return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
    //            OriginalProperty, ConfirmProperty);
    //    }

    //    public override bool IsValid(object value)
    //    {
    //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
    //        object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
    //        object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
    //        return Object.Equals(originalValue, confirmValue);
    //    }
    //}
}
