using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public struct UInt32EqualityComparer : IEqualityComparer<uint>
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000023D2 File Offset: 0x000005D2
		public int GetHashCode(uint i)
		{
			return i.GetHashCode();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000023DB File Offset: 0x000005DB
		public bool Equals(uint x, uint y)
		{
			return x == y;
		}

		// Token: 0x04000009 RID: 9
		public static readonly UInt32EqualityComparer Instance;
	}
}
