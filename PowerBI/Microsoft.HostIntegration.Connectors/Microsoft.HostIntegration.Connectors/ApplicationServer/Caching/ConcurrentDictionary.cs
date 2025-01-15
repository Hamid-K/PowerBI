using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001BC RID: 444
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	internal sealed class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x0003106B File Offset: 0x0002F26B
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31)
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003107A File Offset: 0x0002F27A
		public ConcurrentDictionary(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00031089 File Offset: 0x0002F289
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00031097 File Offset: 0x0002F297
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, comparer)
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000310A7 File Offset: 0x0002F2A7
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, collection, comparer)
		{
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000310B6 File Offset: 0x0002F2B6
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

		// Token: 0x06000E8A RID: 3722 RVA: 0x000310E8 File Offset: 0x0002F2E8
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					throw new ArgumentNullException("collection", "Collection cannot contain a null key value");
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
				}
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00031174 File Offset: 0x0002F374
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
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

		// Token: 0x06000E8C RID: 3724 RVA: 0x00031224 File Offset: 0x0002F424
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryAddInternal(key, value, false, true, out tvalue);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00031250 File Offset: 0x0002F450
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003127C File Offset: 0x0002F47C
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000312B0 File Offset: 0x0002F4B0
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
				int num;
				int num2;
				this.GetBucketAndLockNo(this.m_comparer.GetHashCode(key), out num, out num2, buckets.Length);
				lock (this.m_locks[num2])
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

		// Token: 0x06000E90 RID: 3728 RVA: 0x000313EC File Offset: 0x0002F5EC
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

		// Token: 0x06000E91 RID: 3729 RVA: 0x00031470 File Offset: 0x0002F670
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int hashCode = this.m_comparer.GetHashCode(key);
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool flag2;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Node[] buckets = this.m_buckets;
				int num;
				int num2;
				this.GetBucketAndLockNo(hashCode, out num, out num2, buckets.Length);
				lock (this.m_locks[num2])
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
					flag2 = false;
				}
				break;
			}
			return flag2;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00031580 File Offset: 0x0002F780
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

		// Token: 0x06000E93 RID: 3731 RVA: 0x000315DC File Offset: 0x0002F7DC
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_IndexIsNegative"));
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
					throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003167C File Offset: 0x0002F87C
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

		// Token: 0x06000E95 RID: 3733 RVA: 0x000316E4 File Offset: 0x0002F8E4
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

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003173C File Offset: 0x0002F93C
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

		// Token: 0x06000E97 RID: 3735 RVA: 0x000317A0 File Offset: 0x0002F9A0
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

		// Token: 0x06000E98 RID: 3736 RVA: 0x000317F4 File Offset: 0x0002F9F4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node current in this.m_buckets)
			{
				Thread.MemoryBarrier();
				while (current != null)
				{
					yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
					current = current.m_next;
				}
			}
			yield break;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00031810 File Offset: 0x0002FA10
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

		// Token: 0x1700033F RID: 831
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000319BC File Offset: 0x0002FBBC
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x00031A14 File Offset: 0x0002FC14
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
			this.TryAddInternal(key, valueFactory(key), false, true, out tvalue);
			return tvalue;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00031A64 File Offset: 0x0002FC64
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

		// Token: 0x06000E9F RID: 3743 RVA: 0x00031A94 File Offset: 0x0002FC94
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
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdate(key, tvalue2, tvalue))
					{
						break;
					}
				}
				else
				{
					tvalue2 = addValueFactory(key);
					TValue tvalue3;
					if (this.TryAddInternal(key, tvalue2, false, true, out tvalue3))
					{
						return tvalue3;
					}
				}
			}
			return tvalue2;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00031B08 File Offset: 0x0002FD08
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
					tvalue2 = updateValueFactory(key, tvalue);
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

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00031B68 File Offset: 0x0002FD68
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

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00031BC4 File Offset: 0x0002FDC4
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00031BE0 File Offset: 0x0002FDE0
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00031BF6 File Offset: 0x0002FDF6
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00031BFE File Offset: 0x0002FDFE
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00031C06 File Offset: 0x0002FE06
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00031C1C File Offset: 0x0002FE1C
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00006F04 File Offset: 0x00005104
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00031C50 File Offset: 0x0002FE50
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00031C92 File Offset: 0x0002FE92
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00031C9C File Offset: 0x0002FE9C
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
			}
			((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), tvalue);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00031D08 File Offset: 0x0002FF08
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00031D2E File Offset: 0x0002FF2E
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00006F04 File Offset: 0x00005104
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00006F04 File Offset: 0x00005104
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00031BF6 File Offset: 0x0002FDF6
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00031D38 File Offset: 0x0002FF38
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00031BFE File Offset: 0x0002FDFE
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000349 RID: 841
		object IDictionary.this[object key]
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
					throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
				}
				if (!(value is TValue))
				{
					throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00031E08 File Offset: 0x00030008
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_IndexIsNegative"));
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
					throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
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
							throw new ArgumentException(ConcurrentDictionary<TKey, TValue>.GetResource("ConcurrentDictionary_ArrayIncorrectType"), "array");
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

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00006F04 File Offset: 0x00005104
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00003D71 File Offset: 0x00001F71
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00031EF8 File Offset: 0x000300F8
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

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00032020 File Offset: 0x00030220
		private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % this.m_locks.Length;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003203B File Offset: 0x0003023B
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return 4 * Environment.ProcessorCount;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00032044 File Offset: 0x00030244
		private void AcquireAllLocks(ref int locksAcquired)
		{
			this.AcquireLocks(0, this.m_locks.Length, ref locksAcquired);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00032058 File Offset: 0x00030258
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

		// Token: 0x06000EBD RID: 3773 RVA: 0x000320A0 File Offset: 0x000302A0
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this.m_locks[i]);
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000320C8 File Offset: 0x000302C8
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

		// Token: 0x06000EBF RID: 3775 RVA: 0x00032144 File Offset: 0x00030344
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

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000321C0 File Offset: 0x000303C0
		[Conditional("DEBUG")]
		private static void Assert(bool condition)
		{
			ReleaseAssert.IsTrue(condition);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00008948 File Offset: 0x00006B48
		private static string GetResource(string key)
		{
			return key;
		}

		// Token: 0x04000A19 RID: 2585
		private const int DEFAULT_CONCURRENCY_MULTIPLIER = 4;

		// Token: 0x04000A1A RID: 2586
		private const int DEFAULT_CAPACITY = 31;

		// Token: 0x04000A1B RID: 2587
		private volatile ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;

		// Token: 0x04000A1C RID: 2588
		private object[] m_locks;

		// Token: 0x04000A1D RID: 2589
		private volatile int[] m_countPerLock;

		// Token: 0x04000A1E RID: 2590
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x020001BD RID: 445
		private class Node
		{
			// Token: 0x06000EC2 RID: 3778 RVA: 0x000321C8 File Offset: 0x000303C8
			internal Node(TKey key, TValue value, int hashcode)
				: this(key, value, hashcode, null)
			{
			}

			// Token: 0x06000EC3 RID: 3779 RVA: 0x000321D4 File Offset: 0x000303D4
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this.m_key = key;
				this.m_value = value;
				this.m_next = next;
				this.m_hashcode = hashcode;
			}

			// Token: 0x04000A1F RID: 2591
			internal TKey m_key;

			// Token: 0x04000A20 RID: 2592
			internal TValue m_value;

			// Token: 0x04000A21 RID: 2593
			internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;

			// Token: 0x04000A22 RID: 2594
			internal int m_hashcode;
		}

		// Token: 0x020001BE RID: 446
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000EC4 RID: 3780 RVA: 0x000321FB File Offset: 0x000303FB
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this.m_enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x1700034D RID: 845
			// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00032210 File Offset: 0x00030410
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					KeyValuePair<TKey, TValue> keyValuePair2 = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair2.Value);
				}
			}

			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00032254 File Offset: 0x00030454
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0003227C File Offset: 0x0003047C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000322A1 File Offset: 0x000304A1
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x000322AE File Offset: 0x000304AE
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x000322BB File Offset: 0x000304BB
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x04000A23 RID: 2595
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}
	}
}
