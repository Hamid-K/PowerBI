using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062D RID: 1581
	public class ConcurrentUnboundedCache<TKey, TValue> : UnboundedCache<TKey, TValue>
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000618C4 File Offset: 0x0005FAC4
		public ConcurrentUnboundedCache()
			: this(null, null, null)
		{
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000618CF File Offset: 0x0005FACF
		public ConcurrentUnboundedCache(IEqualityComparer<TKey> equalityComparer = null, Func<TKey, TKey> keyCloner = null, Func<TValue, TValue> valueCloner = null)
			: base(equalityComparer, keyCloner, valueCloner)
		{
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000618DC File Offset: 0x0005FADC
		public override TValue GetOrAdd(TKey key, Func<TKey, TValue> f)
		{
			TValue orAdd;
			lock (this)
			{
				orAdd = base.GetOrAdd(key, f);
			}
			return orAdd;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0006191C File Offset: 0x0005FB1C
		public override TValue AddOrUpdate(TKey key, Func<TKey, TValue> updateValueFunc, Func<TKey, TValue, TValue> insertValueFunc)
		{
			TValue tvalue;
			lock (this)
			{
				tvalue = base.AddOrUpdate(key, updateValueFunc, insertValueFunc);
			}
			return tvalue;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0006195C File Offset: 0x0005FB5C
		public override bool Lookup(TKey key, out TValue value)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Lookup(key, out value);
			}
			return flag2;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0006199C File Offset: 0x0005FB9C
		public override UnboundedCache<TKey, TValue> ShallowClone()
		{
			UnboundedCache<TKey, TValue> unboundedCache;
			lock (this)
			{
				unboundedCache = (ConcurrentUnboundedCache<TKey, TValue>)base.ShallowClone();
			}
			return unboundedCache;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000619E0 File Offset: 0x0005FBE0
		public override UnboundedCache<TKey, TValue> DeepClone()
		{
			UnboundedCache<TKey, TValue> unboundedCache;
			lock (this)
			{
				unboundedCache = (ConcurrentUnboundedCache<TKey, TValue>)base.DeepClone();
			}
			return unboundedCache;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00061A24 File Offset: 0x0005FC24
		public override void Add(TKey key, TValue value)
		{
			lock (this)
			{
				base.Add(key, value);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00061A64 File Offset: 0x0005FC64
		public override bool Replace(TKey key, TValue newValue, out TValue oldValue)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Replace(key, newValue, out oldValue);
			}
			return flag2;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00061AA4 File Offset: 0x0005FCA4
		public override bool Remove(TKey key, out TValue removedValue)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Remove(key, out removedValue);
			}
			return flag2;
		}
	}
}
