using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015F RID: 351
	internal class ExpandAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00030C70 File Offset: 0x0002EE70
		public ExpandAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ExpandAttribute), true)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00030C98 File Offset: 0x0002EE98
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
			if (!edmTypeConfiguration.AddedExplicitly)
			{
				ExpandAttribute expandAttribute = attribute as ExpandAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
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
