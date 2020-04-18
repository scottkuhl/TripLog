using Ninject.Modules;
using System;
using TripLog.Services;
using TripLog.ViewModels;

namespace TripLog.Modules
{
    public class TripLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            // Core Services
            var tripLogService = new TripLogApiDataService(new Uri("http://localhost:7071/"));
            Bind<ITripLogDataService>().ToMethod(x => tripLogService).InSingletonScope();

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);
        }
    }
}