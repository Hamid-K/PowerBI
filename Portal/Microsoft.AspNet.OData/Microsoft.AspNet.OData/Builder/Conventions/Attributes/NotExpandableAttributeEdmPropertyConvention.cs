using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000172 RID: 370
	internal class NotExpandableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<NavigationPropertyConfiguration>
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x00031B10 File Offset: 0x0002FD10
		public NotExpandableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NotExpandableAttribute), false)
		{
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00031B38 File Offset: 0x0002FD38
		public override void Apply(NavigationPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				edmProperty.IsNotExpandable();
			}
		}
	}
}
