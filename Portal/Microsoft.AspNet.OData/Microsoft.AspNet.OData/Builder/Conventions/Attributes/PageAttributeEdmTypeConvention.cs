using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000163 RID: 355
	internal class PageAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C76 RID: 3190 RVA: 0x00031090 File Offset: 0x0002F290
		public PageAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(PageAttribute), false)
		{
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000310B8 File Offset: 0x0002F2B8
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
				PageAttribute pageAttribute = attribute as PageAttribute;
				ModelBoundQuerySettings modelBoundQuerySettingsOrDefault = edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
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
