using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000022 RID: 34
	internal class TransientCallSite : IServiceCallSite
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003E16 File Offset: 0x00002016
		internal IServiceCallSite Service { get; }

		// Token: 0x060000CF RID: 207 RVA: 0x00003E1E File Offset: 0x0000201E
		public TransientCallSite(IServiceCallSite service)
		{
			this.Service = service;
		}
	}
}
