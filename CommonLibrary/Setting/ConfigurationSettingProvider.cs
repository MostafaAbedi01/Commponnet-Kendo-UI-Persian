using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace Mehr.Setting
{
    public class ConfigurationSettingProvider : ISettingProvider
    {
        public readonly string MasterCategory;

        public readonly IDictionary<string, string>[] SettingsDefaults;

        public ConfigurationSettingProvider() : this(null) { }

        public ConfigurationSettingProvider(string masterCategory, params IDictionary<string, string>[] settingsDefaults)
        {
            this.MasterCategory = masterCategory;
            this.SettingsDefaults = settingsDefaults;
        }

        public virtual string this[string key]
        {
            get
            {
                var configuredValue = ConfigurationManager.AppSettings[key];
                if (configuredValue == null && this.SettingsDefaults != null)
                    foreach (var settingsDefault in this.SettingsDefaults)
                        if (settingsDefault.TryGetValue(key, out configuredValue))
                            return configuredValue;
                return configuredValue;
            }
        }

        public string this[string category, string key]
        {
            get
            {
                if (string.IsNullOrEmpty(category))
                    return this[key];

                var fullCategoryName = category;
                if (!string.IsNullOrEmpty(MasterCategory))
                    fullCategoryName = MasterCategory + "/" + category.ToLower();

                var customSectionSettings = ConfigurationManager.GetSection(fullCategoryName) as NameValueCollection;
                if (customSectionSettings != null)
                    return customSectionSettings[key];

                var fullKey = category + "." + key;
                return this[fullKey];
            }
        }
    }
}
