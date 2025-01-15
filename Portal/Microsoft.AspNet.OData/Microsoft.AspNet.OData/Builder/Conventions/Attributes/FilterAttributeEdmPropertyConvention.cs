using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015B RID: 347
	internal class FilterAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x0003087F File Offset: 0x0002EA7F
		public FilterAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(FilterAttribute), true)
		{
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000308A8 File Offset: 0x0002EAA8
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				FilterAttribute filterAttribute = attribute as FilterAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
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
