using System;
using System.Collections.Generic;
using WalledCityLahore.Models;
using WalledCityLahore.ViewModels;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
	public partial class RestaurantsListPage : ContentPage
	{
		private RestaurantsListPageViewModel ViewModel => this.BindingContext as RestaurantsListPageViewModel;

		public RestaurantsListPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			
		}

		private async void onListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null || ViewModel.IsBusy)
			{
				((ListView)sender).SelectedItem = null;
				return;
			}

			var item = e.SelectedItem as RestaurantItem;
			((ListView)sender).SelectedItem = null;
			await ViewModel.PageChangeAsync(item);
		}

		async void OnClickOptionsMenu(object sender, System.EventArgs e)
		{
			string[] modes = { "Settings" };
			string selectedMode = await DisplayActionSheet(
					"Choose Option", "Cancel", null, modes
				);

			switch (selectedMode)
			{
				case "Settings":
					break;
				default:
					break;
			}
		}
	}
}

