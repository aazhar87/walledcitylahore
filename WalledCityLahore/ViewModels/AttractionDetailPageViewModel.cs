using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;
using WalledCityLahore.Services;
using Xamarin.Forms;

namespace WalledCityLahore.ViewModels
{
    public class AttractionDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private IDependencyService _dependencyService;

        public DelegateCommand OnClickBack { set; get; }

        private AttractionItem _recievedAttraction = new AttractionItem();
        public AttractionItem RecievedAttraction
        {
            get { return _recievedAttraction; }
            set { SetProperty(ref _recievedAttraction, value); }
        }

        public AttractionDetailPageViewModel(INavigationService navigationService,
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

        public void OpenWebUrl()
        {
            if (RecievedAttraction.website != null || RecievedAttraction.website.Length != 0)
                Device.OpenUri(new Uri(RecievedAttraction.website));
        }

        public void GetDirections()
        {
            if (_dependencyService.Get<IGoogleDirections>() != null)
            {
                _dependencyService.Get<IGoogleDirections>().OpenGoogleMapDirectionsApp(RecievedAttraction.latitude + "," + RecievedAttraction.longitude);
            }
        }

        public async void OpenViewMore()
		{
			if (IsBusy) return;
			IsBusy = true;

			var navigationParams = new NavigationParameters();
            navigationParams.Add("topbarcolor", "#47a0d9");
            navigationParams.Add("id", RecievedAttraction.id);
            navigationParams.Add("items_type", "attraction");
			await _navigationService.NavigateAsync("GalleryPage", navigationParams, true, true);

			IsBusy = false;
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                RecievedAttraction = (AttractionItem)parameters["model"];
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private async Task showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }

    }
}
