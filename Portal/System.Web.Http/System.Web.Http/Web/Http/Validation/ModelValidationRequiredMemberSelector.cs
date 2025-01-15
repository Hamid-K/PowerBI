using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x02000094 RID: 148
	public sealed class ModelValidationRequiredMemberSelector : IRequiredMemberSelector
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000AC18 File Offset: 0x00008E18
		public ModelValidationRequiredMemberSelector(ModelMetadataProvider metadataProvider, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (metadataProvider == null)
			{
				throw Error.ArgumentNull("metadataProvider");
			}
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			this._metadataProvider = metadataProvider;
			this._validatorProviders = validatorProviders.ToList<ModelValidatorProvider>();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AC50 File Offset: 0x00008E50
		public bool IsRequiredMember(MemberInfo member)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			if (this._validatorProviders == null || !this._validatorProviders.Any<ModelValidatorProvider>())
			{
				return false;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null || propertyInfo.GetGetMethod() == null)
			{
				return false;
			}
			ModelMetadata metadataForProperty = this._metadataProvider.GetMetadataForProperty(() => null, member.DeclaringType, member.Name);
			if (metadataForProperty.ModelType.IsNullable())
			{
				return false;
			}
			return metadataForProperty.GetValidators(this._validatorProviders).Any((ModelValidator validator) => validator.IsRequired);
		}

		// Token: 0x040000D7 RID: 215
		private readonly ModelMetadataProvider _metadataProvider;

		// Token: 0x040000D8 RID: 216
		private readonly List<ModelValidatorProvider> _validatorProviders;
	}
}
