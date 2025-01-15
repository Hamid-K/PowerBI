using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000014 RID: 20
	internal interface IGenericService
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149
		ServiceLifetime Lifetime { get; }

		// Token: 0x06000096 RID: 150
		IService GetService(Type closedServiceType);
	}
}
