using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016C RID: 364
	internal class ConcurrencyCheckAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0003162F File Offset: 0x0002F82F
		public ConcurrencyCheckAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ConcurrencyCheckAttribute), false)
		{
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00031658 File Offset: 0x0002F858
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			bool flag = structuralTypeConfiguration is EntityTypeConfiguration;
			PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty as PrimitivePropertyConfiguration;
			if (flag && primitivePropertyConfiguration != null)
			{
				primitivePropertyConfiguration.ConcurrencyToken = true;
			}
		}
	}
}
