using System.Collections.Generic;
using System.Linq;
using DailyFitNative.Infrastructure.Core.ViewModels.Implementations;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;

namespace DailyFitNative.Infrastructure.Utilities.Validation.Implementations
{
	public class ValidatableObject<T> : NotifyPropertyChangedImplementation, IValidatableObject<T>
	{

		#region Private Fields

		private IList<string> _errors;

		private T _value;

		private bool _isValid;

		#endregion

		#region Constructors

		public ValidatableObject()
		{
			_isValid = true;

			_errors = new List<string>();

			ValidationRules = new List<IValidationRule<T>>();
		}

		#endregion

		#region Properties

		public IList<IValidationRule<T>> ValidationRules { get; }

		public IList<string> Errors
		{
			get => _errors;
			set => SetProperty(ref _errors, value);
		}

		public T Value
		{
			get => _value;
			set => SetProperty(ref _value, value);
		}

		public bool IsValid
		{
			get => _isValid;
			set => SetProperty(ref _isValid, value);
		}

		#endregion

		#region Public Methods

		public bool Validate()
		{
			Errors.Clear();

			Errors = ValidationRules.Where(v => !v.Check(Value))
				.Select(v => v.ValidationMessage).ToList(); 

			IsValid = !Errors.Any();

			return IsValid;
		}

		#endregion
	}
}
