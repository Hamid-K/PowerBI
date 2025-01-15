using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	internal sealed class HashMultiSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x000213F9 File Offset: 0x0001F5F9
		public HashMultiSet()
		{
			this.m_dictionary = new Dictionary<T, HashMultiSet<T>.Node>();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002140C File Offset: 0x0001F60C
		public HashMultiSet(IEqualityComparer<T> comparer)
		{
			this.m_dictionary = new Dictionary<T, HashMultiSet<T>.Node>(comparer);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00021420 File Offset: 0x0001F620
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00021428 File Offset: 0x0001F628
		private Dictionary<T, HashMultiSet<T>.Node> Dictionary
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00021431 File Offset: 0x0001F631
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this.m_dictionary.Comparer;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0002143E File Offset: 0x0001F63E
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00021446 File Offset: 0x0001F646
		public int Count
		{
			get
			{
				return this.m_count;
			}
			private set
			{
				this.m_count = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0002144F File Offset: 0x0001F64F
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00021454 File Offset: 0x0001F654
		public void Add(T item)
		{
			HashMultiSet<T>.Node node = null;
			if (this.Dictionary.TryGetValue(item, ref node))
			{
				this.Dictionary.Remove(item);
			}
			this.Dictionary.Add(item, new HashMultiSet<T>.Node(item, node));
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000214A3 File Offset: 0x0001F6A3
		public void Clear()
		{
			this.Dictionary.Clear();
			this.m_count = 0;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000214B7 File Offset: 0x0001F6B7
		public bool Contains(T item)
		{
			return this.Dictionary.ContainsKey(item);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public void CopyTo(T[] array, int arrayIndex)
		{
			foreach (HashMultiSet<T>.Node node in this.Dictionary.Values)
			{
				while (node != null && arrayIndex < array.Length)
				{
					array[arrayIndex++] = node.item;
					node = node.next;
				}
				if (arrayIndex >= array.Length)
				{
					break;
				}
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00021548 File Offset: 0x0001F748
		public bool Remove(T item)
		{
			HashMultiSet<T>.Node node = null;
			if (this.Dictionary.TryGetValue(item, ref node))
			{
				this.Dictionary.Remove(item);
				if (node.next != null)
				{
					this.Dictionary.Add(node.next.item, node.next);
				}
				int count = this.Count;
				this.Count = count - 1;
				return true;
			}
			return false;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000215AB File Offset: 0x0001F7AB
		public IEnumerator<T> GetEnumerator()
		{
			return new HashMultiSet<T>.Enumerator(this.Dictionary.Values.GetEnumerator());
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000215C7 File Offset: 0x0001F7C7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashMultiSet<T>.Enumerator(this.Dictionary.Values.GetEnumerator());
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000215E4 File Offset: 0x0001F7E4
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x04000116 RID: 278
		private Dictionary<T, HashMultiSet<T>.Node> m_dictionary;

		// Token: 0x04000117 RID: 279
		private int m_count;

		// Token: 0x0200012C RID: 300
		[Serializable]
		private sealed class Node
		{
			// Token: 0x060009F4 RID: 2548 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
			public Node(T _item, HashMultiSet<T>.Node _next)
			{
				this.item = _item;
				this.next = _next;
			}

			// Token: 0x04000307 RID: 775
			public T item;

			// Token: 0x04000308 RID: 776
			public HashMultiSet<T>.Node next;
		}

		// Token: 0x0200012D RID: 301
		[Serializable]
		private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060009F5 RID: 2549 RVA: 0x0002E60E File Offset: 0x0002C80E
			public Enumerator(IEnumerator<HashMultiSet<T>.Node> values)
			{
				this.m_values = values;
				this.m_current = values.Current ?? null;
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002E62E File Offset: 0x0002C82E
			public T Current
			{
				get
				{
					if (this.m_current == null)
					{
						throw new InvalidOperationException();
					}
					return this.m_current.item;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002E649 File Offset: 0x0002C849
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x0002E658 File Offset: 0x0002C858
			public bool MoveNext()
			{
				if (this.m_current != null && this.m_current.next != null)
				{
					this.m_current = this.m_current.next;
					return true;
				}
				if (this.m_values.MoveNext())
				{
					this.m_current = this.m_values.Current;
					return true;
				}
				return false;
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x0002E6AE File Offset: 0x0002C8AE
			public void Reset()
			{
				this.m_values.Reset();
				this.m_current = null;
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x0002E6C2 File Offset: 0x0002C8C2
			public void Dispose()
			{
				this.m_values.Dispose();
			}

			// Token: 0x04000309 RID: 777
			private IEnumerator<HashMultiSet<T>.Node> m_values;

			// Token: 0x0400030A RID: 778
			private HashMultiSet<T>.Node m_current;
		}
	}
}
