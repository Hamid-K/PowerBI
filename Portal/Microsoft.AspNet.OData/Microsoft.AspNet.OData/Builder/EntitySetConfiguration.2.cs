using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000131 RID: 305
	public class EntitySetConfiguration<TEntityType> : NavigationSourceConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0002ACFC File Offset: 0x00028EFC
		internal EntitySetConfiguration(ODataModelBuilder modelBuilder, string name)
			: base(modelBuilder, new EntitySetConfiguration(modelBuilder, typeof(TEntityType), name))
		{
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002AD16 File Offset: 0x00028F16
		internal EntitySetConfiguration(ODataModelBuilder modelBuilder, EntitySetConfiguration configuration)
			: base(modelBuilder, configuration)
		{
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002AD20 File Offset: 0x00028F20
		internal EntitySetConfiguration EntitySet
		{
			get
			{
				return (EntitySetConfiguration)base.Configuration;
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002AD30 File Offset: 0x00028F30
		public virtual void HasFeedSelfLink(Func<ResourceSetContext, string> feedSelfLinkFactory)
		{
			if (feedSelfLinkFactory == null)
			{
				throw Error.ArgumentNull("feedSelfLinkFactory");
			}
			this.EntitySet.HasFeedSelfLink((ResourceSetContext feedContext) => new Uri(feedSelfLinkFactory(feedContext)));
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002AD75 File Offset: 0x00028F75
		public virtual void HasFeedSelfLink(Func<ResourceSetContext, Uri> feedSelfLinkFactory)
		{
			if (feedSelfLinkFactory == null)
			{
				throw Error.ArgumentNull("feedSelfLinkFactory");
			}
			this.EntitySet.HasFeedSelfLink(feedSelfLinkFactory);
		}
	}
}
