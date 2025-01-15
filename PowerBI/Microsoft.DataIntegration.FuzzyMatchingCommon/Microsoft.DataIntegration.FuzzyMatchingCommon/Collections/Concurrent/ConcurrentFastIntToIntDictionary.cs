using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public sealed class ConcurrentFastIntToIntDictionary
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0002900E File Offset: 0x0002720E
		public ConcurrentFastIntToIntDictionary()
			: this(0.75f)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002901B File Offset: 0x0002721B
		public ConcurrentFastIntToIntDictionary(float load)
			: this(load, 4 * Environment.ProcessorCount)
		{
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002902C File Offset: 0x0002722C
		public ConcurrentFastIntToIntDictionary(float load, int maxDegreeOfParallelism)
		{
			this.m_load = Math.Min(load, 0.9f);
			this.m_maxDegreeOfParallelism = Math.Max(1, maxDegreeOfParallelism);
			if (!((uint)maxDegreeOfParallelism).IsPowerOf2())
			{
				this.m_maxDegreeOfParallelism = 1 << ((uint)this.m_maxDegreeOfParallelism).Log2() + 1;
			}
			this.m_dopMask = ~(-1 << ((uint)this.m_maxDegreeOfParallelism).Log2());
			this.m_locks = new long[this.m_maxDegreeOfParallelism];
			this.m_atomicState = new ConcurrentFastIntToIntDictionary.AtomicState();
			this.m_mask = 15;
			int num = 16;
			while (num - (int)((float)num * load) < 2 * this.m_maxDegreeOfParallelism)
			{
				num *= 2;
				this.m_mask <<= 1;
				this.m_mask |= 1;
			}
			this.m_buckets = new long[num];
			this.m_maxLoadCount = (int)(load * (float)this.m_buckets.Length) + 1;
			this.m_atomicState = new ConcurrentFastIntToIntDictionary.AtomicState
			{
				m_buckets = this.m_buckets,
				m_mask = this.m_mask,
				m_maxLoadCount = this.m_maxLoadCount
			};
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002914C File Offset: 0x0002734C
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00029154 File Offset: 0x00027354
		public void Add(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			this.GetWriteLock(k);
			int num = k & this.m_mask;
			for (;;)
			{
				long num2 = Interlocked.CompareExchange(ref this.m_buckets[num], ((long)k << 32) | (long)((ulong)v), 0L);
				if (num2 == 0L)
				{
					break;
				}
				if (num2 >> 32 == (long)k)
				{
					goto Block_4;
				}
				num = (num + 1) & this.m_mask;
			}
			if (Interlocked.Increment(ref this.m_count) >= this.m_maxLoadCount)
			{
				this.ReleaseWriteLock(k);
				this.Rehash();
				return;
			}
			this.ReleaseWriteLock(k);
			return;
			Block_4:
			this.ReleaseWriteLock(k);
			throw new ArgumentException("Key already present!");
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000291F0 File Offset: 0x000273F0
		public void Increment(int k, int inc = 1)
		{
			if (inc == 0)
			{
				throw new ArgumentException("Increment may not be zero.");
			}
			this.GetWriteLock(k);
			int num = k & this.m_mask;
			for (;;)
			{
				long num2 = Interlocked.CompareExchange(ref this.m_buckets[num], ((long)k << 32) | (long)((ulong)inc), 0L);
				if (num2 == 0L)
				{
					goto Block_2;
				}
				if (num2 >> 32 == (long)k)
				{
					break;
				}
				num = (num + 1) & this.m_mask;
			}
			while (this.m_buckets[num] != Interlocked.CompareExchange(ref this.m_buckets[num], ((long)k << 32) | (long)((ulong)((int)this.m_buckets[num] + inc)), this.m_buckets[num]))
			{
			}
			this.ReleaseWriteLock(k);
			return;
			Block_2:
			if (Interlocked.Increment(ref this.m_count) >= this.m_maxLoadCount)
			{
				this.ReleaseWriteLock(k);
				this.Rehash();
				return;
			}
			this.ReleaseWriteLock(k);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000292B9 File Offset: 0x000274B9
		private void ReleaseWriteLock(int k)
		{
			this.m_locks[k & this.m_dopMask] = 0L;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000292CC File Offset: 0x000274CC
		private void GetWriteLock(int k)
		{
			for (;;)
			{
				if (this.m_rehashLock != 0)
				{
					Thread.Sleep(0);
				}
				else if (Interlocked.CompareExchange(ref this.m_locks[k & this.m_dopMask], 1L, 0L) == 0L)
				{
					if (this.m_rehashLock == 0)
					{
						break;
					}
					this.ReleaseWriteLock(k);
					Thread.Sleep(0);
				}
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00029324 File Offset: 0x00027524
		public bool TryAdd(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			this.GetWriteLock(k);
			int num = k & this.m_mask;
			while (this.m_buckets[num] != 0L || Interlocked.CompareExchange(ref this.m_buckets[num], ((long)k << 32) | (long)((ulong)v), 0L) != 0L)
			{
				if (this.m_buckets[num] >> 32 == (long)k)
				{
					this.ReleaseWriteLock(k);
					return false;
				}
				num = (num + 1) & this.m_mask;
			}
			if (Interlocked.Increment(ref this.m_count) >= this.m_maxLoadCount)
			{
				this.ReleaseWriteLock(k);
				this.Rehash();
			}
			else
			{
				this.ReleaseWriteLock(k);
			}
			return true;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000293C8 File Offset: 0x000275C8
		public bool ContainsKey(int k)
		{
			ConcurrentFastIntToIntDictionary.AtomicState atomicState = this.m_atomicState;
			long[] buckets = atomicState.m_buckets;
			int mask = atomicState.m_mask;
			int num = k & mask;
			while (buckets[num] != 0L)
			{
				if (buckets[num] >> 32 == (long)k)
				{
					return true;
				}
				num = (num + 1) & mask;
			}
			return false;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002940C File Offset: 0x0002760C
		public bool TryGetValue(int k, out int v)
		{
			ConcurrentFastIntToIntDictionary.AtomicState atomicState = this.m_atomicState;
			long[] buckets = atomicState.m_buckets;
			int mask = atomicState.m_mask;
			int num = k & mask;
			while (buckets[num] != 0L)
			{
				if (buckets[num] >> 32 == (long)k)
				{
					v = (int)buckets[num];
					return true;
				}
				num = (num + 1) & mask;
			}
			v = 0;
			return false;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00029458 File Offset: 0x00027658
		public void Remove(int k)
		{
			this.GetWriteLock(k);
			int num = k & this.m_mask;
			while (this.m_buckets[num] != 0L)
			{
				if (this.m_buckets[num] >> 32 == (long)k)
				{
					this.m_buckets[num] = 0L;
					this.ReleaseWriteLock(k);
					return;
				}
				num = (num + 1) & this.m_mask;
			}
			this.ReleaseWriteLock(k);
			throw new KeyNotFoundException();
		}

		// Token: 0x17000138 RID: 312
		public int this[int k]
		{
			get
			{
				ConcurrentFastIntToIntDictionary.AtomicState atomicState = this.m_atomicState;
				long[] buckets = atomicState.m_buckets;
				int mask = atomicState.m_mask;
				int num = k & mask;
				while (buckets[num] != 0L)
				{
					if (buckets[num] >> 32 == (long)k)
					{
						return (int)buckets[num];
					}
					num = (num + 1) & mask;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (value == 0)
				{
					throw new ArgumentException("Value may not be zero.");
				}
				this.GetWriteLock(k);
				int num = k & this.m_mask;
				for (;;)
				{
					long num2 = Interlocked.CompareExchange(ref this.m_buckets[num], ((long)k << 32) | (long)((ulong)value), 0L);
					if (num2 == 0L)
					{
						break;
					}
					if (num2 >> 32 == (long)k)
					{
						goto Block_4;
					}
					num = (num + 1) & this.m_mask;
				}
				if (Interlocked.Increment(ref this.m_count) >= this.m_maxLoadCount)
				{
					this.ReleaseWriteLock(k);
					this.Rehash();
					return;
				}
				this.ReleaseWriteLock(k);
				return;
				Block_4:
				this.m_buckets[num] = ((long)k << 32) | (long)((ulong)value);
				this.ReleaseWriteLock(k);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000295AC File Offset: 0x000277AC
		private void Rehash()
		{
			lock (this)
			{
				if (this.m_count >= this.m_maxLoadCount)
				{
					this.m_rehashLock = 1;
					if (this.m_mask == 2147483647)
					{
						this.m_rehashLock = 0;
						throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
					}
					for (int i = 0; i < this.m_locks.Length; i++)
					{
						while (Interlocked.CompareExchange(ref this.m_locks[i], 1L, 0L) != 0L)
						{
						}
					}
					long[] buckets = this.m_buckets;
					this.m_buckets = new long[this.m_buckets.Length * 2];
					this.m_mask = (this.m_mask << 1) | 1;
					int count = this.m_count;
					foreach (long num in buckets)
					{
						if (num != 0L)
						{
							int num2 = (int)(num >> 32) & this.m_mask;
							while (this.m_buckets[num2] != 0L)
							{
								num2 = (num2 + 1) & this.m_mask;
							}
							this.m_buckets[num2] = num;
						}
					}
					this.m_maxLoadCount = (int)(this.m_load * (float)this.m_buckets.Length);
					this.m_atomicState = new ConcurrentFastIntToIntDictionary.AtomicState
					{
						m_buckets = this.m_buckets,
						m_mask = this.m_mask,
						m_maxLoadCount = this.m_maxLoadCount
					};
					this.m_rehashLock = 0;
					for (int k = 0; k < this.m_locks.Length; k++)
					{
						this.m_locks[k] = 0L;
					}
				}
			}
		}

		// Token: 0x0400018E RID: 398
		private readonly float m_load = 0.75f;

		// Token: 0x0400018F RID: 399
		private readonly int m_maxDegreeOfParallelism;

		// Token: 0x04000190 RID: 400
		private readonly long[] m_locks;

		// Token: 0x04000191 RID: 401
		private readonly int m_dopMask;

		// Token: 0x04000192 RID: 402
		private volatile int m_rehashLock;

		// Token: 0x04000193 RID: 403
		private volatile ConcurrentFastIntToIntDictionary.AtomicState m_atomicState;

		// Token: 0x04000194 RID: 404
		private long[] m_buckets;

		// Token: 0x04000195 RID: 405
		private int m_mask;

		// Token: 0x04000196 RID: 406
		private int m_maxLoadCount;

		// Token: 0x04000197 RID: 407
		private int m_count;

		// Token: 0x0200014D RID: 333
		[Serializable]
		private class AtomicState
		{
			// Token: 0x0400037F RID: 895
			public long[] m_buckets;

			// Token: 0x04000380 RID: 896
			public int m_mask;

			// Token: 0x04000381 RID: 897
			public int m_maxLoadCount;
		}
	}
}
