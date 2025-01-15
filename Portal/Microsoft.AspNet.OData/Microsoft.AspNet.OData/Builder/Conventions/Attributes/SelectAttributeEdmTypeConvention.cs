using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000161 RID: 353
	internal class SelectAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00030E90 File Offset: 0x0002F090
		public SelectAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(SelectAttribute), true)
		{
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00030EB8 File Offset: 0x0002F0B8
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
				SelectAttribute selectAttribute = attribute as SelectAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
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
