using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000170 RID: 368
	internal class NonFilterableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x00031AA1 File Offset: 0x0002FCA1
		public NonFilterableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NonFilterableAttribute), false)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00031AC9 File Offset: 0x0002FCC9
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
