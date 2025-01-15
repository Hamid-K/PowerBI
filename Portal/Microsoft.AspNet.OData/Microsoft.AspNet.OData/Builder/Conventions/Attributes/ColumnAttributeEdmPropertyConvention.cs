using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000167 RID: 359
	internal class ColumnAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x000312F3 File Offset: 0x0002F4F3
		public ColumnAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ColumnAttribute), false)
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0003131C File Offset: 0x0002F51C
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (edmProperty.AddedExplicitly)
			{
				return;
			}
			ColumnAttribute columnAttribute = attribute as ColumnAttribute;
			if (columnAttribute != null && columnAttribute.Order > 0)
			{
				edmProperty.Order = columnAttribute.Order;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty as PrimitivePropertyConfiguration;
			if (primitivePropertyConfiguration == null)
			{
				return;
			}
			if (columnAttribute == null || columnAttribute.TypeName == null)
			{
				return;
			}
			string typeName = columnAttribute.TypeName;
			if (string.Compare(typeName, "date", StringComparison.OrdinalIgnoreCase) == 0)
			{
				primitivePropertyConfiguration.AsDate();
				return;
			}
			if (string.Compare(typeName, "time", StringComparison.OrdinalIgnoreCase) == 0)
			{
				primitivePropertyConfiguration.AsTimeOfDay();
			}
		}
	}
}
