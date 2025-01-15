using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000C RID: 12
	public struct ImmutableDictionaryBuilder<TKey, TValue> : IEnumerable
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002507 File Offset: 0x00000707
		public ImmutableDictionaryBuilder(int dummy)
		{
			this._builder = ImmutableDictionary.CreateBuilder<TKey, TValue>();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002514 File Offset: 0x00000714
		public ImmutableDictionaryBuilder(IEqualityComparer<TKey> keyComparer)
		{
			this._builder = ImmutableDictionary.CreateBuilder<TKey, TValue>(keyComparer);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002522 File Offset: 0x00000722
		public ImmutableDictionaryBuilder(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			this._builder = ImmutableDictionary.CreateBuilder<TKey, TValue>(keyComparer, valueComparer);
		}

		// Token: 0x17000009 RID: 9
		public TValue this[TKey key]
		{
			set
			{
				this._builder[key] = value;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002540 File Offset: 0x00000740
		public void Add(TKey key, TValue value)
		{
			this._builder.Add(key, value);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000254F File Offset: 0x0000074F
		public ImmutableDictionary<TKey, TValue> ToImmutable()
		{
			return this._builder.ToImmutable();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000033 RID: 51
		private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;
	}
}
