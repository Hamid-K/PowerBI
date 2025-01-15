using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	public interface IImmutableDictionary<[Nullable(2)] TKey, [Nullable(2)] TValue> : IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000073 RID: 115
		IImmutableDictionary<TKey, TValue> Clear();

		// Token: 0x06000074 RID: 116
		IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

		// Token: 0x06000075 RID: 117
		IImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> pairs);

		// Token: 0x06000076 RID: 118
		IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

		// Token: 0x06000077 RID: 119
		IImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items);

		// Token: 0x06000078 RID: 120
		IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

		// Token: 0x06000079 RID: 121
		IImmutableDictionary<TKey, TValue> Remove(TKey key);

		// Token: 0x0600007A RID: 122
		bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> pair);

		// Token: 0x0600007B RID: 123
		bool TryGetKey(TKey equalKey, out TKey actualKey);
	}
}
