using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class ConcurrentFastIntToRefDictionary<TValue> : ConcurrentFastDictionary<int, TValue, FastHashHelper<int, TValue, Int32EqualityComparer, RefEqualityComparer<TValue>, Interlocked<TValue>>> where TValue : class
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x000282C2 File Offset: 0x000264C2
		public ConcurrentFastIntToRefDictionary()
			: base(0.7f)
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000282CF File Offset: 0x000264CF
		public ConcurrentFastIntToRefDictionary(float load)
			: base(load)
		{
		}
	}
}
