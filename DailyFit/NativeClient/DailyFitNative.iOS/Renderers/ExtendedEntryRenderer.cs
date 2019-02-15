using DailyFitNative.iOS.Renderers;
using DailyFitNative.Infrastructure.Controls.Ovverides;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace DailyFitNative.iOS.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var extendedEnty = (ExtendedEntry)Element;

            if (Element != null && Control != null)
			{
				if (extendedEnty.ClearFormatting)
				{
					Control.BorderStyle = UITextBorderStyle.None;
				}
			}
		}
	}
}