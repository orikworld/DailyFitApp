﻿using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;

namespace DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstactions
{
	public interface IValidationContainer
	{
		IValidationRuleScope ValidationRuleScope { get; set; }

		bool IsValidationEnabled { get; set; }

		void SetError(string error);

		void ClearError();
	}
}