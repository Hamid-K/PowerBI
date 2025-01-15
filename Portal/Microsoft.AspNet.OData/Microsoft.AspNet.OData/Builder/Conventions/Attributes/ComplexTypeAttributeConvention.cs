using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016B RID: 363
	internal class ComplexTypeAttributeConvention : AttributeEdmTypeConvention<EntityTypeConfiguration>
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00031596 File Offset: 0x0002F796
		public ComplexTypeAttributeConvention()
			: base((Attribute attr) => attr.GetType() == typeof(ComplexTypeAttribute), false)
		{
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public override void Apply(EntityTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			if (!edmTypeConfiguration.AddedExplicitly)
			{
				foreach (PrimitivePropertyConfiguration primitivePropertyConfiguration in edmTypeConfiguration.Keys.ToArray<PrimitivePropertyConfiguration>())
				{
					edmTypeConfiguration.RemoveKey(primitivePropertyConfiguration);
				}
				foreach (EnumPropertyConfiguration enumPropertyConfiguration in edmTypeConfiguration.EnumKeys.ToArray<EnumPropertyConfiguration>())
				{
					edmTypeConfiguration.RemoveKey(enumPropertyConfiguration);
				}
			}
		}
	}
}
