using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000173 RID: 371
	internal class NotNavigableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<NavigationPropertyConfiguration>
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x00031B57 File Offset: 0x0002FD57
		public NotNavigableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NotNavigableAttribute), false)
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00031B7F File Offset: 0x0002FD7F
		public override void Apply(NavigationPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				edmProperty.IsNotNavigable();
			}
		}
	}
}
