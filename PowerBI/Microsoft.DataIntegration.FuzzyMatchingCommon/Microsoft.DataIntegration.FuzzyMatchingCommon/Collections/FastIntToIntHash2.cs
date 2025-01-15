using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	public sealed class FastIntToIntHash2 : FastIntToIntHash2<int, int, Int32HashAdapter, Int32HashAdapter>
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00020AA0 File Offset: 0x0001ECA0
		public FastIntToIntHash2(float load)
			: base(load)
		{
			this.KeyIsDefault = (int k) => k == 0;
			this.ValueIsDefault = (int v) => v == 0;
			this.KeyEqual = (int x, int y) => x == y;
			this.ValueEqual = (int x, int y) => x == y;
			this.GetBucket = (int k, int m) => k & m;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00020B70 File Offset: 0x0001ED70
		public bool TryAdd2(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			int num = k & this.mask;
			while (this.buckets[num].v != 0)
			{
				if (k == this.buckets[num].k)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].k = k;
			this.buckets[num].v = v;
			int num2 = this.count + 1;
			this.count = num2;
			if (num2 > this.m_loadMark)
			{
				base.Rehash();
			}
			return true;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00020C10 File Offset: 0x0001EE10
		public new bool TryAdd(int k, int v)
		{
			if (v == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			int num = k & this.mask;
			for (;;)
			{
				if (this.buckets[num].v == 0)
				{
					int num2 = Interlocked.CompareExchange(ref this.buckets[num].k, k, 0);
					if (num2 == 0)
					{
						break;
					}
					if (num2 == k)
					{
						return false;
					}
				}
				else if (this.buckets[num].k == k)
				{
					return false;
				}
				num = (num + 1) & this.mask;
			}
			this.buckets[num].v = v;
			if (Interlocked.Increment(ref this.count) > this.m_loadMark)
			{
				base.Rehash();
			}
			return true;
		}
	}
}
