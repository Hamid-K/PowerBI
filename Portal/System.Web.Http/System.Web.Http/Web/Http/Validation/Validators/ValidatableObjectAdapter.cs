using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x0200009B RID: 155
	public class ValidatableObjectAdapter : ModelValidator
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000ADDA File Offset: 0x00008FDA
		public ValidatableObjectAdapter(IEnumerable<ModelValidatorProvider> validatorProviders)
			: base(validatorProviders)
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			object model = metadata.Model;
			if (model == null)
			{
				return Enumerable.Empty<ModelValidationResult>();
			}
			IValidatableObject validatableObject = model as IValidatableObject;
			if (validatableObject == null)
			{
				throw Error.InvalidOperation(SRResources.ValidatableObjectAdapter_IncompatibleType, new object[] { model.GetType() });
			}
			ValidationContext validationContext = new ValidationContext(validatableObject, null, null);
			return ValidatableObjectAdapter.ConvertResults(validatableObject.Validate(validationContext));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000AE42 File Offset: 0x00009042
		private static IEnumerable<ModelValidationResult> ConvertResults(IEnumerable<ValidationResult> results)
		{
			foreach (ValidationResult result in results)
			{
				if (result != ValidationResult.Success)
				{
					if (result.MemberNames == null || !result.MemberNames.Any<string>())
					{
						yield return new ModelValidationResult
						{
							Message = result.ErrorMessage
						};
					}
					else
					{
						foreach (string text in result.MemberNames)
						{
							yield return new ModelValidationResult
							{
								Message = result.ErrorMessage,
								MemberName = text
							};
						}
						IEnumerator<string> enumerator2 = null;
					}
				}
				result = null;
			}
			IEnumerator<ValidationResult> enumerator = null;
			yield break;
			yield break;
		}
	}
}
