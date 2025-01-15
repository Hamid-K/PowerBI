using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015C RID: 348
	internal class SelectAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x00030974 File Offset: 0x0002EB74
		public SelectAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(SelectAttribute), true)
		{
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003099C File Offset: 0x0002EB9C
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				SelectAttribute selectAttribute = attribute as SelectAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
				if (modelBoundQuerySettingsOrDefault.SelectConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.CopySelectConfigurations(selectAttribute.SelectConfigurations);
				}
				else
				{
					foreach (string text in selectAttribute.SelectConfigurations.Keys)
					{
						modelBoundQuerySettingsOrDefault.SelectConfigurations[text] = selectAttribute.SelectConfigurations[text];
					}
				}
				if (selectAttribute.SelectConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.DefaultSelectType = selectAttribute.DefaultSelectType;
				}
			}
		}
	}
}
