﻿using System;
using System.Collections.Generic;
using Plugin.Messaging;
using WalledCityLahore.ViewModels;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class AttractionDetailPage : ContentPage
    {
        private AttractionDetailPageViewModel ViewModel => this.BindingContext as AttractionDetailPageViewModel;

        public AttractionDetailPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

		void OnClickPhone(object sender, System.EventArgs e)
		{
			// Make Phone Call
			var phoneDialer = CrossMessaging.Current.PhoneDialer;
			if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(ViewModel.RecievedAttraction.contact_numb);
		}

		void OnClickPhone2(object sender, System.EventArgs e)
		{
			// Make Phone Call
			var phoneDialer = CrossMessaging.Current.PhoneDialer;
			if (phoneDialer.CanMakePhoneCall)
				phoneDialer.MakePhoneCall(ViewModel.RecievedAttraction.contact_numb2);
		}

		void OnClickViewMore(object sender, System.EventArgs e)
		{
			ViewModel.OpenViewMore();
		}

		void OnClickUrl(object sender, System.EventArgs e)
		{
			ViewModel.OpenWebUrl();
		}

		void OnClickGetDirections(object sender, System.EventArgs e)
		{
			ViewModel.GetDirections();
		}
    }
}
