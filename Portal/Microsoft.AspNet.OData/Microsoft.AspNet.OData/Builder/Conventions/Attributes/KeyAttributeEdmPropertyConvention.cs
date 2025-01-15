using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200017E RID: 382
	internal class KeyAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<StructuralPropertyConfiguration>
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003218E File Offset: 0x0003038E
		public KeyAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(KeyAttribute), false)
		{
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000321B8 File Offset: 0x000303B8
		public override void Apply(StructuralPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (edmProperty.Kind == PropertyKind.Primitive || edmProperty.Kind == PropertyKind.Enum)
			{
				EntityTypeConfiguration entityTypeConfiguration = structuralTypeConfiguration as EntityTypeConfiguration;
				if (entityTypeConfiguration != null)
				{
					entityTypeConfiguration.HasKey(edmProperty.PropertyInfo);
				}
			}
		}
	}
}
