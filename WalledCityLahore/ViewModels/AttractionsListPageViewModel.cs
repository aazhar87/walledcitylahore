using System;
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
    public class AttractionsListPageViewModel : BaseViewModel, INavigationAware
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

        private ObservableCollection<AttractionItem> _attractionsList = new ObservableCollection<AttractionItem>();
        public ObservableCollection<AttractionItem> AttractionsList
        {
            get { return _attractionsList; }
            set { SetProperty(ref _attractionsList, value); }
        }

        public AttractionsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
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
			var navigationParams = new NavigationParameters();
            navigationParams.Add("model", AttractionsList);
			navigationParams.Add("type", "attractions");
            navigationParams.Add("background", "#47a0d9");
			await _navigationService.NavigateAsync("MapViewPointsPage", navigationParams, true, true);
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

            AttractionResponse response = await dataService.getAllAttractions();
            if (response != null)
            {
                if (response.status.Equals("success"))
                {
                    int colorFlipper = 0;
                    List<AttractionItem> items = new List<AttractionItem>();
                    foreach (AttractionItem row in response.data)
                    {
						if (row.image == null || row.image.Length == 0)
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
                    AttractionsList = new ObservableCollection<AttractionItem>(items);
                    AttractionsList.OrderByDescending(x => x.entry_datetime);

                    if (AttractionsList.Count() > 0)
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

        public async Task PageChangeAsync(AttractionItem item)
        {
			if (IsBusy) return;
			IsBusy = true;

            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", item);
            await _navigationService.NavigateAsync("AttractionDetailPage", navigationParams, true, true);

            IsBusy = false;
        }

        private async Task showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }

        //helper method
        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("{0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}

