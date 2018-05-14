using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using chat_proto.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(chatproto.Renderers.ButtonRenderer), typeof(ButtonRendererAndroid))]
namespace chat_proto.Droid.Renderers
{
    public class ButtonRendererAndroid : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Drawable.CustomButtonBackgroundAndroid);
                Control.SetPadding(5, 0, 5, 0);
                Control.SetAllCaps(false);
            }
        }
    }
}