using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020C3 RID: 8387
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class KeysOrValuesCollectionAccessor<TKey, [Nullable(2)] TValue, [Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x060119AF RID: 72111 RVA: 0x003C33B9 File Offset: 0x003C15B9
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x17002F3B RID: 12091
		// (get) Token: 0x060119B0 RID: 72112 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002F3C RID: 12092
		// (get) Token: 0x060119B1 RID: 72113 RVA: 0x003C33E5 File Offset: 0x003C15E5
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x17002F3D RID: 12093
		// (get) Token: 0x060119B2 RID: 72114 RVA: 0x003C33F2 File Offset: 0x003C15F2
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x060119B3 RID: 72115 RVA: 0x00002C72 File Offset: 0x00000E72
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060119B4 RID: 72116 RVA: 0x00002C72 File Offset: 0x00000E72
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060119B5 RID: 72117
		public abstract bool Contains(T item);

		// Token: 0x060119B6 RID: 72118 RVA: 0x003C33FC File Offset: 0x003C15FC
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

		// Token: 0x060119B7 RID: 72119 RVA: 0x00002C72 File Offset: 0x00000E72
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060119B8 RID: 72120 RVA: 0x003C3484 File Offset: 0x003C1684
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x060119B9 RID: 72121 RVA: 0x003C3491 File Offset: 0x003C1691
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060119BA RID: 72122 RVA: 0x003C349C File Offset: 0x003C169C
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, arrayIndex++);
			}
		}

		// Token: 0x17002F3E RID: 12094
		// (get) Token: 0x060119BB RID: 72123 RVA: 0x0000A5FD File Offset: 0x000087FD
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002F3F RID: 12095
		// (get) Token: 0x060119BC RID: 72124 RVA: 0x00004FAE File Offset: 0x000031AE
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04006997 RID: 27031
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x04006998 RID: 27032
		private readonly IEnumerable<T> _keysOrValues;
	}
}
