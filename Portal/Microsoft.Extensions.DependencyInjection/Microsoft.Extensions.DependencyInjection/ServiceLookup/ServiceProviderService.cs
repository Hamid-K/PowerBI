using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001C RID: 28
	internal class ServiceProviderService : IService, IServiceCallSite
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003A50 File Offset: 0x00001C50
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003A58 File Offset: 0x00001C58
		public IService Next { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000034F8 File Offset: 0x000016F8
		public ServiceLifetime Lifetime
		{
			get
			{
				return ServiceLifetime.Transient;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003A61 File Offset: 0x00001C61
		public Type ServiceType
		{
			get
			{
				return typeof(IServiceProvider);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000360A File Offset: 0x0000180A
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			return this;
		}
	}
}
