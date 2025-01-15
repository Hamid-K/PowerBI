using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015E RID: 350
	internal class ExpandAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x00030B5C File Offset: 0x0002ED5C
		public ExpandAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ExpandAttribute), true)
		{
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00030B84 File Offset: 0x0002ED84
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				ExpandAttribute expandAttribute = attribute as ExpandAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
				if (modelBoundQuerySettingsOrDefault.ExpandConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.CopyExpandConfigurations(expandAttribute.ExpandConfigurations);
				}
				else
				{
					foreach (string text in expandAttribute.ExpandConfigurations.Keys)
					{
						modelBoundQuerySettingsOrDefault.ExpandConfigurations[text] = expandAttribute.ExpandConfigurations[text];
					}
				}
				if (expandAttribute.ExpandConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.DefaultExpandType = expandAttribute.DefaultExpandType;
					modelBoundQuerySettingsOrDefault.DefaultMaxDepth = expandAttribute.DefaultMaxDepth ?? 2;
				}
			}
		}
	}
}
