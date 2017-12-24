using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WalledCityLahore.Widgets
{
    public partial class MenuItem : ContentView
    {
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            propertyName: "Image",
            returnType: typeof(ImageSource),
            declaringType: typeof(MenuItem),
            defaultValue: null);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(MenuItem),
            defaultValue: null);

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public event EventHandler Clicked;

        public MenuItem()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Clicked(this, e);
        }
    }
}
