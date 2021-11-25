using DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstactions;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Interactions.Behaviors.Validation.Abstactions
{
	public abstract class ValidationRuleBehavior<T> : BaseBehavior<T>, IValidity where T : VisualElement
	{
		#region Properties

		public virtual IValidationContainer ValidationContainer => AssociatedObject.Parent as IValidationContainer;

		public string ValidationMessage { get; set; }

		#endregion

		#region Implementation of BehaviorBase

		protected override void OnAttachedTo(T visualElement)
		{
			base.OnAttachedTo(visualElement);

			if (visualElement is IValidatableObject validatableObject)
			{
				validatableObject.Unfocused += ElementUnfocused;
			}
			else
			{
				visualElement.Unfocused += ElementUnfocused;
			}
		}

		protected override void OnDetachingFrom(T visualElement)
		{
			if (visualElement is IValidatableObject validatableObject)
			{
				validatableObject.Unfocused -= ElementUnfocused;
			}
			else
			{
				visualElement.Unfocused -= ElementUnfocused;
			}

			base.OnDetachingFrom(visualElement);
		}

		#endregion

		#region Implementation of IValidity

		public abstract bool Validate();

		#endregion

		#region Public Methods

		public virtual void NotifyValidationContainer(bool validationResult)
		{
			if (validationResult)
			{
				ValidationContainer.ClearError();
			}
			else
			{
				ValidationContainer.SetError(ValidationMessage);
			}
		}

		#endregion

		#region Event Handlers

		private void ElementUnfocused(object sender, FocusEventArgs focusEventArgs)
		{
			if (ValidationContainer.IsValidationEnabled)
			{
				Validate();
			}
		}

		#endregion
	}
}
