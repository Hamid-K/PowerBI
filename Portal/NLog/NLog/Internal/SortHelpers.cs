using System;
using System.Collections;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x02000141 RID: 321
	internal static class SortHelpers
	{
		// Token: 0x06000FA3 RID: 4003 RVA: 0x00027D54 File Offset: 0x00025F54
		public static Dictionary<TKey, List<TValue>> BucketSort<TValue, TKey>(this IEnumerable<TValue> inputs, SortHelpers.KeySelector<TValue, TKey> keySelector)
		{
			Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();
			foreach (TValue tvalue in inputs)
			{
				TKey tkey = keySelector(tvalue);
				List<TValue> list;
				if (!dictionary.TryGetValue(tkey, out list))
				{
					list = new List<TValue>();
					dictionary.Add(tkey, list);
				}
				list.Add(tvalue);
			}
			return dictionary;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00027DC8 File Offset: 0x00025FC8
		public static SortHelpers.ReadOnlySingleBucketDictionary<TKey, IList<TValue>> BucketSort<TValue, TKey>(this IList<TValue> inputs, SortHelpers.KeySelector<TValue, TKey> keySelector)
		{
			return inputs.BucketSort(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00027DD8 File Offset: 0x00025FD8
		public static SortHelpers.ReadOnlySingleBucketDictionary<TKey, IList<TValue>> BucketSort<TValue, TKey>(this IList<TValue> inputs, SortHelpers.KeySelector<TValue, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			Dictionary<TKey, IList<TValue>> dictionary = null;
			bool flag = false;
			TKey tkey = default(TKey);
			for (int i = 0; i < inputs.Count; i++)
			{
				TKey tkey2 = keySelector(inputs[i]);
				if (!flag)
				{
					flag = true;
					tkey = tkey2;
				}
				else if (dictionary == null)
				{
					if (!keyComparer.Equals(tkey, tkey2))
					{
						dictionary = SortHelpers.CreateBucketDictionaryWithValue<TValue, TKey>(inputs, keyComparer, i, tkey, tkey2);
					}
				}
				else
				{
					IList<TValue> list;
					if (!dictionary.TryGetValue(tkey2, out list))
					{
						list = new List<TValue>();
						dictionary.Add(tkey2, list);
					}
					list.Add(inputs[i]);
				}
			}
			if (dictionary != null)
			{
				return new SortHelpers.ReadOnlySingleBucketDictionary<TKey, IList<TValue>>(dictionary, keyComparer);
			}
			return new SortHelpers.ReadOnlySingleBucketDictionary<TKey, IList<TValue>>(new KeyValuePair<TKey, IList<TValue>>(tkey, inputs), keyComparer);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00027E7C File Offset: 0x0002607C
		private static Dictionary<TKey, IList<TValue>> CreateBucketDictionaryWithValue<TValue, TKey>(IList<TValue> inputs, IEqualityComparer<TKey> keyComparer, int currentIndex, TKey singleBucketKey, TKey keyValue)
		{
			Dictionary<TKey, IList<TValue>> dictionary = new Dictionary<TKey, IList<TValue>>(keyComparer);
			List<TValue> list = new List<TValue>(currentIndex);
			for (int i = 0; i < currentIndex; i++)
			{
				list.Add(inputs[i]);
			}
			dictionary[singleBucketKey] = list;
			list = new List<TValue> { inputs[currentIndex] };
			dictionary[keyValue] = list;
			return dictionary;
		}

		// Token: 0x02000280 RID: 640
		// (Invoke) Token: 0x06001670 RID: 5744
		internal delegate TKey KeySelector<in TValue, out TKey>(TValue value);

		// Token: 0x02000281 RID: 641
		public struct ReadOnlySingleBucketDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x0003ABAB File Offset: 0x00038DAB
			public IEqualityComparer<TKey> Comparer
			{
				get
				{
					return this._comparer;
				}
			}

			// Token: 0x06001674 RID: 5748 RVA: 0x0003ABB3 File Offset: 0x00038DB3
			public ReadOnlySingleBucketDictionary(KeyValuePair<TKey, TValue> singleBucket)
			{
				this = new SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>(singleBucket, EqualityComparer<TKey>.Default);
			}

			// Token: 0x06001675 RID: 5749 RVA: 0x0003ABC1 File Offset: 0x00038DC1
			public ReadOnlySingleBucketDictionary(Dictionary<TKey, TValue> multiBucket)
			{
				this = new SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>(multiBucket, EqualityComparer<TKey>.Default);
			}

			// Token: 0x06001676 RID: 5750 RVA: 0x0003ABCF File Offset: 0x00038DCF
			public ReadOnlySingleBucketDictionary(KeyValuePair<TKey, TValue> singleBucket, IEqualityComparer<TKey> comparer)
			{
				this._comparer = comparer;
				this._multiBucket = null;
				this._singleBucket = new KeyValuePair<TKey, TValue>?(singleBucket);
			}

			// Token: 0x06001677 RID: 5751 RVA: 0x0003ABEC File Offset: 0x00038DEC
			public ReadOnlySingleBucketDictionary(Dictionary<TKey, TValue> multiBucket, IEqualityComparer<TKey> comparer)
			{
				this._comparer = comparer;
				this._multiBucket = multiBucket;
				this._singleBucket = new KeyValuePair<TKey, TValue>?(default(KeyValuePair<TKey, TValue>));
			}

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x06001678 RID: 5752 RVA: 0x0003AC1C File Offset: 0x00038E1C
			public int Count
			{
				get
				{
					if (this._multiBucket != null)
					{
						return this._multiBucket.Count;
					}
					if (this._singleBucket != null)
					{
						return 1;
					}
					return 0;
				}
			}

			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x06001679 RID: 5753 RVA: 0x0003AC50 File Offset: 0x00038E50
			public ICollection<TKey> Keys
			{
				get
				{
					if (this._multiBucket != null)
					{
						return this._multiBucket.Keys;
					}
					if (this._singleBucket != null)
					{
						return new TKey[] { this._singleBucket.Value.Key };
					}
					return ArrayHelper.Empty<TKey>();
				}
			}

			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x0600167A RID: 5754 RVA: 0x0003ACAC File Offset: 0x00038EAC
			public ICollection<TValue> Values
			{
				get
				{
					if (this._multiBucket != null)
					{
						return this._multiBucket.Values;
					}
					if (this._singleBucket != null)
					{
						return new TValue[] { this._singleBucket.Value.Value };
					}
					return ArrayHelper.Empty<TValue>();
				}
			}

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x0600167B RID: 5755 RVA: 0x0003AD06 File Offset: 0x00038F06
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000427 RID: 1063
			public TValue this[TKey key]
			{
				get
				{
					if (this._multiBucket != null)
					{
						return this._multiBucket[key];
					}
					if (this._singleBucket != null && this._comparer.Equals(this._singleBucket.Value.Key, key))
					{
						return this._singleBucket.Value.Value;
					}
					throw new KeyNotFoundException();
				}
				set
				{
					throw new NotSupportedException("Readonly");
				}
			}

			// Token: 0x0600167E RID: 5758 RVA: 0x0003AD8C File Offset: 0x00038F8C
			public SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				if (this._multiBucket != null)
				{
					return new SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>.Enumerator(this._multiBucket);
				}
				if (this._singleBucket != null)
				{
					return new SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>.Enumerator(this._singleBucket.Value);
				}
				return new SortHelpers.ReadOnlySingleBucketDictionary<TKey, TValue>.Enumerator(new Dictionary<TKey, TValue>());
			}

			// Token: 0x0600167F RID: 5759 RVA: 0x0003ADDB File Offset: 0x00038FDB
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x0003ADE8 File Offset: 0x00038FE8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001681 RID: 5761 RVA: 0x0003ADF8 File Offset: 0x00038FF8
			public bool ContainsKey(TKey key)
			{
				if (this._multiBucket != null)
				{
					return this._multiBucket.ContainsKey(key);
				}
				return this._singleBucket != null && this._comparer.Equals(this._singleBucket.Value.Key, key);
			}

			// Token: 0x06001682 RID: 5762 RVA: 0x0003AE4E File Offset: 0x0003904E
			public void Add(TKey key, TValue value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001683 RID: 5763 RVA: 0x0003AE55 File Offset: 0x00039055
			public bool Remove(TKey key)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x0003AE5C File Offset: 0x0003905C
			public bool TryGetValue(TKey key, out TValue value)
			{
				if (this._multiBucket != null)
				{
					return this._multiBucket.TryGetValue(key, out value);
				}
				if (this._singleBucket != null && this._comparer.Equals(this._singleBucket.Value.Key, key))
				{
					value = this._singleBucket.Value.Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x06001685 RID: 5765 RVA: 0x0003AED9 File Offset: 0x000390D9
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x0003AEE0 File Offset: 0x000390E0
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001687 RID: 5767 RVA: 0x0003AEE8 File Offset: 0x000390E8
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				if (this._multiBucket != null)
				{
					return ((ICollection<KeyValuePair<TKey, TValue>>)this._multiBucket).Contains(item);
				}
				return this._singleBucket != null && this._comparer.Equals(this._singleBucket.Value.Key, item.Key) && EqualityComparer<TValue>.Default.Equals(this._singleBucket.Value.Value, item.Value);
			}

			// Token: 0x06001688 RID: 5768 RVA: 0x0003AF70 File Offset: 0x00039170
			public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				if (this._multiBucket != null)
				{
					((ICollection<KeyValuePair<TKey, TValue>>)this._multiBucket).CopyTo(array, arrayIndex);
					return;
				}
				if (this._singleBucket != null)
				{
					array[arrayIndex] = this._singleBucket.Value;
				}
			}

			// Token: 0x06001689 RID: 5769 RVA: 0x0003AFB8 File Offset: 0x000391B8
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040006CE RID: 1742
			private readonly KeyValuePair<TKey, TValue>? _singleBucket;

			// Token: 0x040006CF RID: 1743
			private readonly Dictionary<TKey, TValue> _multiBucket;

			// Token: 0x040006D0 RID: 1744
			private readonly IEqualityComparer<TKey> _comparer;

			// Token: 0x020002D4 RID: 724
			public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
			{
				// Token: 0x060017AC RID: 6060 RVA: 0x0003DB6E File Offset: 0x0003BD6E
				internal Enumerator(Dictionary<TKey, TValue> multiBucket)
				{
					this._singleBucketFirstRead = false;
					this._singleBucket = default(KeyValuePair<TKey, TValue>);
					this._multiBuckets = multiBucket.GetEnumerator();
				}

				// Token: 0x060017AD RID: 6061 RVA: 0x0003DB94 File Offset: 0x0003BD94
				internal Enumerator(KeyValuePair<TKey, TValue> singleBucket)
				{
					this._singleBucketFirstRead = false;
					this._singleBucket = singleBucket;
					this._multiBuckets = null;
				}

				// Token: 0x17000452 RID: 1106
				// (get) Token: 0x060017AE RID: 6062 RVA: 0x0003DBAC File Offset: 0x0003BDAC
				public KeyValuePair<TKey, TValue> Current
				{
					get
					{
						if (this._multiBuckets != null)
						{
							KeyValuePair<TKey, TValue> keyValuePair = this._multiBuckets.Current;
							TKey key = keyValuePair.Key;
							keyValuePair = this._multiBuckets.Current;
							return new KeyValuePair<TKey, TValue>(key, keyValuePair.Value);
						}
						return new KeyValuePair<TKey, TValue>(this._singleBucket.Key, this._singleBucket.Value);
					}
				}

				// Token: 0x17000453 RID: 1107
				// (get) Token: 0x060017AF RID: 6063 RVA: 0x0003DC0E File Offset: 0x0003BE0E
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060017B0 RID: 6064 RVA: 0x0003DC1B File Offset: 0x0003BE1B
				public void Dispose()
				{
					if (this._multiBuckets != null)
					{
						this._multiBuckets.Dispose();
					}
				}

				// Token: 0x060017B1 RID: 6065 RVA: 0x0003DC30 File Offset: 0x0003BE30
				public bool MoveNext()
				{
					if (this._multiBuckets != null)
					{
						return this._multiBuckets.MoveNext();
					}
					return !this._singleBucketFirstRead && (this._singleBucketFirstRead = true);
				}

				// Token: 0x060017B2 RID: 6066 RVA: 0x0003DC65 File Offset: 0x0003BE65
				public void Reset()
				{
					if (this._multiBuckets != null)
					{
						this._multiBuckets.Reset();
						return;
					}
					this._singleBucketFirstRead = false;
				}

				// Token: 0x040007C2 RID: 1986
				private bool _singleBucketFirstRead;

				// Token: 0x040007C3 RID: 1987
				private readonly KeyValuePair<TKey, TValue> _singleBucket;

				// Token: 0x040007C4 RID: 1988
				private readonly IEnumerator<KeyValuePair<TKey, TValue>> _multiBuckets;
			}
		}
	}
}
