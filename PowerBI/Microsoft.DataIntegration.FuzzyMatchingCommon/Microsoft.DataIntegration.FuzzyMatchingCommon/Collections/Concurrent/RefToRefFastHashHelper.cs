using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public struct RefToRefFastHashHelper<TKey, TValue> : IFastHashHelper<TKey, TValue> where TKey : class where TValue : class
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00028172 File Offset: 0x00026372
		public bool IsDefault(TValue v)
		{
			return v == null;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002817D File Offset: 0x0002637D
		public bool Equals(TKey k1, TKey k2)
		{
			return k1 == k2;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002818D File Offset: 0x0002638D
		public bool Equals(TValue v1, TValue v2)
		{
			return v1 == v2;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002819D File Offset: 0x0002639D
		public int GetHashCode(TKey k)
		{
			return k.GetHashCode();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000281AA File Offset: 0x000263AA
		public TValue CompareExchange(ref TValue location1, TValue value, TValue comparand)
		{
			return Interlocked.CompareExchange<TValue>(ref location1, value, comparand);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000281B4 File Offset: 0x000263B4
		public TValue DefaultValue
		{
			get
			{
				return default(TValue);
			}
		}
	}
}
