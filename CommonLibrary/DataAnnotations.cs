using System;
using System.Web.Mvc;

namespace CommonLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DisplayPluralNameAttribute : Attribute, IMetadataAware
    {
        public DisplayPluralNameAttribute(string DisplyNamePlural)
        {
            this.DisplyNamePlural = DisplyNamePlural;
        }

        public string DisplyNamePlural { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("DisplyNamePlural", DisplyNamePlural);
        }
    }

    public class TooltipAttribute : Attribute, IMetadataAware
    {
        public TooltipAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }

        public string Tooltip { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["Tooltip"] = Tooltip;
        }
    }
}