using System;
using WalledCityLahore.Droid.Renderers;
using WalledCityLahore.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LongLabel), typeof(LongLabelRenderer))]
namespace WalledCityLahore.Droid.Renderers
{ 
    public class LongLabelRenderer : LabelRenderer 
    { 
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e) 
        { 
            base.OnElementChanged(e); 
            Control.SetMaxLines(1000); 
        } 
    } 
}
