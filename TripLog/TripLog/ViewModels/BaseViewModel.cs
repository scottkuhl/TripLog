using System.ComponentModel;
using System.Runtime.CompilerServices;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;

        protected BaseViewModel(INavService navService, IAnalyticsService analyticsService)
        {
            NavService = navService;
            AnalyticsService = analyticsService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        protected IAnalyticsService AnalyticsService { get; private set; }
        protected INavService NavService { get; private set; }

        public virtual void Init()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INavService navService, IAnalyticsService analyticsService)
            : base(navService, analyticsService)
        {
        }

        public override void Init()
        {
            Init(default(TParameter));
        }

        public virtual void Init(TParameter parameter)
        {
        }
    }
}