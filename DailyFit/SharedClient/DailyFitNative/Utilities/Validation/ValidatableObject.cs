using System.Collections.Generic;
using System.Linq;
using DailyFitNative.Infrastructure.Core.ViewModels.Implementations;
using DailyFitNative.Utilities.Validation.Abstractions;

namespace DailyFitNative.Utilities.Validation
{
	public class ValidatableObject<T> : NotifyPropertyChangedImplementation, IValidity
	{
		#region Private Fields

		private readonly List<IValidationRule<T>> _validations;

		private List<string> _errors;

		private T _value;

		private bool _isValid;

		#endregion

		#region Constructors

		public ValidatableObject()
		{
			_isValid = true;
			_errors = new List<string>();
			_validations = new List<IValidationRule<T>>();
		}

		#endregion

		#region Properties

		public List<IValidationRule<T>> Validations => _validations;

		public List<string> Errors
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

		#region Public Mehods

		public bool Validate()
		{
			Errors.Clear();

			var errors = _validations.Where(v => !v.Check(Value))
				.Select(v => v.ValidationMessage);

			Errors = errors.ToList();

			IsValid = !Errors.Any();

			return IsValid;
		}

		#endregion
	}
}
