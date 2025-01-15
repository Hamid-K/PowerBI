using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public struct UInt64EqualityComparer : IEqualityComparer<ulong>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002415 File Offset: 0x00000615
		public int GetHashCode(ulong i)
		{
			return i.GetHashCode();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000241E File Offset: 0x0000061E
		public bool Equals(ulong x, ulong y)
		{
			return x == y;
		}

		// Token: 0x0400000D RID: 13
		public static readonly UInt64EqualityComparer Instance;
	}
}
