using NetCoreMRD.Shared.Business.Contract;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Business
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            _cache.SetString(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            });
            return value;
        }
    }
}
