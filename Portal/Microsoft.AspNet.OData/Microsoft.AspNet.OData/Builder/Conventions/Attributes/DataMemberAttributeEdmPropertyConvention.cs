using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000176 RID: 374
	internal class DataMemberAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00031C0D File Offset: 0x0002FE0D
		public DataMemberAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(DataMemberAttribute), false)
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00031C38 File Offset: 0x0002FE38
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			bool flag = TypeHelper.AsMemberInfo(structuralTypeConfiguration.ClrType).GetCustomAttributes(typeof(DataContractAttribute), true).Any<object>();
			DataMemberAttribute dataMemberAttribute = attribute as DataMemberAttribute;
			if (flag && dataMemberAttribute != null && !edmProperty.AddedExplicitly)
			{
				if (model.ModelAliasingEnabled && !string.IsNullOrWhiteSpace(dataMemberAttribute.Name))
				{
					edmProperty.Name = dataMemberAttribute.Name;
				}
				StructuralPropertyConfiguration structuralPropertyConfiguration = edmProperty as StructuralPropertyConfiguration;
				if (structuralPropertyConfiguration != null)
				{
					structuralPropertyConfiguration.OptionalProperty = !dataMemberAttribute.IsRequired;
				}
				NavigationPropertyConfiguration navigationPropertyConfiguration = edmProperty as NavigationPropertyConfiguration;
				if (navigationPropertyConfiguration != null && navigationPropertyConfiguration.Multiplicity != EdmMultiplicity.Many)
				{
					if (dataMemberAttribute.IsRequired)
					{
						navigationPropertyConfiguration.Required();
						return;
					}
					navigationPropertyConfiguration.Optional();
				}
			}
		}
	}
}
