using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000159 RID: 345
	internal class CountAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x00030762 File Offset: 0x0002E962
		public CountAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(CountAttribute), false)
		{
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0003078C File Offset: 0x0002E98C
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				if ((attribute as CountAttribute).Disabled)
				{
					edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = new bool?(false);
					return;
				}
				edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = new bool?(true);
			}
		}
	}
}
