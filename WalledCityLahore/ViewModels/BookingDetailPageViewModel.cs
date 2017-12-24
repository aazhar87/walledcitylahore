using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;

namespace WalledCityLahore.ViewModels
{
    public class BookingDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private IDependencyService _dependencyService;

        public DelegateCommand OnClickBack { set; get; }

        private BookingItem _recievedBooking = new BookingItem();
        public BookingItem RecievedBooking
        {
            get { return _recievedBooking; }
            set { SetProperty(ref _recievedBooking, value); }
        }

        public BookingDetailPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                     IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dependencyService = dependencyService;

            OnClickBack = new DelegateCommand(GoBack);
        }

        public async void GoBack()
        {
            await _navigationService.GoBackAsync(null, true);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                RecievedBooking = (BookingItem)parameters["model"];
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private async Task showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }

		public async Task PageChangeAsync()
		{
			if (IsBusy) return;
			IsBusy = true;

			var navigationParams = new NavigationParameters();
            navigationParams.Add("model", RecievedBooking);
			await _navigationService.NavigateAsync("BookNowPage", navigationParams, true, true);

            IsBusy = false;
		}
    }
}
