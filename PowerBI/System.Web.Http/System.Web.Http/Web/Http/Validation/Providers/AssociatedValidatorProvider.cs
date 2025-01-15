using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x0200009D RID: 157
	public abstract class AssociatedValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000AFA4 File Offset: 0x000091A4
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return TypeDescriptorHelper.Get(type);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000AFAC File Offset: 0x000091AC
		public sealed override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			if (metadata.ContainerType != null && !string.IsNullOrEmpty(metadata.PropertyName))
			{
				return this.GetValidatorsForProperty(metadata, validatorProviders);
			}
			return this.GetValidatorsForType(metadata, validatorProviders);
		}

		// Token: 0x060003CD RID: 973
		protected abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes);

		// Token: 0x060003CE RID: 974 RVA: 0x0000B004 File Offset: 0x00009204
		private IEnumerable<ModelValidator> GetValidatorsForProperty(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			PropertyDescriptor propertyDescriptor = this.GetTypeDescriptor(metadata.ContainerType).GetProperties().Find(metadata.PropertyName, true);
			if (propertyDescriptor == null)
			{
				throw Error.Argument("metadata", SRResources.Common_PropertyNotFound, new object[] { metadata.ContainerType, metadata.PropertyName });
			}
			return this.GetValidators(metadata, validatorProviders, propertyDescriptor.Attributes.OfType<Attribute>());
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B06D File Offset: 0x0000926D
		private IEnumerable<ModelValidator> GetValidatorsForType(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			return this.GetValidators(metadata, validatorProviders, this.GetTypeDescriptor(metadata.ModelType).GetAttributes().Cast<Attribute>());
		}
	}
}
