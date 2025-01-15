using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public struct Int32EqualityComparerWithRobustHashing : IEqualityComparer<int>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000023C2 File Offset: 0x000005C2
		public int GetHashCode(int i)
		{
			return Utilities.GetHashCode(i);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000023CA File Offset: 0x000005CA
		public bool Equals(int x, int y)
		{
			return x == y;
		}

		// Token: 0x04000008 RID: 8
		public static readonly Int32EqualityComparerWithRobustHashing Instance;
	}
}
