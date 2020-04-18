using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Ninject;
using Ninject.Modules;
using TripLog.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TripLog
{
    public partial class App : Application
    {
        public const string API = "https://YOUR_URL_HERE.azurewebsites.net/";
        public const string AppCenterAndroidAppSecret = "YOUR_APP_SECRET_HERE";
        public const string AppCenterIosAppSecret = "YOUR_APP_SECRET_HERE";
        public const string AppCenterUwpAppSecret = "YOUR_APP_SECRET_HERE";
        public const string BingMapsKey = "YOUR_KEY_HERE";
        public const string FacebookAppId = "YOUR_APP_ID_HERE";
        public const string GoogleMapsKey = "YOUR_KEY_HERE";

        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            // Register core services
            Kernel = new StandardKernel(
                new TripLogCoreModule(),
                new TripLogNavModule());
            // Register platform specific services
            Kernel.Load(platformModules);
            // Setup data service authentication delegates
            var dataService = Kernel.Get<ITripLogDataService>();
            dataService.AuthorizedDelegate = OnSignIn;
            dataService.UnauthorizedDelegate = SignOut;
            SetMainPage();
        }

        public IKernel Kernel { get; set; }
        private bool IsSignedIn => !string.IsNullOrWhiteSpace(Preferences.Get("apitoken", ""));

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios={Your iOS app secret here};"
                + "android={Your Android app secret here};"
                + "uwp={Your UWP app secret here}",
                typeof(Analytics), typeof(Crashes));
        }

        private void OnSignIn(string accessToken)
        {
            Preferences.Set("apitoken", accessToken);
            SetMainPage();
        }

        private void SetMainPage()
        {
            var mainPage = IsSignedIn ? new NavigationPage(new MainPage())
            {
                BindingContext = Kernel.Get<MainViewModel>()
            }
                : new NavigationPage(new SignInPage())
                {
                    BindingContext = Kernel.Get<SignInViewModel>()
                };
            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = mainPage.Navigation;
            MainPage = mainPage;
        }

        private void SignOut()
        {
            Preferences.Remove("apitoken");
            SetMainPage();
        }
    }
}