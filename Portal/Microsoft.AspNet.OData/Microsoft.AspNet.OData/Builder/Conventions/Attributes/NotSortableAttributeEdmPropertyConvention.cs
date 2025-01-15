using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000174 RID: 372
	internal class NotSortableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00031B9E File Offset: 0x0002FD9E
		public NotSortableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NotSortableAttribute), false)
		{
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00031BC6 File Offset: 0x0002FDC6
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
