using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000272 RID: 626
	public sealed class QuickAccessPool<Tkey, Tvalue> : IObjectPool<Tkey, Tvalue>
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x00039624 File Offset: 0x00037824
		public QuickAccessPool(int maxSize, PoolPolicy poolPolicy)
			: this(maxSize, poolPolicy, null)
		{
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003962F File Offset: 0x0003782F
		public QuickAccessPool(int maxSize, PoolPolicy poolPolicy, Action<Tkey, Tvalue> onRemoveObjectFromPool)
			: this(maxSize, poolPolicy, onRemoveObjectFromPool, EqualityComparer<Tkey>.Default)
		{
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00039640 File Offset: 0x00037840
		public QuickAccessPool(int maxSize, PoolPolicy poolPolicy, Action<Tkey, Tvalue> onRemoveObjectFromPool, IEqualityComparer<Tkey> comparator)
		{
			Ensure.ArgIsPositive((long)maxSize, "maxSize", 0);
			Ensure.SlowEnumIsDefined<PoolPolicy>(poolPolicy, "poolPolicy", 0);
			this.m_maxSize = maxSize;
			this.m_poolPolicy = poolPolicy;
			this.m_fastLookupDictionary = new Dictionary<Tkey, LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>>>(comparator);
			this.m_objectList = new LinkedList<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>();
			this.m_onRemoveObjectFromPool = onRemoveObjectFromPool;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003969C File Offset: 0x0003789C
		public void CheckIn(Tkey key, Tvalue obj)
		{
			if (this.m_objectList.Count == this.m_maxSize)
			{
				LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem> last = this.m_objectList.Last;
				if (this.m_fastLookupDictionary[last.Value.Key].Count > 1)
				{
					LinkedListNode<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> referenceToKeySpecificNode = last.Value.ReferenceToKeySpecificNode;
					referenceToKeySpecificNode.List.Remove(referenceToKeySpecificNode);
				}
				else
				{
					this.m_fastLookupDictionary.Remove(last.Value.Key);
				}
				this.m_objectList.Remove(last);
				if (this.m_onRemoveObjectFromPool != null)
				{
					AsyncInvoker.InvokeMethodAsynchronously<Tkey, Tvalue>(this.m_onRemoveObjectFromPool, last.Value.Key, last.Value.Value, WaitOrNot.DontWait, "Remove object from pool notification");
				}
			}
			PoolPolicy poolPolicy = this.m_poolPolicy;
			LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem> linkedListNode;
			if (poolPolicy != PoolPolicy.PreferMostRecentlyUsed)
			{
				if (poolPolicy != PoolPolicy.PreferLeastRecentlyUsed)
				{
					return;
				}
				linkedListNode = this.m_objectList.AddLast(new QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem(key, obj));
			}
			else
			{
				linkedListNode = this.m_objectList.AddFirst(new QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem(key, obj));
			}
			LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> linkedList;
			if (!this.m_fastLookupDictionary.TryGetValue(key, out linkedList))
			{
				linkedList = new LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>>();
				this.m_fastLookupDictionary.Add(key, linkedList);
			}
			poolPolicy = this.m_poolPolicy;
			LinkedListNode<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> linkedListNode2;
			if (poolPolicy != PoolPolicy.PreferMostRecentlyUsed)
			{
				if (poolPolicy != PoolPolicy.PreferLeastRecentlyUsed)
				{
					return;
				}
				linkedListNode2 = linkedList.AddLast(linkedListNode);
			}
			else
			{
				linkedListNode2 = linkedList.AddFirst(linkedListNode);
			}
			linkedListNode.Value.ReferenceToKeySpecificNode = linkedListNode2;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000397EC File Offset: 0x000379EC
		public void Remove(Tkey key)
		{
			LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> linkedList;
			if (this.m_fastLookupDictionary.TryGetValue(key, out linkedList))
			{
				foreach (LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem> linkedListNode in linkedList)
				{
					this.m_objectList.Remove(linkedListNode);
				}
				this.m_fastLookupDictionary.Remove(key);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003985C File Offset: 0x00037A5C
		public bool Contains(Tkey key)
		{
			return this.m_fastLookupDictionary.ContainsKey(key);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003986C File Offset: 0x00037A6C
		public void ClearOn(Predicate<Tkey> predicate)
		{
			foreach (Tkey tkey in this.m_fastLookupDictionary.Keys)
			{
				if (predicate(tkey))
				{
					this.Remove(tkey);
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000398D0 File Offset: 0x00037AD0
		public bool TryCheckOut(Tkey key, out Tvalue obj)
		{
			bool flag = false;
			obj = default(Tvalue);
			LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> linkedList;
			if (this.m_fastLookupDictionary.TryGetValue(key, out linkedList))
			{
				LinkedListNode<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> first = linkedList.First;
				LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem> value = first.Value;
				this.m_objectList.Remove(value);
				obj = value.Value.Value;
				if (linkedList.Count > 1)
				{
					linkedList.Remove(first);
				}
				else
				{
					this.m_fastLookupDictionary.Remove(key);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00039943 File Offset: 0x00037B43
		public bool TryTouch(Tkey key, out Tvalue obj)
		{
			if (this.TryCheckOut(key, out obj))
			{
				this.CheckIn(key, obj);
				return true;
			}
			return false;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0003995F File Offset: 0x00037B5F
		public IEnumerable<Tvalue> Values
		{
			get
			{
				foreach (QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem objectPoolItem in this.m_objectList)
				{
					yield return objectPoolItem.Value;
				}
				LinkedList<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>.Enumerator enumerator = default(LinkedList<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0003996F File Offset: 0x00037B6F
		public int Count
		{
			get
			{
				return this.m_objectList.Count;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003997C File Offset: 0x00037B7C
		public void Clear()
		{
			this.m_fastLookupDictionary.Clear();
			this.m_objectList.Clear();
		}

		// Token: 0x04000628 RID: 1576
		private LinkedList<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem> m_objectList;

		// Token: 0x04000629 RID: 1577
		private Dictionary<Tkey, LinkedList<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>>> m_fastLookupDictionary;

		// Token: 0x0400062A RID: 1578
		private int m_maxSize;

		// Token: 0x0400062B RID: 1579
		private PoolPolicy m_poolPolicy;

		// Token: 0x0400062C RID: 1580
		private readonly Action<Tkey, Tvalue> m_onRemoveObjectFromPool;

		// Token: 0x020006E0 RID: 1760
		private sealed class ObjectPoolItem
		{
			// Token: 0x06002EC0 RID: 11968 RVA: 0x000A2A24 File Offset: 0x000A0C24
			public ObjectPoolItem(Tkey key, Tvalue value)
			{
				this.m_key = key;
				this.m_value = value;
			}

			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000A2A3A File Offset: 0x000A0C3A
			public Tkey Key
			{
				get
				{
					return this.m_key;
				}
			}

			// Token: 0x1700073F RID: 1855
			// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000A2A42 File Offset: 0x000A0C42
			public Tvalue Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x17000740 RID: 1856
			// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000A2A4A File Offset: 0x000A0C4A
			// (set) Token: 0x06002EC4 RID: 11972 RVA: 0x000A2A52 File Offset: 0x000A0C52
			public LinkedListNode<LinkedListNode<QuickAccessPool<Tkey, Tvalue>.ObjectPoolItem>> ReferenceToKeySpecificNode { get; set; }

			// Token: 0x0400138B RID: 5003
			private readonly Tkey m_key;

			// Token: 0x0400138C RID: 5004
			private readonly Tvalue m_value;
		}
	}
}
