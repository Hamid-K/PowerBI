using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class KeysOrValuesCollectionAccessor<TKey, [Nullable(2)] TValue, [Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00008FF1 File Offset: 0x000071F1
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000901D File Offset: 0x0000721D
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00009020 File Offset: 0x00007220
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000902D File Offset: 0x0000722D
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009035 File Offset: 0x00007235
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000903C File Offset: 0x0000723C
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600035D RID: 861
		public abstract bool Contains(T item);

		// Token: 0x0600035E RID: 862 RVA: 0x00009044 File Offset: 0x00007244
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000090CC File Offset: 0x000072CC
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000090D3 File Offset: 0x000072D3
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000090E0 File Offset: 0x000072E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000090E8 File Offset: 0x000072E8
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, new int[] { arrayIndex++ });
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00009180 File Offset: 0x00007380
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00009183 File Offset: 0x00007383
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x0400003E RID: 62
		private readonly IEnumerable<T> _keysOrValues;
	}
}
