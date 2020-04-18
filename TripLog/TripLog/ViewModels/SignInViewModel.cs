using System;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly ITripLogDataService _tripLogService;

        private Command _signInCommand;

        public SignInViewModel(INavService navService, IAuthService authService, ITripLogDataService tripLogService)
                    : base(navService)
        {
            _authService = authService;
            _tripLogService = tripLogService;
        }

        public Command SignInCommand => _signInCommand ?? (_signInCommand = new Command(SignIn));

        private void SignIn()
        {
            // TODO: Update with your Facebook App Id and Function App name
            _authService.SignInAsync(App.FacebookAppId,
                new Uri("https://m.facebook.com/dialog/oauth"),
                new Uri("https://triplogscottkuhl.azurewebsites.net/.auth/login/facebook/callback"),
                tokenCallback: async token =>
                {
                    // Use Facebook token to get Azure auth token
                    await _tripLogService.AuthenticateAsync("facebook", token);
                },
                errorCallback: e =>
                {
                    // TODO: Handle invalid authentication here
                });
        }
    }
}