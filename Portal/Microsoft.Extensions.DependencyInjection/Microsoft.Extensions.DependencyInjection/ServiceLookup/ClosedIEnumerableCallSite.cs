using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000C RID: 12
	internal class ClosedIEnumerableCallSite : IServiceCallSite
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000034AB File Offset: 0x000016AB
		internal Type ItemType { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000034B3 File Offset: 0x000016B3
		internal IServiceCallSite[] ServiceCallSites { get; }

		// Token: 0x0600007A RID: 122 RVA: 0x000034BB File Offset: 0x000016BB
		public ClosedIEnumerableCallSite(Type itemType, IServiceCallSite[] serviceCallSites)
		{
			this.ItemType = itemType;
			this.ServiceCallSites = serviceCallSites;
		}
	}
}
