using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x020000A1 RID: 161
	public class DataMemberModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000B5D4 File Offset: 0x000097D4
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			if (metadata.ContainerType == null || string.IsNullOrEmpty(metadata.PropertyName))
			{
				return Enumerable.Empty<ModelValidator>();
			}
			if (DataMemberModelValidatorProvider.IsRequiredDataMember(metadata.ContainerType, attributes))
			{
				return new RequiredMemberModelValidator[]
				{
					new RequiredMemberModelValidator(validatorProviders)
				};
			}
			return Enumerable.Empty<ModelValidator>();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000B628 File Offset: 0x00009828
		internal static bool IsRequiredDataMember(Type containerType, IEnumerable<Attribute> attributes)
		{
			DataMemberAttribute dataMemberAttribute = attributes.OfType<DataMemberAttribute>().FirstOrDefault<DataMemberAttribute>();
			return dataMemberAttribute != null && TypeDescriptorHelper.Get(containerType).GetAttributes().OfType<DataContractAttribute>()
				.Any<DataContractAttribute>() && dataMemberAttribute.IsRequired;
		}
	}
}
