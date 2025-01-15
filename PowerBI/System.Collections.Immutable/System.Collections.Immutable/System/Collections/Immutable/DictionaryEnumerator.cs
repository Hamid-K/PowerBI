using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryEnumerator<TKey, [Nullable(2)] TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002D84 File Offset: 0x00000F84
		internal DictionaryEnumerator([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002DA0 File Offset: 0x00000FA0
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E0C File Offset: 0x0000100C
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E31 File Offset: 0x00001031
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E3E File Offset: 0x0000103E
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E4B File Offset: 0x0000104B
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x04000010 RID: 16
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
