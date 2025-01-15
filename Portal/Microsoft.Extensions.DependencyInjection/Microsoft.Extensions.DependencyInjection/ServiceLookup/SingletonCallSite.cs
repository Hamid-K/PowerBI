using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000021 RID: 33
	internal class SingletonCallSite : ScopedCallSite
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003E0C File Offset: 0x0000200C
		public SingletonCallSite(IService key, IServiceCallSite serviceCallSite)
			: base(key, serviceCallSite)
		{
		}
	}
}
