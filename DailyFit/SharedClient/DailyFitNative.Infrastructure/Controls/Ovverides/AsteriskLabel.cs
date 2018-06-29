using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides
{
	public class AsteriskLabel : Label
	{
		#region Bindable

		public new static readonly BindableProperty TextProperty = BindableProperty.Create(
			nameof(Text),
			typeof(string),
			typeof(AsteriskLabel),
			propertyChanged: AsteriskLabelChanged);

		public static readonly BindableProperty IsAsteriskVisibleProperty = BindableProperty.Create(
			nameof(IsAsteriskVisible),
			typeof(bool),
			typeof(AsteriskLabel),
			false,
			propertyChanged: AsteriskLabelChanged);

		#endregion

		#region Properties

		public new string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public bool IsAsteriskVisible
		{
			get => (bool)GetValue(IsAsteriskVisibleProperty);
			set => SetValue(IsAsteriskVisibleProperty, value);
		}

		public Color AsteriskColor { get; set; }

		#endregion

		#region Public Methods

		public void BuildLabelWithAsterisk(bool isAsteriskVisible)
		{
			var formattedString = new FormattedString();
			var asteriskSpan = new Span { Text = "*", FontSize = FontSize, ForegroundColor = AsteriskColor };
			var labelSpan = new Span { Text = Text, FontSize = FontSize, ForegroundColor = TextColor };

			formattedString.Spans.Add(labelSpan);

			if (isAsteriskVisible)
			{
				formattedString.Spans.Add(asteriskSpan);
			}
			else
			{
				formattedString.Spans.Remove(asteriskSpan);
			}

			FormattedText = formattedString;
		}

		#endregion

		#region Private Methods

		private static void AsteriskLabelChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (oldValue != newValue)
			{
				var asteriskLabel = (AsteriskLabel)bindable;
				asteriskLabel.BuildLabelWithAsterisk(asteriskLabel.IsAsteriskVisible);
			}
		}

		#endregion
	}
}
