using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public struct Int64EqualityComparerWithRobustHashing : IEqualityComparer<long>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002405 File Offset: 0x00000605
		public int GetHashCode(long i)
		{
			return Utilities.GetHashCode(i);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000240D File Offset: 0x0000060D
		public bool Equals(long x, long y)
		{
			return x == y;
		}

		// Token: 0x0400000C RID: 12
		public static readonly Int64EqualityComparerWithRobustHashing Instance;
	}
}
