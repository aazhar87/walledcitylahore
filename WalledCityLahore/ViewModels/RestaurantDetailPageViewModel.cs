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
	public class RestaurantDetailPageViewModel : BaseViewModel, INavigationAware
	{
		private INavigationService _navigationService;
		private IPageDialogService _dialogService;
		private IDependencyService _dependencyService;

		public DelegateCommand OnClickBack { set; get; }

		private RestaurantItem _recievedRestaurant = new RestaurantItem();
		public RestaurantItem RecievedRestaurant
		{
			get { return _recievedRestaurant; }
			set { SetProperty(ref _recievedRestaurant, value); }
		}

		public RestaurantDetailPageViewModel(INavigationService navigationService,
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

			if (RecievedRestaurant.website != null || RecievedRestaurant.website.Length != 0)
				Device.OpenUri(new Uri(RecievedRestaurant.website));

            IsBusy = false;
		}

		public void GetDirections()
		{
			if (_dependencyService.Get<IGoogleDirections>() != null)
			{
				_dependencyService.Get<IGoogleDirections>().OpenGoogleMapDirectionsApp(RecievedRestaurant.latitude + "," + RecievedRestaurant.longitude);
			}
		}

		public async void OpenViewMore()
		{
			if (IsBusy) return;
			IsBusy = true;

			var navigationParams = new NavigationParameters();
			navigationParams.Add("topbarcolor", "#006442");
            navigationParams.Add("id", RecievedRestaurant.id);
			navigationParams.Add("items_type", "restaurant");
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
				RecievedRestaurant = (RestaurantItem)parameters["model"];
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
