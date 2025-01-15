using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public struct FastHashHelper<TKey, TValue, TKComparer, TVComparer, TInterlocked> : IFastHashHelper<TKey, TValue> where TKComparer : struct, IEqualityComparer<TKey> where TVComparer : struct, IEqualityComparer<TValue> where TInterlocked : struct, IInterlocked<TValue>
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00028234 File Offset: 0x00026434
		public bool IsDefault(TValue v)
		{
			return this.valueComparer.Equals(v, default(TValue));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002825C File Offset: 0x0002645C
		public bool Equals(TKey k1, TKey k2)
		{
			return this.keyComparer.Equals(k1, k2);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00028271 File Offset: 0x00026471
		public bool Equals(TValue v1, TValue v2)
		{
			return this.valueComparer.Equals(v1, v2);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00028286 File Offset: 0x00026486
		public int GetHashCode(TKey k)
		{
			return k.GetHashCode();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00028295 File Offset: 0x00026495
		public TValue CompareExchange(ref TValue location1, TValue value, TValue comparand)
		{
			return this.interlocked.CompareExchange(ref location1, value, comparand);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000282AC File Offset: 0x000264AC
		public TValue DefaultValue
		{
			get
			{
				return default(TValue);
			}
		}

		// Token: 0x04000180 RID: 384
		private TKComparer keyComparer;

		// Token: 0x04000181 RID: 385
		private TVComparer valueComparer;

		// Token: 0x04000182 RID: 386
		private TInterlocked interlocked;
	}
}
