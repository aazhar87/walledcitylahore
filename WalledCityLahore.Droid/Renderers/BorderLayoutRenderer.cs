using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Widget;
using System.Linq;
using System.ComponentModel;
using WalledCityLahore.Widgets;
using WalledCityLahore.Droid.Renderers;

[assembly: ExportRendererAttribute(typeof(BorderLayout), typeof(BorderLayoutRenderer))]

namespace WalledCityLahore.Droid.Renderers
{
	public class BorderLayoutRenderer : VisualElementRenderer<BorderLayout>
	{
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			//HandlePropertyChanged (sender, e);
			BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<BorderLayout> e)
		{
			base.OnElementChanged(e);
			BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
		}

		/*void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Content")
			{
				BorderRendererVisual.UpdateBackground (Element, this.ViewGroup);
			}
		}*/

		protected override void DispatchDraw(Canvas canvas)
		{
			if (Element.IsClippedToBorder)
			{
				canvas.Save(SaveFlags.Clip);
				BorderRendererVisual.SetClipPath(this, canvas);
				base.DispatchDraw(canvas);
				canvas.Restore();
			}
			else
			{
				base.DispatchDraw(canvas);
			}
		}
	}
}