using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public struct IntToRefFastHashHelper<TValue> : IFastHashHelper<int, TValue> where TValue : class
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x000280D9 File Offset: 0x000262D9
		public bool IsDefault(TValue v)
		{
			return v == null;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000280E4 File Offset: 0x000262E4
		public bool Equals(int k1, int k2)
		{
			return k1 == k2;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000280EA File Offset: 0x000262EA
		public bool Equals(TValue v1, TValue v2)
		{
			return v1 == v2;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000280FA File Offset: 0x000262FA
		public int GetHashCode(int k)
		{
			return k.GetHashCode();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00028103 File Offset: 0x00026303
		public TValue CompareExchange(ref TValue location1, TValue value, TValue comparand)
		{
			return Interlocked.CompareExchange<TValue>(ref location1, value, comparand);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00028110 File Offset: 0x00026310
		public TValue DefaultValue
		{
			get
			{
				return default(TValue);
			}
		}
	}
}
