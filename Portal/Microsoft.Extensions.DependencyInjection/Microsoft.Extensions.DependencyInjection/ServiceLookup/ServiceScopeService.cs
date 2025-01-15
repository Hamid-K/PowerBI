using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001F RID: 31
	internal class ServiceScopeService : IService, IServiceCallSite
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003AB2 File Offset: 0x00001CB2
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003ABA File Offset: 0x00001CBA
		public IService Next { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003AC3 File Offset: 0x00001CC3
		public ServiceLifetime Lifetime
		{
			get
			{
				return ServiceLifetime.Scoped;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003AC6 File Offset: 0x00001CC6
		public Type ServiceType
		{
			get
			{
				return typeof(IServiceScopeFactory);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000360A File Offset: 0x0000180A
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			return this;
		}
	}
}
