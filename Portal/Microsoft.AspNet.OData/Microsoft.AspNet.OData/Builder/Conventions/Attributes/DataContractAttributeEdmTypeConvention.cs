using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200017B RID: 379
	internal class DataContractAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00031F62 File Offset: 0x00030162
		public DataContractAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(DataContractAttribute), false)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00031F8C File Offset: 0x0003018C
		public override void Apply(StructuralTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (!edmTypeConfiguration.AddedExplicitly && model.ModelAliasingEnabled)
			{
				DataContractAttribute dataContractAttribute = attribute as DataContractAttribute;
				if (dataContractAttribute != null)
				{
					if (dataContractAttribute.Name != null)
					{
						edmTypeConfiguration.Name = dataContractAttribute.Name;
					}
					if (dataContractAttribute.Namespace != null)
					{
						edmTypeConfiguration.Namespace = dataContractAttribute.Namespace;
					}
				}
				edmTypeConfiguration.AddedExplicitly = false;
			}
			foreach (PropertyConfiguration propertyConfiguration in ((IEnumerable<PropertyConfiguration>)edmTypeConfiguration.Properties.ToArray<PropertyConfiguration>()))
			{
				if (!propertyConfiguration.PropertyInfo.GetCustomAttributes(typeof(DataMemberAttribute), true).Any<object>() && !propertyConfiguration.AddedExplicitly)
				{
					edmTypeConfiguration.RemoveProperty(propertyConfiguration.PropertyInfo);
				}
			}
		}
	}
}
