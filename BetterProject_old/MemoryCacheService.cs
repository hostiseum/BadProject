using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using ThirdParty;

namespace BetterProject
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private  static MemoryCache cache = new MemoryCache("");
        private const string keyFormat = "AdvKey_{0}";

        public MemoryCacheService()
        {

        }

        public Advertisement Get(string key)
        {
            return (Advertisement)cache.Get(string.Format(keyFormat, key));
        }

        public void Set(string key, Advertisement adv, DateTimeOffset expiryTime)
        {
            cache.Set(string.Format(keyFormat, key), adv, expiryTime);
        }
    }
}
