using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000015 RID: 21
	internal class InstanceService : IService, IServiceCallSite
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003669 File Offset: 0x00001869
		internal ServiceDescriptor Descriptor { get; }

		// Token: 0x06000098 RID: 152 RVA: 0x00003671 File Offset: 0x00001871
		public InstanceService(ServiceDescriptor descriptor)
		{
			this.Descriptor = descriptor;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003680 File Offset: 0x00001880
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003688 File Offset: 0x00001888
		public IService Next { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003691 File Offset: 0x00001891
		public ServiceLifetime Lifetime
		{
			get
			{
				return this.Descriptor.Lifetime;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000369E File Offset: 0x0000189E
		public Type ServiceType
		{
			get
			{
				return this.Descriptor.ServiceType;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000360A File Offset: 0x0000180A
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			return this;
		}
	}
}
