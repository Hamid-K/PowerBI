using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000175 RID: 373
	internal class UnsortableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x00031BE5 File Offset: 0x0002FDE5
		public UnsortableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(UnsortableAttribute), false)
		{
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00031BC6 File Offset: 0x0002FDC6
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				edmProperty.IsNotSortable();
			}
		}
	}
}
