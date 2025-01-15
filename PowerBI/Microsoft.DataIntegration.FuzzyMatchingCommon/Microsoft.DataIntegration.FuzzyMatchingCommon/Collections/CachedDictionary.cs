using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	internal sealed class CachedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IMemoryUsage, IMemoryLimit
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0001A8CC File Offset: 0x00018ACC
		public CachedDictionary(CachedDictionary<TKey, TValue>.ReplacementPolicyEnum policy, IEqualityComparer<TKey> comparer)
		{
			this.ItemCache = new Dictionary<TKey, CachedDictionary<TKey, TValue>.Node>(comparer);
			this.ReplacementPolicy = policy;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001A8FD File Offset: 0x00018AFD
		public CachedDictionary(CachedDictionary<TKey, TValue>.ReplacementPolicyEnum policy)
		{
			this.ItemCache = new Dictionary<TKey, CachedDictionary<TKey, TValue>.Node>();
			this.ReplacementPolicy = policy;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001A930 File Offset: 0x00018B30
		public CachedDictionary(CachedDictionary<TKey, TValue>.Func<TKey, TValue, long> getMemUsage, CachedDictionary<TKey, TValue>.ResolveCacheMissHandler resolveCacheMiss, CachedDictionary<TKey, TValue>.OnRemoveHandler onRemove)
		{
			if (getMemUsage == null || resolveCacheMiss == null)
			{
				throw new ArgumentException("GetMemoryUsage and ResolveCacheMiss delegates are required.");
			}
			this.GetMemoryUsage = getMemUsage;
			this.ResolveCacheMiss = resolveCacheMiss;
			this.OnRemove = onRemove;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001A97F File Offset: 0x00018B7F
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0001A987 File Offset: 0x00018B87
		public long MemoryUsage
		{
			get
			{
				return this.m_cbMemoryUsage;
			}
			set
			{
				this.m_cbMemoryUsage = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001A990 File Offset: 0x00018B90
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0001A998 File Offset: 0x00018B98
		public long MemoryLimit
		{
			get
			{
				return this.m_cbMemoryLimit;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentException("Memory limit must be >= 0.");
				}
				this.m_cbMemoryLimit = value;
				this.Compact(this.m_cbMemoryLimit);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001A9BD File Offset: 0x00018BBD
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0001A9C5 File Offset: 0x00018BC5
		public CachedDictionary<TKey, TValue>.ReplacementPolicyEnum ReplacementPolicy
		{
			get
			{
				return this.m_replacementPolicy;
			}
			set
			{
				if (CachedDictionary<TKey, TValue>.ReplacementPolicyEnum.MaxMemUsage == value && this.m_replacementPolicy != CachedDictionary<TKey, TValue>.ReplacementPolicyEnum.MaxMemUsage)
				{
					throw new InvalidOperationException("Switching replacement policy from LRU to MaxMemUsage is not supported.");
				}
				this.m_replacementPolicy = value;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001A9E6 File Offset: 0x00018BE6
		public void Compact(long cbTargetUsage)
		{
			if (this.MemoryUsage > cbTargetUsage)
			{
				this.FreeNodes = null;
			}
			while (this.MemoryUsage > cbTargetUsage && this.Last != null)
			{
				this.Remove(this.Last.Key);
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001AA20 File Offset: 0x00018C20
		public void CompactAboveAverageSize()
		{
			this.FreeNodes = null;
			long num = this.MemoryUsage / (long)this.Count;
			long num2 = (long)(0.5 * (double)this.MemoryUsage);
			CachedDictionary<TKey, TValue>.Node node = this.First;
			for (CachedDictionary<TKey, TValue>.Node node2 = node.Next; node2 != this.First; node2 = node.Next)
			{
				if (this.GetNodeMemoryUsage(node) >= num)
				{
					this.Remove(node.Key);
				}
				if (this.MemoryUsage < num2)
				{
					break;
				}
				node = node2;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001AA98 File Offset: 0x00018C98
		public void CompactLeastRecentlyUsed(int numItems)
		{
			this.FreeNodes = null;
			CachedDictionary<TKey, TValue>.Node node = this.Last;
			CachedDictionary<TKey, TValue>.Node node2 = this.Last.Prev;
			while (node2 != this.First && numItems-- > 0)
			{
				this.Remove(node.Key);
				node = node2;
				node2 = node.Prev;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001AAEC File Offset: 0x00018CEC
		private void Remove(CachedDictionary<TKey, TValue>.Node n)
		{
			if (n.PinCount == 0)
			{
				if (this.First == this.Last)
				{
					this.First = (this.Last = (n.Next = (n.Prev = null)));
					return;
				}
				n.Prev.Next = n.Next;
				n.Next.Prev = n.Prev;
				if (this.First == n)
				{
					this.First = n.Next;
				}
				if (this.Last == n)
				{
					this.Last = n.Prev;
				}
				n.Next = (n.Prev = null);
				return;
			}
			else
			{
				if (this.FirstPinned == this.LastPinned)
				{
					this.FirstPinned = (this.LastPinned = (n.Next = (n.Prev = null)));
					return;
				}
				n.Prev.Next = n.Next;
				n.Next.Prev = n.Prev;
				if (this.FirstPinned == n)
				{
					this.FirstPinned = n.Next;
				}
				if (this.LastPinned == n)
				{
					this.LastPinned = n.Prev;
				}
				n.Next = (n.Prev = null);
				return;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001AC20 File Offset: 0x00018E20
		private void Add(CachedDictionary<TKey, TValue>.Node n)
		{
			if (n.PinCount != 0)
			{
				if (this.FirstPinned == null)
				{
					n.Prev = n;
					n.Next = n;
					this.LastPinned = n;
					this.FirstPinned = n;
				}
				this.FirstPinned.Prev = n;
				this.LastPinned.Next = n;
				n.Prev = this.LastPinned;
				n.Next = this.FirstPinned;
				this.FirstPinned = n;
				return;
			}
			if (this.First == null)
			{
				n.Prev = n;
				n.Next = n;
				this.Last = n;
				this.First = n;
				return;
			}
			this.First.Prev = n;
			this.Last.Next = n;
			n.Prev = this.Last;
			n.Next = this.First;
			this.First = n;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001ACF9 File Offset: 0x00018EF9
		private long GetNodeMemoryUsage(CachedDictionary<TKey, TValue>.Node n)
		{
			return this.GetMemoryUsage(n.Key, n.Value) + CachedDictionary<TKey, TValue>.Node.MemoryUsage + 8L;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001AD1C File Offset: 0x00018F1C
		private CachedDictionary<TKey, TValue>.Node NewNode(TKey key, TValue value)
		{
			CachedDictionary<TKey, TValue>.Node node;
			if (this.FreeNodes != null)
			{
				node = this.FreeNodes;
				this.FreeNodes = this.FreeNodes.Next;
				node.Key = key;
				node.Value = value;
			}
			else
			{
				node = new CachedDictionary<TKey, TValue>.Node(key, value, null, null);
			}
			return node;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001AD66 File Offset: 0x00018F66
		private void FreeNode(CachedDictionary<TKey, TValue>.Node n)
		{
			n.Reset();
			n.Next = this.FreeNodes;
			this.FreeNodes = n;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001AD81 File Offset: 0x00018F81
		public void Add(TKey key, TValue value)
		{
			this.Add(key, value, false);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public void Add(TKey key, TValue value, bool pin)
		{
			CachedDictionary<TKey, TValue>.Node node = this.NewNode(key, value);
			long nodeMemoryUsage = this.GetNodeMemoryUsage(node);
			this.Compact(Math.Max(0L, this.MemoryLimit - nodeMemoryUsage));
			this.ItemCache.Add(key, node);
			if (pin)
			{
				node.PinCount = 1;
			}
			this.Add(node);
			this.MemoryUsage += nodeMemoryUsage;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public bool Remove(TKey key)
		{
			CachedDictionary<TKey, TValue>.Node node;
			if (this.ItemCache.TryGetValue(key, ref node))
			{
				this.ItemCache.Remove(key);
				this.Remove(node);
				this.MemoryUsage -= this.GetNodeMemoryUsage(node);
				if (this.OnRemove != null)
				{
					this.OnRemove(key, node.Value);
					this.FreeNode(node);
				}
				return true;
			}
			throw new ArgumentOutOfRangeException("Key " + key.ToString() + " could not be found.");
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001AE79 File Offset: 0x00019079
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.TryGetValue(key, out value, false);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001AE84 File Offset: 0x00019084
		public bool TryGetValue(TKey key, out TValue value, bool pin)
		{
			CachedDictionary<TKey, TValue>.Node node;
			if (this.ItemCache.TryGetValue(key, ref node))
			{
				this.TouchByMRU(node);
				value = node.Value;
				if (pin)
				{
					this.Pin(node);
				}
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001AEC8 File Offset: 0x000190C8
		public void Touch(TKey key)
		{
			CachedDictionary<TKey, TValue>.Node node;
			if (this.ItemCache.TryGetValue(key, ref node))
			{
				this.TouchByMRU(node);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001AEEC File Offset: 0x000190EC
		private void TouchByMRU(CachedDictionary<TKey, TValue>.Node n)
		{
			if (n.PinCount == 0 && n != this.First)
			{
				this.Remove(n);
				this.Add(n);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001AF0D File Offset: 0x0001910D
		private void Pin(TKey key)
		{
			if (this.ReplacementPolicy != CachedDictionary<TKey, TValue>.ReplacementPolicyEnum.LRU)
			{
				throw new InvalidOperationException("Can only Pin when ReplacementPolicy is LRU.");
			}
			this.Pin(this.ItemCache[key]);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001AF34 File Offset: 0x00019134
		private void Pin(CachedDictionary<TKey, TValue>.Node n)
		{
			if (n.PinCount == 0)
			{
				this.Remove(n);
				n.PinCount++;
				this.Add(n);
			}
			else
			{
				n.PinCount++;
			}
			this.PinnedCount++;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001AF84 File Offset: 0x00019184
		public void Unpin(TKey key)
		{
			if (this.ReplacementPolicy != CachedDictionary<TKey, TValue>.ReplacementPolicyEnum.LRU)
			{
				throw new InvalidOperationException("Can only Unpin when ReplacementPolicy is LRU.");
			}
			CachedDictionary<TKey, TValue>.Node node = this.ItemCache[key];
			if (1 == node.PinCount)
			{
				this.Remove(node);
				node.PinCount--;
				this.Add(node);
			}
			else
			{
				node.PinCount--;
			}
			this.PinnedCount--;
		}

		// Token: 0x17000096 RID: 150
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue = default(TValue);
				if (!this.TryGetValue(key, out tvalue))
				{
					tvalue = this.ResolveCacheMiss(key);
					this.Add(key, tvalue);
				}
				return tvalue;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001B034 File Offset: 0x00019234
		public bool ContainsKey(TKey key)
		{
			return this.ItemCache.ContainsKey(key);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001B042 File Offset: 0x00019242
		public ICollection<TKey> Keys
		{
			get
			{
				return this.ItemCache.Keys;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001B04F File Offset: 0x0001924F
		public ICollection<TValue> Values
		{
			get
			{
				return new CachedDictionary<TKey, TValue>.ValueCollection(this.ItemCache.Values);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001B061 File Offset: 0x00019261
		public int Count
		{
			get
			{
				return this.ItemCache.Count;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001B06E File Offset: 0x0001926E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001B074 File Offset: 0x00019274
		public void Clear()
		{
			this.ItemCache.Clear();
			this.First = (this.Last = null);
			this.FirstPinned = (this.LastPinned = null);
			this.m_cbMemoryUsage = 0L;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			bool flag = false;
			CachedDictionary<TKey, TValue>.Node node;
			if (this.ItemCache.TryGetValue(item.Key, ref node))
			{
				flag = (node.Value == null && item.Value == null) || node.Value.Equals(item.Value);
			}
			return flag;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001B118 File Offset: 0x00019318
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			foreach (KeyValuePair<TKey, CachedDictionary<TKey, TValue>.Node> keyValuePair in this.ItemCache)
			{
				if (arrayIndex >= array.Length)
				{
					break;
				}
				array[arrayIndex++] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value.Value);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001B190 File Offset: 0x00019390
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001B1A6 File Offset: 0x000193A6
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this.Contains(item))
			{
				this.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001B1C2 File Offset: 0x000193C2
		private static KeyValuePair<TKey, TValue> FormKVP(KeyValuePair<TKey, CachedDictionary<TKey, TValue>.Node> kvp)
		{
			return new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value.Value);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001B1DC File Offset: 0x000193DC
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new ConversionEnumerator<KeyValuePair<TKey, CachedDictionary<TKey, TValue>.Node>, KeyValuePair<TKey, TValue>>(this.ItemCache.GetEnumerator(), new Converter<KeyValuePair<TKey, CachedDictionary<TKey, TValue>.Node>, KeyValuePair<TKey, TValue>>(CachedDictionary<TKey, TValue>.FormKVP));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001B1FF File Offset: 0x000193FF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000A3 RID: 163
		public CachedDictionary<TKey, TValue>.Func<TKey, TValue, long> GetMemoryUsage;

		// Token: 0x040000A4 RID: 164
		public CachedDictionary<TKey, TValue>.ResolveCacheMissHandler ResolveCacheMiss;

		// Token: 0x040000A5 RID: 165
		public CachedDictionary<TKey, TValue>.OnRemoveHandler OnRemove;

		// Token: 0x040000A6 RID: 166
		private Dictionary<TKey, CachedDictionary<TKey, TValue>.Node> ItemCache;

		// Token: 0x040000A7 RID: 167
		private CachedDictionary<TKey, TValue>.Node First;

		// Token: 0x040000A8 RID: 168
		private CachedDictionary<TKey, TValue>.Node Last;

		// Token: 0x040000A9 RID: 169
		private CachedDictionary<TKey, TValue>.Node FirstPinned;

		// Token: 0x040000AA RID: 170
		private CachedDictionary<TKey, TValue>.Node LastPinned;

		// Token: 0x040000AB RID: 171
		private CachedDictionary<TKey, TValue>.ReplacementPolicyEnum m_replacementPolicy = CachedDictionary<TKey, TValue>.ReplacementPolicyEnum.MaxMemUsage;

		// Token: 0x040000AC RID: 172
		private int PinnedCount;

		// Token: 0x040000AD RID: 173
		private CachedDictionary<TKey, TValue>.Node FreeNodes;

		// Token: 0x040000AE RID: 174
		private long m_cbMemoryUsage;

		// Token: 0x040000AF RID: 175
		private long m_cbMemoryLimit = 1152921504606846975L;

		// Token: 0x020000EB RID: 235
		public enum ReplacementPolicyEnum
		{
			// Token: 0x04000250 RID: 592
			LRU,
			// Token: 0x04000251 RID: 593
			MaxMemUsage
		}

		// Token: 0x020000EC RID: 236
		// (Invoke) Token: 0x060008F5 RID: 2293
		public delegate Int64 Func<TKey, TValue, Int64>(TKey key, TValue value);

		// Token: 0x020000ED RID: 237
		// (Invoke) Token: 0x060008F9 RID: 2297
		public delegate TValue ResolveCacheMissHandler(TKey key);

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x060008FD RID: 2301
		public delegate void OnRemoveHandler(TKey key, TValue value);

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x06000901 RID: 2305
		private delegate void TouchHandler(CachedDictionary<TKey, TValue>.Node n);

		// Token: 0x020000F0 RID: 240
		[DebuggerDisplay("Key={Key} Value={Value} Next={Next} Prev={Prev} PinCount={PinCount}")]
		[Serializable]
		private sealed class Node
		{
			// Token: 0x06000904 RID: 2308 RVA: 0x0002CC1C File Offset: 0x0002AE1C
			public Node(TKey key, TValue value, CachedDictionary<TKey, TValue>.Node next, CachedDictionary<TKey, TValue>.Node prev)
			{
				this.Key = key;
				this.Value = value;
				this.Next = next;
				this.Prev = prev;
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000905 RID: 2309 RVA: 0x0002CC41 File Offset: 0x0002AE41
			public static long MemoryUsage
			{
				get
				{
					return 44L;
				}
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x0002CC48 File Offset: 0x0002AE48
			public void Reset()
			{
				this.Key = default(TKey);
				this.Value = default(TValue);
				this.Next = (this.Prev = null);
				this.PinCount = 0;
			}

			// Token: 0x04000252 RID: 594
			public TKey Key;

			// Token: 0x04000253 RID: 595
			public TValue Value;

			// Token: 0x04000254 RID: 596
			public CachedDictionary<TKey, TValue>.Node Next;

			// Token: 0x04000255 RID: 597
			public CachedDictionary<TKey, TValue>.Node Prev;

			// Token: 0x04000256 RID: 598
			public int PinCount;
		}

		// Token: 0x020000F1 RID: 241
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
		{
			// Token: 0x06000907 RID: 2311 RVA: 0x0002CC84 File Offset: 0x0002AE84
			public ValueCollection(ICollection<CachedDictionary<TKey, TValue>.Node> nodeCollection)
			{
				this.m_nodeCollection = nodeCollection;
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000908 RID: 2312 RVA: 0x0002CC93 File Offset: 0x0002AE93
			public int Count
			{
				get
				{
					return this.m_nodeCollection.Count;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0002CCA3 File Offset: 0x0002AEA3
			public void Add(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0002CCAF File Offset: 0x0002AEAF
			public void Clear()
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x0002CCBC File Offset: 0x0002AEBC
			public bool Contains(TValue item)
			{
				foreach (CachedDictionary<TKey, TValue>.Node node in this.m_nodeCollection)
				{
					if ((node.Value == null && item == null) || node.Value.Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x0002CD38 File Offset: 0x0002AF38
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				foreach (CachedDictionary<TKey, TValue>.Node node in this.m_nodeCollection)
				{
					if (arrayIndex >= array.Length)
					{
						break;
					}
					array[arrayIndex++] = node.Value;
				}
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x0002CD98 File Offset: 0x0002AF98
			public bool Remove(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x0002CDA4 File Offset: 0x0002AFA4
			public IEnumerator<TValue> GetEnumerator()
			{
				return new CachedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.m_nodeCollection.GetEnumerator());
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new CachedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.m_nodeCollection.GetEnumerator());
			}

			// Token: 0x04000257 RID: 599
			private ICollection<CachedDictionary<TKey, TValue>.Node> m_nodeCollection;

			// Token: 0x02000159 RID: 345
			[Serializable]
			public sealed class Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06000AB8 RID: 2744 RVA: 0x000300EE File Offset: 0x0002E2EE
				public Enumerator(IEnumerator<CachedDictionary<TKey, TValue>.Node> enumerator)
				{
					this.m_enum = enumerator;
				}

				// Token: 0x170001CE RID: 462
				// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000300FD File Offset: 0x0002E2FD
				public TValue Current
				{
					get
					{
						return this.m_enum.Current.Value;
					}
				}

				// Token: 0x170001CF RID: 463
				// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0003010F File Offset: 0x0002E30F
				object IEnumerator.Current
				{
					get
					{
						return this.m_enum.Current.Value;
					}
				}

				// Token: 0x06000ABB RID: 2747 RVA: 0x00030126 File Offset: 0x0002E326
				public bool MoveNext()
				{
					return this.m_enum.MoveNext();
				}

				// Token: 0x06000ABC RID: 2748 RVA: 0x00030133 File Offset: 0x0002E333
				public void Reset()
				{
					this.m_enum.Reset();
				}

				// Token: 0x06000ABD RID: 2749 RVA: 0x00030140 File Offset: 0x0002E340
				public void Dispose()
				{
					this.m_enum.Dispose();
				}

				// Token: 0x040003A2 RID: 930
				private IEnumerator<CachedDictionary<TKey, TValue>.Node> m_enum;
			}
		}
	}
}
