using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;
using WalledCityLahore.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;

namespace WalledCityLahore.ViewModels
{
	public class GalleryPageViewModel : BaseViewModel, INavigationAware
	{
		private INavigationService _navigationService;
		private IPageDialogService _dialogService;
		private IDependencyService _dependencyService;
        private DataService dataService = new DataService();

		public DelegateCommand OnClickBack { set; get; }
        public string SelectedItemId { set; get; }
        public string SelectedItemsType { set; get; }

        private Color _topbarcolor = Color.Black;
		public Color TopBarColor
		{
			get { return _topbarcolor; }
			set { SetProperty(ref _topbarcolor, value); }
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

		public ObservableCollection<GalleryItem> _imageslist = new ObservableCollection<GalleryItem>();
		public ObservableCollection<GalleryItem> ImagesList
		{
			get { return _imageslist; }
			set { SetProperty(ref _imageslist, value); }
		}

		public GalleryPageViewModel(INavigationService navigationService,
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

		public async void LoadDataFromServer()
		{
			if (IsBusy) return;
			IsBusy = true;

            ImagesList.Clear();
            StatusText = "Getting new data...";
            var dialog = UserDialogs.Instance;
            dialog.ShowLoading("Loading...");

            GalleryResponse response = await dataService.getAllImages(SelectedItemId,SelectedItemsType);
			if (response != null)
			{
				if (response.status.Equals("success"))
				{
                    ImagesList = new ObservableCollection<GalleryItem>(response.data);
                    ImagesList.OrderByDescending(x => x.img_link);
				}

                if (ImagesList.Count() > 0)
                    IsHasDataFlag = false;
                else
                {
                    StatusText = "No data is available";
                    IsHasDataFlag = true;
                }
                
                dialog.HideLoading();
			}
			else
			{
                StatusText = "Service is unavailable";
				dialog.HideLoading();
			}

			IsBusy = false;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("topbarcolor") && 
                parameters.ContainsKey("id") && 
                parameters.ContainsKey("items_type"))
			{
                string mColor = (string)parameters["topbarcolor"];
                TopBarColor = Color.FromHex(mColor);

                SelectedItemId = (string)parameters["id"];
                SelectedItemsType = (string)parameters["items_type"];
                LoadDataFromServer();
			}
            else
            {
                GoBack();
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
