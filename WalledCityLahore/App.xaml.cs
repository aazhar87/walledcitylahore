using DLToolkit.Forms.Controls;
using Prism.Unity;
using WalledCityLahore.Helpers;
using WalledCityLahore.Views;

namespace WalledCityLahore
{
    public partial class App : PrismApplication
    {
        public static App Instance { get; private set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            initializer?.RegisterTypes(Container);
            SetStartPage();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            FlowListView.Init();
            Settings.AppToken = "wall3dc1tylah0r3";
            Instance = this;
        }

        private async void SetStartPage()
        {
            await NavigationService.NavigateAsync("MainPage", null, true, true);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AttractionsListPage>();
            Container.RegisterTypeForNavigation<EventsListPage>();
            Container.RegisterTypeForNavigation<RestaurantsListPage>();
            Container.RegisterTypeForNavigation<BookingTypeListPage>();

            Container.RegisterTypeForNavigation<BookingsListPage>();
            Container.RegisterTypeForNavigation<BookingDetailPage>();
            Container.RegisterTypeForNavigation<BookNowPage>();
            Container.RegisterTypeForNavigation<EventDetailPage>();
            Container.RegisterTypeForNavigation<AttractionDetailPage>();
            Container.RegisterTypeForNavigation<RestaurantDetailPage>();

            Container.RegisterTypeForNavigation<GalleryPage>();
            Container.RegisterTypeForNavigation<MapViewPointsPage>();
            Container.RegisterTypeForNavigation<FeedbackPage>();
        }
    }
}

