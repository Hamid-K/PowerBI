using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public struct UInt64EqualityComparerWithRobustHashing : IEqualityComparer<ulong>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002426 File Offset: 0x00000626
		public int GetHashCode(ulong i)
		{
			return Utilities.GetHashCode((long)i);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000242E File Offset: 0x0000062E
		public bool Equals(ulong x, ulong y)
		{
			return x == y;
		}

		// Token: 0x0400000E RID: 14
		public static readonly UInt64EqualityComparerWithRobustHashing Instance;
	}
}
