using System;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062B RID: 1579
	public class ConcurrentIntegerKeyedCache<TValue> : IntegerKeyedCache<TValue>
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x00061470 File Offset: 0x0005F670
		public ConcurrentIntegerKeyedCache(uint cacheSize, Func<TValue, TValue> valueCloner = null)
			: base(cacheSize, valueCloner)
		{
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0006147C File Offset: 0x0005F67C
		public override bool Lookup(int key, out TValue value)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Lookup(key, out value);
			}
			return flag2;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000614BC File Offset: 0x0005F6BC
		public override void Clear()
		{
			lock (this)
			{
				base.Clear();
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000614F8 File Offset: 0x0005F6F8
		public override IntegerKeyedCache<TValue> ShallowClone()
		{
			IntegerKeyedCache<TValue> integerKeyedCache;
			lock (this)
			{
				integerKeyedCache = (ConcurrentIntegerKeyedCache<TValue>)base.ShallowClone();
			}
			return integerKeyedCache;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0006153C File Offset: 0x0005F73C
		public override IntegerKeyedCache<TValue> DeepClone()
		{
			IntegerKeyedCache<TValue> integerKeyedCache;
			lock (this)
			{
				integerKeyedCache = (ConcurrentIntegerKeyedCache<TValue>)base.DeepClone();
			}
			return integerKeyedCache;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00061580 File Offset: 0x0005F780
		public override TValue GetOrAdd(int key, Func<int, TValue> insertValueFunc)
		{
			TValue orAdd;
			lock (this)
			{
				orAdd = base.GetOrAdd(key, insertValueFunc);
			}
			return orAdd;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000615C0 File Offset: 0x0005F7C0
		public override TValue AddOrUpdate(int key, Func<int, TValue> updateValueFunc, Func<int, TValue, TValue> insertValueFunc)
		{
			TValue tvalue;
			lock (this)
			{
				tvalue = base.AddOrUpdate(key, updateValueFunc, insertValueFunc);
			}
			return tvalue;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00061600 File Offset: 0x0005F800
		public override void Add(int key, TValue value)
		{
			TValue tvalue;
			this.Replace(key, value, out tvalue);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00061618 File Offset: 0x0005F818
		public override bool Replace(int key, TValue value, out TValue oldValue)
		{
			bool flag2;
			lock (this)
			{
				flag2 = base.Replace(key, value, out oldValue);
			}
			return flag2;
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00061658 File Offset: 0x0005F858
		public override bool Remove(int key, out TValue removedValue)
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
