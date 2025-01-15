using System;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004F0 RID: 1264
	public interface ICommonHISTracing
	{
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002ACF RID: 10959
		long TracingOptions { get; }

		// Token: 0x06002AD0 RID: 10960
		void HISTraceEntry(string traceData);
	}
}
