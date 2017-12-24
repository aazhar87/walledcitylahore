using System;
using System.Linq;
using CoreGraphics;
using WalledCityLahore.iOS.Renderers;
using WalledCityLahore.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("WalledCityLahore")]
[assembly: ExportEffect(typeof(LabelShadowEffectRenderer), "LabelShadowEffect")]

namespace WalledCityLahore.iOS.Renderers
{
    public class LabelShadowEffectRenderer : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    Control.Layer.CornerRadius = effect.Radius;
                    Control.Layer.ShadowColor = effect.Color.ToCGColor();
                    Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
                    Control.Layer.ShadowOpacity = 1.0f;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
