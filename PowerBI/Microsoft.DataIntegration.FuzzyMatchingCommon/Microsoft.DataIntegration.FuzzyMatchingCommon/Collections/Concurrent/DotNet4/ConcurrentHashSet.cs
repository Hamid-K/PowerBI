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
	// Token: 0x020000BD RID: 189
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[HostProtection(6, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentHashSet<TKey> : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0002A935 File Offset: 0x00028B35
		public ConcurrentHashSet()
			: this(ConcurrentHashSet<TKey>.DefaultConcurrencyLevel, 31)
		{
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002A944 File Offset: 0x00028B44
		public ConcurrentHashSet(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002A953 File Offset: 0x00028B53
		public ConcurrentHashSet(IEnumerable<TKey> collection)
			: this(collection, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002A961 File Offset: 0x00028B61
		public ConcurrentHashSet(IEqualityComparer<TKey> comparer)
			: this(ConcurrentHashSet<TKey>.DefaultConcurrencyLevel, 31, comparer)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002A971 File Offset: 0x00028B71
		public ConcurrentHashSet(IEnumerable<TKey> collection, IEqualityComparer<TKey> comparer)
			: this(ConcurrentHashSet<TKey>.DefaultConcurrencyLevel, collection, comparer)
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002A980 File Offset: 0x00028B80
		public ConcurrentHashSet(int concurrencyLevel, IEnumerable<TKey> collection, IEqualityComparer<TKey> comparer)
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

		// Token: 0x0600080C RID: 2060 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		private void InitializeFromCollection(IEnumerable<TKey> collection)
		{
			foreach (TKey tkey in collection)
			{
				if (tkey == null)
				{
					throw new ArgumentNullException("key");
				}
				if (!this.TryAddInternal(tkey, false, false))
				{
					throw new ArgumentException(this.GetResource("ConcurrentHashSet_SourceContainsDuplicateKeys"));
				}
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002AA20 File Offset: 0x00028C20
		public ConcurrentHashSet(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", this.GetResource("ConcurrentHashSet_ConcurrencyLevelMustBePositive"));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", this.GetResource("ConcurrentHashSet_CapacityMustNotBeNegative"));
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
			this.m_buckets = new ConcurrentHashSet<TKey>.Node[capacity];
			this.m_comparer = comparer;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002AAD2 File Offset: 0x00028CD2
		public bool TryAdd(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryAddInternal(key, false, true);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002AAF0 File Offset: 0x00028CF0
		public void Add(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.TryAddInternal(key, false, true);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002AB0F File Offset: 0x00028D0F
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryGetValue(key);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002AB2B File Offset: 0x00028D2B
		public bool TryRemove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.TryRemoveInternal(key);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002AB48 File Offset: 0x00028D48
		private bool TryRemoveInternal(TKey key)
		{
			for (;;)
			{
				ConcurrentHashSet<TKey>.Node[] buckets = this.m_buckets;
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
					ConcurrentHashSet<TKey>.Node node = null;
					for (ConcurrentHashSet<TKey>.Node node2 = this.m_buckets[num]; node2 != null; node2 = node2.m_next)
					{
						if (this.m_comparer.Equals(node2.m_key, key))
						{
							if (node == null)
							{
								this.m_buckets[num] = node2.m_next;
							}
							else
							{
								node.m_next = node2.m_next;
							}
							this.m_countPerLock[num2]--;
							return true;
						}
						node = node2;
					}
				}
				break;
			}
			return false;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002AC2C File Offset: 0x00028E2C
		protected bool TryGetValue(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ConcurrentHashSet<TKey>.Node[] buckets = this.m_buckets;
			int num;
			int num2;
			this.GetBucketAndLockNo(this.m_comparer.GetHashCode(key), out num, out num2, buckets.Length);
			ConcurrentHashSet<TKey>.Node node = buckets[num];
			Thread.MemoryBarrier();
			while (node != null)
			{
				if (this.m_comparer.Equals(node.m_key, key))
				{
					return true;
				}
				node = node.m_next;
			}
			return false;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002AC9C File Offset: 0x00028E9C
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				this.m_buckets = new ConcurrentHashSet<TKey>.Node[31];
				Array.Clear(this.m_countPerLock, 0, this.m_countPerLock.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002ACF8 File Offset: 0x00028EF8
		void ICollection<TKey>.CopyTo(TKey[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentHashSet_IndexIsNegative"));
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
					throw new ArgumentException(this.GetResource("ConcurrentHashSet_ArrayNotLargeEnough"));
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002AD9C File Offset: 0x00028F9C
		public TKey[] ToArray()
		{
			int num = 0;
			checked
			{
				TKey[] array2;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this.m_locks.Length; i++)
					{
						num2 += this.m_countPerLock[i];
					}
					TKey[] array = new TKey[num2];
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

		// Token: 0x06000817 RID: 2071 RVA: 0x0002AE04 File Offset: 0x00029004
		private void CopyToPairs(TKey[] array, int index)
		{
			foreach (ConcurrentHashSet<TKey>.Node node in this.m_buckets)
			{
				while (node != null)
				{
					array[index] = node.m_key;
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002AE4C File Offset: 0x0002904C
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentHashSet<TKey>.Node node in this.m_buckets)
			{
				while (node != null)
				{
					array[index] = node;
					index++;
					node = node.m_next;
				}
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002AE8B File Offset: 0x0002908B
		public IEnumerator<TKey> GetEnumerator()
		{
			ConcurrentHashSet<TKey>.Node[] buckets = this.m_buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentHashSet<TKey>.Node current = buckets[i];
				Thread.MemoryBarrier();
				while (current != null)
				{
					yield return current.m_key;
					current = current.m_next;
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002AE9C File Offset: 0x0002909C
		private bool TryAddInternal(TKey key, bool updateIfExists, bool acquireLock)
		{
			int hashCode = this.m_comparer.GetHashCode(key);
			checked
			{
				ConcurrentHashSet<TKey>.Node[] buckets;
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
						}
						flag2 = true;
						if (buckets != this.m_buckets)
						{
							continue;
						}
						ConcurrentHashSet<TKey>.Node node = null;
						for (ConcurrentHashSet<TKey>.Node node2 = buckets[num]; node2 != null; node2 = node2.m_next)
						{
							if (this.m_comparer.Equals(node2.m_key, key))
							{
								if (updateIfExists)
								{
									ConcurrentHashSet<TKey>.Node node3 = new ConcurrentHashSet<TKey>.Node(node2.m_key, hashCode, node2.m_next);
									if (node == null)
									{
										buckets[num] = node3;
									}
									else
									{
										node.m_next = node3;
									}
								}
								return false;
							}
							node = node2;
						}
						buckets[num] = new ConcurrentHashSet<TKey>.Node(key, hashCode, buckets[num]);
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
				return true;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0002AFCC File Offset: 0x000291CC
		public int ExactCount
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0002B024 File Offset: 0x00029224
		public int Count
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.m_countPerLock.Length; i++)
				{
					num += this.m_countPerLock[i];
				}
				return num;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002B058 File Offset: 0x00029258
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

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0002B0B4 File Offset: 0x000292B4
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002B0BC File Offset: 0x000292BC
		void ICollection<TKey>.Add(TKey key)
		{
			this.TryAdd(key);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002B0C6 File Offset: 0x000292C6
		bool ICollection<TKey>.Contains(TKey key)
		{
			return this.ContainsKey(key);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002B0CF File Offset: 0x000292CF
		bool ICollection<TKey>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002B0D2 File Offset: 0x000292D2
		bool ICollection<TKey>.Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException(this.GetResource("ConcurrentHashSet_ItemKeyIsNull"));
			}
			return this.TryRemoveInternal(key);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002B0F4 File Offset: 0x000292F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002B0FC File Offset: 0x000292FC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", this.GetResource("ConcurrentHashSet_IndexIsNegative"));
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
					throw new ArgumentException(this.GetResource("ConcurrentHashSet_ArrayNotLargeEnough"));
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					object[] array3 = array as object[];
					if (array3 == null)
					{
						throw new ArgumentException(this.GetResource("ConcurrentHashSet_ArrayIncorrectType"), "array");
					}
					this.CopyToObjects(array3, index);
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002B1DC File Offset: 0x000293DC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0002B1DF File Offset: 0x000293DF
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("ConcurrentCollection_SyncRoot_NotSupported");
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002B1EC File Offset: 0x000293EC
		private void GrowTable(ConcurrentHashSet<TKey>.Node[] buckets)
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
						ConcurrentHashSet<TKey>.Node[] array = new ConcurrentHashSet<TKey>.Node[num2];
						int[] array2 = new int[this.m_locks.Length];
						this.AcquireLocks(1, this.m_locks.Length, ref num);
						foreach (ConcurrentHashSet<TKey>.Node node in buckets)
						{
							while (node != null)
							{
								ConcurrentHashSet<TKey>.Node next = node.m_next;
								int num3;
								int num4;
								this.GetBucketAndLockNo(node.m_hashcode, out num3, out num4, array.Length);
								array[num3] = new ConcurrentHashSet<TKey>.Node(node.m_key, node.m_hashcode, array[num3]);
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

		// Token: 0x06000828 RID: 2088 RVA: 0x0002B300 File Offset: 0x00029500
		private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % this.m_locks.Length;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0002B31B File Offset: 0x0002951B
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return 4 * Environment.ProcessorCount;
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002B324 File Offset: 0x00029524
		private void AcquireAllLocks(ref int locksAcquired)
		{
			this.AcquireLocks(0, this.m_locks.Length, ref locksAcquired);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002B338 File Offset: 0x00029538
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

		// Token: 0x0600082C RID: 2092 RVA: 0x0002B380 File Offset: 0x00029580
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this.m_locks[i]);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002B3A8 File Offset: 0x000295A8
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
					for (ConcurrentHashSet<TKey>.Node node = this.m_buckets[i]; node != null; node = node.m_next)
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

		// Token: 0x0600082E RID: 2094 RVA: 0x0002B424 File Offset: 0x00029624
		[Conditional("DEBUG")]
		private void Assert(bool condition)
		{
			if (!condition)
			{
				throw new Exception("Assertion failed.");
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002B434 File Offset: 0x00029634
		private string GetResource(string key)
		{
			return key;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002B437 File Offset: 0x00029637
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
			this.m_serializationConcurrencyLevel = this.m_locks.Length;
			this.m_serializationCapacity = this.m_buckets.Length;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002B464 File Offset: 0x00029664
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			TKey[] serializationArray = this.m_serializationArray;
			this.m_buckets = new ConcurrentHashSet<TKey>.Node[this.m_serializationCapacity];
			this.m_countPerLock = new int[this.m_serializationConcurrencyLevel];
			this.m_locks = new object[this.m_serializationConcurrencyLevel];
			for (int i = 0; i < this.m_locks.Length; i++)
			{
				this.m_locks[i] = new object();
			}
			this.InitializeFromCollection(serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x040001A1 RID: 417
		[NonSerialized]
		private volatile ConcurrentHashSet<TKey>.Node[] m_buckets;

		// Token: 0x040001A2 RID: 418
		[NonSerialized]
		private object[] m_locks;

		// Token: 0x040001A3 RID: 419
		[NonSerialized]
		private volatile int[] m_countPerLock;

		// Token: 0x040001A4 RID: 420
		private IEqualityComparer<TKey> m_comparer;

		// Token: 0x040001A5 RID: 421
		private TKey[] m_serializationArray;

		// Token: 0x040001A6 RID: 422
		private int m_serializationConcurrencyLevel;

		// Token: 0x040001A7 RID: 423
		private int m_serializationCapacity;

		// Token: 0x040001A8 RID: 424
		private const int DEFAULT_CONCURRENCY_MULTIPLIER = 4;

		// Token: 0x040001A9 RID: 425
		private const int DEFAULT_CAPACITY = 31;

		// Token: 0x02000151 RID: 337
		private class Node
		{
			// Token: 0x06000A9D RID: 2717 RVA: 0x0002FC3B File Offset: 0x0002DE3B
			internal Node(TKey key, int hashcode)
				: this(key, hashcode, null)
			{
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x0002FC46 File Offset: 0x0002DE46
			internal Node(TKey key, int hashcode, ConcurrentHashSet<TKey>.Node next)
			{
				this.m_key = key;
				this.m_next = next;
				this.m_hashcode = hashcode;
			}

			// Token: 0x0400038D RID: 909
			internal TKey m_key;

			// Token: 0x0400038E RID: 910
			internal volatile ConcurrentHashSet<TKey>.Node m_next;

			// Token: 0x0400038F RID: 911
			internal int m_hashcode;
		}
	}
}
