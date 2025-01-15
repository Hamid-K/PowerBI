using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020C5 RID: 8389
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal sealed class ValuesCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
	{
		// Token: 0x060119BF RID: 72127 RVA: 0x003C3549 File Offset: 0x003C1749
		internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary, dictionary.Values)
		{
		}

		// Token: 0x060119C0 RID: 72128 RVA: 0x003C3558 File Offset: 0x003C1758
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
