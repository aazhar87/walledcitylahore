using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Widget;
using Plugin.CurrentActivity;
using WalledCityLahore.Droid.Dependencies;
using WalledCityLahore.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(GoogleDirections))]

namespace WalledCityLahore.Droid.Dependencies
{
    public class GoogleDirections : IGoogleDirections
    {
        public void OpenGoogleMapDirectionsApp(string location)
        {
            //string uri = "geo:" + location;
            //var geoUri = Android.Net.Uri.Parse(uri);
            //var mapIntent = new Intent(Intent.ActionView, geoUri);
            //CrossCurrentActivity.Current.Activity.StartActivity(mapIntent);

            string[] destination = location.Split(',');

            String uri = "http://maps.google.com/maps?daddr=" + destination[0] + "," + destination[1] + " (" + "Where the party is at" + ")";
            Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
            intent.SetPackage("com.google.android.apps.maps");
            try
            {
                CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }
            catch (ActivityNotFoundException ex)
            {
                try
                {
                    Intent unrestrictedIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                    CrossCurrentActivity.Current.Activity.StartActivity(unrestrictedIntent);
                }
                catch (ActivityNotFoundException innerEx)
                {
                    Toast.MakeText(CrossCurrentActivity.Current.Activity, "Please install a maps application", ToastLength.Long).Show();
                }
            }
        }
    }
}
