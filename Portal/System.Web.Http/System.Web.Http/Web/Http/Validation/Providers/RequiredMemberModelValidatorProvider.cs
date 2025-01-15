using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Metadata;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x020000A3 RID: 163
	public class RequiredMemberModelValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000B68C File Offset: 0x0000988C
		public RequiredMemberModelValidatorProvider(IRequiredMemberSelector requiredMemberSelector)
		{
			this._requiredMemberSelector = requiredMemberSelector;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B69C File Offset: 0x0000989C
		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			string propertyName = metadata.PropertyName;
			if (propertyName != null)
			{
				PropertyInfo property = metadata.ContainerType.GetProperty(propertyName);
				if (this._requiredMemberSelector.IsRequiredMember(property))
				{
					return new ModelValidator[]
					{
						new RequiredMemberModelValidator(validatorProviders)
					};
				}
			}
			return Enumerable.Empty<ModelValidator>();
		}

		// Token: 0x040000E5 RID: 229
		private IRequiredMemberSelector _requiredMemberSelector;
	}
}
