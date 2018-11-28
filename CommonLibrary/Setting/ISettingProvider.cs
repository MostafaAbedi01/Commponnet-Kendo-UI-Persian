using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Setting
{
    public interface ISettingProvider
    {
        [Obsolete("Use new overload instead.")]
        string this[string key] { get; }
        string this[string category, string key] { get; }
    }
}
