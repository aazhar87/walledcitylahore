using System;
using System.Collections.Generic;
using WalledCityLahore.Models;
using WalledCityLahore.ViewModels;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class AttractionsListPage : ContentPage
    {
        private AttractionsListPageViewModel ViewModel => this.BindingContext as AttractionsListPageViewModel;

        public AttractionsListPage()
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

			var item = e.SelectedItem as AttractionItem;
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
