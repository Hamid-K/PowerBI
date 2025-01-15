using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public struct RefToDoubleFastHashHelper<TKey> : IFastHashHelper<TKey, double> where TKey : class
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x000281EC File Offset: 0x000263EC
		public bool IsDefault(double v)
		{
			return v == 0.0;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000281FA File Offset: 0x000263FA
		public bool Equals(TKey k1, TKey k2)
		{
			return k1 == k2;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002820A File Offset: 0x0002640A
		public bool Equals(double v1, double v2)
		{
			return v1 == v2;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00028210 File Offset: 0x00026410
		public int GetHashCode(TKey k)
		{
			return k.GetHashCode();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002821D File Offset: 0x0002641D
		public double CompareExchange(ref double location1, double value, double comparand)
		{
			return Interlocked.CompareExchange(ref location1, value, comparand);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00028227 File Offset: 0x00026427
		public double DefaultValue
		{
			get
			{
				return 0.0;
			}
		}
	}
}
