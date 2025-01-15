using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000162 RID: 354
	internal class OrderByAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x00030F90 File Offset: 0x0002F190
		public OrderByAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(OrderByAttribute), true)
		{
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00030FB8 File Offset: 0x0002F1B8
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
				OrderByAttribute orderByAttribute = attribute as OrderByAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
				if (modelBoundQuerySettingsOrDefault.OrderByConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.CopyOrderByConfigurations(orderByAttribute.OrderByConfigurations);
				}
				else
				{
					foreach (string text in orderByAttribute.OrderByConfigurations.Keys)
					{
						modelBoundQuerySettingsOrDefault.OrderByConfigurations[text] = orderByAttribute.OrderByConfigurations[text];
					}
				}
				if (orderByAttribute.OrderByConfigurations.Count == 0)
				{
					modelBoundQuerySettingsOrDefault.DefaultEnableOrderBy = orderByAttribute.DefaultEnableOrderBy;
				}
			}
		}
	}
}
