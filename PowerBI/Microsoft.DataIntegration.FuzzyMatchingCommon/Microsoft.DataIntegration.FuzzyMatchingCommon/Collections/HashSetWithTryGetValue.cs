using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	public sealed class HashSetWithTryGetValue<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0002162C File Offset: 0x0001F82C
		public HashSetWithTryGetValue()
		{
			this.m_dictionary = new Dictionary<T, T>();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002163F File Offset: 0x0001F83F
		public HashSetWithTryGetValue(IEqualityComparer<T> comparer)
		{
			this.m_dictionary = new Dictionary<T, T>(comparer);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00021653 File Offset: 0x0001F853
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0002165B File Offset: 0x0001F85B
		public Dictionary<T, T> Dictionary
		{
			get
			{
				return this.m_dictionary;
			}
			set
			{
				this.m_dictionary = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00021664 File Offset: 0x0001F864
		public int Count
		{
			get
			{
				return this.Dictionary.Count;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00021671 File Offset: 0x0001F871
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00021674 File Offset: 0x0001F874
		public void Add(T item)
		{
			this.Dictionary.Add(item, item);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00021683 File Offset: 0x0001F883
		public void Clear()
		{
			this.Dictionary.Clear();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00021690 File Offset: 0x0001F890
		public bool Contains(T item)
		{
			return this.Dictionary.ContainsKey(item);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002169E File Offset: 0x0001F89E
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.Dictionary.Values.CopyTo(array, arrayIndex);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000216B2 File Offset: 0x0001F8B2
		public bool Remove(T item)
		{
			return this.Dictionary.Remove(item);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000216C0 File Offset: 0x0001F8C0
		public IEnumerator<T> GetEnumerator()
		{
			return this.Dictionary.Values.GetEnumerator();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000216D7 File Offset: 0x0001F8D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Dictionary.Values.GetEnumerator();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000216F0 File Offset: 0x0001F8F0
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00021738 File Offset: 0x0001F938
		public bool TryGetValue(T key, out T value)
		{
			return this.m_dictionary.TryGetValue(key, ref value);
		}

		// Token: 0x04000118 RID: 280
		private Dictionary<T, T> m_dictionary;
	}
}
