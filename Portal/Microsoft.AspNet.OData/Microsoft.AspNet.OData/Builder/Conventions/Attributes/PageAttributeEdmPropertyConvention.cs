using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000164 RID: 356
	internal class PageAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C78 RID: 3192 RVA: 0x00031141 File Offset: 0x0002F341
		public PageAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(PageAttribute), false)
		{
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0003116C File Offset: 0x0002F36C
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				PageAttribute pageAttribute = attribute as PageAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
				if (pageAttribute.MaxTop < 0)
				{
					modelBoundQuerySettingsOrDefault.MaxTop = null;
				}
				else
				{
					modelBoundQuerySettingsOrDefault.MaxTop = new int?(pageAttribute.MaxTop);
				}
				if (pageAttribute.PageSize > 0)
				{
					modelBoundQuerySettingsOrDefault.PageSize = new int?(pageAttribute.PageSize);
				}
			}
		}
	}
}
