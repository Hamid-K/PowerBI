using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000130 RID: 304
	public class EntitySetConfiguration : NavigationSourceConfiguration
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x000281E4 File Offset: 0x000263E4
		public EntitySetConfiguration()
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000281EC File Offset: 0x000263EC
		public EntitySetConfiguration(ODataModelBuilder modelBuilder, Type entityClrType, string name)
			: base(modelBuilder, entityClrType, name)
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000281F7 File Offset: 0x000263F7
		public EntitySetConfiguration(ODataModelBuilder modelBuilder, EntityTypeConfiguration entityType, string name)
			: base(modelBuilder, entityType, name)
		{
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002ACEA File Offset: 0x00028EEA
		public virtual NavigationSourceConfiguration HasFeedSelfLink(Func<ResourceSetContext, Uri> feedSelfLinkFactory)
		{
			this._feedSelfLinkFactory = feedSelfLinkFactory;
			return this;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		public virtual Func<ResourceSetContext, Uri> GetFeedSelfLink()
		{
			return this._feedSelfLinkFactory;
		}

		// Token: 0x04000346 RID: 838
		private Func<ResourceSetContext, Uri> _feedSelfLinkFactory;
	}
}
