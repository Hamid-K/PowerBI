using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000354 RID: 852
	internal class Bag<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06001C16 RID: 7190 RVA: 0x00071F12 File Offset: 0x00070112
		public Bag()
		{
			this.m_dictionary = new Dictionary<T, T>();
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00071F25 File Offset: 0x00070125
		public Bag(IEnumerable<T> items)
			: this()
		{
			this.AddRange(items);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00071F34 File Offset: 0x00070134
		public Bag(IEnumerable<T> items, bool ignoreDuplicates)
			: this()
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00071F44 File Offset: 0x00070144
		public Bag(IEqualityComparer<T> comparer)
		{
			this.m_dictionary = new Dictionary<T, T>(comparer);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00071F58 File Offset: 0x00070158
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			this.AddRange(items);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00071F68 File Offset: 0x00070168
		public Bag(IEnumerable<T> items, IEqualityComparer<T> comparer, bool ignoreDuplicates)
			: this(comparer)
		{
			this.AddRange(items, ignoreDuplicates);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00071F79 File Offset: 0x00070179
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.m_dictionary.Keys).CopyTo(array, index);
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00005BEF File Offset: 0x00003DEF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00071F8D File Offset: 0x0007018D
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.m_dictionary).SyncRoot;
			}
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00071F9A File Offset: 0x0007019A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00071FB1 File Offset: 0x000701B1
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00005BEF File Offset: 0x00003DEF
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00071FBE File Offset: 0x000701BE
		public void Add(T item)
		{
			this.Add(item, false);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00071FC8 File Offset: 0x000701C8
		public void Add(T item, bool overwriteExisting)
		{
			if (overwriteExisting)
			{
				this.m_dictionary[item] = item;
				return;
			}
			this.m_dictionary.Add(item, item);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00071FE8 File Offset: 0x000701E8
		public void AddRange(IEnumerable<T> items)
		{
			this.AddRange(items, false);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00071FF4 File Offset: 0x000701F4
		public void AddRange(IEnumerable<T> items, bool overwriteExisting)
		{
			foreach (T t in items)
			{
				this.Add(t, overwriteExisting);
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00072040 File Offset: 0x00070240
		public T Get(T item)
		{
			return this.m_dictionary[item];
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0007204E File Offset: 0x0007024E
		public bool TryGet(T item, out T itemInBag)
		{
			return this.m_dictionary.TryGetValue(item, out itemInBag);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0007205D File Offset: 0x0007025D
		public void Clear()
		{
			this.m_dictionary.Clear();
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0007206A File Offset: 0x0007026A
		public bool Contains(T item)
		{
			return this.m_dictionary.ContainsKey(item);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00072078 File Offset: 0x00070278
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_dictionary.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0007208C File Offset: 0x0007028C
		public bool Remove(T item)
		{
			return this.m_dictionary.Remove(item);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00071F9A File Offset: 0x0007019A
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007209C File Offset: 0x0007029C
		public T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000B9D RID: 2973
		private readonly Dictionary<T, T> m_dictionary;
	}
}
