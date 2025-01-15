using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000169 RID: 361
	internal class MediaTypeAttributeConvention : AttributeEdmTypeConvention<EntityTypeConfiguration>
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x000314E8 File Offset: 0x0002F6E8
		public MediaTypeAttributeConvention()
			: base((Attribute attr) => attr.GetType() == typeof(MediaTypeAttribute), false)
		{
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00031510 File Offset: 0x0002F710
		public override void Apply(EntityTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			edmTypeConfiguration.MediaType();
		}
	}
}
