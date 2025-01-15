using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public struct Int64EqualityComparer : IEqualityComparer<long>
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000023F4 File Offset: 0x000005F4
		public int GetHashCode(long i)
		{
			return i.GetHashCode();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000023FD File Offset: 0x000005FD
		public bool Equals(long x, long y)
		{
			return x == y;
		}

		// Token: 0x0400000B RID: 11
		public static readonly Int64EqualityComparer Instance;
	}
}
