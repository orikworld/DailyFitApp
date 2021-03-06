using System;
using DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DailyFitNative.Infrastructure.Controls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryWithFrameBorder : Grid, IValidationControl
	{
		#region Bindable

		public static readonly BindableProperty TextProperty = BindableProperty.Create(
			nameof(Text),
			typeof(string),
			typeof(EntryWithFrameBorder),
			string.Empty,
			BindingMode.TwoWay);

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
			nameof(TextColor),
			typeof(Color),
			typeof(EntryWithFrameBorder),
			Color.Black);

		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
			nameof(Placeholder),
			typeof(string),
			typeof(EntryWithFrameBorder));

		public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
			nameof(PlaceholderColor),
			typeof(Color),
			typeof(EntryWithFrameBorder),
			Color.Silver);

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
			nameof(FontSize),
			typeof(double),
			typeof(EntryWithFrameBorder),
			16d);

		public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
			nameof(BackgroundColor),
			typeof(Color),
			typeof(EntryWithFrameBorder), 
			Color.Gray);

		public  static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
			nameof(BorderColor),
			typeof(Color),
			typeof(EntryWithFrameBorder),
			Color.Gray);

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(EntryWithFrameBorder),
            false);

		#endregion

		#region Constructors

		public EntryWithFrameBorder()
		{
			InitializeComponent();
		}

		#endregion

		#region Properties

		public new event EventHandler<FocusEventArgs> Unfocused
		{
			add => Entry.Unfocused += value;
			remove => Entry.Unfocused -= value;
		}

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public Color TextColor
		{
			get => (Color)GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		public Color PlaceholderColor
		{
			get => (Color)GetValue(PlaceholderColorProperty);
			set => SetValue(PlaceholderColorProperty, value);
		}

		[TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get => (double)GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		public new Color BackgroundColor
		{
			get => (Color)GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

        public bool IsPassword 
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

		public Color BorderColor
		{
			get => (Color)GetValue(BorderColorProperty);
			set => SetValue(BorderColorProperty, value);
		}

		#endregion
	}
}