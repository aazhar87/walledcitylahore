using System;
using System.Collections.Generic;
using DLToolkit.Forms.Controls;
using Rg.Plugins.Popup.Services;
using WalledCityLahore.Helpers;
using WalledCityLahore.Models;
using WalledCityLahore.Widgets;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        async void Handle_FlowItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e.Item as GalleryItem;
            ((FlowListView)sender).SelectedItem = null;
            
            Settings.ImageUrl = item.img_link;
			var page = new ViewImagePopup();
			await PopupNavigation.PushAsync(page);
        }
    }
}
