using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public struct IntToIntFastHashHelper : IFastHashHelper<int, int>
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x000281CA File Offset: 0x000263CA
		public bool IsDefault(int v)
		{
			return v == 0;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000281D0 File Offset: 0x000263D0
		public bool Equals(int k1, int k2)
		{
			return k1 == k2;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000281D6 File Offset: 0x000263D6
		public int GetHashCode(int k)
		{
			return k.GetHashCode();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000281DF File Offset: 0x000263DF
		public int CompareExchange(ref int location1, int value, int comparand)
		{
			return Interlocked.CompareExchange(ref location1, value, comparand);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000281E9 File Offset: 0x000263E9
		public int DefaultValue
		{
			get
			{
				return 0;
			}
		}
	}
}
