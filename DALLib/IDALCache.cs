using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public interface IDALCache
    {
        T GetFromCache<R, T>(Func<R, T> getter, CacheItemPolicy policy = null, R param = default(R)) where T : class;
        T GetFromCache<T>(Func<T> getter, CacheItemPolicy policy = null) where T : class;
    }
}
