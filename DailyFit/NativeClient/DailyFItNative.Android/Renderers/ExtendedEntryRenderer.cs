using Android.Content;
using Android.Graphics.Drawables;
using DailyFitNative.Droid.Renderers;
using DailyFitNative.Infrastructure.Controls.Ovverides;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(ExtendedEnty), typeof(ExtendedEntryRenderer))]
namespace DailyFitNative.Droid.Renderers
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		#region Constructors

		public ExtendedEntryRenderer(Context context) : base(context)
		{

		}

		#endregion

		#region Protected Methods

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			var extendedEnty = (ExtendedEnty) Element;

			if (Control != null)
			{
				if (extendedEnty.ClearFormatting)
				{
					var gd = new GradientDrawable();

					gd.SetColor(Color.Transparent);

					Control.SetBackgroundDrawable(gd);
				}		
			}
		}

		#endregion
	}
}