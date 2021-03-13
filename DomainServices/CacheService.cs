using Microsoft.Extensions.Caching.Memory;
using ReadingIsGood.DomainInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.DomainServices
{
    public class CacheService : ICacheService
    {
        public MemoryCache Cache { get; set; }

        public CacheService()
        {
            Cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });
        }

        public void AddItem(string key, object value)
        {
            Cache.Set(key, value);
        }

        public void DeleteItem(string key)
        {
            Cache.Remove(key);
        }

        public object GetItem(string key)
        {
            object result;
            var any = Cache.TryGetValue(key, out result);
            return result;
        }
    }
}
