using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200007A RID: 122
	internal interface IFastDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004F5 RID: 1269
		int Count { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004F6 RID: 1270
		TValue DefaultValue { get; }

		// Token: 0x060004F7 RID: 1271
		void Add(TKey k, TValue v);

		// Token: 0x060004F8 RID: 1272
		void Remove(TKey k);

		// Token: 0x060004F9 RID: 1273
		void Increment(TKey key, int increment = 1);

		// Token: 0x170000AC RID: 172
		TValue this[TKey k] { get; set; }

		// Token: 0x060004FC RID: 1276
		bool TryAdd(TKey k, TValue v);

		// Token: 0x060004FD RID: 1277
		bool TryGetValue(TKey k, out TValue v);

		// Token: 0x060004FE RID: 1278
		bool ContainsKey(TKey k);

		// Token: 0x060004FF RID: 1279
		void Clear();
	}
}
