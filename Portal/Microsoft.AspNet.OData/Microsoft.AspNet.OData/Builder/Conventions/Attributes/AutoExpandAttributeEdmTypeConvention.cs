using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000165 RID: 357
	internal class AutoExpandAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x000311E7 File Offset: 0x0002F3E7
		public AutoExpandAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(AutoExpandAttribute), false)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00031210 File Offset: 0x0002F410
		public override void Apply(StructuralTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			StructuralTypeConfiguration structuralTypeConfiguration = edmTypeConfiguration as EntityTypeConfiguration;
			AutoExpandAttribute autoExpandAttribute = attribute as AutoExpandAttribute;
			foreach (NavigationPropertyConfiguration navigationPropertyConfiguration in structuralTypeConfiguration.NavigationProperties)
			{
				if (!navigationPropertyConfiguration.AddedExplicitly)
				{
					navigationPropertyConfiguration.AutomaticallyExpand(autoExpandAttribute.DisableWhenSelectPresent);
				}
			}
		}
	}
}
