using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Reflection;

namespace Mehr.Reflection
{
    public class EnumMetadata<T>
    {
        Dictionary<T, string> items = null;

        public Dictionary<T, string> Items { get { return items ?? (items = BuildAll()); } }

        Array enumValues = null;
        public Array EnumValues
        {
            get
            {
                if (enumValues == null)
                    enumValues = Enum.GetValues(EnumType);
                return enumValues;
            }
        }

        public static Type EnumType
        {
            get
            {
                var type = typeof(T);
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);
                return type;
            }
        }
        public EnumMetadata()
        {
        }

        Dictionary<T, string> BuildAll()
        {
            return EnumItems.
                OrderBy(i => i.Attribute.Order).
                ToDictionary(i => i.Value, i => i.Attribute.Caption);
        }

        List<EnumItem<T>> enumItems = null;
        private List<EnumItem<T>> EnumItems
        {
            get
            {
                if (enumItems == null)
                {
                    enumItems = new List<EnumItem<T>>();
                    foreach (var item in EnumValues)
                    {
                        string name = ((T)item).ToString();
                        var attr = EnumType.GetField(name).GetAttribute<EnumItemAttribute>();
                        if (!enumItems.Any(i => i.Value.Equals((T)item)))
                            enumItems.Add(new EnumItem<T>()
                            {
                                Value = (T)item,
                                Attribute = attr ?? new EnumItemAttribute(name),
                            });
                    }
                }
                return enumItems;
            }
        }

        private class EnumItem<TEnum>
        {
            public EnumItemAttribute Attribute { get; set; }

            //public string Caption { get; set; }

            public TEnum Value { get; set; }
        }
    }
}
