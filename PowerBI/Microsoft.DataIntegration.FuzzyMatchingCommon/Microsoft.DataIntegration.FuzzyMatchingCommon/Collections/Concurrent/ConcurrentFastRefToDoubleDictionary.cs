using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class ConcurrentFastRefToDoubleDictionary<TKey> : ConcurrentFastDictionary<TKey, double, RefToDoubleFastHashHelper<TKey>> where TKey : class
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x000282D8 File Offset: 0x000264D8
		public ConcurrentFastRefToDoubleDictionary()
			: base(0.7f)
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000282E5 File Offset: 0x000264E5
		public ConcurrentFastRefToDoubleDictionary(float load)
			: base(load)
		{
		}
	}
}
