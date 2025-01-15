using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000632 RID: 1586
	public class IntegerKeyedCache<TValue> : CloneableCache<int, TValue, IntegerKeyedCache<TValue>>
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x00061AE4 File Offset: 0x0005FCE4
		private IntegerKeyedCache(Func<TValue, TValue> valueCloner)
			: base(null, null, valueCloner)
		{
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00061AEF File Offset: 0x0005FCEF
		public IntegerKeyedCache(uint cacheSize, Func<TValue, TValue> valueCloner = null)
			: base(null, null, valueCloner)
		{
			this._cache = new TValue[cacheSize];
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00061B08 File Offset: 0x0005FD08
		private IntegerKeyedCache(IntegerKeyedCache<TValue> other, Func<TValue, TValue> valueCloner = null)
			: base(null, null, null)
		{
			valueCloner = valueCloner.IdentityIfNull<TValue>();
			this._cache = other._cache.Select((TValue v) => valueCloner(v)).ToArray<TValue>();
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00061B5E File Offset: 0x0005FD5E
		public override bool Lookup(int key, out TValue value)
		{
			if (key >= this._cache.Length)
			{
				value = default(TValue);
				return false;
			}
			value = this._cache[key];
			return true;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00061B87 File Offset: 0x0005FD87
		public override IntegerKeyedCache<TValue> ShallowClone()
		{
			return new IntegerKeyedCache<TValue>(this, null);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00061B90 File Offset: 0x0005FD90
		public override IntegerKeyedCache<TValue> DeepClone()
		{
			return new IntegerKeyedCache<TValue>(this, base.ValueCloner);
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00061B9E File Offset: 0x0005FD9E
		public override int Count
		{
			get
			{
				return this._cache.Length;
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00061BA8 File Offset: 0x0005FDA8
		public override void Clear()
		{
			this._cache = new TValue[this._cache.Length];
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x00061BBD File Offset: 0x0005FDBD
		public override IEnumerable<int> Keys
		{
			get
			{
				return Enumerable.Range(0, this._cache.Length);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00061BCD File Offset: 0x0005FDCD
		public override IEnumerable<TValue> Values
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x00061BD5 File Offset: 0x0005FDD5
		public override IEnumerable<KeyValuePair<int, TValue>> Mappings
		{
			get
			{
				return this._cache.Select((TValue v, int idx) => new KeyValuePair<int, TValue>(idx, v));
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00061C04 File Offset: 0x0005FE04
		public override TValue GetOrAdd(int key, Func<int, TValue> f)
		{
			if (this._cache[key] == null)
			{
				TValue tvalue = f(key);
				this.Add(key, tvalue);
				return tvalue;
			}
			TValue tvalue2;
			this.Lookup(key, out tvalue2);
			return tvalue2;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00061C44 File Offset: 0x0005FE44
		public override TValue AddOrUpdate(int key, Func<int, TValue> v, Func<int, TValue, TValue> f)
		{
			if (this._cache[key] == null)
			{
				TValue tvalue = v(key);
				this.Add(key, tvalue);
				return tvalue;
			}
			TValue tvalue2 = f(key, this._cache[key]);
			this.Add(key, tvalue2);
			return tvalue2;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00061C94 File Offset: 0x0005FE94
		public override void Add(int key, TValue value)
		{
			TValue tvalue;
			this.Replace(key, value, out tvalue);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00061CAC File Offset: 0x0005FEAC
		public override bool Replace(int key, TValue value, out TValue oldValue)
		{
			oldValue = this._cache[key];
			this._cache[key] = value;
			return true;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00061CD0 File Offset: 0x0005FED0
		public override bool Remove(int key, out TValue removedValue)
		{
			removedValue = this._cache[key];
			this._cache[key] = default(TValue);
			return true;
		}

		// Token: 0x04001063 RID: 4195
		private TValue[] _cache;
	}
}
