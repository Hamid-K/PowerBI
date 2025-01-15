using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016F RID: 367
	internal class TimestampAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x000319E3 File Offset: 0x0002FBE3
		public TimestampAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(TimestampAttribute), false)
		{
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00031A0C File Offset: 0x0002FC0C
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			EntityTypeConfiguration entityTypeConfiguration = structuralTypeConfiguration as EntityTypeConfiguration;
			if (entityTypeConfiguration != null)
			{
				PrimitivePropertyConfiguration[] propertiesWithTimestamp = TimestampAttributeEdmPropertyConvention.GetPropertiesWithTimestamp(entityTypeConfiguration);
				if (propertiesWithTimestamp.Length == 1)
				{
					propertiesWithTimestamp[0].IsConcurrencyToken();
				}
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00031A3C File Offset: 0x0002FC3C
		private static PrimitivePropertyConfiguration[] GetPropertiesWithTimestamp(EntityTypeConfiguration config)
		{
			return (from pc in config.ThisAndBaseTypes().SelectMany((StructuralTypeConfiguration p) => p.Properties).OfType<PrimitivePropertyConfiguration>()
				where pc.PropertyInfo.GetCustomAttributes(typeof(TimestampAttribute), true).Any<object>()
				select pc).ToArray<PrimitivePropertyConfiguration>();
		}
	}
}
