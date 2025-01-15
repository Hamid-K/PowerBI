using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000155 RID: 341
	internal abstract class EntityTypeConvention : IEdmTypeConvention, IConvention
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x00030640 File Offset: 0x0002E840
		public void Apply(IEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model)
		{
			EntityTypeConfiguration entityTypeConfiguration = edmTypeConfiguration as EntityTypeConfiguration;
			if (entityTypeConfiguration != null)
			{
				this.Apply(entityTypeConfiguration, model);
			}
		}

		// Token: 0x06000C5C RID: 3164
		public abstract void Apply(EntityTypeConfiguration entity, ODataConventionModelBuilder model);
	}
}
