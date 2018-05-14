using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chat_proto.iOS.Renderers;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(chatproto.Renderers.ButtonUser), typeof(ButtonUseriOS))]
namespace chat_proto.iOS.Renderers
{
    public class ButtonUseriOS : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                if (value.Width > 0 && value.Height > 0)
                {
                    foreach (var layer in Control?.Layer.Sublayers.Where(layer => layer is CAGradientLayer))
                        layer.Frame = new CGRect(0, 0, value.Width, value.Height);
                }
                base.Frame = value;
            }
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradient = new CAGradientLayer();
                gradient.CornerRadius = Control.Layer.CornerRadius = 8;
                gradient.Colors = new CGColor[]
                {
                UIColor.FromRGB(240, 240, 240).CGColor,
                UIColor.FromRGB(249, 249, 249).CGColor
                };
                var layer = Control?.Layer.Sublayers.LastOrDefault();
                Control?.Layer.InsertSublayerBelow(gradient, layer);
            }
        }
    }
}
