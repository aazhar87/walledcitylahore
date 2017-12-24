using System;
using WalledCityLahore.ViewModels;
using WalledCityLahore.Widgets;
using Xamarin.Forms;

namespace WalledCityLahore.Views
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel ViewModel => this.BindingContext as MainPageViewModel;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        async void OnClickOptionsMenu(object sender, System.EventArgs e)
        {
            string[] modes = { "Feedback" };
            string selectedMode = await DisplayActionSheet(
                    "Choose Option", "Cancel", null, modes
                );

            switch (selectedMode)
            {
                case "Feedback":
                    await ViewModel.NavigateToFeedback();
                    break;
                default:
                    break;
            }
        }
    }
}

