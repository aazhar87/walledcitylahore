using System;
using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using UIKit;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using WalledCityLahore.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using WalledCityLahore.Widgets;

[assembly: ExportRendererAttribute(typeof(BorderLayout), typeof(BorderLayoutRenderer))]
namespace WalledCityLahore.iOS.Renderers
{
	public class BorderLayoutRenderer : VisualElementRenderer<BorderLayout>
	{
		CALayer[] borderLayers = new CALayer[4];

		public BorderLayoutRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<BorderLayout> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				this.SetupLayer();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName ||
				e.PropertyName == BorderLayout.StrokeProperty.PropertyName ||
				e.PropertyName == BorderLayout.StrokeThicknessProperty.PropertyName ||
				e.PropertyName == BorderLayout.CornerRadiusProperty.PropertyName ||
				e.PropertyName == BorderLayout.WidthProperty.PropertyName ||
				e.PropertyName == BorderLayout.HeightProperty.PropertyName)
			{
				this.SetupLayer();
			}
		}

		private void SetupLayer()
		{
			if (Element == null || Element.Width <= 0 || Element.Height <= 0)
			{
				return;
			}

			Layer.CornerRadius = (nfloat)Element.CornerRadius;
			if (Element.BackgroundColor != Xamarin.Forms.Color.Default)
			{
				Layer.BackgroundColor = Element.BackgroundColor.ToCGColor();
			}
			else
			{
				Layer.BackgroundColor = UIColor.White.CGColor;
			}

			UpdateBorderLayer(BorderPosition.Left, (nfloat)Element.StrokeThickness.Left);
			UpdateBorderLayer(BorderPosition.Top, (nfloat)Element.StrokeThickness.Top);
			UpdateBorderLayer(BorderPosition.Right, (nfloat)Element.StrokeThickness.Right);
			UpdateBorderLayer(BorderPosition.Bottom, (nfloat)Element.StrokeThickness.Bottom);

			Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			Layer.ShouldRasterize = true;
		}

		enum BorderPosition
		{
			Left,
			Top,
			Right,
			Bottom
		}

		void UpdateBorderLayer(BorderPosition borderPosition, nfloat thickness)
		{
			var borderLayer = borderLayers[(int)borderPosition];
			if (thickness <= 0)
			{
				if (borderLayer != null)
				{
					borderLayer.RemoveFromSuperLayer();
					borderLayers[(int)borderPosition] = null;
				}
			}
			else
			{
				if (borderLayer == null)
				{
					borderLayer = new CALayer();
					Layer.AddSublayer(borderLayer);
					borderLayers[(int)borderPosition] = borderLayer;
				}

				switch (borderPosition)
				{
					case BorderPosition.Left:
						borderLayer.Frame = new CGRect(0, 0, thickness, (nfloat)Element.Height);
						break;
					case BorderPosition.Top:
						borderLayer.Frame = new CGRect(0, 0, (nfloat)Element.Width, thickness);
						break;
					case BorderPosition.Right:
						borderLayer.Frame = new CGRect((nfloat)Element.Width - thickness, 0, thickness, (nfloat)Element.Height);
						break;
					case BorderPosition.Bottom:
						borderLayer.Frame = new CGRect(0, (nfloat)Element.Height - thickness, (nfloat)Element.Width, thickness);
						break;
				}


				borderLayer.BackgroundColor = Element.Stroke.ToCGColor();
				borderLayer.CornerRadius = (nfloat)Element.CornerRadius;
			}
		}
	}
}

