using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using Prism.Services;
using WalledCityLahore.Services;
using WalledCityLahore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WalledCityLahore.ViewModels
{
    public class EventsListPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private DataService dataService = new DataService();

        public DelegateCommand OnClickBack { set; get; }
        public DelegateCommand OnClickMap { set; get; }
        public DelegateCommand OnListRefreshing { get; set; }

        private bool _IsHasDataFlag = true;
        public bool IsHasDataFlag
        {
            get { return _IsHasDataFlag; }
            set { SetProperty(ref _IsHasDataFlag, value); }
        }

        private bool _isRefreshingList = false;
        public bool isRefreshingList
        {
            get { return _isRefreshingList; }
            set { SetProperty(ref _isRefreshingList, value); }
        }

        private string _statusText = "Getting new data...";
        public string StatusText
        {
            get { return _statusText; }
            set { SetProperty(ref _statusText, value); }
        }

        private ObservableCollection<EventItem> _eventsList = new ObservableCollection<EventItem>();
        public ObservableCollection<EventItem> EventsList
        {
            get { return _eventsList; }
            set { SetProperty(ref _eventsList, value); }
        }

        public EventsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnClickBack = new DelegateCommand(GoBack);
            OnClickMap = new DelegateCommand(GoToMapView);
            OnListRefreshing = new DelegateCommand(RefreshList);
        }

        public async void GoBack()
        {
            await _navigationService.GoBackAsync(null, true);
        }

		public async void GoToMapView()
		{
			if (IsBusy) return;
			IsBusy = true;

			var navigationParams = new NavigationParameters();
            navigationParams.Add("model", EventsList);
            navigationParams.Add("type", "events");
            navigationParams.Add("background", "#fe1fc7");
			await _navigationService.NavigateAsync("MapViewPointsPage", navigationParams, true, true);

            IsBusy = false;
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            dataService.client.CancelPendingRequests();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            RefreshList();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void RefreshList()
        {
			if (IsBusy) return;
			IsBusy = true;
            isRefreshingList = true;
            StatusText = "Getting new data...";

            EventResponse response = await dataService.getAllEvents();
            if (response != null)
            {
                if (response.status.Equals("success"))
                {
                    StatusText = response.message;
                    int colorFlipper = 0;
                    List<EventItem> items = new List<EventItem>();
                    foreach (EventItem row in response.data)
                    {
                        if(row.image == null || row.image.Length == 0)
                        {
                            row.image = "img_na_placeholder.png";
                        }
                            
						if ((colorFlipper % 2) == 0)
							row.background = Color.FromHex("#FFFFFF");
						else
							row.background = Color.FromHex("#99999D");

                        items.Add(row);
                        colorFlipper++;
                    }
                    EventsList = new ObservableCollection<EventItem>(items);
                    EventsList.OrderByDescending(x => x.event_datetime);

                    if (EventsList.Count() > 0)
                        IsHasDataFlag = false;
                    else
                    {
                        StatusText = "No Data Available";
                        IsHasDataFlag = true;
                    }
                }
            }
            else
            {
                StatusText = "Service is Unavailable";
            }

            IsBusy = false;
            isRefreshingList = false;
        }

        public async Task PageChangeAsync(EventItem item)
        {
			if (IsBusy) return;
			IsBusy = true;

            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", item);
            await _navigationService.NavigateAsync("EventDetailPage", navigationParams, true, true);

            IsBusy = false;
        }

        private async Task showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }
    }
}

