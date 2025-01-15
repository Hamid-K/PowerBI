using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000002 RID: 2
	internal class Bag<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Bag()
		{
			this.m_dictionary = new Dictionary<T, T>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		public Bag(IEnumerable<T> items)
			: this()
		{
			this.AddRange(items);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
		public Bag(IEnumerable<T> items, bool ignoreDuplicates)
			: this()
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002082 File Offset: 0x00000282
		public Bag(IEqualityComparer<T> comparer)
		{
			this.m_dictionary = new Dictionary<T, T>(comparer);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002096 File Offset: 0x00000296
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			this.AddRange(items);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A6 File Offset: 0x000002A6
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer, bool ignoreDuplicates)
			: this(comparer)
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B7 File Offset: 0x000002B7
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.m_dictionary.Keys).CopyTo(array, index);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020CB File Offset: 0x000002CB
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020CE File Offset: 0x000002CE
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.m_dictionary).SyncRoot;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020DB File Offset: 0x000002DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020F2 File Offset: 0x000002F2
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020FF File Offset: 0x000002FF
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002102 File Offset: 0x00000302
		public void Add(T item)
		{
			this.Add(item, false);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000210C File Offset: 0x0000030C
		public void Add(T item, bool overwriteExisting)
		{
			if (overwriteExisting)
			{
				this.m_dictionary[item] = item;
				return;
			}
			this.m_dictionary.Add(item, item);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000212C File Offset: 0x0000032C
		public void AddRange(IEnumerable<T> items)
		{
			this.AddRange(items, false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002138 File Offset: 0x00000338
		public void AddRange(IEnumerable<T> items, bool overwriteExisting)
		{
			foreach (T t in items)
			{
				this.Add(t, overwriteExisting);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002184 File Offset: 0x00000384
		public T Get(T item)
		{
			return this.m_dictionary[item];
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002192 File Offset: 0x00000392
		public bool TryGet(T item, out T itemInBag)
		{
			return this.m_dictionary.TryGetValue(item, out itemInBag);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021A1 File Offset: 0x000003A1
		public void Clear()
		{
			this.m_dictionary.Clear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021AE File Offset: 0x000003AE
		public bool Contains(T item)
		{
			return this.m_dictionary.ContainsKey(item);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021BC File Offset: 0x000003BC
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_dictionary.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool Remove(T item)
		{
			return this.m_dictionary.Remove(item);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021DE File Offset: 0x000003DE
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021F8 File Offset: 0x000003F8
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000001 RID: 1
		private readonly Dictionary<T, T> m_dictionary;
	}
}
