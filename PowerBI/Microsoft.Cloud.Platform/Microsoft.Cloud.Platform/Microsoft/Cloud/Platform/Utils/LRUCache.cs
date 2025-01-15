using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A7 RID: 423
	public sealed class LRUCache<TKey, TValue> : IEnumerable<LRUCache<TKey, TValue>.KeyValue>, IEnumerable
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000259A5 File Offset: 0x00023BA5
		public LRUCache(int maxSize)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxSize, "maxSize");
			this.m_list = new DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>(maxSize);
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000259CF File Offset: 0x00023BCF
		public int Size
		{
			get
			{
				return this.m_list.Length;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000259DC File Offset: 0x00023BDC
		public bool TryGet(TKey key, out TValue value)
		{
			DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node node;
			if (this.m_cache.TryGetValue(key, out node))
			{
				this.m_list.MoveToHead(node);
				value = node.Value.Value;
				return true;
			}
			value = LRUCache<TKey, TValue>.s_defaultValue;
			return false;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00025A24 File Offset: 0x00023C24
		public bool TryUpdateValue(TKey key, Func<TValue, TValue> valueUpdater, out TValue updatedValue)
		{
			DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node node;
			if (this.m_cache.TryGetValue(key, out node))
			{
				node.Value.Value = valueUpdater(node.Value.Value);
				this.m_list.MoveToHead(node);
				updatedValue = node.Value.Value;
				return true;
			}
			updatedValue = LRUCache<TKey, TValue>.s_defaultValue;
			return false;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00025A88 File Offset: 0x00023C88
		public TValue Set(TKey key, TValue value)
		{
			DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node node;
			if (this.m_cache.TryGetValue(key, out node))
			{
				node.Value.Value = value;
				this.m_list.MoveToHead(node);
				return LRUCache<TKey, TValue>.s_defaultValue;
			}
			node = new DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node(new LRUCache<TKey, TValue>.KeyValue(key, value));
			this.m_cache[key] = node;
			DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node node2 = this.m_list.AddToHead(node);
			if (node2 != null)
			{
				this.m_cache.Remove(node2.Value.Key);
				return node2.Value.Value;
			}
			return LRUCache<TKey, TValue>.s_defaultValue;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00025B18 File Offset: 0x00023D18
		public bool TryRemove(TKey key, out TValue removedValue)
		{
			DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node node;
			if (!this.m_cache.TryGetValue(key, out node))
			{
				removedValue = LRUCache<TKey, TValue>.s_defaultValue;
				return false;
			}
			removedValue = node.Value.Value;
			this.m_list.Remove(node);
			this.m_cache.Remove(key);
			return true;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00025B6D File Offset: 0x00023D6D
		public IEnumerator<LRUCache<TKey, TValue>.KeyValue> GetEnumerator()
		{
			foreach (LRUCache<TKey, TValue>.KeyValue keyValue in this.m_list)
			{
				yield return keyValue;
			}
			IEnumerator<LRUCache<TKey, TValue>.KeyValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00025B7C File Offset: 0x00023D7C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400044A RID: 1098
		private static readonly TValue s_defaultValue;

		// Token: 0x0400044B RID: 1099
		private readonly Dictionary<TKey, DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node> m_cache = new Dictionary<TKey, DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue>.Node>();

		// Token: 0x0400044C RID: 1100
		private readonly DoublyLinkedList<LRUCache<TKey, TValue>.KeyValue> m_list;

		// Token: 0x0200066E RID: 1646
		public sealed class KeyValue
		{
			// Token: 0x06002D7D RID: 11645 RVA: 0x000A0CE3 File Offset: 0x0009EEE3
			public KeyValue(TKey key, TValue value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000A0CF9 File Offset: 0x0009EEF9
			// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000A0D01 File Offset: 0x0009EF01
			public TKey Key { get; set; }

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000A0D0A File Offset: 0x0009EF0A
			// (set) Token: 0x06002D81 RID: 11649 RVA: 0x000A0D12 File Offset: 0x0009EF12
			public TValue Value { get; set; }
		}
	}
}
