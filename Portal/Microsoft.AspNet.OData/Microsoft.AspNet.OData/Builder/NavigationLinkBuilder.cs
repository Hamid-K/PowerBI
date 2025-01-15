using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000125 RID: 293
	public class NavigationLinkBuilder
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x000296C9 File Offset: 0x000278C9
		public NavigationLinkBuilder(Func<ResourceContext, IEdmNavigationProperty, Uri> navigationLinkFactory, bool followsConventions)
		{
			if (navigationLinkFactory == null)
			{
				throw Error.ArgumentNull("navigationLinkFactory");
			}
			this.Factory = navigationLinkFactory;
			this.FollowsConventions = followsConventions;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000296ED File Offset: 0x000278ED
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x000296F5 File Offset: 0x000278F5
		public Func<ResourceContext, IEdmNavigationProperty, Uri> Factory { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x000296FE File Offset: 0x000278FE
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00029706 File Offset: 0x00027906
		public bool FollowsConventions { get; private set; }
	}
}
