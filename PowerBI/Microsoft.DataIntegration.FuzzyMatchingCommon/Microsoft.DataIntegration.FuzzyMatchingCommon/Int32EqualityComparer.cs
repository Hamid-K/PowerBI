using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public struct Int32EqualityComparer : IEqualityComparer<int>
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000023B1 File Offset: 0x000005B1
		public int GetHashCode(int i)
		{
			return i.GetHashCode();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000023BA File Offset: 0x000005BA
		public bool Equals(int x, int y)
		{
			return x == y;
		}

		// Token: 0x04000007 RID: 7
		public static readonly Int32EqualityComparer Instance;
	}
}
