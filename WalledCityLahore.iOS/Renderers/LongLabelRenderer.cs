using System;
using UIKit;
using WalledCityLahore.iOS.Renderers;
using WalledCityLahore.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LongLabel), typeof(LongLabelRenderer))]
namespace WalledCityLahore.iOS.Renderers
{
	public class LongLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				UILabel label = Control;
				label.Lines = 1000;
			}
		}
	}
}
