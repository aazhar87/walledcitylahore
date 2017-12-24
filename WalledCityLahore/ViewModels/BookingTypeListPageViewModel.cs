using Prism.Commands;
using Prism.Navigation;
using System.Linq;
using Prism.Services;
using WalledCityLahore.Services;
using WalledCityLahore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WalledCityLahore.ViewModels
{
	public class BookingTypeListPageViewModel : BaseViewModel, INavigationAware
	{
		private INavigationService _navigationService;
		private IPageDialogService _dialogService;
		private DataService dataService = new DataService();

		public DelegateCommand OnClickBack { set; get; }
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

		private ObservableCollection<BookingTypeItem> _bookingsList = new ObservableCollection<BookingTypeItem>();
		public ObservableCollection<BookingTypeItem> BookingsTypeList
		{
			get { return _bookingsList; }
			set { SetProperty(ref _bookingsList, value); }
		}

		public BookingTypeListPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
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

			BookingTypeResponse response = await dataService.getAllBookingsType();
			if (response != null)
			{
				if (response.status.Equals("success"))
				{
                    BookingsTypeList = new ObservableCollection<BookingTypeItem>(response.data);
                    BookingsTypeList.OrderByDescending(x => x.booking_type_id);

					if (BookingsTypeList.Count() > 0)
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

		public async Task PageChangeAsync(BookingTypeItem item)
		{
			if (IsBusy) return;
			IsBusy = true;

			var navigationParams = new NavigationParameters();
			navigationParams.Add("model", item);
			await _navigationService.NavigateAsync("BookingsListPage", navigationParams, true, true);

			IsBusy = false;
		}

		private async Task showDialog(string title, string msg)
		{
			await _dialogService.DisplayAlertAsync(title, msg, "OK");
		}
	}
}

