using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000BC RID: 188
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[HostProtection(6, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public sealed class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00029740 File Offset: 0x00027940
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002974F File Offset: 0x0002794F
		public ConcurrentDictionary(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002975E File Offset: 0x0002795E
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002976C File Offset: 0x0002796C
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, comparer)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002977C File Offset: 0x0002797C
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, collection, comparer)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002978B File Offset: 0x0002798B
		public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, 31, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000297BC File Offset: 0x000279BC
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
				}
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00029844 File Offset: 0x00027A44
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", this.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", this.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			this.m_locks = new object[concurrencyLevel];
			for (int i = 0; i < this.m_locks.Length; i++)
			{
				this.m_locks[i] = new object();
			}
			this.m_countPerLock = new int[this.m_locks.Length];
			this.m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this.m_comparer = comparer;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000298F8 File Offset: 0x00027AF8
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryAddInternal(key, value, false, true, out tvalue);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00029924 File Offset: 0x00027B24
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00029950 File Offset: 0x00027B50
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00029984 File Offset: 0x00027B84
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
				int num;
				int num2;
				this.GetBucketAndLockNo(this.m_comparer.GetHashCode(key), out num, out num2, buckets.Length);
				object obj = this.m_locks[num2];
				lock (obj)
				{
					if (buckets != this.m_buckets)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = this.m_buckets[num];
					while (node2 != null)
					{
						if (this.m_comparer.Equals(node2.m_key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2.m_value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								this.m_buckets[num] = node2.m_next;
							}
							else
							{
								node.m_next = node2.m_next;
							}
							value = node2.m_value;
							this.m_countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00029AA8 File Offset: 0x00027CA8
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
			int num;
			int num2;
			this.GetBucketAndLockNo(this.m_comparer.GetHashCode(key), out num, out num2, buckets.Length);
			ConcurrentDictionary<TKey, TValue>.Node node = buckets[num];
			Thread.MemoryBarrier();
			while (node != null)
			{
				if (this.m_comparer.Equals(node.m_key, key))
				{
					value = node.m_value;
					return true;
				}
				node = node.m_next;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00029B2C File Offset: 0x00027D2C
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int hashCode = this.m_comparer.GetHashCode(key);
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool flag;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, buckets.Length);
				object obj = this.m_locks[num2];
				lock (obj)
				{
					if (buckets != this.m_buckets)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = buckets[num];
					while (node2 != null)
					{
						if (this.m_comparer.Equals(node2.m_key, key))
						{
							if (@default.Equals(node2.m_value, comparisonValue))
							{
								ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, newValue, hashCode, node2.m_next);
								if (node == null)
								{
									buckets[num] = node3;
								}
								else
								{
									node.m_next = node3;
								}
								return true;
							}
							return false;
						}
						else
						{
							node = node2;
							node2 = node2.m_next;
						}
					}
					flag = false;
				}
				break;
			}
			return flag;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00029C34 File Offset: 0x00027E34
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				this.m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[31];
				Array.Clear(this.m_countPerLock, 0, this.m_countPerLock.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00029C90 File Offset: 0x00027E90
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				for (int i = 0; i < this.m_locks.Length; i++)
				{
					num2 += this.m_countPerLock[i];
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00029D34 File Offset: 0x00027F34
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int num = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] array2;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this.m_locks.Length; i++)
					{
						num2 += this.m_countPerLock[i];
					}
					KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[num2];
					this.CopyToPairs(array, 0);
					array2 = array;
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return array2;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00029D9C File Offset: 0x00027F9C
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00029DF0 File Offset: 0x00027FF0
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00029E50 File Offset: 0x00028050
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this.m_buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00029EA4 File Offset: 0x000280A4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current = buckets[i];
				Thread.MemoryBarrier();
				while (current != null)
				{
					yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
					current = current.m_next;
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00029EB4 File Offset: 0x000280B4
		private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			int hashCode = this.m_comparer.GetHashCode(key);
			checked
			{
				ConcurrentDictionary<TKey, TValue>.Node[] buckets;
				bool flag;
				for (;;)
				{
					buckets = this.m_buckets;
					int num;
					int num2;
					this.GetBucketAndLockNo(hashCode, out num, out num2, buckets.Length);
					flag = false;
					bool flag2 = false;
					try
					{
						if (acquireLock)
						{
							Monitor.Enter(this.m_locks[num2]);
							flag2 = true;
						}
						if (buckets != this.m_buckets)
						{
							continue;
						}
						ConcurrentDictionary<TKey, TValue>.Node node = null;
						for (ConcurrentDictionary<TKey, TValue>.Node node2 = buckets[num]; node2 != null; node2 = node2.m_next)
						{
							if (this.m_comparer.Equals(node2.m_key, key))
							{
								if (updateIfExists)
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
									if (node == null)
									{
										buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
									resultingValue = value;
								}
								else
								{
									resultingValue = node2.m_value;
								}
								return false;
							}
							node = node2;
						}
						buckets[num] = new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, buckets[num]);
						this.m_countPerLock[num2]++;
						if (this.m_countPerLock[num2] > buckets.Length / this.m_locks.Length)
						{
							flag = true;
						}
					}
					finally
					{
						if (flag2)
						{
							Monitor.Exit(this.m_locks[num2]);
						}
					}
					break;
				}
				if (flag)
				{
					this.GrowTable(buckets);
				}
				resultingValue = value;
				return true;
			}
		}

		// Token: 0x17000139 RID: 313
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					throw new KeyNotFoundException();
				}
				return tvalue;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				this.TryAddInternal(key, value, true, true, out tvalue);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002A054 File Offset: 0x00028254
		public int Count
		{
			get
			{
				int num = 0;
				int num2 = 0;
				try
				{
					this.AcquireAllLocks(ref num2);
					for (int i = 0; i < this.m_countPerLock.Length; i++)
					{
						num += this.m_countPerLock[i];
					}
				}
				finally
				{
					this.ReleaseLocks(0, num2);
				}
				return num;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002A0AC File Offset: 0x000282AC
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			this.TryAddInternal(key, valueFactory.Invoke(key), false, true, out tvalue);
			return tvalue;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002A0FC File Offset: 0x000282FC
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			this.TryAddInternal(key, value, false, true, out tvalue);
			return tvalue;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002A12C File Offset: 0x0002832C
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else
				{
					tvalue2 = addValueFactory.Invoke(key);
					TValue tvalue3;
					if (this.TryAddInternal(key, tvalue2, false, true, out tvalue3))
					{
						return tvalue3;
					}
				}
			}
			return tvalue2;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002A1A0 File Offset: 0x000283A0
		public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory.Invoke(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, addValue, false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002A200 File Offset: 0x00028400
		public bool IsEmpty
		{
			get
			{
				int num = 0;
				try
				{
					this.AcquireAllLocks(ref num);
					for (int i = 0; i < this.m_countPerLock.Length; i++)
					{
						if (this.m_countPerLock[i] != 0)
						{
							return false;
						}
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return true;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002A25C File Offset: 0x0002845C
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002A27C File Offset: 0x0002847C
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0002A292 File Offset: 0x00028492
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0002A29A File Offset: 0x0002849A
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002A2A2 File Offset: 0x000284A2
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002A2B8 File Offset: 0x000284B8
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002A2EA File Offset: 0x000284EA
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002A2F0 File Offset: 0x000284F0
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException(this.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0002A333 File Offset: 0x00028533
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002A33C File Offset: 0x0002853C
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
			}
			this.Add((TKey)((object)key), tvalue);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002A3AC File Offset: 0x000285AC
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002A3D2 File Offset: 0x000285D2
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002A3DA File Offset: 0x000285DA
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0002A3DD File Offset: 0x000285DD
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002A3E0 File Offset: 0x000285E0
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002A3E8 File Offset: 0x000285E8
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002A41A File Offset: 0x0002861A
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0002A424 File Offset: 0x00028624
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0002A460 File Offset: 0x00028660
		object IDictionary.Item
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!(key is TKey))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
				}
				if (!(value is TValue))
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002A4C0 File Offset: 0x000286C0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentDictionary_IndexIsNegative"));
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				for (int i = 0; i < this.m_locks.Length; i++)
				{
					num2 += this.m_countPerLock[i];
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002A5B4 File Offset: 0x000287B4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0002A5B7 File Offset: 0x000287B7
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002A5C0 File Offset: 0x000287C0
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Node[] buckets)
		{
			int num = 0;
			checked
			{
				try
				{
					this.AcquireLocks(0, 1, ref num);
					if (buckets == this.m_buckets)
					{
						int num2;
						try
						{
							num2 = buckets.Length * 2 + 1;
							while (num2 % 3 == 0 || num2 % 5 == 0 || num2 % 7 == 0)
							{
								num2 += 2;
							}
						}
						catch (OverflowException)
						{
							return;
						}
						ConcurrentDictionary<TKey, TValue>.Node[] array = new ConcurrentDictionary<TKey, TValue>.Node[num2];
						int[] array2 = new int[this.m_locks.Length];
						this.AcquireLocks(1, this.m_locks.Length, ref num);
						foreach (ConcurrentDictionary<TKey, TValue>.Node node in buckets)
						{
							while (node != null)
							{
								ConcurrentDictionary<TKey, TValue>.Node next = node.m_next;
								int num3;
								int num4;
								this.GetBucketAndLockNo(node.m_hashcode, out num3, out num4, array.Length);
								array[num3] = new ConcurrentDictionary<TKey, TValue>.Node(node.m_key, node.m_value, node.m_hashcode, array[num3]);
								array2[num4]++;
								node = next;
							}
						}
						this.m_buckets = array;
						this.m_countPerLock = array2;
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002A6DC File Offset: 0x000288DC
		private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % this.m_locks.Length;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0002A6F7 File Offset: 0x000288F7
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return 4 * Environment.ProcessorCount;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002A700 File Offset: 0x00028900
		private void AcquireAllLocks(ref int locksAcquired)
		{
			this.AcquireLocks(0, this.m_locks.Length, ref locksAcquired);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002A714 File Offset: 0x00028914
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(this.m_locks[i]);
					flag = true;
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002A75C File Offset: 0x0002895C
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this.m_locks[i]);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002A784 File Offset: 0x00028984
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int num = 0;
			ReadOnlyCollection<TKey> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				List<TKey> list = new List<TKey>();
				for (int i = 0; i < this.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_key);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002A800 File Offset: 0x00028A00
		private ReadOnlyCollection<TValue> GetValues()
		{
			int num = 0;
			ReadOnlyCollection<TValue> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				List<TValue> list = new List<TValue>();
				for (int i = 0; i < this.m_buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_buckets[i]; node != null; node = node.m_next)
					{
						list.Add(node.m_value);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0002A87C File Offset: 0x00028A7C
		[Conditional("DEBUG")]
		private void Assert(bool condition)
		{
			if (!condition)
			{
				throw new Exception("Assertion failed.");
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002A88C File Offset: 0x00028A8C
		private string GetResource(string key)
		{
			return key;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002A88F File Offset: 0x00028A8F
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
			this.m_serializationConcurrencyLevel = this.m_locks.Length;
			this.m_serializationCapacity = this.m_buckets.Length;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002A8BC File Offset: 0x00028ABC
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			KeyValuePair<TKey, TValue>[] serializationArray = this.m_serializationArray;
			this.m_buckets = new ConcurrentDictionary<TKey, TValue>.Node[this.m_serializationCapacity];
			this.m_countPerLock = new int[this.m_serializationConcurrencyLevel];
			this.m_locks = new object[this.m_serializationConcurrencyLevel];
			for (int i = 0; i < this.m_locks.Length; i++)
			{
				this.m_locks[i] = new object();
			}
			this.InitializeFromCollection(serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x04000198 RID: 408
		[NonSerialized]
		private volatile ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;

		// Token: 0x04000199 RID: 409
		[NonSerialized]
		private object[] m_locks;

		// Token: 0x0400019A RID: 410
		[NonSerialized]
		private volatile int[] m_countPerLock;

		// Token: 0x0400019B RID: 411
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x0400019C RID: 412
		private KeyValuePair<TKey, TValue>[] m_serializationArray;

		// Token: 0x0400019D RID: 413
		private int m_serializationConcurrencyLevel;

		// Token: 0x0400019E RID: 414
		private int m_serializationCapacity;

		// Token: 0x0400019F RID: 415
		private const int DEFAULT_CONCURRENCY_MULTIPLIER = 4;

		// Token: 0x040001A0 RID: 416
		private const int DEFAULT_CAPACITY = 31;

		// Token: 0x0200014E RID: 334
		private class Node
		{
			// Token: 0x06000A8E RID: 2702 RVA: 0x0002FA38 File Offset: 0x0002DC38
			internal Node(TKey key, TValue value, int hashcode)
				: this(key, value, hashcode, null)
			{
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x0002FA44 File Offset: 0x0002DC44
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this.m_key = key;
				this.m_value = value;
				this.m_next = next;
				this.m_hashcode = hashcode;
			}

			// Token: 0x04000382 RID: 898
			internal TKey m_key;

			// Token: 0x04000383 RID: 899
			internal TValue m_value;

			// Token: 0x04000384 RID: 900
			internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;

			// Token: 0x04000385 RID: 901
			internal int m_hashcode;
		}

		// Token: 0x0200014F RID: 335
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000A90 RID: 2704 RVA: 0x0002FA6B File Offset: 0x0002DC6B
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002FA80 File Offset: 0x0002DC80
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002FAEC File Offset: 0x0002DCEC
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002FB11 File Offset: 0x0002DD11
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x0002FB1E File Offset: 0x0002DD1E
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x0002FB2B File Offset: 0x0002DD2B
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04000386 RID: 902
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}
	}
}
