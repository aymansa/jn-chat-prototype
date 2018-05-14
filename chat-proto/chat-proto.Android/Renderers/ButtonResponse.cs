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

[assembly: ExportRenderer(typeof(chatproto.Renderers.ButtonResponse), typeof(ButtonResponse))]
namespace chat_proto.Droid.Renderers
{
    public class ButtonResponse : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Drawable.ButtonResponseLayout);
                Control.SetPadding(5, 0, 5, 0);
                Control.SetAllCaps(false);
            }
        }
    }
}