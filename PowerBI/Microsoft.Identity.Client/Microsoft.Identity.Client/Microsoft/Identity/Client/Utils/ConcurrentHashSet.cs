﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C2 RID: 450
	[DebuggerDisplay("Count = {Count}")]
	internal class ConcurrentHashSet<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ICollection<T>
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00043CF7 File Offset: 0x00041EF7
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return ProcessorCounter.ProcessorCount;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00043D00 File Offset: 0x00041F00
		public int Count
		{
			get
			{
				int num = 0;
				int num2 = 0;
				try
				{
					this.AcquireAllLocks(ref num2);
					for (int i = 0; i < this._tables.CountPerLock.Length; i++)
					{
						num += this._tables.CountPerLock[i];
					}
				}
				finally
				{
					this.ReleaseLocks(0, num2);
				}
				return num;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00043D68 File Offset: 0x00041F68
		public bool IsEmpty
		{
			get
			{
				int num = 0;
				try
				{
					this.AcquireAllLocks(ref num);
					for (int i = 0; i < this._tables.CountPerLock.Length; i++)
					{
						if (this._tables.CountPerLock[i] != 0)
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

		// Token: 0x060013FB RID: 5115 RVA: 0x00043DD0 File Offset: 0x00041FD0
		public ConcurrentHashSet()
			: this(ConcurrentHashSet<T>.DefaultConcurrencyLevel, 31, true, null)
		{
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00043DE1 File Offset: 0x00041FE1
		public ConcurrentHashSet(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, false, null)
		{
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00043DED File Offset: 0x00041FED
		public ConcurrentHashSet(IEnumerable<T> collection)
			: this(collection, null)
		{
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00043DF7 File Offset: 0x00041FF7
		public ConcurrentHashSet(IEqualityComparer<T> comparer)
			: this(ConcurrentHashSet<T>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00043E08 File Offset: 0x00042008
		public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00043E26 File Offset: 0x00042026
		public ConcurrentHashSet(int concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T> comparer)
			: this(concurrencyLevel, 31, false, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00043E48 File Offset: 0x00042048
		public ConcurrentHashSet(int concurrencyLevel, int capacity, IEqualityComparer<T> comparer)
			: this(concurrencyLevel, capacity, false, comparer)
		{
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00043E54 File Offset: 0x00042054
		private ConcurrentHashSet(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<T> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] array2 = new int[array.Length];
			ConcurrentHashSet<T>.Node[] array3 = new ConcurrentHashSet<T>.Node[capacity];
			this._tables = new ConcurrentHashSet<T>.Tables(array3, array, array2);
			this._growLockArray = growLockArray;
			this._budget = array3.Length / array.Length;
			this._comparer = comparer ?? EqualityComparer<T>.Default;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00043EEE File Offset: 0x000420EE
		public bool Add(T item)
		{
			return this.AddInternal(item, this._comparer.GetHashCode(item), true);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00043F04 File Offset: 0x00042104
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentHashSet<T>.Tables tables = new ConcurrentHashSet<T>.Tables(new ConcurrentHashSet<T>.Node[31], this._tables.Locks, new int[this._tables.CountPerLock.Length]);
				this._tables = tables;
				this._budget = Math.Max(1, tables.Buckets.Length / tables.Locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00043F8C File Offset: 0x0004218C
		public bool Contains(T item)
		{
			int hashCode = this._comparer.GetHashCode(item);
			ConcurrentHashSet<T>.Tables tables = this._tables;
			int bucket = ConcurrentHashSet<T>.GetBucket(hashCode, tables.Buckets.Length);
			for (ConcurrentHashSet<T>.Node node = Volatile.Read<ConcurrentHashSet<T>.Node>(ref tables.Buckets[bucket]); node != null; node = node.Next)
			{
				if (hashCode == node.Hashcode && this._comparer.Equals(node.Item, item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00044000 File Offset: 0x00042200
		public bool TryRemove(T item)
		{
			int hashCode = this._comparer.GetHashCode(item);
			for (;;)
			{
				ConcurrentHashSet<T>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentHashSet<T>.GetBucketAndLockNo(hashCode, out num, out num2, tables.Buckets.Length, tables.Locks.Length);
				object obj = tables.Locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentHashSet<T>.Node node = null;
					for (ConcurrentHashSet<T>.Node node2 = tables.Buckets[num]; node2 != null; node2 = node2.Next)
					{
						if (hashCode == node2.Hashcode && this._comparer.Equals(node2.Item, item))
						{
							if (node == null)
							{
								Volatile.Write<ConcurrentHashSet<T>.Node>(ref tables.Buckets[num], node2.Next);
							}
							else
							{
								node.Next = node2.Next;
							}
							tables.CountPerLock[num2]--;
							return true;
						}
						node = node2;
					}
				}
				break;
			}
			return false;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00044110 File Offset: 0x00042310
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00044118 File Offset: 0x00042318
		public IEnumerator<T> GetEnumerator()
		{
			ConcurrentHashSet<T>.Node[] buckets = this._tables.Buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentHashSet<T>.Node current;
				for (current = Volatile.Read<ConcurrentHashSet<T>.Node>(ref buckets[i]); current != null; current = current.Next)
				{
					yield return current.Item;
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00044127 File Offset: 0x00042327
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00044131 File Offset: 0x00042331
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00044134 File Offset: 0x00042334
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				int num3 = 0;
				while (num3 < this._tables.Locks.Length && num2 >= 0)
				{
					num2 += this._tables.CountPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < arrayIndex || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the set is greater than the available space from index to the end of the destination array.");
				}
				this.CopyToItems(array, arrayIndex);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000441D8 File Offset: 0x000423D8
		bool ICollection<T>.Remove(T item)
		{
			return this.TryRemove(item);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000441E4 File Offset: 0x000423E4
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				this.AddInternal(t, this._comparer.GetHashCode(t), false);
			}
			if (this._budget == 0)
			{
				this._budget = this._tables.Buckets.Length / this._tables.Locks.Length;
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00044268 File Offset: 0x00042468
		private bool AddInternal(T item, int hashcode, bool acquireLock)
		{
			checked
			{
				ConcurrentHashSet<T>.Tables tables;
				bool flag;
				for (;;)
				{
					tables = this._tables;
					int num;
					int num2;
					ConcurrentHashSet<T>.GetBucketAndLockNo(hashcode, out num, out num2, tables.Buckets.Length, tables.Locks.Length);
					flag = false;
					bool flag2 = false;
					try
					{
						if (acquireLock)
						{
							Monitor.Enter(tables.Locks[num2], ref flag2);
						}
						if (tables != this._tables)
						{
							continue;
						}
						for (ConcurrentHashSet<T>.Node node = tables.Buckets[num]; node != null; node = node.Next)
						{
							if (hashcode == node.Hashcode && this._comparer.Equals(node.Item, item))
							{
								return false;
							}
						}
						Volatile.Write<ConcurrentHashSet<T>.Node>(ref tables.Buckets[num], new ConcurrentHashSet<T>.Node(item, hashcode, tables.Buckets[num]));
						tables.CountPerLock[num2]++;
						if (tables.CountPerLock[num2] > this._budget)
						{
							flag = true;
						}
					}
					finally
					{
						if (flag2)
						{
							Monitor.Exit(tables.Locks[num2]);
						}
					}
					break;
				}
				if (flag)
				{
					this.GrowTable(tables);
				}
				return true;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00044374 File Offset: 0x00042574
		private static int GetBucket(int hashcode, int bucketCount)
		{
			return (hashcode & int.MaxValue) % bucketCount;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004437F File Offset: 0x0004257F
		private static void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00044394 File Offset: 0x00042594
		private void GrowTable(ConcurrentHashSet<T>.Tables tables)
		{
			int num = 0;
			try
			{
				this.AcquireLocks(0, 1, ref num);
				if (tables == this._tables)
				{
					long num2 = 0L;
					for (int i = 0; i < tables.CountPerLock.Length; i++)
					{
						num2 += (long)tables.CountPerLock[i];
					}
					if (num2 < (long)(tables.Buckets.Length / 4))
					{
						this._budget = 2 * this._budget;
						if (this._budget < 0)
						{
							this._budget = int.MaxValue;
						}
					}
					else
					{
						int num3 = 0;
						bool flag = false;
						object[] array;
						checked
						{
							try
							{
								num3 = tables.Buckets.Length * 2 + 1;
								while (num3 % 3 == 0 || num3 % 5 == 0 || num3 % 7 == 0)
								{
									num3 += 2;
								}
								if (num3 > 2146435071)
								{
									flag = true;
								}
							}
							catch (OverflowException)
							{
								flag = true;
							}
							if (flag)
							{
								num3 = 2146435071;
								this._budget = int.MaxValue;
							}
							this.AcquireLocks(1, tables.Locks.Length, ref num);
							array = tables.Locks;
						}
						if (this._growLockArray && tables.Locks.Length < 1024)
						{
							array = new object[tables.Locks.Length * 2];
							Array.Copy(tables.Locks, 0, array, 0, tables.Locks.Length);
							for (int j = tables.Locks.Length; j < array.Length; j++)
							{
								array[j] = new object();
							}
						}
						ConcurrentHashSet<T>.Node[] array2 = new ConcurrentHashSet<T>.Node[num3];
						int[] array3 = new int[array.Length];
						for (int k = 0; k < tables.Buckets.Length; k++)
						{
							checked
							{
								ConcurrentHashSet<T>.Node next;
								for (ConcurrentHashSet<T>.Node node = tables.Buckets[k]; node != null; node = next)
								{
									next = node.Next;
									int num4;
									int num5;
									ConcurrentHashSet<T>.GetBucketAndLockNo(node.Hashcode, out num4, out num5, array2.Length, array.Length);
									array2[num4] = new ConcurrentHashSet<T>.Node(node.Item, node.Hashcode, array2[num4]);
									array3[num5]++;
								}
							}
						}
						this._budget = Math.Max(1, array2.Length / array.Length);
						this._tables = new ConcurrentHashSet<T>.Tables(array2, array, array3);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000445D4 File Offset: 0x000427D4
		private void AcquireAllLocks(ref int locksAcquired)
		{
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this._tables.Locks.Length, ref locksAcquired);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000445F8 File Offset: 0x000427F8
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this._tables.Locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
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

		// Token: 0x06001414 RID: 5140 RVA: 0x00044648 File Offset: 0x00042848
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this._tables.Locks[i]);
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00044678 File Offset: 0x00042878
		private void CopyToItems(T[] array, int index)
		{
			foreach (ConcurrentHashSet<T>.Node node in this._tables.Buckets)
			{
				while (node != null)
				{
					array[index] = node.Item;
					index++;
					node = node.Next;
				}
			}
		}

		// Token: 0x0400083B RID: 2107
		private const int DefaultCapacity = 31;

		// Token: 0x0400083C RID: 2108
		private const int MaxLockNumber = 1024;

		// Token: 0x0400083D RID: 2109
		private readonly IEqualityComparer<T> _comparer;

		// Token: 0x0400083E RID: 2110
		private readonly bool _growLockArray;

		// Token: 0x0400083F RID: 2111
		private int _budget;

		// Token: 0x04000840 RID: 2112
		private volatile ConcurrentHashSet<T>.Tables _tables;

		// Token: 0x02000456 RID: 1110
		private class Tables
		{
			// Token: 0x06001FBB RID: 8123 RVA: 0x0007054E File Offset: 0x0006E74E
			public Tables(ConcurrentHashSet<T>.Node[] buckets, object[] locks, int[] countPerLock)
			{
				this.Buckets = buckets;
				this.Locks = locks;
				this.CountPerLock = countPerLock;
			}

			// Token: 0x0400132C RID: 4908
			public readonly ConcurrentHashSet<T>.Node[] Buckets;

			// Token: 0x0400132D RID: 4909
			public readonly object[] Locks;

			// Token: 0x0400132E RID: 4910
			public volatile int[] CountPerLock;
		}

		// Token: 0x02000457 RID: 1111
		private class Node
		{
			// Token: 0x06001FBC RID: 8124 RVA: 0x0007056D File Offset: 0x0006E76D
			public Node(T item, int hashcode, ConcurrentHashSet<T>.Node next)
			{
				this.Item = item;
				this.Hashcode = hashcode;
				this.Next = next;
			}

			// Token: 0x0400132F RID: 4911
			public readonly T Item;

			// Token: 0x04001330 RID: 4912
			public readonly int Hashcode;

			// Token: 0x04001331 RID: 4913
			public volatile ConcurrentHashSet<T>.Node Next;
		}
	}
}
