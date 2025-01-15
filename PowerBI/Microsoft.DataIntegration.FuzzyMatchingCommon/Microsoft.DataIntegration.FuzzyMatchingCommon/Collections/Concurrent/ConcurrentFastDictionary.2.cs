using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class ConcurrentFastDictionary<TKey, TValue, THashHelper> : IFastDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where THashHelper : struct, IFastHashHelper<TKey, TValue>
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0002831A File Offset: 0x0002651A
		public ConcurrentFastDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items)
			: this(0.7f)
		{
			items.ForEach(delegate(KeyValuePair<TKey, TValue> kv)
			{
				this.Add(kv.Key, kv.Value);
			});
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00028339 File Offset: 0x00026539
		public ConcurrentFastDictionary(float load = 0.7f)
			: this(load, 8 * Environment.ProcessorCount)
		{
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002834C File Offset: 0x0002654C
		public ConcurrentFastDictionary(float load, int maxDegreeOfParallelism)
		{
			this.m_maxLoad = Math.Min(load, 0.9f);
			this.m_maxDegreeOfParallelism = Math.Max(1, maxDegreeOfParallelism);
			if (!((uint)maxDegreeOfParallelism).IsPowerOf2())
			{
				this.m_maxDegreeOfParallelism = 1 << ((uint)this.m_maxDegreeOfParallelism).Log2() + 1;
			}
			this.m_dopMask = ~(-1 << ((uint)this.m_maxDegreeOfParallelism).Log2());
			this.m_locks = new long[this.m_maxDegreeOfParallelism];
			this.Clear();
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000283D8 File Offset: 0x000265D8
		public TValue DefaultValue
		{
			get
			{
				THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				return helper.DefaultValue;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000283F8 File Offset: 0x000265F8
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00028400 File Offset: 0x00026600
		public void Increment(TKey key, int increment = 1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00028408 File Offset: 0x00026608
		public void Add(TKey k, TValue v)
		{
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			if (thashHelper.IsDefault(v))
			{
				throw new ArgumentException("Value may not be default.");
			}
			thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int hashCode = thashHelper.GetHashCode(k);
			this.AcquireWriteLock(hashCode);
			int num = hashCode & this.m_mask;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				TValue tvalue = thashHelper.CompareExchange(ref this.m_buckets[num].Value, v, default(TValue));
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(tvalue))
				{
					break;
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(this.m_buckets[num].Key, k))
				{
					goto Block_4;
				}
				num = (num + 1) & this.m_mask;
			}
			this.m_buckets[num].Key = k;
			Interlocked.Increment(ref this.m_count);
			this.ReleaseWriteLock(hashCode);
			if (this.RehashNeeded())
			{
				this.Rehash();
			}
			return;
			Block_4:
			this.ReleaseWriteLock(hashCode);
			throw new ArgumentException("Key already present!");
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0002851F File Offset: 0x0002671F
		private static void Yield()
		{
			Thread.Sleep(0);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00028528 File Offset: 0x00026728
		private void AcquireGlobalLock()
		{
			for (;;)
			{
				lock (this)
				{
					if (this.m_globalLockRequested == 0)
					{
						this.m_globalLockRequested = 1;
						for (int i = 0; i < this.m_locks.Length; i++)
						{
							while (Interlocked.CompareExchange(ref this.m_locks[i], 1L, 0L) != 0L)
							{
								ConcurrentFastDictionary<TKey, TValue, THashHelper>.Yield();
							}
						}
						break;
					}
				}
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.Yield();
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000285A4 File Offset: 0x000267A4
		private void ReleaseGlobalLock()
		{
			lock (this)
			{
				for (int i = 0; i < this.m_locks.Length; i++)
				{
					this.m_locks[i] = 0L;
				}
				this.m_globalLockRequested = 0;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000285F8 File Offset: 0x000267F8
		private void ReleaseWriteLock(int kHash)
		{
			this.m_locks[kHash & this.m_dopMask] = 0L;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002860C File Offset: 0x0002680C
		private void AcquireWriteLock(int kHash)
		{
			for (;;)
			{
				if (this.m_globalLockRequested != 0)
				{
					ConcurrentFastDictionary<TKey, TValue, THashHelper>.Yield();
				}
				else if (Interlocked.CompareExchange(ref this.m_locks[kHash & this.m_dopMask], 1L, 0L) == 0L)
				{
					if (this.m_globalLockRequested == 0)
					{
						break;
					}
					this.ReleaseWriteLock(kHash);
					ConcurrentFastDictionary<TKey, TValue, THashHelper>.Yield();
				}
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00028660 File Offset: 0x00026860
		public TValue AddOrUpdate(TKey k, Func<TKey, TValue> addFunc, Func<TKey, TValue, TValue> updateFunc)
		{
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int hashCode = thashHelper.GetHashCode(k);
			this.AcquireWriteLock(hashCode);
			int num = hashCode & this.m_mask;
			TValue tvalue;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(this.m_buckets[num].Value))
				{
					tvalue = addFunc.Invoke(k);
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.IsDefault(tvalue))
					{
						break;
					}
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.IsDefault(helper.CompareExchange(ref this.m_buckets[num].Value, tvalue, default(TValue))))
					{
						goto Block_3;
					}
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(this.m_buckets[num].Key, k))
				{
					goto Block_5;
				}
				num = (num + 1) & this.m_mask;
			}
			this.ReleaseWriteLock(hashCode);
			throw new ArgumentException("Value may not be default.");
			Block_3:
			this.m_buckets[num].Key = k;
			Interlocked.Increment(ref this.m_count);
			this.ReleaseWriteLock(hashCode);
			if (this.RehashNeeded())
			{
				this.Rehash();
			}
			return tvalue;
			Block_5:
			tvalue = updateFunc.Invoke(k, this.m_buckets[num].Value);
			thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			if (thashHelper.IsDefault(tvalue))
			{
				this.ReleaseWriteLock(hashCode);
				throw new ArgumentException("Value may not be default.");
			}
			this.m_buckets[num].Value = tvalue;
			this.ReleaseWriteLock(hashCode);
			return tvalue;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000287FC File Offset: 0x000269FC
		public bool TryAdd(TKey k, TValue v)
		{
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			if (thashHelper.IsDefault(v))
			{
				throw new ArgumentException("Value may not be default.");
			}
			thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int hashCode = thashHelper.GetHashCode(k);
			this.AcquireWriteLock(hashCode);
			int num = hashCode & this.m_mask;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(this.m_buckets[num].Value))
				{
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.IsDefault(helper.CompareExchange(ref this.m_buckets[num].Value, v, default(TValue))))
					{
						break;
					}
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(this.m_buckets[num].Key, k))
				{
					goto Block_5;
				}
				num = (num + 1) & this.m_mask;
			}
			this.m_buckets[num].Key = k;
			Interlocked.Increment(ref this.m_count);
			this.ReleaseWriteLock(hashCode);
			if (this.RehashNeeded())
			{
				this.Rehash();
			}
			return true;
			Block_5:
			this.ReleaseWriteLock(hashCode);
			return false;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002892F File Offset: 0x00026B2F
		public IEnumerable<TValue> Values
		{
			get
			{
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
				int num;
				for (int i = 0; i < buckets.Length; i = num + 1)
				{
					THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (!helper.IsDefault(buckets[i].Value))
					{
						yield return buckets[i].Value;
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002893F File Offset: 0x00026B3F
		public IEnumerable<TKey> Keys
		{
			get
			{
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
				int num;
				for (int i = 0; i < buckets.Length; i = num + 1)
				{
					THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (!helper.IsDefault(buckets[i].Value))
					{
						yield return buckets[i].Key;
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002894F File Offset: 0x00026B4F
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0002895C File Offset: 0x00026B5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00028969 File Offset: 0x00026B69
		public IEnumerable<KeyValuePair<TKey, TValue>> Items
		{
			get
			{
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
				int num;
				for (int i = 0; i < buckets.Length; i = num + 1)
				{
					THashHelper helper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (!helper.IsDefault(buckets[i].Value))
					{
						yield return new KeyValuePair<TKey, TValue>(buckets[i].Key, buckets[i].Value);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002897C File Offset: 0x00026B7C
		public bool ContainsKey(TKey k)
		{
			ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
			ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
			int mask = atomicState.m_mask;
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int num = thashHelper.GetHashCode(k) & mask;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(buckets[num].Value))
				{
					return false;
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(buckets[num].Key, k))
				{
					break;
				}
				num = (num + 1) & mask;
			}
			return true;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00028A08 File Offset: 0x00026C08
		public bool TryGetValue(TKey k, out TValue v)
		{
			ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
			ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
			int mask = atomicState.m_mask;
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int num = thashHelper.GetHashCode(k) & mask;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(buckets[num].Value))
				{
					goto Block_2;
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(buckets[num].Key, k))
				{
					break;
				}
				num = (num + 1) & mask;
			}
			v = buckets[num].Value;
			return true;
			Block_2:
			v = default(TValue);
			return false;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00028AAC File Offset: 0x00026CAC
		public void Remove(TKey k)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00028AC0 File Offset: 0x00026CC0
		public bool TryRemove(TKey k)
		{
			THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
			int hashCode = thashHelper.GetHashCode(k);
			this.AcquireWriteLock(hashCode);
			int num = hashCode & this.m_mask;
			for (;;)
			{
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(this.m_buckets[num].Value))
				{
					goto Block_2;
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.Equals(this.m_buckets[num].Key, k))
				{
					break;
				}
				num = (num + 1) & this.m_mask;
			}
			this.m_buckets[num].Key = default(TKey);
			this.m_buckets[num].Value = default(TValue);
			this.ReleaseWriteLock(hashCode);
			Interlocked.Decrement(ref this.m_count);
			return true;
			Block_2:
			this.ReleaseWriteLock(hashCode);
			return false;
		}

		// Token: 0x17000136 RID: 310
		public TValue this[TKey k]
		{
			get
			{
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState atomicState = this.m_atomicState;
				ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = atomicState.m_buckets;
				int mask = atomicState.m_mask;
				THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				int num = thashHelper.GetHashCode(k) & mask;
				for (;;)
				{
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.IsDefault(buckets[num].Value))
					{
						goto Block_2;
					}
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.Equals(buckets[num].Key, k))
					{
						break;
					}
					num = (num + 1) & mask;
				}
				return buckets[num].Value;
				Block_2:
				throw new KeyNotFoundException();
			}
			set
			{
				THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				if (thashHelper.IsDefault(value))
				{
					throw new ArgumentException("Value may not be default.");
				}
				thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
				int hashCode = thashHelper.GetHashCode(k);
				this.AcquireWriteLock(hashCode);
				int num = hashCode & this.m_mask;
				for (;;)
				{
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.IsDefault(this.m_buckets[num].Value))
					{
						thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
						TValue tvalue = thashHelper.CompareExchange(ref this.m_buckets[num].Value, value, default(TValue));
						thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
						if (thashHelper.IsDefault(tvalue))
						{
							break;
						}
					}
					thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
					if (thashHelper.Equals(this.m_buckets[num].Key, k))
					{
						goto Block_5;
					}
					num = (num + 1) & this.m_mask;
				}
				this.m_buckets[num].Key = k;
				Interlocked.Increment(ref this.m_count);
				this.ReleaseWriteLock(hashCode);
				if (this.RehashNeeded())
				{
					this.Rehash();
				}
				return;
				Block_5:
				this.m_buckets[num].Value = value;
				this.ReleaseWriteLock(hashCode);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00028D7C File Offset: 0x00026F7C
		public void Clear()
		{
			try
			{
				this.AcquireGlobalLock();
				this.m_mask = 15;
				int num = 16;
				while (num - (int)((float)num * this.m_maxLoad) < 2 * this.m_maxDegreeOfParallelism)
				{
					num *= 2;
					this.m_mask <<= 1;
					this.m_mask |= 1;
				}
				this.m_count = 0;
				this.m_buckets = new ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[num];
				this.m_maxLoadCount = (int)(this.m_maxLoad * (float)this.m_buckets.Length) + 1;
				this.m_atomicState = new ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState
				{
					m_buckets = this.m_buckets,
					m_mask = this.m_mask,
					m_maxLoadCount = this.m_maxLoadCount
				};
			}
			finally
			{
				this.ReleaseGlobalLock();
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00028E48 File Offset: 0x00027048
		private bool RehashNeeded()
		{
			return this.m_count >= this.m_maxLoadCount;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00028E5C File Offset: 0x0002705C
		private void Rehash()
		{
			if (this.m_count >= this.m_maxLoadCount)
			{
				try
				{
					this.AcquireGlobalLock();
					if (this.m_count >= this.m_maxLoadCount)
					{
						if (this.m_mask == 2147483647)
						{
							throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
						}
						ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] buckets = this.m_buckets;
						this.m_buckets = new ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[this.m_buckets.Length * 2];
						this.m_mask = (this.m_mask << 1) | 1;
						int count = this.m_count;
						foreach (ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket bucket in buckets)
						{
							THashHelper thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
							if (!thashHelper.IsDefault(bucket.Value))
							{
								thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
								int num = thashHelper.GetHashCode(bucket.Key) & this.m_mask;
								for (;;)
								{
									thashHelper = ConcurrentFastDictionary<TKey, TValue, THashHelper>.m_helper;
									if (thashHelper.IsDefault(this.m_buckets[num].Value))
									{
										break;
									}
									num = (num + 1) & this.m_mask;
								}
								this.m_buckets[num] = bucket;
							}
						}
						this.m_maxLoadCount = (int)(this.m_maxLoad * (float)this.m_buckets.Length);
						this.m_atomicState = new ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState
						{
							m_buckets = this.m_buckets,
							m_mask = this.m_mask,
							m_maxLoadCount = this.m_maxLoadCount
						};
					}
				}
				finally
				{
					this.ReleaseGlobalLock();
				}
			}
		}

		// Token: 0x04000183 RID: 387
		private static readonly THashHelper m_helper = new THashHelper();

		// Token: 0x04000184 RID: 388
		private readonly float m_maxLoad = 0.75f;

		// Token: 0x04000185 RID: 389
		private readonly int m_maxDegreeOfParallelism;

		// Token: 0x04000186 RID: 390
		private readonly long[] m_locks;

		// Token: 0x04000187 RID: 391
		private readonly int m_dopMask;

		// Token: 0x04000188 RID: 392
		private volatile int m_globalLockRequested;

		// Token: 0x04000189 RID: 393
		private volatile ConcurrentFastDictionary<TKey, TValue, THashHelper>.AtomicState m_atomicState;

		// Token: 0x0400018A RID: 394
		private ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] m_buckets;

		// Token: 0x0400018B RID: 395
		private int m_mask;

		// Token: 0x0400018C RID: 396
		private int m_maxLoadCount;

		// Token: 0x0400018D RID: 397
		private int m_count;

		// Token: 0x02000148 RID: 328
		[DebuggerDisplay("Key={Key} Value={Value}")]
		[Serializable]
		public struct Bucket
		{
			// Token: 0x04000368 RID: 872
			public TKey Key;

			// Token: 0x04000369 RID: 873
			public TValue Value;
		}

		// Token: 0x02000149 RID: 329
		[Serializable]
		private class AtomicState
		{
			// Token: 0x0400036A RID: 874
			public ConcurrentFastDictionary<TKey, TValue, THashHelper>.Bucket[] m_buckets;

			// Token: 0x0400036B RID: 875
			public int m_mask;

			// Token: 0x0400036C RID: 876
			public int m_maxLoadCount;
		}
	}
}
