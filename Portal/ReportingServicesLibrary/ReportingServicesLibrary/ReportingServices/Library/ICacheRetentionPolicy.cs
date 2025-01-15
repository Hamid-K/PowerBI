using System;
using System.Text;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D9 RID: 729
	internal interface ICacheRetentionPolicy<TKey, TValue>
	{
		// Token: 0x06001A11 RID: 6673
		bool Add(TKey key, TValue value);

		// Token: 0x06001A12 RID: 6674
		bool Remove(TKey key);

		// Token: 0x06001A13 RID: 6675
		void MarkAsUsed(TKey key);

		// Token: 0x06001A14 RID: 6676
		void PerformEviction(CachePolicyDelegates<TValue>.EvictionCallback callback, bool aggressivePurge);

		// Token: 0x06001A15 RID: 6677
		void Reset();

		// Token: 0x06001A16 RID: 6678
		void RetrieveTracingInfo(TKey key, StringBuilder targetBuilder);
	}
}
