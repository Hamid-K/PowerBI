using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200206B RID: 8299
	[NullableContext(1)]
	public interface IImmutableDictionary<[Nullable(2)] TKey, [Nullable(2)] TValue> : IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x0601140C RID: 70668
		IImmutableDictionary<TKey, TValue> Clear();

		// Token: 0x0601140D RID: 70669
		IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

		// Token: 0x0601140E RID: 70670
		IImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs);

		// Token: 0x0601140F RID: 70671
		IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

		// Token: 0x06011410 RID: 70672
		IImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items);

		// Token: 0x06011411 RID: 70673
		IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

		// Token: 0x06011412 RID: 70674
		IImmutableDictionary<TKey, TValue> Remove(TKey key);

		// Token: 0x06011413 RID: 70675
		bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair);

		// Token: 0x06011414 RID: 70676
		bool TryGetKey(TKey equalKey, out TKey actualKey);
	}
}
