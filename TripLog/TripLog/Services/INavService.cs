using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TripLog.ViewModels;

namespace TripLog.Services
{
    public interface INavService
    {
        event PropertyChangedEventHandler CanGoBackChanged;

        bool CanGoBack { get; }

        void ClearBackStack();

        Task GoBack();

        Task NavigateTo<TVM>() where TVM : BaseViewModel;

        Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;

        Task NavigateToUri(Uri uri);

        void RemoveLastView();
    }
}