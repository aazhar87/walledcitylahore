using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using Prism.Services;
using WalledCityLahore.Services;
using WalledCityLahore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WalledCityLahore.ViewModels
{
    public class BookingsListPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private DataService dataService = new DataService();

        public DelegateCommand OnClickBack { set; get; }
        public DelegateCommand OnListRefreshing { get; set; }

		private BookingTypeItem _recievedBookingType = new BookingTypeItem();
		public BookingTypeItem RecievedBookingType
		{
			get { return _recievedBookingType; }
			set { SetProperty(ref _recievedBookingType, value); }
		}

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

        private ObservableCollection<BookingItem> _bookingsList = new ObservableCollection<BookingItem>();
        public ObservableCollection<BookingItem> BookingsList
        {
            get { return _bookingsList; }
            set { SetProperty(ref _bookingsList, value); }
        }

        public BookingsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnClickBack = new DelegateCommand(GoBack);
            OnListRefreshing = new DelegateCommand(RefreshList);
        }

        public async void GoBack()
        {
            await _navigationService.GoBackAsync(null, true);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            dataService.client.CancelPendingRequests();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
			if (parameters.ContainsKey("model"))
			{
				RecievedBookingType = (BookingTypeItem)parameters["model"];
                RefreshList();
			}
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

            BookingResponse response = await dataService.getAllBookingsOf(RecievedBookingType.booking_type_id);
            if (response != null)
            {
                if (response.status.Equals("success"))
                {
                    int colorFlipper = 0;
                    List<BookingItem> items = new List<BookingItem>();
                    foreach (BookingItem row in response.data)
                    {
						if (row.image == null || row.image.Length == 0)
						{
							row.image = "img_na_placeholder.png";
						}

                        row.Randomise();
                        items.Add(row);
                        colorFlipper++;
                    }
                    BookingsList = new ObservableCollection<BookingItem>(items);
                    BookingsList.OrderByDescending(x => x.id);

                    if (BookingsList.Count() > 0)
                        IsHasDataFlag = false;
                    else{
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

        public async Task PageChangeAsync(BookingItem item)
        {
			if (IsBusy) return;
			IsBusy = true;

            var navigationParams = new NavigationParameters();
            navigationParams.Add("model", item);
            await _navigationService.NavigateAsync("BookingDetailPage", navigationParams, true, true);

            IsBusy = false;
        }

        private async Task showDialog(string title, string msg)
        {
            await _dialogService.DisplayAlertAsync(title, msg, "OK");
        }
    }
}

