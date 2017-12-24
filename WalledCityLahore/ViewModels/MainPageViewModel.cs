using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Services;
using System.Threading.Tasks;

namespace WalledCityLahore.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private DataService dataService = new DataService();

        public DelegateCommand OnEventsClicked { get; set; }
        public DelegateCommand OnRestaurantsClicked { get; set; }
        public DelegateCommand OnAttractionsClicked { get; set; }
        public DelegateCommand OnBookingsClicked { get; set; }

        private string _weatherForcast = "loading";
        public string WeatherForcast
        {
            get { return _weatherForcast; }
            set { SetProperty(ref _weatherForcast, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnEventsClicked = new DelegateCommand(NavigateToEvents);
            OnRestaurantsClicked = new DelegateCommand(NavigateToRestaurants);
            OnAttractionsClicked = new DelegateCommand(NavigateToAttractions);
            OnBookingsClicked = new DelegateCommand(NavigateToBookings);
        }

        private async void NavigateToEvents()
        {
            if (IsBusy) return;
            IsBusy = true;
            await _navigationService.NavigateAsync("EventsListPage", null, true, true);
            IsBusy = false;
        }

        private async void NavigateToRestaurants()
        {
			if (IsBusy) return;
			IsBusy = true;
            await _navigationService.NavigateAsync("RestaurantsListPage", null, true, true);
            IsBusy = false;
        }

        private async void NavigateToAttractions()
        {
			if (IsBusy) return;
			IsBusy = true;
            await _navigationService.NavigateAsync("AttractionsListPage", null, true, true);
            IsBusy = false;
        }

        private async void NavigateToBookings()
        {
			if (IsBusy) return;
			IsBusy = true;
            await _navigationService.NavigateAsync("BookingTypeListPage", null, true, true);
            IsBusy = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
			dataService.client.CancelPendingRequests();
		}

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private async void showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }

		public async Task NavigateToFeedback()
		{
			if (IsBusy) return;
			IsBusy = true;

            await _navigationService.NavigateAsync("FeedbackPage", null, true, true);
            IsBusy = false;
		}
    }
}

