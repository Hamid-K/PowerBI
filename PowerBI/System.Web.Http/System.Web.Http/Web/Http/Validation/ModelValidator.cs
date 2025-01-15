using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x02000096 RID: 150
	public abstract class ModelValidator
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000AD52 File Offset: 0x00008F52
		protected ModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			this.ValidatorProviders = validatorProviders;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000AD6F File Offset: 0x00008F6F
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000AD77 File Offset: 0x00008F77
		protected internal IEnumerable<ModelValidatorProvider> ValidatorProviders { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00003B5D File Offset: 0x00001D5D
		public virtual bool IsRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000AD80 File Offset: 0x00008F80
		public static ModelValidator GetModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			return new ModelValidator.CompositeModelValidator(validatorProviders);
		}

		// Token: 0x060003B2 RID: 946
		public abstract IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container);

		// Token: 0x020001C0 RID: 448
		private class CompositeModelValidator : ModelValidator
		{
			// Token: 0x06000AE2 RID: 2786 RVA: 0x0000ADDA File Offset: 0x00008FDA
			public CompositeModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
				: base(validatorProviders)
			{
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
			public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
			{
				bool propertiesValid = true;
				foreach (ModelMetadata propertyMetadata in metadata.Properties)
				{
					foreach (ModelValidator modelValidator in propertyMetadata.GetValidators(base.ValidatorProviders))
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(metadata, container))
						{
							propertiesValid = false;
							yield return new ModelValidationResult
							{
								MemberName = ModelBindingHelper.CreatePropertyModelName(propertyMetadata.PropertyName, modelValidationResult.MemberName),
								Message = modelValidationResult.Message
							};
						}
						IEnumerator<ModelValidationResult> enumerator3 = null;
					}
					IEnumerator<ModelValidator> enumerator2 = null;
					propertyMetadata = null;
				}
				IEnumerator<ModelMetadata> enumerator = null;
				if (propertiesValid)
				{
					foreach (ModelValidator modelValidator2 in metadata.GetValidators(base.ValidatorProviders))
					{
						foreach (ModelValidationResult modelValidationResult2 in modelValidator2.Validate(metadata, container))
						{
							yield return modelValidationResult2;
						}
						IEnumerator<ModelValidationResult> enumerator3 = null;
					}
					IEnumerator<ModelValidator> enumerator2 = null;
				}
				yield break;
				yield break;
			}
		}
	}
}
