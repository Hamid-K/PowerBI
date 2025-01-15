using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class ConcurrentFastDictionary<TKey, TValue> : ConcurrentFastDictionary<TKey, TValue, RefToRefFastHashHelper<TKey, TValue>> where TKey : class where TValue : class
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x000282EE File Offset: 0x000264EE
		public ConcurrentFastDictionary()
			: base(0.7f)
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000282FB File Offset: 0x000264FB
		public ConcurrentFastDictionary(float load)
			: base(load)
		{
		}
	}
}
