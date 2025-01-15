using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public struct UInt32EqualityComparerWithRobustHashing : IEqualityComparer<uint>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000023E3 File Offset: 0x000005E3
		public int GetHashCode(uint i)
		{
			return Utilities.GetHashCode((long)((ulong)i));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000023EC File Offset: 0x000005EC
		public bool Equals(uint x, uint y)
		{
			return x == y;
		}

		// Token: 0x0400000A RID: 10
		public static readonly UInt32EqualityComparerWithRobustHashing Instance;
	}
}
