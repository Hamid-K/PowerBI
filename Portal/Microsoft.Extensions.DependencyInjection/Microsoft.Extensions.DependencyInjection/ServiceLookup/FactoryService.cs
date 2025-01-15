using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000012 RID: 18
	internal class FactoryService : IService, IServiceCallSite
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000035C8 File Offset: 0x000017C8
		internal ServiceDescriptor Descriptor { get; }

		// Token: 0x0600008C RID: 140 RVA: 0x000035D0 File Offset: 0x000017D0
		public FactoryService(ServiceDescriptor descriptor)
		{
			this.Descriptor = descriptor;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000035DF File Offset: 0x000017DF
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000035E7 File Offset: 0x000017E7
		public IService Next { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000035F0 File Offset: 0x000017F0
		public ServiceLifetime Lifetime
		{
			get
			{
				return this.Descriptor.Lifetime;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000035FD File Offset: 0x000017FD
		public Type ServiceType
		{
			get
			{
				return this.Descriptor.ServiceType;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000360A File Offset: 0x0000180A
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			return this;
		}
	}
}
