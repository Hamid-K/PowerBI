using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal sealed class ValuesCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
	{
		// Token: 0x06000367 RID: 871 RVA: 0x000091A3 File Offset: 0x000073A3
		internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary, dictionary.Values)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000091B4 File Offset: 0x000073B4
		public override bool Contains(TValue item)
		{
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = base.Dictionary as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.ContainsValue(item);
			}
			IImmutableDictionaryInternal<TKey, TValue> immutableDictionaryInternal = base.Dictionary as IImmutableDictionaryInternal<TKey, TValue>;
			if (immutableDictionaryInternal != null)
			{
				return immutableDictionaryInternal.ContainsValue(item);
			}
			throw new NotSupportedException();
		}
	}
}
