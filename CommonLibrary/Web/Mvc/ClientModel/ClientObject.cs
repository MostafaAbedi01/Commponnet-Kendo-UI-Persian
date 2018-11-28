using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web.Mvc.ClientModel
{
    public class ClientObject : List<ClientProperty>, IJsonSerializable
    {
        public ClientProperty this[string propertyName]
        {
            get { return this.FirstOrDefault(c => c.Name == propertyName); }
            set
            {
                if (value.Name != propertyName)
                    throw new NotSupportedException(value.Name + " != " + propertyName);
                var t = this.FirstOrDefault(c => c.Name == propertyName);
                if (t != null)
                    t.Value = value.Value;
                else
                    this.Add(value);
            }
        }

        public void SetProprty<T>(string propertyName, T propertyValue)
        {
            this[propertyName] = new ClientProperty<T>(propertyName, propertyValue);
        }

        public T GetProprty<T>(string propertyName)
        {
            return (this[propertyName] as ClientProperty<T>).Value;
        }

        //Abedi
        public virtual string GetClientModelAsJson()
        {
            //ISerializeMinifyManager serilizeMinifyManager = null;
            //if (ServiceLocator.Current != null)
            //    serilizeMinifyManager = ServiceLocator.Current.Resolve<ISerializeMinifyManager>();
            //serilizeMinifyManager = serilizeMinifyManager ?? new DefaultSerializeMinifyManager();

            StringBuilder builder = new StringBuilder(this.Count * 20);

            builder.Append('{');
            var count = this.Count;
            bool isAnyAdded = false;
            for (int i = 0; i < count; i++)
            {
                var property = this[i];
                //if (!serilizeMinifyManager.Igonrable(property))
                //{
                //    if (isAnyAdded)
                //        builder.Append(',');
                //    isAnyAdded = true;
                //    builder.Append(property.GetClientModelAsJson());
                //}
            }
            builder.Append('}');

            return builder.ToString();
        }

        public override string ToString()
        {
            return GetClientModelAsJson();
        }
    }
}
