﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        private readonly ILocationService _locService;
        private readonly ITripLogDataService _tripLogService;

        private DateTime _date;
        private double _latitude;
        private double _longitude;
        private string _notes;
        private int _rating;
        private Command _saveCommand;
        private string _title;

        public NewEntryViewModel(INavService navService, ILocationService locService, ITripLogDataService tripLogService, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
            _locService = locService;
            _tripLogService = tripLogService;

            Date = DateTime.Today;
            Rating = 1;
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                Validate(() => _rating >= 1 && _rating <= 5, "Rating must be between 1 and 5.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title), "Title must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public override async void Init()
        {
            try
            {
                var coords = await _locService.GetGeoCoordinatesAsync();
                Latitude = coords.Latitude;
                Longitude = coords.Longitude;
            }
            catch (Exception e)
            {
                AnalyticsService.TrackError(e, new Dictionary<string, string>
                {
                    { "Method", "NewEntryViewModel.Init()" }
                });
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;

        private async Task Save()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var newItem = new TripLogEntry
                {
                    Title = Title,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Date = Date,
                    Rating = Rating,
                    Notes = Notes
                };

                await _tripLogService.AddEntryAsync(newItem);
                await NavService.GoBack();
            }
            catch (Exception e)
            {
                AnalyticsService.TrackError(e, new Dictionary<string, string>
                {
                    { "Method", "NewEntryViewModel.Save()" }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}