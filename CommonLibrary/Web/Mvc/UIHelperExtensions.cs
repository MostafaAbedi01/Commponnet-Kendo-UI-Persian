using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mehr.Reflection;

namespace Mehr.Web.Mvc
{
    public static class UIHelperExtensions
    {
        public static SelectList ToSelectList(params string[] texts)
        {
            return texts.ToSelectList(0);
        }

        public static SelectList ToSelectList(this IEnumerable<string> texts, params int[] seletedIndexes)
        {
            int i = 1;
            return texts.ToDictionary(s => i++, s => s).ToSelectList(seletedIndexes);
        }

        public static SelectList ToSelectList<TKey,TBaseType>(
            this IEnumerable<TBaseType> items,
            Func<TBaseType, TKey> keySelector,
            Func<TBaseType, string> valueSelector,
            params TKey[] seletedIndexes)
        {
            return items.ToDictionary(keySelector, valueSelector).
                ToSelectList(seletedIndexes);
        }

        public static SelectList ToSelectList<TKey>(this Dictionary<TKey, string> items, params TKey[] seletedIndexes)
        {
            var selectListItems = items.Select(item =>
                new SelectListItem()
                {
                    Value = item.Key.ToString(),
                    Text = item.Value,
                    Selected = seletedIndexes.Contains(item.Key)
                });
            SelectList selectList;
            if ((seletedIndexes != null && seletedIndexes.Length > 0))
                selectList = new SelectList(selectListItems, "Value", "Text", seletedIndexes[0]);
            else
                selectList = new SelectList(selectListItems, "Value", "Text");

            return selectList;
        }

        public static SelectList ToSelectList<T>(this EnumMetadata<T> enumMetadata, params T[] seletedIndexes)
        {
            return enumMetadata.ToSelectList(seletedIndexes.Select(s => Convert.ToInt32(s)).ToArray());
        }

        public static SelectList ToSelectList<T>(this EnumMetadata<T> enumMetadata, params int[] seletedIndexes)
        {
            return enumMetadata.Items.
                ToDictionary(item => Convert.ToInt32(item.Key), item => item.Value).
                ToSelectList(seletedIndexes);
        }

        public static SelectList ToSelectListAsString<T>(this EnumMetadata<T> enumMetadata, params string[] seletedIndexes)
        {
            return enumMetadata.Items.
                ToDictionary(item => item.Key.ToString(), item => item.Value).
                ToSelectList(seletedIndexes);
        }

        public static SelectList ToSelectListAsTringFiltered<T>(this EnumMetadata<T> enumMetadata, T[] filterValues, params T[] seletedIndexes)
        {
            return enumMetadata.Items.
                Where(item => !filterValues.Contains(item.Key)).
                ToDictionary(item => item.Key.ToString(), item => item.Value).
                ToSelectList(seletedIndexes.Select(item => item.ToString()).ToArray());
        }

        public static string GetAsString<T>(this EnumMetadata<T> metaData)
        {
            return string.Join(";", metaData.Items.Select((item) => Convert.ToInt32(item.Key) + ":" + item.Value));
        }

    }
}
