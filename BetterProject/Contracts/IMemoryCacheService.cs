using System;
using ThirdParty;

namespace BetterProject
{
    public interface IMemoryCacheService
    {
         Advertisement Get(string key);
         void Set(string key, Advertisement adv, DateTimeOffset expiryTime);

    }
}