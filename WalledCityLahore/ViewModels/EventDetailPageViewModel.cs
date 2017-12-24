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
    public class EventDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private IDependencyService _dependencyService;

        public DelegateCommand OnClickBack { set; get; }

        private EventItem _recievedEvent = new EventItem();
        public EventItem RecievedEvent
        {
            get { return _recievedEvent; }
            set { SetProperty(ref _recievedEvent, value); }
        }

        public EventDetailPageViewModel(INavigationService navigationService,
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
            if (IsBusy) return;
            IsBusy = true;

            if (RecievedEvent.website != null || RecievedEvent.website.Length != 0)
                Device.OpenUri(new Uri(RecievedEvent.website));
            IsBusy = false;
        }

        public void GetDirections()
        {
            if (_dependencyService.Get<IGoogleDirections>() != null)
            {
                _dependencyService.Get<IGoogleDirections>().OpenGoogleMapDirectionsApp(RecievedEvent.latitude + "," + RecievedEvent.longitude);
            }
        }

        public async void OpenViewMore()
        {
            if (IsBusy) return;
            IsBusy = true;

            var navigationParams = new NavigationParameters();
            navigationParams.Add("topbarcolor", "#fe1fc7");
            navigationParams.Add("id", RecievedEvent.id);
            navigationParams.Add("items_type", "event");
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
                RecievedEvent = (EventItem)parameters["model"];
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
