using System.Threading.Tasks;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;
using WalledCityLahore.Services;

namespace WalledCityLahore.ViewModels
{
    public class FeedbackPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private DataService dataService = new DataService();

        public DelegateCommand OnClickBack { set; get; }
        public DelegateCommand OnClickSubmit { set; get; }

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

        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _comments = "";
        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        public FeedbackPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnClickBack = new DelegateCommand(GoBack);
            OnClickSubmit = new DelegateCommand(SubmitNow);
        }

        public async void GoBack()
        {
            await _navigationService.GoBackAsync(null, true);
        }

        public async void SubmitNow()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (Email.Length == 0 || !Email.Contains("@") ||
                Name.Length == 0 ||
                Phone.Length == 0 ||
                Comments.Length == 0
               )
            {
                await showDialog("Alert!", "Please enter all information first.");
            }
            else
            {
                var dialog = UserDialogs.Instance;
                dialog.ShowLoading("Please wait...", MaskType.Black);
                BookingResponse response = await dataService.PostFeedback(Name, Phone, (string)Email.ToLower(), Comments);
                if (response != null)
                {
                    if (response.status.Equals("failure"))
                    {
                        await showDialog("Alert!", "Oops, " + response.message);
                    }
                    else if (response.status.Equals("success"))
                    {
                        dialog.HideLoading();
                        await showDialog("Feedback is Sent,", "Thank you for your feedback.");
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

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
