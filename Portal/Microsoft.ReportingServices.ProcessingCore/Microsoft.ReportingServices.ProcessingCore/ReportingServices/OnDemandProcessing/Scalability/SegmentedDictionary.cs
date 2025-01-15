using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089D RID: 2205
	internal class SegmentedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060078D6 RID: 30934 RVA: 0x001F2208 File Offset: 0x001F0408
		internal SegmentedDictionary(int priority, IScalabilityCache cache)
			: this(23, 5, null)
		{
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x001F2214 File Offset: 0x001F0414
		internal SegmentedDictionary(int nodeCapacity, int entryCapacity)
			: this(nodeCapacity, entryCapacity, null)
		{
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x001F2220 File Offset: 0x001F0420
		internal SegmentedDictionary(int nodeCapacity, int entryCapacity, IEqualityComparer<TKey> comparer)
		{
			this.m_nodeCapacity = nodeCapacity;
			this.m_valuesCapacity = entryCapacity;
			this.m_comparer = comparer;
			this.m_version = 0;
			this.m_count = 0;
			if (this.m_comparer == null)
			{
				this.m_comparer = EqualityComparer<TKey>.Default;
			}
			this.m_root = this.BuildNode(0, this.m_nodeCapacity);
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x001F227C File Offset: 0x001F047C
		public void Add(TKey key, TValue value)
		{
			if (this.Insert(this.m_root, this.m_comparer.GetHashCode(key), key, value, true, 0))
			{
				this.m_count++;
			}
			this.m_version++;
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x001F22B8 File Offset: 0x001F04B8
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x001F22CE File Offset: 0x001F04CE
		public ICollection<TKey> Keys
		{
			get
			{
				if (this.m_keysCollection == null)
				{
					this.m_keysCollection = new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryKeysCollection(this);
				}
				return this.m_keysCollection;
			}
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x001F22EA File Offset: 0x001F04EA
		public ICollection<TValue> Values
		{
			get
			{
				if (this.m_valuesCollection == null)
				{
					this.m_valuesCollection = new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValuesCollection(this);
				}
				return this.m_valuesCollection;
			}
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x001F2308 File Offset: 0x001F0508
		public bool Remove(TKey key)
		{
			int num;
			bool flag = this.Remove(this.m_root, this.m_comparer.GetHashCode(key), key, 0, out num);
			if (flag)
			{
				this.m_count--;
				this.m_version++;
			}
			return flag;
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x001F2350 File Offset: 0x001F0550
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				Global.Tracer.Assert(false, "SegmentedDictionary: Key cannot be null");
			}
			return this.Find(this.m_root, this.m_comparer.GetHashCode(key), key, 0, out value);
		}

		// Token: 0x17002815 RID: 10261
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					Global.Tracer.Assert(false, "Given key is not present in the dictionary");
				}
				return tvalue;
			}
			set
			{
				if (this.Insert(this.m_root, this.m_comparer.GetHashCode(key), key, value, false, 0))
				{
					this.m_count++;
				}
				this.m_version++;
			}
		}

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x060078E1 RID: 30945 RVA: 0x001F23ED File Offset: 0x001F05ED
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.m_comparer;
			}
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x001F23F8 File Offset: 0x001F05F8
		public bool ContainsValue(TValue value)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (@default.Equals(keyValuePair.Value, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x001F2458 File Offset: 0x001F0658
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x001F246E File Offset: 0x001F066E
		public void Clear()
		{
			this.m_root = this.BuildNode(0, this.m_nodeCapacity);
			this.m_count = 0;
			this.m_version++;
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x001F2498 File Offset: 0x001F0698
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			TValue tvalue;
			return this.TryGetValue(item.Key, out tvalue) && @default.Equals(item.Value, tvalue);
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x001F24CC File Offset: 0x001F06CC
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				Global.Tracer.Assert(false, "SegmentedDictionary.CopyTo: Index must be greater than 0");
			}
			if (array == null)
			{
				Global.Tracer.Assert(false, "SegmentedDictionary.CopyTo: Specified array must not be null");
			}
			if (array.Rank > 1)
			{
				Global.Tracer.Assert(false, "SegmentedDictionary.CopyTo: Specified array must be 1 dimensional", new object[] { "array" });
			}
			if (arrayIndex + this.Count > array.Length)
			{
				Global.Tracer.Assert(false, "SegmentedDictionary.CopyTo: Insufficent space in destination array");
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex] = keyValuePair;
				arrayIndex++;
			}
		}

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x060078E7 RID: 30951 RVA: 0x001F2588 File Offset: 0x001F0788
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x060078E8 RID: 30952 RVA: 0x001F2590 File Offset: 0x001F0790
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x001F2593 File Offset: 0x001F0793
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this).Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x001F25AD File Offset: 0x001F07AD
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator(this);
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x001F25BA File Offset: 0x001F07BA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x001F25C2 File Offset: 0x001F07C2
		private SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode BuildNode(int level, int capacity)
		{
			return new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode(capacity);
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x001F25CC File Offset: 0x001F07CC
		private bool Insert(SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode node, int hashCode, TKey key, TValue value, bool add, int level)
		{
			bool flag = false;
			int num = this.HashToSlot(node, hashCode, level);
			SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry segmentedDictionaryEntry = node.Entries[num];
			if (segmentedDictionaryEntry == null)
			{
				SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues = new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues(this.m_valuesCapacity);
				segmentedDictionaryValues.Keys[0] = key;
				segmentedDictionaryValues.Values[0] = value;
				SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues2 = segmentedDictionaryValues;
				int num2 = segmentedDictionaryValues2.Count;
				segmentedDictionaryValues2.Count = num2 + 1;
				node.Entries[num] = segmentedDictionaryValues;
				flag = true;
			}
			else
			{
				SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType entryType = segmentedDictionaryEntry.EntryType;
				if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Node)
				{
					if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Values)
					{
						Global.Tracer.Assert(false, "Unknown ObjectType");
					}
					else
					{
						SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues3 = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues;
						bool flag2 = false;
						for (int i = 0; i < segmentedDictionaryValues3.Count; i++)
						{
							if (this.m_comparer.Equals(key, segmentedDictionaryValues3.Keys[i]))
							{
								if (add)
								{
									Global.Tracer.Assert(false, "SegmentedDictionary: An element with the same key already exists within the Dictionary");
								}
								segmentedDictionaryValues3.Values[i] = value;
								flag2 = true;
								flag = false;
								break;
							}
						}
						if (!flag2)
						{
							if (segmentedDictionaryValues3.Count < segmentedDictionaryValues3.Capacity)
							{
								int count = segmentedDictionaryValues3.Count;
								segmentedDictionaryValues3.Keys[count] = key;
								segmentedDictionaryValues3.Values[count] = value;
								SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues4 = segmentedDictionaryValues3;
								int num2 = segmentedDictionaryValues4.Count;
								segmentedDictionaryValues4.Count = num2 + 1;
								flag = true;
							}
							else
							{
								SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode segmentedDictionaryNode = this.BuildNode(level + 1, this.m_nodeCapacity);
								node.Entries[num] = segmentedDictionaryNode;
								for (int j = 0; j < segmentedDictionaryValues3.Count; j++)
								{
									TKey tkey = segmentedDictionaryValues3.Keys[j];
									this.Insert(segmentedDictionaryNode, this.m_comparer.GetHashCode(tkey), tkey, segmentedDictionaryValues3.Values[j], false, level + 1);
								}
								flag = this.Insert(segmentedDictionaryNode, hashCode, key, value, add, level + 1);
							}
						}
					}
				}
				else
				{
					SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode segmentedDictionaryNode2 = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode;
					flag = this.Insert(segmentedDictionaryNode2, hashCode, key, value, add, level + 1);
				}
			}
			if (flag)
			{
				node.Count++;
			}
			return flag;
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x001F27D8 File Offset: 0x001F09D8
		private int HashToSlot(SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode node, int hashCode, int level)
		{
			int prime = PrimeHelper.GetPrime(level);
			int hashInputA = PrimeHelper.GetHashInputA(level);
			int hashInputB = PrimeHelper.GetHashInputB(level);
			return Math.Abs(hashInputA * hashCode + hashInputB) % prime % node.Entries.Length;
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x001F2810 File Offset: 0x001F0A10
		private bool Find(SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode node, int hashCode, TKey key, int level, out TValue value)
		{
			value = default(TValue);
			bool flag = false;
			int num = this.HashToSlot(node, hashCode, level);
			SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry segmentedDictionaryEntry = node.Entries[num];
			if (segmentedDictionaryEntry != null)
			{
				SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType entryType = segmentedDictionaryEntry.EntryType;
				if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Node)
				{
					if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Values)
					{
						Global.Tracer.Assert(false, "Unknown ObjectType");
					}
					else
					{
						SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues;
						for (int i = 0; i < segmentedDictionaryValues.Count; i++)
						{
							if (this.m_comparer.Equals(key, segmentedDictionaryValues.Keys[i]))
							{
								value = segmentedDictionaryValues.Values[i];
								return true;
							}
						}
					}
				}
				else
				{
					SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode segmentedDictionaryNode = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode;
					flag = this.Find(segmentedDictionaryNode, hashCode, key, level + 1, out value);
				}
			}
			return flag;
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x001F28D4 File Offset: 0x001F0AD4
		private bool Remove(SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode node, int hashCode, TKey key, int level, out int newCount)
		{
			bool flag = false;
			int num = this.HashToSlot(node, hashCode, level);
			SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry segmentedDictionaryEntry = node.Entries[num];
			if (segmentedDictionaryEntry == null)
			{
				flag = false;
			}
			else
			{
				SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType entryType = segmentedDictionaryEntry.EntryType;
				if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Node)
				{
					if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Values)
					{
						Global.Tracer.Assert(false, "Unknown ObjectType");
					}
					else
					{
						SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues;
						for (int i = 0; i < segmentedDictionaryValues.Count; i++)
						{
							if (this.m_comparer.Equals(key, segmentedDictionaryValues.Keys[i]))
							{
								if (segmentedDictionaryValues.Count == 1)
								{
									node.Entries[num] = null;
								}
								else
								{
									segmentedDictionaryValues.Keys[i] = default(TKey);
									segmentedDictionaryValues.Values[i] = default(TValue);
									SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues2 = segmentedDictionaryValues;
									int count = segmentedDictionaryValues2.Count;
									segmentedDictionaryValues2.Count = count - 1;
									int num2 = segmentedDictionaryValues.Count - i;
									if (num2 > 0)
									{
										Array.Copy(segmentedDictionaryValues.Keys, i + 1, segmentedDictionaryValues.Keys, i, num2);
										Array.Copy(segmentedDictionaryValues.Values, i + 1, segmentedDictionaryValues.Values, i, num2);
									}
								}
								flag = true;
								break;
							}
						}
					}
				}
				else
				{
					SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode segmentedDictionaryNode = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode;
					int num3;
					flag = this.Remove(segmentedDictionaryNode, hashCode, key, level + 1, out num3);
					if (flag && num3 == 0)
					{
						node.Entries[num] = null;
					}
				}
			}
			if (flag)
			{
				node.Count--;
			}
			newCount = node.Count;
			return flag;
		}

		// Token: 0x04003CB3 RID: 15539
		private int m_nodeCapacity;

		// Token: 0x04003CB4 RID: 15540
		private int m_valuesCapacity;

		// Token: 0x04003CB5 RID: 15541
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x04003CB6 RID: 15542
		private int m_count;

		// Token: 0x04003CB7 RID: 15543
		private int m_version;

		// Token: 0x04003CB8 RID: 15544
		private SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode m_root;

		// Token: 0x04003CB9 RID: 15545
		private SegmentedDictionary<TKey, TValue>.SegmentedDictionaryKeysCollection m_keysCollection;

		// Token: 0x04003CBA RID: 15546
		private SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValuesCollection m_valuesCollection;

		// Token: 0x02000D12 RID: 3346
		internal interface ISegmentedDictionaryEntry
		{
			// Token: 0x17002BC9 RID: 11209
			// (get) Token: 0x06008EC5 RID: 36549
			SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType EntryType { get; }
		}

		// Token: 0x02000D13 RID: 3347
		internal enum SegmentedDictionaryEntryType
		{
			// Token: 0x04005045 RID: 20549
			Node,
			// Token: 0x04005046 RID: 20550
			Values
		}

		// Token: 0x02000D14 RID: 3348
		internal class SegmentedDictionaryNode : SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry
		{
			// Token: 0x06008EC6 RID: 36550 RVA: 0x00245CF4 File Offset: 0x00243EF4
			internal SegmentedDictionaryNode(int capacity)
			{
				this.Entries = new SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry[capacity];
			}

			// Token: 0x17002BCA RID: 11210
			// (get) Token: 0x06008EC7 RID: 36551 RVA: 0x00245D08 File Offset: 0x00243F08
			public SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType EntryType
			{
				get
				{
					return SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Node;
				}
			}

			// Token: 0x04005047 RID: 20551
			internal SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry[] Entries;

			// Token: 0x04005048 RID: 20552
			internal int Count;
		}

		// Token: 0x02000D15 RID: 3349
		internal class SegmentedDictionaryValues : SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry
		{
			// Token: 0x06008EC8 RID: 36552 RVA: 0x00245D0B File Offset: 0x00243F0B
			public SegmentedDictionaryValues(int capacity)
			{
				this.m_count = 0;
				this.m_keys = new TKey[capacity];
				this.m_values = new TValue[capacity];
			}

			// Token: 0x17002BCB RID: 11211
			// (get) Token: 0x06008EC9 RID: 36553 RVA: 0x00245D32 File Offset: 0x00243F32
			public TKey[] Keys
			{
				get
				{
					return this.m_keys;
				}
			}

			// Token: 0x17002BCC RID: 11212
			// (get) Token: 0x06008ECA RID: 36554 RVA: 0x00245D3A File Offset: 0x00243F3A
			public TValue[] Values
			{
				get
				{
					return this.m_values;
				}
			}

			// Token: 0x17002BCD RID: 11213
			// (get) Token: 0x06008ECB RID: 36555 RVA: 0x00245D42 File Offset: 0x00243F42
			// (set) Token: 0x06008ECC RID: 36556 RVA: 0x00245D4A File Offset: 0x00243F4A
			public int Count
			{
				get
				{
					return this.m_count;
				}
				set
				{
					this.m_count = value;
				}
			}

			// Token: 0x17002BCE RID: 11214
			// (get) Token: 0x06008ECD RID: 36557 RVA: 0x00245D53 File Offset: 0x00243F53
			public int Capacity
			{
				get
				{
					return this.m_keys.Length;
				}
			}

			// Token: 0x17002BCF RID: 11215
			// (get) Token: 0x06008ECE RID: 36558 RVA: 0x00245D5D File Offset: 0x00243F5D
			public SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType EntryType
			{
				get
				{
					return SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Values;
				}
			}

			// Token: 0x04005049 RID: 20553
			private TKey[] m_keys;

			// Token: 0x0400504A RID: 20554
			private TValue[] m_values;

			// Token: 0x0400504B RID: 20555
			private int m_count;
		}

		// Token: 0x02000D16 RID: 3350
		internal struct SegmentedDictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06008ECF RID: 36559 RVA: 0x00245D60 File Offset: 0x00243F60
			internal SegmentedDictionaryEnumerator(SegmentedDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_version = dictionary.m_version;
				this.m_currentValueIndex = -1;
				this.m_currentPair = default(KeyValuePair<TKey, TValue>);
				this.m_context = null;
				this.Reset();
			}

			// Token: 0x17002BD0 RID: 11216
			// (get) Token: 0x06008ED0 RID: 36560 RVA: 0x00245D98 File Offset: 0x00243F98
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this.m_dictionary.m_version != this.m_version)
					{
						Global.Tracer.Assert(false, "SegmentedDictionaryEnumerator: Cannot use enumerator after modifying the underlying collection");
					}
					if (this.m_context.Count < 1)
					{
						Global.Tracer.Assert(false, "SegmentedDictionaryEnumerator: Enumerator beyond the bounds of the underlying collection");
					}
					return this.m_currentPair;
				}
			}

			// Token: 0x06008ED1 RID: 36561 RVA: 0x00245DEC File Offset: 0x00243FEC
			public void Dispose()
			{
				this.m_context = null;
				this.m_dictionary = null;
			}

			// Token: 0x17002BD1 RID: 11217
			// (get) Token: 0x06008ED2 RID: 36562 RVA: 0x00245DFC File Offset: 0x00243FFC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008ED3 RID: 36563 RVA: 0x00245E0C File Offset: 0x0024400C
			public bool MoveNext()
			{
				if (this.m_dictionary.m_version != this.m_version)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryEnumerator: Cannot use enumerator after modifying the underlying collection");
				}
				if (this.m_context.Count < 1 && this.m_currentValueIndex != -1)
				{
					return false;
				}
				if (this.m_context.Count == 0)
				{
					SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode> contextItem = new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode>(0, this.m_dictionary.m_root);
					this.m_currentValueIndex = 0;
					this.m_context.Push(contextItem);
				}
				return this.FindNext();
			}

			// Token: 0x06008ED4 RID: 36564 RVA: 0x00245E90 File Offset: 0x00244090
			private bool FindNext()
			{
				bool flag = false;
				while (this.m_context.Count > 0 && !flag)
				{
					SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode> contextItem = this.m_context.Peek();
					flag = this.FindNext(contextItem.Value, contextItem);
				}
				return flag;
			}

			// Token: 0x06008ED5 RID: 36565 RVA: 0x00245ED0 File Offset: 0x002440D0
			private bool FindNext(SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode node, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode> curContext)
			{
				bool flag = false;
				while (!flag && curContext.Key < node.Entries.Length)
				{
					SegmentedDictionary<TKey, TValue>.ISegmentedDictionaryEntry segmentedDictionaryEntry = node.Entries[curContext.Key];
					if (segmentedDictionaryEntry != null)
					{
						SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType entryType = segmentedDictionaryEntry.EntryType;
						if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Node)
						{
							if (entryType != SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEntryType.Values)
							{
								Global.Tracer.Assert(false, "Unknown ObjectType");
							}
							else
							{
								SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues segmentedDictionaryValues = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValues;
								if (this.m_currentValueIndex < segmentedDictionaryValues.Count)
								{
									this.m_currentPair = new KeyValuePair<TKey, TValue>(segmentedDictionaryValues.Keys[this.m_currentValueIndex], segmentedDictionaryValues.Values[this.m_currentValueIndex]);
									this.m_currentValueIndex++;
									return true;
								}
								this.m_currentValueIndex = 0;
							}
						}
						else
						{
							SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode segmentedDictionaryNode = segmentedDictionaryEntry as SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode;
							this.m_context.Push(new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode>(0, segmentedDictionaryNode));
							flag = this.FindNext();
						}
					}
					curContext.Key++;
				}
				if (!flag)
				{
					this.m_currentValueIndex = 0;
					this.m_context.Pop();
				}
				return flag;
			}

			// Token: 0x06008ED6 RID: 36566 RVA: 0x00245FD3 File Offset: 0x002441D3
			public void Reset()
			{
				this.m_currentValueIndex = -1;
				this.m_context = new Stack<SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode>>();
				this.m_version = this.m_dictionary.m_version;
			}

			// Token: 0x0400504C RID: 20556
			private int m_currentValueIndex;

			// Token: 0x0400504D RID: 20557
			private KeyValuePair<TKey, TValue> m_currentPair;

			// Token: 0x0400504E RID: 20558
			private Stack<SegmentedDictionary<TKey, TValue>.SegmentedDictionaryEnumerator.ContextItem<int, SegmentedDictionary<TKey, TValue>.SegmentedDictionaryNode>> m_context;

			// Token: 0x0400504F RID: 20559
			private int m_version;

			// Token: 0x04005050 RID: 20560
			private SegmentedDictionary<TKey, TValue> m_dictionary;

			// Token: 0x02000D4F RID: 3407
			private class ContextItem<KeyType, ValueType>
			{
				// Token: 0x06008FE5 RID: 36837 RVA: 0x00247F32 File Offset: 0x00246132
				public ContextItem(KeyType key, ValueType value)
				{
					this.Key = key;
					this.Value = value;
				}

				// Token: 0x04005102 RID: 20738
				public KeyType Key;

				// Token: 0x04005103 RID: 20739
				public ValueType Value;
			}
		}

		// Token: 0x02000D17 RID: 3351
		internal struct SegmentedDictionaryKeysEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06008ED7 RID: 36567 RVA: 0x00245FF8 File Offset: 0x002441F8
			internal SegmentedDictionaryKeysEnumerator(SegmentedDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17002BD2 RID: 11218
			// (get) Token: 0x06008ED8 RID: 36568 RVA: 0x00246010 File Offset: 0x00244210
			public TKey Current
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x06008ED9 RID: 36569 RVA: 0x00246030 File Offset: 0x00244230
			public void Dispose()
			{
			}

			// Token: 0x17002BD3 RID: 11219
			// (get) Token: 0x06008EDA RID: 36570 RVA: 0x00246032 File Offset: 0x00244232
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008EDB RID: 36571 RVA: 0x0024603F File Offset: 0x0024423F
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06008EDC RID: 36572 RVA: 0x0024604C File Offset: 0x0024424C
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04005051 RID: 20561
			private SegmentedDictionary<TKey, TValue> m_dictionary;

			// Token: 0x04005052 RID: 20562
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		// Token: 0x02000D18 RID: 3352
		internal struct SegmentedDictionaryValuesEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x06008EDD RID: 36573 RVA: 0x00246059 File Offset: 0x00244259
			internal SegmentedDictionaryValuesEnumerator(SegmentedDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17002BD4 RID: 11220
			// (get) Token: 0x06008EDE RID: 36574 RVA: 0x00246070 File Offset: 0x00244270
			public TValue Current
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x06008EDF RID: 36575 RVA: 0x00246090 File Offset: 0x00244290
			public void Dispose()
			{
			}

			// Token: 0x17002BD5 RID: 11221
			// (get) Token: 0x06008EE0 RID: 36576 RVA: 0x00246092 File Offset: 0x00244292
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008EE1 RID: 36577 RVA: 0x0024609F File Offset: 0x0024429F
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06008EE2 RID: 36578 RVA: 0x002460AC File Offset: 0x002442AC
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04005053 RID: 20563
			private SegmentedDictionary<TKey, TValue> m_dictionary;

			// Token: 0x04005054 RID: 20564
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		// Token: 0x02000D19 RID: 3353
		internal class SegmentedDictionaryKeysCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable
		{
			// Token: 0x06008EE3 RID: 36579 RVA: 0x002460B9 File Offset: 0x002442B9
			internal SegmentedDictionaryKeysCollection(SegmentedDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
			}

			// Token: 0x06008EE4 RID: 36580 RVA: 0x002460C8 File Offset: 0x002442C8
			public void Add(TKey item)
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection: Dictionary keys collection is read only");
			}

			// Token: 0x06008EE5 RID: 36581 RVA: 0x002460DA File Offset: 0x002442DA
			public void Clear()
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection: Dictionary keys collection is read only");
			}

			// Token: 0x06008EE6 RID: 36582 RVA: 0x002460EC File Offset: 0x002442EC
			public bool Contains(TKey item)
			{
				return this.m_dictionary.ContainsKey(item);
			}

			// Token: 0x06008EE7 RID: 36583 RVA: 0x002460FC File Offset: 0x002442FC
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				if (arrayIndex < 0)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection.CopyTo: Index must be greater than 0");
				}
				if (array == null)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection.CopyTo: Specified array must not be null");
				}
				if (array.Rank > 1)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection.CopyTo: Specified array must be 1 dimensional");
				}
				if (arrayIndex + this.Count > array.Length)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection.CopyTo: Insufficent space in destination array");
				}
				foreach (TKey tkey in this)
				{
					array[arrayIndex] = tkey;
					arrayIndex++;
				}
			}

			// Token: 0x17002BD6 RID: 11222
			// (get) Token: 0x06008EE8 RID: 36584 RVA: 0x002461A8 File Offset: 0x002443A8
			public int Count
			{
				get
				{
					return this.m_dictionary.Count;
				}
			}

			// Token: 0x17002BD7 RID: 11223
			// (get) Token: 0x06008EE9 RID: 36585 RVA: 0x002461B5 File Offset: 0x002443B5
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008EEA RID: 36586 RVA: 0x002461B8 File Offset: 0x002443B8
			public bool Remove(TKey item)
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryKeysCollection.Remove: Dictionary keys collection is read only");
				return false;
			}

			// Token: 0x06008EEB RID: 36587 RVA: 0x002461CB File Offset: 0x002443CB
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryKeysEnumerator(this.m_dictionary);
			}

			// Token: 0x06008EEC RID: 36588 RVA: 0x002461DD File Offset: 0x002443DD
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005055 RID: 20565
			private SegmentedDictionary<TKey, TValue> m_dictionary;
		}

		// Token: 0x02000D1A RID: 3354
		internal class SegmentedDictionaryValuesCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
		{
			// Token: 0x06008EED RID: 36589 RVA: 0x002461E5 File Offset: 0x002443E5
			internal SegmentedDictionaryValuesCollection(SegmentedDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
			}

			// Token: 0x06008EEE RID: 36590 RVA: 0x002461F4 File Offset: 0x002443F4
			public void Add(TValue item)
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.Add: Dictionary values collection is read only");
			}

			// Token: 0x06008EEF RID: 36591 RVA: 0x00246206 File Offset: 0x00244406
			public void Clear()
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.Clear: Dictionary values collection is read only");
			}

			// Token: 0x06008EF0 RID: 36592 RVA: 0x00246218 File Offset: 0x00244418
			public bool Contains(TValue item)
			{
				return this.m_dictionary.ContainsValue(item);
			}

			// Token: 0x06008EF1 RID: 36593 RVA: 0x00246228 File Offset: 0x00244428
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				if (arrayIndex < 0)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.CopyTo: Index must be greater than 0");
				}
				if (array == null)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.CopyTo: Specified array must not be null");
				}
				if (array.Rank > 1)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.CopyTo: Specified array must be 1 dimensional");
				}
				if (arrayIndex + this.Count > array.Length)
				{
					Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.CopyTo: Insufficent space in destination array");
				}
				foreach (TValue tvalue in this)
				{
					array[arrayIndex] = tvalue;
					arrayIndex++;
				}
			}

			// Token: 0x17002BD8 RID: 11224
			// (get) Token: 0x06008EF2 RID: 36594 RVA: 0x002462D4 File Offset: 0x002444D4
			public int Count
			{
				get
				{
					return this.m_dictionary.Count;
				}
			}

			// Token: 0x17002BD9 RID: 11225
			// (get) Token: 0x06008EF3 RID: 36595 RVA: 0x002462E1 File Offset: 0x002444E1
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008EF4 RID: 36596 RVA: 0x002462E4 File Offset: 0x002444E4
			public bool Remove(TValue item)
			{
				Global.Tracer.Assert(false, "SegmentedDictionaryValuesCollection.Remove: Dictionary values collection is read only");
				return false;
			}

			// Token: 0x06008EF5 RID: 36597 RVA: 0x002462F7 File Offset: 0x002444F7
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SegmentedDictionary<TKey, TValue>.SegmentedDictionaryValuesEnumerator(this.m_dictionary);
			}

			// Token: 0x06008EF6 RID: 36598 RVA: 0x00246309 File Offset: 0x00244509
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005056 RID: 20566
			private SegmentedDictionary<TKey, TValue> m_dictionary;
		}
	}
}
