using BetterProject;
using BetterProject.Contracts;
using BetterProject.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;
using System.Threading;
using ThirdParty;

namespace Adv
{
    // **************************************************************************************************
    // Loads Advertisement information by id
    // from cache or if not possible uses the "mainProvider" or if not possible uses the "backupProvider"
    // **************************************************************************************************
    // Detailed Logic:
    // 
    // 1. Tries to use cache (and retuns the data or goes to STEP2)
    //
    // 2. If the cache is empty it uses the NoSqlDataProvider (mainProvider), 
    //    in case of an error it retries it as many times as needed based on AppSettings
    //    (returns the data if possible or goes to STEP3)
    //
    // 3. If it can't retrive the data or the ErrorCount in the last hour is more than 10, 
    //    it uses the SqlDataProvider (backupProvider)
    public class AdvertisementService : IAdvertisementService
    {
        //private static MemoryCache cache = new MemoryCache("");
        private Object lockObj = new Object();

        private IMemoryCacheService _memoryCacheService;
        private IQueueService _queueService;
        private IConfigurationService _configurationService;
        private IDataStoreProvider _noSqlProvider;
        private IDataStoreProvider _sqlProvider;
        public AdvertisementService(IConfigurationService configurationService, 
                                    IMemoryCacheService memoryCacheService, 
                                    IQueueService queueService,
                                    INoSqlProvider noSqlProvider,
                                    ISqlProvider sqlProvider)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _memoryCacheService = memoryCacheService ?? throw new ArgumentNullException(nameof(memoryCacheService));
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
            _noSqlProvider = (IDataStoreProvider)noSqlProvider ?? throw new ArgumentNullException(nameof(noSqlProvider));
            _sqlProvider = (IDataStoreProvider)sqlProvider ?? throw new ArgumentNullException(nameof(sqlProvider));
        }
        
        public Advertisement GetAdvertisement(string id)
        {
            Advertisement adv = null;

            lock (lockObj)
            {
                // Use Cache if available
                adv = (Advertisement)_memoryCacheService.Get(id);

                if (adv != null)
                    return adv;

                var errorCount = _queueService.GetErrors(1, 20);

                // If Cache is empty and ErrorCount<10 then use HTTP provider
                if (errorCount < 10)
                {
                    int retry = 0;
                    while(retry < _configurationService.GetSetting<int>("RetryCount"))
                    {
                        
                        try
                        {
                            adv = _noSqlProvider.GetAdv(id);

                            if (adv != null)
                                break;
                        }
                        catch
                        {
                            Thread.Sleep(1000); //Do we need this Thread.Sleep for each iteration?; Try to avoid if we can avoid Thread.Sleep
                            _queueService.Enqueue(DateTime.Now); // Store HTTP error timestamp              
                        }

                        retry++;
                    } 
                   
                }
                else
                { 
                    // if needed try to use Backup provider
                    adv = _sqlProvider.GetAdv(id);
                }

                if (adv != null)
                {
                    _memoryCacheService.Set(id, adv, DateTimeOffset.Now.AddMinutes(5));
                }
            }

            return adv;
        }
    }
}
