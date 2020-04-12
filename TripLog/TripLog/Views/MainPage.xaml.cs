using System;
using System.Linq;
using TripLog.Models;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void New_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewEntryPage());
        }

        private async void Trips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var trip = (TripLogEntry)e.CurrentSelection.FirstOrDefault();
            if (trip != null)
            {
                await Navigation.PushAsync(new DetailPage(trip));
            }
            // Clear selection
            trips.SelectedItem = null;
        }
    }
}