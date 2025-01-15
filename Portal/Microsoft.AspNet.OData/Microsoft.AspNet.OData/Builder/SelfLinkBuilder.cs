using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000124 RID: 292
	public class SelfLinkBuilder<T>
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00029683 File Offset: 0x00027883
		public SelfLinkBuilder(Func<ResourceContext, T> linkFactory, bool followsConventions)
		{
			if (linkFactory == null)
			{
				throw Error.ArgumentNull("linkFactory");
			}
			this.Factory = linkFactory;
			this.FollowsConventions = followsConventions;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000296A7 File Offset: 0x000278A7
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000296AF File Offset: 0x000278AF
		public Func<ResourceContext, T> Factory { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000296B8 File Offset: 0x000278B8
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x000296C0 File Offset: 0x000278C0
		public bool FollowsConventions { get; private set; }
	}
}
