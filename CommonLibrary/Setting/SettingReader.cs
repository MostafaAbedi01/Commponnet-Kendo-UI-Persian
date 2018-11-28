using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IranSoftjo.Core.Setting;

namespace Mehr.Setting
{
    [Obsolete("User PackSettingReader instead")]
    public static class SettingReader
    {
        public static string Get(string settingKey, string defaultValue)
        {
            return new PackSettingReader().Get(settingKey, defaultValue);
        }

        public static TValue Get<TValue>(string settingKey, TValue defaultValueOnNotFound = default(TValue), bool throwExceptionOnNotFound = false)
        {
            return new PackSettingReader().Get(settingKey, defaultValueOnNotFound, throwExceptionOnNotFound);
        }
    }
}
