namespace TripLog.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("YOUR-MAPS-API-KEY-HERE");
            LoadApplication(new TripLog.App());
        }
    }
}