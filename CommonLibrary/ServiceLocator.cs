using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Web;
using Mehr.Web;
using Mehr.Setting;

namespace Mehr
{
    public class ServiceLocator
    {
        public Dictionary<Type, object> List { get; private set; }
        public ServiceLocator UpLevelServiceLocator { get; private set; }

        protected ServiceLocator(ServiceLocator upLevelServiceLocator = null)
        {
            List = new Dictionary<Type, object>();
            UpLevelServiceLocator = upLevelServiceLocator;
        }

        public void Set<TInterface>(TInterface instance)
        {
            List[typeof(TInterface)] = instance;
        }

        public void SetBySettingKey<TInterface>(string settingKey)
            where TInterface : class
        {
            List[typeof(TInterface)] = new SettingTypeActivator<TInterface>(settingKey).Instance;
            //new Lazy<TInterface>(() => new SettingTypeActivator<TInterface>(settingKey).Instance).Value;
        }

        public virtual TInterface Resolve<TInterface>(bool throwExceptionOnNotFound) where TInterface : class
        {
            var returnValue = Resolve<TInterface>();
            if (returnValue == null)
                throw new InvalidOperationException();
            return returnValue;
        }

        public virtual TInterface Resolve<TInterface>() where TInterface : class
        {
            object instanceObject = null;
            List.TryGetValue(typeof(TInterface), out instanceObject);
            var result = instanceObject as TInterface;
            if (result == null)
                if (UpLevelServiceLocator != null)
                    result = UpLevelServiceLocator.Resolve<TInterface>();
            return result;
        }

        public ServiceLocator CreateNewDownLevel()
        {
            return new ServiceLocator(this);
        }

        public static TInterface ResolveOnCurrentInstance<TInterface, TDefault>()
            where TInterface : class
            where TDefault : TInterface, new()
        {
            TInterface resolved = null;
            if (Current != null)
                resolved = Current.Resolve<TInterface>();
            if (resolved == null)
                resolved = new TDefault();
            return resolved;
        }

        public static TInterface ResolveOnCurrentInstance<TInterface>(Func<TInterface> defaultInstanceBuilder = null) where TInterface : class
        {
            TInterface resolved = null;
            if (Current != null)
                resolved = Current.Resolve<TInterface>();
            if (resolved == null && defaultInstanceBuilder != null)
                resolved = defaultInstanceBuilder();
            return resolved;
        }

        public static ServiceLocator Current;

        public static ServiceLocator EmptyContext
        {
            get { return new ContextServiceLocator(); }
        }

        public static ServiceLocator WebContext
        {
            get
            {
                return new ContextServiceLocator()
                {
                    CacheProvider = new HttpContextCacheProvider(),
                    PathResolver = new WebPathResolver()
                };
            }
        }

        public static ServiceLocator WindowsAppContext
        {
            get
            {
                return new ContextServiceLocator()
                    {
                        CacheProvider = new ProccessCacheProvider(),
                        PathResolver = new DefaultPathResolver()
                    };
            }
        }

        public static ServiceLocator WebServiceContext
        {
            get
            {
                return new ContextServiceLocator()
                    {
                        CacheProvider = new ProccessCacheProvider(),
                        PathResolver = new WebServicePathResolver()
                    };
            }
        }

        private class ContextServiceLocator : ServiceLocator
        {
            public ICacheProvider CacheProvider { set { Set(value); } }
            public IPathResolver PathResolver { set { Set(value); } }
        }
    }
}
