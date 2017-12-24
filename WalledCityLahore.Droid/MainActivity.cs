using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism.Unity;
using Acr.UserDialogs;
using Xamarin.Forms.Platform.Android;
using Xamarin;
using Plugin.Messaging;
using FFImageLoading.Forms.Droid;
using Microsoft.Practices.Unity;

namespace WalledCityLahore.Droid
{
    [Activity(Label = "Walled City Lahore", Theme = "@style/MyTheme", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CrossMessaging.Current.Settings().Phone.AutoDial = true;
            FormsGoogleMaps.Init(this, bundle);
            UserDialogs.Init(this);
            CachedImageRenderer.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnBackPressed()
        {
            //do nothing
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
        }
    }
}
