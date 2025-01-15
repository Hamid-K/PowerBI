using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000011 RID: 17
	internal class EmptyIEnumerableCallSite : IServiceCallSite
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000035A2 File Offset: 0x000017A2
		internal object ServiceInstance { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000035AA File Offset: 0x000017AA
		internal Type ServiceType { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x000035B2 File Offset: 0x000017B2
		public EmptyIEnumerableCallSite(Type serviceType, object serviceInstance)
		{
			this.ServiceType = serviceType;
			this.ServiceInstance = serviceInstance;
		}
	}
}
