﻿using TripLog.Models;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public class DetailViewModel : BaseViewModel<TripLogEntry>
    {
        private TripLogEntry _entry;

        public DetailViewModel(INavService navService)
            : base(navService)
        {
        }

        public TripLogEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public override void Init(TripLogEntry parameter)
        {
            Entry = parameter;
        }
    }
}