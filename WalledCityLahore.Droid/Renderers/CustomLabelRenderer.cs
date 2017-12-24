using System;
using Android.Graphics;
using Android.Widget;
using WalledCityLahore.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace WalledCityLahore.Droid.Renderers
{
	public class CustomLabelRenderer : LabelRenderer
	{
		private object _xamFormsSender;

		public CustomLabelRenderer()
		{
			_xamFormsSender = null;
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			// Update native Textview
			if (_xamFormsSender != sender || e.PropertyName == Label.FontFamilyProperty.PropertyName)
			{
				var font = ((Label)sender).FontFamily;

				// valid font 
				if (!string.IsNullOrEmpty(font))
				{
					// check font file name
					if (!font.Contains(".ttf")) font += ".ttf";
					var typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/" + font);

					// update font
					var label = Control as TextView;
					if (label != null)
						label.Typeface = typeface;
				}

				_xamFormsSender = sender;
			}
		}
	}
}
