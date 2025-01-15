using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000019 RID: 25
	internal class ScopedCallSite : IServiceCallSite
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000036F0 File Offset: 0x000018F0
		internal IService Key { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000036F8 File Offset: 0x000018F8
		internal IServiceCallSite ServiceCallSite { get; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00003700 File Offset: 0x00001900
		public ScopedCallSite(IService key, IServiceCallSite serviceCallSite)
		{
			this.Key = key;
			this.ServiceCallSite = serviceCallSite;
		}
	}
}
