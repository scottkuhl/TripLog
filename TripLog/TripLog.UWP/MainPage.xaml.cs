using TripLog.UWP.Modules;

namespace TripLog.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.Auth.Presenters.UWP.AuthenticationConfiguration.Init();
            Xamarin.FormsMaps.Init(TripLog.App.BingMapsKey);
            LoadApplication(new TripLog.App(new TripLogPlatformModule()));
        }
    }
}