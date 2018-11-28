using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Reflection
{
    public static class EnumMappingExtensions
    {
        public static Nullable<TDestinationEnum> MapTo<TDestinationEnum>(this Enum sourceValue, string correlationName = null)
            where TDestinationEnum : struct
        {
            return AttributeMapper<TDestinationEnum>.MapFrom(sourceValue, correlationName);
        }

        public static TDestinationEnum[] MapToMultiple<TDestinationEnum>(this Enum sourceValue, string correlationName = null)
           where TDestinationEnum : struct
        {
            return AttributeMapper<TDestinationEnum>.MapFromToMultiple(sourceValue, correlationName);
        }
    }

    public class AttributeMapper<TDestinationEnum>
         where TDestinationEnum : struct
    {
        public static Nullable<TDestinationEnum> MapFrom(Enum sourceValue, string correlationName = null)
        {
            var result = MapFromToMultiple(sourceValue, correlationName);
            if (result.Length == 0)
                return null;
            return result[0];
        }

        public static TDestinationEnum[] MapFromToMultiple(Enum sourceValue, string correlationName = null)
        {
            if (correlationName == "") correlationName = null;
            var sourceEnumType = sourceValue.GetType();
            var destinationEnumType = typeof(TDestinationEnum);

            var enumMapAttribute = GetEnumMapAttribute(sourceEnumType, destinationEnumType, correlationName);

            return Map(sourceValue, sourceEnumType, destinationEnumType, enumMapAttribute);
        }


        public static Nullable<TDestinationEnum> FindMap(Enum sourceValue, string correlationName = null)
        {
            if (correlationName == "") correlationName = null;
            var sourceEnumType = sourceValue.GetType();
            var destinationEnumType = typeof(TDestinationEnum);
            var enumMapAttribute = GetEnumMapAttribute(destinationEnumType, sourceEnumType, correlationName);

            Nullable<TDestinationEnum> result = null;
            if (enumMapAttribute is NamingEnumMapAttribute)
            {
                TDestinationEnum resultValue;
                if (Enum.TryParse<TDestinationEnum>(sourceValue.ToString(), out resultValue))
                    result =resultValue ;
            }
            else if (enumMapAttribute is AttributeEnumMapAttribute)
            {
                foreach (var field in destinationEnumType.GetFields())
                {
                    var mappingAttribute = field.
                        GetCustomAttributes((enumMapAttribute as AttributeEnumMapAttribute).MapperAttrbuiteType, true).
                        FirstOrDefault() as EnumItemMappingAttribute;

                    if (mappingAttribute != null)
                    {
                        var destinationEnums = mappingAttribute.MappedValues.Select(e =>
                            (TDestinationEnum)Convert.ChangeType(e, destinationEnumType)).
                            ToArray();

                        if(destinationEnums.Length != 0 &&
                           (long)Convert.ChangeType(destinationEnums[0], typeof(long)) == (long)Convert.ChangeType(sourceValue, typeof(long)))
                            result = destinationEnums[0] ;
                    }
                }
            }
            return result;
        }

        private static TDestinationEnum[] Map(Enum sourceValue, Type sourceEnumType, Type destinationEnumType, EnumMapAttribute enumMapAttribute)
        {
            var result = new TDestinationEnum[0];
            if (enumMapAttribute is NamingEnumMapAttribute)
            {
                TDestinationEnum resultValue;
                if (Enum.TryParse<TDestinationEnum>(sourceValue.ToString(), out resultValue))
                    result = new[] { resultValue };
            }
            else if (enumMapAttribute is AttributeEnumMapAttribute)
            {
                var sourceMemberInfo = sourceEnumType.GetField(sourceValue.ToString());
                var mappingAttribute = sourceMemberInfo.
                    GetCustomAttributes((enumMapAttribute as AttributeEnumMapAttribute).MapperAttrbuiteType, true).
                    FirstOrDefault() as EnumItemMappingAttribute;

                if (mappingAttribute != null)
                {
                    var destinationEnums = mappingAttribute.MappedValues.Select(e =>
                        (TDestinationEnum)Convert.ChangeType(e, destinationEnumType));

                    result = destinationEnums.ToArray();
                }
            }

            return result;
        }

        private static EnumMapAttribute GetEnumMapAttribute(Type sourceEnumType, Type destinationEnumType, string correlationName)
        {
            var enumMapAttributes = sourceEnumType.
                GetCustomAttributes(typeof(EnumMapAttribute), true).
                Cast<EnumMapAttribute>().
                AsEnumerable();

            var enumMapAttribute = enumMapAttributes.
                FirstOrDefault(e =>
                    e.MapEnumType == destinationEnumType &&
                    e.CorrelationName == correlationName);
            return enumMapAttribute;
        }
    }
}
