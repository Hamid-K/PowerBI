using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public struct LongToRefFastHashHelper<TValue> : IFastHashHelper<long, TValue> where TValue : class
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x00028126 File Offset: 0x00026326
		public bool IsDefault(TValue v)
		{
			return v == null;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00028131 File Offset: 0x00026331
		public bool Equals(long k1, long k2)
		{
			return k1 == k2;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00028137 File Offset: 0x00026337
		public bool Equals(TValue v1, TValue v2)
		{
			return v1 == v2;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00028147 File Offset: 0x00026347
		public int GetHashCode(long k)
		{
			return k.GetHashCode();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00028150 File Offset: 0x00026350
		public TValue CompareExchange(ref TValue location1, TValue value, TValue comparand)
		{
			return Interlocked.CompareExchange<TValue>(ref location1, value, comparand);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0002815C File Offset: 0x0002635C
		public TValue DefaultValue
		{
			get
			{
				return default(TValue);
			}
		}
	}
}
