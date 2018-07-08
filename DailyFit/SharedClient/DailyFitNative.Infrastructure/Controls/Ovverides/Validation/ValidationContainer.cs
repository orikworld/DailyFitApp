using System.Linq;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using DailyFitNative.Models.Enums;
using DailyFitNative.Models.Models.Base.Abstractions;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides.Validation
{
    public class ValidationContainer<T> : Grid
	{
	    #region Bindable

	    public static readonly BindableProperty ValidatableObjectProperty = BindableProperty.Create(
		    nameof(ValidatableObject),
		    typeof(IValidatableObject<T>),
		    typeof(ValidationContainer<T>));

	    #endregion

	    #region Private Fields

	    private View _input;

	    private Label _errorLabel;

	    #endregion

	    #region Properties

	    public IValidatableObject<T> ValidatableObject
		{
		    get => (IValidatableObject<T>)GetValue(ValidatableObjectProperty);
		    set => SetValue(ValidatableObjectProperty, value);
	    }

	    public Style ValidationLabelStyle { get; set; }

	    public View Input
	    {
		    get => _input;

		    set
		    {
			    if (_input != null)
			    {
				    if (_input is IFocusable focusableInput)
				    {
					    focusableInput.Unfocused -= InputOnUnfocused;
					}
			    }

			    _input = value;

			    BuildValidationContainer();

				if (_input != null)
			    {
					if (_input is IFocusable focusableInput)
					{		
						focusableInput.Unfocused += InputOnUnfocused;
					}
				}
		    }
	    }

		#endregion

	    #region Private Methods

	    private void BuildValidationContainer()
	    {
		    VisualStateManager.GoToState(Input, ValidationState.Normal.ToString());

			_errorLabel = new Label
		    {
			    Style = ValidationLabelStyle,
			    IsVisible = false
		    };

		    RowDefinitions = new RowDefinitionCollection
		    {
			    new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)},
			    new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)}
		    };

		    Children.Add(Input, 0, 1);
		    Children.Add(_errorLabel, 0, 2);
		}

		private void InputOnUnfocused(object sender, FocusEventArgs focusEventArgs)
		{
			var isValid = ValidatableObject.Validate();

			if (isValid)
			{
				_errorLabel.IsVisible = false;
			
				VisualStateManager.GoToState(Input, ValidationState.Valid.ToString());
			}
			else
			{
				_errorLabel.IsVisible = true;
				_errorLabel.Text = ValidatableObject.Errors.First();

				VisualStateManager.GoToState(Input, ValidationState.Invalid.ToString());
			}	
		}

		#endregion
	}
}
