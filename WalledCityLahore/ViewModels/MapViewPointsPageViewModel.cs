using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using WalledCityLahore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WalledCityLahore.ViewModels
{
    public class MapViewPointsPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private IDependencyService _dependencyService;

        public DelegateCommand OnClickBack { set; get; }

        private Color _BackgroundColor;
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { SetProperty(ref _BackgroundColor, value); }
        }

		private Map _map = new Map();
		public Map mMap
		{
			get { return _map; }
			set { SetProperty(ref _map, value); }
		}

        private ObservableCollection<EventItem> _eventsList = new ObservableCollection<EventItem>();
        public ObservableCollection<EventItem> EventsList
        {
            get { return _eventsList; }
            set { SetProperty(ref _eventsList, value); }
        }

        private ObservableCollection<AttractionItem> _attractionsList = new ObservableCollection<AttractionItem>();
        public ObservableCollection<AttractionItem> AttractionsList
        {
            get { return _attractionsList; }
            set { SetProperty(ref _attractionsList, value); }
        }

        private ObservableCollection<RestaurantItem> _restaurantsList = new ObservableCollection<RestaurantItem>();
        public ObservableCollection<RestaurantItem> RestaurantsList
        {
            get { return _restaurantsList; }
            set { SetProperty(ref _restaurantsList, value); }
        }

        public MapViewPointsPageViewModel(INavigationService navigationService,
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

        public void SetupEventsMarkers()
        {
            if (mMap == null) return;
            mMap.Pins.Clear();

            foreach (EventItem item in EventsList)
            {
                System.Diagnostics.Debug.WriteLine("events map count  position: " + item.latitude + " , " + item.longitude);
                Pin eventlocation = new Pin()
                {
                    Type = PinType.Generic,
                    Label = item.name,
                    Position = new Position(Double.Parse(item.longitude),
                                            Double.Parse(item.latitude))
                };

                mMap.Pins.Add(eventlocation);
				mMap.MoveCamera(CameraUpdateFactory.NewPositionZoom(
					eventlocation.Position, 13d));
			}
        }

        public void SetupRestaurantsMarkers()
        {
            if (mMap == null) return;
            mMap.Pins.Clear();

            foreach (RestaurantItem item in RestaurantsList)
            {
                System.Diagnostics.Debug.WriteLine("restaurants map count  position: " + item.latitude + " , " + item.longitude);
                Pin eventlocation = new Pin()
                {
                    Type = PinType.Generic,
                    Label = item.name,
                    Position = new Position(Double.Parse(item.longitude),
                                            Double.Parse(item.latitude))
                };
                mMap.Pins.Add(eventlocation);
				mMap.MoveCamera(CameraUpdateFactory.NewPositionZoom(
					eventlocation.Position, 13d));
            }
        }

        public void SetupAttractionsMarkers()
        {
            if (mMap == null) return;
            mMap.Pins.Clear();

            foreach (AttractionItem item in AttractionsList)
            {
                System.Diagnostics.Debug.WriteLine("attraction map count  position: "+item.latitude+" , "+item.longitude);
                Pin eventlocation = new Pin()
                {
                    Type = PinType.Generic,
                    Label = item.name,
                    Position = new Position(Double.Parse(item.longitude),
                                            Double.Parse(item.latitude))
                };
                mMap.Pins.Add(eventlocation);
				mMap.MoveCamera(CameraUpdateFactory.NewPositionZoom(
					eventlocation.Position, 13d));
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("model") && parameters.ContainsKey("type"))
            {
                string type = (string)parameters["type"];
				string bg = (string)parameters["background"];
				BackgroundColor = Color.FromHex(bg);

                if (type.Equals("events"))
                {
                    EventsList = (ObservableCollection<EventItem>)parameters["model"];
                    SetupEventsMarkers();
                }
                else if (type.Equals("attractions"))
                {
                    AttractionsList = (ObservableCollection<AttractionItem>)parameters["model"];
                    SetupAttractionsMarkers();
                }
                else if (type.Equals("restaurants"))
                {
                    RestaurantsList = (ObservableCollection<RestaurantItem>)parameters["model"];
                    SetupRestaurantsMarkers();
                }

                mMap.PinClicked += (sender, e) => {
                    mMap.MoveCamera(CameraUpdateFactory.NewPositionZoom(
                        e.Pin.Position, 15d));
                };
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
