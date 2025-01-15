using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DA RID: 730
	internal static class CachePolicyDelegates<TValue>
	{
		// Token: 0x020004E9 RID: 1257
		// (Invoke) Token: 0x060024AF RID: 9391
		internal delegate bool EvictionCallback(TValue item);
	}
}
