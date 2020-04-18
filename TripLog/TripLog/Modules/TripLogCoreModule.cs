using Ninject.Modules;
using System;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Essentials;

namespace TripLog.Modules
{
    public class TripLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            Bind<SignInViewModel>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            // Core Services
            var apiAuthToken = Preferences.Get("apitoken", "");
            var tripLogService = new TripLogApiDataService(new Uri(App.API), apiAuthToken);
            Bind<ITripLogDataService>().ToMethod(x => tripLogService).InSingletonScope();

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);
            Bind<IAuthService>().To<AuthService>().InSingletonScope();
        }
    }
}