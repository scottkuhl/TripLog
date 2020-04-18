using Akavache;
using System;
using System.Collections.ObjectModel;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IBlobCache _cache;
        private readonly ITripLogDataService _tripLogService;

        private ObservableCollection<TripLogEntry> _logEntries;
        private Command _newCommand;
        private Command _refreshCommand;

        public MainViewModel(INavService navService, ITripLogDataService tripLogService, IBlobCache cache)
                    : base(navService)
        {
            _tripLogService = tripLogService;
            _cache = cache;
            LogEntries = new ObservableCollection<TripLogEntry>();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public Command NewCommand => _newCommand ?? (_newCommand = new Command(async () => await NavService.NavigateTo<NewEntryViewModel>(), CanNew));
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>(async entry => await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry));

        public override void Init()
        {
            LoadEntries();
        }

        private bool CanNew() => Connectivity.NetworkAccess == NetworkAccess.Internet;

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            NewCommand.ChangeCanExecute();
        }

        private void LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _cache.GetAndFetchLatest("entries", async () => await _tripLogService.GetEntriesAsync())
                    .Subscribe(entries =>
                    {
                        LogEntries = new ObservableCollection<TripLogEntry>(entries);
                        IsBusy = false;
                    });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}