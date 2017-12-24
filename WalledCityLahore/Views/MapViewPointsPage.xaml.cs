using System;
using System.Collections.Generic;
using WalledCityLahore.ViewModels;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class MapViewPointsPage : ContentPage
    {
        private MapViewPointsPageViewModel ViewModel => this.BindingContext as MapViewPointsPageViewModel;

        public MapViewPointsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (map == null)
				return;

			ViewModel.mMap = map;
		}
    }
}
