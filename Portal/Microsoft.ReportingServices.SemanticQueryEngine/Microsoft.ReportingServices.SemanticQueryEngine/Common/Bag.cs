using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200007C RID: 124
	internal class Bag<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00015DC6 File Offset: 0x00013FC6
		public Bag()
		{
			this.m_dictionary = new Dictionary<T, T>();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015DD9 File Offset: 0x00013FD9
		public Bag(IEnumerable<T> items)
			: this()
		{
			this.AddRange(items);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00015DE8 File Offset: 0x00013FE8
		public Bag(IEnumerable<T> items, bool ignoreDuplicates)
			: this()
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public Bag(IEqualityComparer<T> comparer)
		{
			this.m_dictionary = new Dictionary<T, T>(comparer);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015E0C File Offset: 0x0001400C
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			this.AddRange(items);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015E1C File Offset: 0x0001401C
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer, bool ignoreDuplicates)
			: this(comparer)
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015E2D File Offset: 0x0001402D
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.m_dictionary.Keys).CopyTo(array, index);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00004555 File Offset: 0x00002755
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00015E41 File Offset: 0x00014041
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.m_dictionary).SyncRoot;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015E4E File Offset: 0x0001404E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00015E65 File Offset: 0x00014065
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00004555 File Offset: 0x00002755
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015E72 File Offset: 0x00014072
		public void Add(T item)
		{
			this.Add(item, false);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015E7C File Offset: 0x0001407C
		public void Add(T item, bool overwriteExisting)
		{
			if (overwriteExisting)
			{
				this.m_dictionary[item] = item;
				return;
			}
			this.m_dictionary.Add(item, item);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015E9C File Offset: 0x0001409C
		public void AddRange(IEnumerable<T> items)
		{
			this.AddRange(items, false);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015EA8 File Offset: 0x000140A8
		public void AddRange(IEnumerable<T> items, bool overwriteExisting)
		{
			foreach (T t in items)
			{
				this.Add(t, overwriteExisting);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015EF4 File Offset: 0x000140F4
		public T Get(T item)
		{
			return this.m_dictionary[item];
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015F02 File Offset: 0x00014102
		public bool TryGet(T item, out T itemInBag)
		{
			return this.m_dictionary.TryGetValue(item, out itemInBag);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015F11 File Offset: 0x00014111
		public void Clear()
		{
			this.m_dictionary.Clear();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015F1E File Offset: 0x0001411E
		public bool Contains(T item)
		{
			return this.m_dictionary.ContainsKey(item);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015F2C File Offset: 0x0001412C
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_dictionary.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015F40 File Offset: 0x00014140
		public bool Remove(T item)
		{
			return this.m_dictionary.Remove(item);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015E4E File Offset: 0x0001404E
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015F50 File Offset: 0x00014150
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0400020F RID: 527
		private readonly Dictionary<T, T> m_dictionary;
	}
}
