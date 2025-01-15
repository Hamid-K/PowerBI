using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000AF RID: 175
	public interface IFastHashHelper<TKey, TValue>
	{
		// Token: 0x06000768 RID: 1896
		bool IsDefault(TValue v);

		// Token: 0x06000769 RID: 1897
		bool Equals(TKey k1, TKey k2);

		// Token: 0x0600076A RID: 1898
		bool Equals(TValue v1, TValue v2);

		// Token: 0x0600076B RID: 1899
		int GetHashCode(TKey k);

		// Token: 0x0600076C RID: 1900
		TValue CompareExchange(ref TValue location1, TValue value, TValue comparand);

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600076D RID: 1901
		TValue DefaultValue { get; }
	}
}
