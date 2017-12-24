using System.Threading.Tasks;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;
using WalledCityLahore.Services;
using System.Collections.ObjectModel;
using System;

namespace WalledCityLahore.ViewModels
{
    public class BookNowPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private DataService dataService = new DataService();

        public DelegateCommand OnClickBack { set; get; }
        public DelegateCommand OnClickBookNow { set; get; } 

        DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetProperty(ref _selectedDate, value); }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private string _persons = "";
        public string Persons
        {
            get { return _persons; }
            set { SetProperty(ref _persons, value); }
        }

        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private int _selecteddateindex = 0;
        public int SelectedDateIndex
        {
            get { return _selecteddateindex; }
            set { SetProperty(ref _selecteddateindex, value); }
        }

        private int _selectedtimeindex = 0;
        public int SelectedTimeIndex
        {
            get { return _selectedtimeindex; }
            set { SetProperty(ref _selectedtimeindex, value); }
        }

        private ObservableCollection<string> _bookingTimes = new ObservableCollection<string>();
        public ObservableCollection<string> BookingTimes
        {
            get { return _bookingTimes; }
            set { SetProperty(ref _bookingTimes, value); }
        }

        private BookingItem _recievedBooking = new BookingItem();
        public BookingItem RecievedBooking
        {
            get { return _recievedBooking; }
            set { SetProperty(ref _recievedBooking, value); }
        }

        public BookNowPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnClickBack = new DelegateCommand(GoBack);
            OnClickBookNow = new DelegateCommand(BookTripNow);
        }

        public async void GoBack()
        {
            await _navigationService.GoBackAsync(null, true);
        }

        public async void BookTripNow()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (Email.Length == 0 || !Email.Contains("@") ||
                Name.Length == 0 || Phone.Length == 0 || Persons.Length == 0)
            {
                await showDialog("Alert!", "Please enter all information first.");
            }
            else
            {
                var dialog = UserDialogs.Instance;
                dialog.ShowLoading("Please wait...", MaskType.Black);
                string time = "";
                
                if (BookingTimes.Count > SelectedTimeIndex)
                    time = BookingTimes[SelectedTimeIndex];

                BookingResponse response = await dataService.BookTrip(RecievedBooking.id, Name, Phone, Persons,
                                                                      (string)Email.ToLower(),
                                                                      SelectedDate.ToString(), time);
                if (response != null)
                {
                    if (response.status.Equals("failure"))
                    {
                        await showDialog("Alert!", "Oops, " + response.message);
                    }
                    else if (response.status.Equals("success"))
                    {
                        dialog.HideLoading();
                        await showDialog("Success", "Your tour booking is sent, you will be contacted by administrator for confirmation. Thank you");
                        await _navigationService.NavigateAsync("MainPage");
                    }
                    dialog.HideLoading();
                }
                else
                {
                    dialog.HideLoading();
                    await showDialog("Alert!", "You are not connected to internet, please try again later.");
                }
            }

            IsBusy = false;
        }

		private void PopulatePickers()
		{
            foreach (string time in RecievedBooking.times)
            {
                BookingTimes.Add(time);
            }
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model"))
            {
                RecievedBooking = (BookingItem)parameters["model"];
                if(RecievedBooking.times.Length > 0)
                    PopulatePickers();
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
