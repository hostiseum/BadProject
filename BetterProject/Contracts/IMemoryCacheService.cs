using System;
using System.Runtime.Caching;
using ThirdParty;

namespace BetterProject
{
    public interface IMemoryCacheService
    {
         Advertisement Get(string key);
         void Set(string key, Advertisement adv, DateTimeOffset expiryTime);
         MemoryCache GetCurrentCache();
    }
}