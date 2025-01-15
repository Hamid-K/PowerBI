using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal sealed class KeysCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00009186 File Offset: 0x00007386
		internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary, dictionary.Keys)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009195 File Offset: 0x00007395
		public override bool Contains(TKey item)
		{
			return base.Dictionary.ContainsKey(item);
		}
	}
}
