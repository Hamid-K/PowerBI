using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000166 RID: 358
	internal class AutoExpandAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<NavigationPropertyConfiguration>
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x00031294 File Offset: 0x0002F494
		public AutoExpandAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(AutoExpandAttribute), false)
		{
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000312BC File Offset: 0x0002F4BC
		public override void Apply(NavigationPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				AutoExpandAttribute autoExpandAttribute = attribute as AutoExpandAttribute;
				edmProperty.AutomaticallyExpand(autoExpandAttribute.DisableWhenSelectPresent);
			}
		}
	}
}
