using Adv;
using BetterProject.Contracts;
using BetterProject.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BetterProject.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddLogging()
           .AddSingleton<IConfigurationService, ConfigurationService>()
           .AddSingleton<IMemoryCacheService, MemoryCacheService>()
           .AddSingleton<IQueueService, QueueService>()
           .AddTransient<INoSqlProvider, NoSqlProvider>()
           .AddTransient<ISqlProvider, SqlProvider>()
           .AddTransient<IAdvertisementService, AdvertisementService>()
           .BuildServiceProvider();

            var advertisementService = serviceProvider.GetService<IAdvertisementService>();


            advertisementService.GetAdvertisement("1");
           
            advertisementService.GetAdvertisement("2");
            Thread.Sleep(10000);
            advertisementService.GetAdvertisement("3");
            
            advertisementService.GetAdvertisement("1");


            var advertisementService1 = serviceProvider.GetService<IAdvertisementService>();
            Parallel.Invoke(
                            () => advertisementService1.GetAdvertisement("1"),
                            () => advertisementService1.GetAdvertisement("2"),
                            () => advertisementService1.GetAdvertisement("3"),
                            () => advertisementService1.GetAdvertisement("1")
                           );

            Console.WriteLine("Press any key to continue..");
            Console.ReadLine();
        }
    }
}
