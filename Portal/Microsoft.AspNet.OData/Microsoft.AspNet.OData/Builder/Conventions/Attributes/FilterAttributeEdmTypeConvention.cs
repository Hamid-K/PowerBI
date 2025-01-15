using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000160 RID: 352
	internal class FilterAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x00030D90 File Offset: 0x0002EF90
		public FilterAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(FilterAttribute), true)
		{
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00030DB8 File Offset: 0x0002EFB8
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
				FilterAttribute filterAttribute = attribute as FilterAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
				if (modelBoundQuerySettingsOrDefault.FilterConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.CopyFilterConfigurations(filterAttribute.FilterConfigurations);
				}
				else
				{
					foreach (string text in filterAttribute.FilterConfigurations.Keys)
					{
						modelBoundQuerySettingsOrDefault.FilterConfigurations[text] = filterAttribute.FilterConfigurations[text];
					}
				}
				if (filterAttribute.FilterConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.DefaultEnableFilter = filterAttribute.DefaultEnableFilter;
				}
			}
		}
	}
}
