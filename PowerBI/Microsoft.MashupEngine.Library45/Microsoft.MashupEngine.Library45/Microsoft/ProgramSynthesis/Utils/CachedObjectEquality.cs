using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000403 RID: 1027
	public class CachedObjectEquality<T> : IEqualityComparer<T> where T : class, ICachedEquatable<T>, IEquatable<T>
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00047010 File Offset: 0x00045210
		public static CachedObjectEquality<T> Instance
		{
			get
			{
				return CachedObjectEquality<T>._instance.Value;
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0004701C File Offset: 0x0004521C
		private CachedObjectEquality()
		{
			this._equalityCache = new ConcurrentLruCache<T, Bucket>(4096, IdentityEquality.Comparer, null, null);
			this._nextUID = 0L;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00047044 File Offset: 0x00045244
		public static void Clear()
		{
			object @lock = CachedObjectEquality<T>._lock;
			lock (@lock)
			{
				CachedObjectEquality<T>._instance.Dispose();
				CachedObjectEquality<T>._instance = new ThreadLocal<CachedObjectEquality<T>>(() => new CachedObjectEquality<T>());
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000470B0 File Offset: 0x000452B0
		private Bucket GetBucketForObject(T obj)
		{
			Bucket bucket;
			if (!this._equalityCache.Lookup(obj, out bucket))
			{
				return null;
			}
			return bucket;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000470D0 File Offset: 0x000452D0
		private long CreateBucketId()
		{
			long num = Interlocked.Increment(ref this._nextUID);
			if (num != 9223372036854775807L)
			{
				return num;
			}
			this._nextUID = 1L;
			this._equalityCache = new ConcurrentLruCache<T, Bucket>(4096, IdentityEquality.Comparer, null, null);
			return 0L;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00047118 File Offset: 0x00045318
		private Bucket CreateBucket(int hashCode)
		{
			return new Bucket(this.CreateBucketId(), hashCode);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00047126 File Offset: 0x00045326
		private bool IsEqual(T obj0, T obj1)
		{
			return obj0.NonCachedEquals(obj1);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00047134 File Offset: 0x00045334
		private bool CheckEquals(T obj0, T obj1)
		{
			Bucket bucket = this.GetBucketForObject(obj0);
			Bucket bucket2 = this.GetBucketForObject(obj1);
			if (bucket != null && bucket2 != null)
			{
				if (bucket.Equals(bucket2))
				{
					return true;
				}
				if (bucket.HashCode == bucket2.HashCode && this.IsEqual(obj0, obj1))
				{
					bucket.Merge(bucket2);
					return true;
				}
				return false;
			}
			else
			{
				if (this.IsEqual(obj0, obj1))
				{
					if (bucket != null)
					{
						this._equalityCache.Add(obj1, bucket);
					}
					else if (bucket2 != null)
					{
						this._equalityCache.Add(obj0, bucket2);
					}
					else
					{
						Bucket bucket3 = this.CreateBucket(obj0.GetHashCode());
						this._equalityCache.Add(obj0, bucket3);
						this._equalityCache.Add(obj1, bucket3);
					}
					return true;
				}
				if (bucket == null)
				{
					bucket = this.CreateBucket(obj0.GetHashCode());
					this._equalityCache.Add(obj0, bucket);
				}
				if (bucket2 == null)
				{
					bucket2 = this.CreateBucket(obj1.GetHashCode());
					this._equalityCache.Add(obj1, bucket2);
				}
				return false;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00047226 File Offset: 0x00045426
		public bool Equals(T x, T y)
		{
			return x == y || (x == null == (y == null) && this.CheckEquals(x, y));
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00047256 File Offset: 0x00045456
		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x04000B28 RID: 2856
		private static ThreadLocal<CachedObjectEquality<T>> _instance = new ThreadLocal<CachedObjectEquality<T>>(() => new CachedObjectEquality<T>());

		// Token: 0x04000B29 RID: 2857
		private static readonly object _lock = new object();

		// Token: 0x04000B2A RID: 2858
		private LruCache<T, Bucket> _equalityCache;

		// Token: 0x04000B2B RID: 2859
		private long _nextUID;
	}
}
