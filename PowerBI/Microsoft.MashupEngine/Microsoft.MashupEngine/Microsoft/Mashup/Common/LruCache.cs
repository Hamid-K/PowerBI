using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BFE RID: 7166
	public sealed class LruCache<K, V> where K : IEquatable<K>
	{
		// Token: 0x0600B2E8 RID: 45800 RVA: 0x00246A84 File Offset: 0x00244C84
		public LruCache(int size, Action<K, V> removalHandler = null)
			: this(removalHandler)
		{
			LruCache<K, V> <>4__this = this;
			this.trimCondition = () => <>4__this.Count > size;
		}

		// Token: 0x0600B2E9 RID: 45801 RVA: 0x00246ABE File Offset: 0x00244CBE
		public LruCache(Func<bool> trimCondition, Action<K, V> removalHandler = null)
			: this(removalHandler)
		{
			this.trimCondition = trimCondition;
		}

		// Token: 0x0600B2EA RID: 45802 RVA: 0x00246ACE File Offset: 0x00244CCE
		private LruCache(Action<K, V> removalHandler = null)
		{
			this.map = new Dictionary<K, LinkedListNode<LruCache<K, V>.Entry>>();
			this.rank = new LinkedList<LruCache<K, V>.Entry>();
			this.removalHandler = removalHandler;
		}

		// Token: 0x17002CDD RID: 11485
		// (get) Token: 0x0600B2EB RID: 45803 RVA: 0x00246AF3 File Offset: 0x00244CF3
		public int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x17002CDE RID: 11486
		// (get) Token: 0x0600B2EC RID: 45804 RVA: 0x00246B00 File Offset: 0x00244D00
		public KeyValuePair<K, V>? Oldest
		{
			get
			{
				LinkedListNode<LruCache<K, V>.Entry> first = this.rank.First;
				if (first != null)
				{
					return new KeyValuePair<K, V>?(new KeyValuePair<K, V>(first.Value.key, first.Value.value));
				}
				return null;
			}
		}

		// Token: 0x17002CDF RID: 11487
		// (get) Token: 0x0600B2ED RID: 45805 RVA: 0x00246B48 File Offset: 0x00244D48
		public KeyValuePair<K, V>[] Contents
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.map.Count];
				int num = 0;
				foreach (KeyValuePair<K, LinkedListNode<LruCache<K, V>.Entry>> keyValuePair in this.map)
				{
					array[num++] = new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value.Value.value);
				}
				return array;
			}
		}

		// Token: 0x0600B2EE RID: 45806 RVA: 0x00246BD0 File Offset: 0x00244DD0
		public bool ContainsKey(K key)
		{
			return this.map.ContainsKey(key);
		}

		// Token: 0x0600B2EF RID: 45807 RVA: 0x00246BE0 File Offset: 0x00244DE0
		public bool TryGetValue(K key, out V value)
		{
			LinkedListNode<LruCache<K, V>.Entry> linkedListNode;
			if (this.map.TryGetValue(key, out linkedListNode))
			{
				this.rank.Remove(linkedListNode);
				this.rank.AddLast(linkedListNode);
				value = linkedListNode.Value.value;
				return true;
			}
			value = default(V);
			return false;
		}

		// Token: 0x0600B2F0 RID: 45808 RVA: 0x00246C30 File Offset: 0x00244E30
		public void Add(K key, V value)
		{
			LinkedListNode<LruCache<K, V>.Entry> linkedListNode = new LinkedListNode<LruCache<K, V>.Entry>(new LruCache<K, V>.Entry(key, value));
			this.map.Add(key, linkedListNode);
			this.rank.AddLast(linkedListNode);
			this.Trim();
		}

		// Token: 0x0600B2F1 RID: 45809 RVA: 0x00246C6C File Offset: 0x00244E6C
		public bool Remove(K key)
		{
			LinkedListNode<LruCache<K, V>.Entry> linkedListNode;
			if (this.map.TryGetValue(key, out linkedListNode))
			{
				this.map.Remove(key);
				this.rank.Remove(linkedListNode);
				if (this.removalHandler != null)
				{
					this.removalHandler(key, linkedListNode.Value.value);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600B2F2 RID: 45810 RVA: 0x00246CC4 File Offset: 0x00244EC4
		public void RemoveOldest()
		{
			LinkedListNode<LruCache<K, V>.Entry> first = this.rank.First;
			if (first != null)
			{
				this.Remove(first.Value.key);
			}
		}

		// Token: 0x0600B2F3 RID: 45811 RVA: 0x00246CF2 File Offset: 0x00244EF2
		public void Trim()
		{
			while (this.map.Count > 0 && this.trimCondition())
			{
				this.RemoveOldest();
			}
		}

		// Token: 0x0600B2F4 RID: 45812 RVA: 0x00246D18 File Offset: 0x00244F18
		public void Clear()
		{
			KeyValuePair<K, LinkedListNode<LruCache<K, V>.Entry>>[] array = EmptyArray<KeyValuePair<K, LinkedListNode<LruCache<K, V>.Entry>>>.Instance;
			if (this.removalHandler != null)
			{
				array = this.map.ToArray<KeyValuePair<K, LinkedListNode<LruCache<K, V>.Entry>>>();
			}
			this.map.Clear();
			this.rank.Clear();
			foreach (KeyValuePair<K, LinkedListNode<LruCache<K, V>.Entry>> keyValuePair in array)
			{
				this.removalHandler(keyValuePair.Key, keyValuePair.Value.Value.value);
			}
		}

		// Token: 0x04005B58 RID: 23384
		private readonly Func<bool> trimCondition;

		// Token: 0x04005B59 RID: 23385
		private readonly Action<K, V> removalHandler;

		// Token: 0x04005B5A RID: 23386
		private readonly Dictionary<K, LinkedListNode<LruCache<K, V>.Entry>> map;

		// Token: 0x04005B5B RID: 23387
		private readonly LinkedList<LruCache<K, V>.Entry> rank;

		// Token: 0x02001BFF RID: 7167
		private struct Entry
		{
			// Token: 0x0600B2F5 RID: 45813 RVA: 0x00246D90 File Offset: 0x00244F90
			public Entry(K key, V value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x04005B5C RID: 23388
			public readonly K key;

			// Token: 0x04005B5D RID: 23389
			public readonly V value;
		}
	}
}
