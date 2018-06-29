using System.Linq;
using DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstactions;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides.Validation
{
	public class ValidationContainer : Grid, IValidator, IValidationContainer
	{
		#region Bindable

		public static readonly BindableProperty ValidationScopeProperty = BindableProperty.Create(
			nameof(ValidationRuleScope),
			typeof(IValidationRuleScope),
			typeof(ValidationContainer),
			propertyChanged: ValidationScopePropertyChanged);

		public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
			nameof(IsValid),
			typeof(bool),
			typeof(ValidationContainer),
			true,
			BindingMode.OneWayToSource);

		public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
			nameof(LabelText),
			typeof(string),
			typeof(ValidationContainer));

		public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
			nameof(IsRequired),
			typeof(bool),
			typeof(ValidationContainer),
			false);

		public static readonly BindableProperty IsValidationEnabledProperty = BindableProperty.Create(
			nameof(IsValidationEnabled),
			typeof(bool),
			typeof(ValidationContainer),
			true,
			propertyChanged: IsValidationEnabledPropertyChanged);

		public static readonly BindableProperty SourceProperty = BindableProperty.Create(
			nameof(Source),
			typeof(object),
			typeof(ValidationContainer),
			propertyChanged: SourcePropertyChanged);

		public static readonly BindableProperty AsteriskColorProperty = BindableProperty.Create(
			nameof(AsteriskColor),
			typeof(Color),
			typeof(ValidationContainer),
			Color.Red);

		#endregion

		#region Private Fields

		private View _input;

		private Style _inputDefaultStyle;

		private Label _errorLabel;

		#endregion

		#region Properties

		public IValidationRuleScope ValidationRuleScope
		{
			get => (IValidationRuleScope) GetValue(ValidationScopeProperty);
			set => SetValue(ValidationScopeProperty, value);
		}

		public bool IsNotValid { get; set; }

		public bool IsValid
		{
			get => (bool) GetValue(IsValidProperty);
			set => SetValue(IsValidProperty, value);
		}

		public string LabelText
		{
			get => (string) GetValue(LabelTextProperty);
			set => SetValue(LabelTextProperty, value);
		}

		public bool IsRequired
		{
			get => (bool) GetValue(IsRequiredProperty);
			set => SetValue(IsRequiredProperty, value);
		}

		public bool IsValidationEnabled
		{
			get => (bool) GetValue(IsValidationEnabledProperty);
			set => SetValue(IsValidationEnabledProperty, value);
		}

		public object Source
		{
			get => GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}

		public Color AsteriskColor
		{
			get => (Color) GetValue(AsteriskColorProperty);
			set => SetValue(AsteriskColorProperty, value);
		}

		public Style LabelStyle { get; set; }

		public Style InvalidInputStyle { get; set; }

		public Style ValidationLabelStyle { get; set; }

		public View Input
		{
			set
			{
				_input = value;
				_inputDefaultStyle = value.Style;
				BuildValidationControl();
			}
		}

		#endregion

		#region Implementation of IValidator

		public bool Validate() =>
			IsValid = !IsValidationEnabled ||
			          _input.Behaviors
				          .OfType<IValidator>()
				          .All(validationBehavior => validationBehavior.Validate());

		#endregion

		#region Implementation of IValidationContainer

		public void SetError(string errorMessage)
		{
			if (!string.IsNullOrEmpty(errorMessage))
			{
				_errorLabel.Text = errorMessage;
				_errorLabel.Style = ValidationLabelStyle;
				_errorLabel.IsVisible = true;
			}

			if (IsValid)
			{
				if (_input is IValidatableObject validatableControl)
				{
					validatableControl.SetInvalidStyle(InvalidInputStyle);
				}
				else
				{
					_input.Style = InvalidInputStyle;
				}
			}

			IsValid = false;
		}

		public void ClearError()
		{
			if (!IsValid)
			{
				_errorLabel.IsVisible = false;

				if (_input is IValidatableObject validatableControl)
				{
					validatableControl.RestoreDefaultStyle();
				}
				else
				{
					_input.Style = _inputDefaultStyle;
				}

				IsValid = true;
			}
		}

		#endregion

		#region Event Handlers

		private static void ValidationScopePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!Equals(oldValue, newValue) && newValue is IValidationRuleScope validationScope)
			{
				validationScope.AddValidator((IValidator) bindable);
			}
		}

		private static void IsValidationEnabledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var isValidationEnabled = (bool) newValue;

			if (!isValidationEnabled)
			{
				var validationConteiner = (ValidationContainer) bindable;
				validationConteiner.ClearError();
			}
		}

		private static void SourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!Equals(oldValue, newValue) && oldValue != null)
			{
				var validationContainer = (ValidationContainer) bindable;
				validationContainer.ClearError();
			}
		}

		#endregion

		#region Private Methods

		private void BuildValidationControl()
		{
			var labelWithAsterisk = new AsteriskLabel
			{
				Text = LabelText,
				Style = LabelStyle,
				AsteriskColor = AsteriskColor
			};

			_errorLabel = new Label
			{
				Style = ValidationLabelStyle,
				IsVisible = false
			};

			labelWithAsterisk.SetBinding(AsteriskLabel.IsAsteriskVisibleProperty,
				new Binding(nameof(IsRequired), source: this));
			labelWithAsterisk.SetBinding(AsteriskLabel.TextProperty, new Binding(nameof(LabelText), source: this));

			RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)},
				new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)},
				new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)}
			};

			Children.Add(labelWithAsterisk, 0, 0);
			Children.Add(_input, 0, 1);
			Children.Add(_errorLabel, 0, 2);
		}

		#endregion
	}
}
	