using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000157 RID: 343
	internal class MaxLengthAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<StructuralPropertyConfiguration>
	{
		// Token: 0x06000C5E RID: 3166 RVA: 0x0003065F File Offset: 0x0002E85F
		public MaxLengthAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(MaxLengthAttribute), false)
		{
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00030688 File Offset: 0x0002E888
		public override void Apply(StructuralPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			MaxLengthAttribute maxLengthAttribute = attribute as MaxLengthAttribute;
			LengthPropertyConfiguration lengthPropertyConfiguration = edmProperty as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration != null && maxLengthAttribute != null)
			{
				lengthPropertyConfiguration.MaxLength = new int?(maxLengthAttribute.Length);
			}
		}
	}
}
