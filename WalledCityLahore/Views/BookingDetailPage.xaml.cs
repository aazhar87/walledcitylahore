using System;
using System.Collections.Generic;
using WalledCityLahore.ViewModels;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class BookingDetailPage : ContentPage
    {
        private BookingDetailPageViewModel ViewModel => this.BindingContext as BookingDetailPageViewModel;

        public BookingDetailPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void OnClickLike(object sender, System.EventArgs e)
        {
            
        }

        async void OnClickBookNow(object sender, System.EventArgs e)
        {
            await ViewModel.PageChangeAsync();
        }
    }
}
