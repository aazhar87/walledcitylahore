using System;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using WalledCityLahore.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WalledCityLahore.Widgets
{
	public partial class ViewImagePopup : PopupPage
	{
		public ViewImagePopup()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
            imgPic.Source = Settings.ImageUrl;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

        async void OnClickClose(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
		{
			return Content.FadeTo(0.5);
		}

		// Method for animation child in PopupPage
		// Invoked before custom animation begin
		protected virtual Task OnDisappearingAnimationBegin()
		{
			return Content.FadeTo(1); ;
		}

		protected override bool OnBackButtonPressed()
		{
			// Prevent hide popup
			return base.OnBackButtonPressed();
		}

		// Invoced when background is clicked
		protected override bool OnBackgroundClicked()
		{
			// Return default value - CloseWhenBackgroundIsClicked
			return base.OnBackgroundClicked();
		}
	}
}
