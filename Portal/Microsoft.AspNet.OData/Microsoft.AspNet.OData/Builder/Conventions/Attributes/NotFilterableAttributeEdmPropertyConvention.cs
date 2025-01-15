using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000171 RID: 369
	internal class NotFilterableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		public NotFilterableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NotFilterableAttribute), false)
		{
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00031AC9 File Offset: 0x0002FCC9
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				edmProperty.IsNotFilterable();
			}
		}
	}
}
