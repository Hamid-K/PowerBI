using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002066 RID: 8294
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryEnumerator<TKey, [Nullable(2)] TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060113F6 RID: 70646 RVA: 0x003B5FE4 File Offset: 0x003B41E4
		internal DictionaryEnumerator([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x17002E13 RID: 11795
		// (get) Token: 0x060113F7 RID: 70647 RVA: 0x003B6000 File Offset: 0x003B4200
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				object obj = keyValuePair.Key;
				keyValuePair = this._inner.Current;
				return new DictionaryEntry(obj, keyValuePair.Value);
			}
		}

		// Token: 0x17002E14 RID: 11796
		// (get) Token: 0x060113F8 RID: 70648 RVA: 0x003B6044 File Offset: 0x003B4244
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17002E15 RID: 11797
		// (get) Token: 0x060113F9 RID: 70649 RVA: 0x003B606C File Offset: 0x003B426C
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x17002E16 RID: 11798
		// (get) Token: 0x060113FA RID: 70650 RVA: 0x003B6091 File Offset: 0x003B4291
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x060113FB RID: 70651 RVA: 0x003B609E File Offset: 0x003B429E
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x060113FC RID: 70652 RVA: 0x003B60AB File Offset: 0x003B42AB
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x040068A3 RID: 26787
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
