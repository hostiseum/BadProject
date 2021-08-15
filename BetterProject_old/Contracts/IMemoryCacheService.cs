using System;
using ThirdParty;

namespace BetterProject
{
    public interface IMemoryCacheService
    {
        public Advertisement Get(string key);
        public void Set(string key, Advertisement adv, DateTimeOffset expiryTime);

    }
}