using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chat_proto.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(chatproto.Renderers.EntryRenderer), typeof(EntryRendereriOS))]
namespace chat_proto.iOS.Renderers
{
    public class EntryRendereriOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                return;
            }

            var entry = this.Control as UITextField;

            entry.ClearButtonMode = UITextFieldViewMode.WhileEditing;
        }
    }
}