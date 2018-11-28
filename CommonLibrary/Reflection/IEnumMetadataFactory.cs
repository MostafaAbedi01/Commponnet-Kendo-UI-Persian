using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    public interface IEnumMetadataFactory
    {
        EnumMetadata<T> Get<T>();

        string GetCaption<T>(T item);

        string GetCombinedCaption<T>(T item, string seperator = ",");
    }
}
