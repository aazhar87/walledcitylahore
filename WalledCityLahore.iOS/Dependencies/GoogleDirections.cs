using Foundation;
using WalledCityLahore.iOS.Dependencies;
using WalledCityLahore.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleDirections))]
namespace WalledCityLahore.iOS.Dependencies
{
	public class GoogleDirections : IGoogleDirections
	{
		public void OpenGoogleMapDirectionsApp(string location)
		{
			string[] destination = location.Split(',');

			string appleMapsUrl = "http://maps.apple.com/?daddr=" + destination[0] + "," + destination[1];
			string googleMapsUrl = "comgooglemaps-x-callback://?daddr=" + destination[0] + "," + destination[1] + "&x-success=sourceapp://?resume=true&x-source=AirApp";
			appleMapsUrl = appleMapsUrl.Replace(" ", "%20");
			googleMapsUrl = googleMapsUrl.Replace(" ", "%20");

			if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl(googleMapsUrl)))
			{
				UIApplication.SharedApplication.OpenUrl(new NSUrl(googleMapsUrl));
			}
			else if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl(appleMapsUrl)))
			{
				UIApplication.SharedApplication.OpenUrl(new NSUrl(appleMapsUrl));
			}
			else
			{
				new UIAlertView("Error", "Maps is not supported on this device", null, "Ok").Show();
			}
		}
	}
}

