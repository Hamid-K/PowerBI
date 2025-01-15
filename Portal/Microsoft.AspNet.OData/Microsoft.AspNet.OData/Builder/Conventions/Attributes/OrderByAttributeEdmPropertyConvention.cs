using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015D RID: 349
	internal class OrderByAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00030A68 File Offset: 0x0002EC68
		public OrderByAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(OrderByAttribute), true)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00030A90 File Offset: 0x0002EC90
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				OrderByAttribute orderByAttribute = attribute as OrderByAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
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
