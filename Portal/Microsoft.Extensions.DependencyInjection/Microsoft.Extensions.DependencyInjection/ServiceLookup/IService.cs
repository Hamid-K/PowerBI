using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000016 RID: 22
	internal interface IService
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158
		// (set) Token: 0x0600009F RID: 159
		IService Next { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A0 RID: 160
		ServiceLifetime Lifetime { get; }

		// Token: 0x060000A1 RID: 161
		IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A2 RID: 162
		Type ServiceType { get; }
	}
}
