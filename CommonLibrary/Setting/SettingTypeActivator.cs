using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Setting
{
    public class SettingTypeActivator<T>
        where T : class
    {
        private T instance;

        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    var type = SettingReader.Get<Type>(settingKey, throwExceptionOnNotFound: true);
                    instance = (T)Activator.CreateInstance(type);
                }
                return instance ;
            }
        }

        private string settingKey;

        public SettingTypeActivator(string settingKey)
        {
            this.settingKey = settingKey;
        }
    }
}
