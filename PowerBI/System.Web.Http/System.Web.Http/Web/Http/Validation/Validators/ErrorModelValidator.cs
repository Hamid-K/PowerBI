using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x02000099 RID: 153
	public class ErrorModelValidator : ModelValidator
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		public ErrorModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders, string errorMessage)
			: base(validatorProviders)
		{
			if (errorMessage == null)
			{
				throw Error.ArgumentNull("errorMessage");
			}
			this._errorMessage = errorMessage;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			throw Error.InvalidOperation(this._errorMessage, new object[0]);
		}

		// Token: 0x040000DD RID: 221
		private string _errorMessage;
	}
}
