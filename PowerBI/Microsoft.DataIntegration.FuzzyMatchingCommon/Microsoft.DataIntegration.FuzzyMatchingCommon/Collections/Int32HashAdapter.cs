using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000082 RID: 130
	public struct Int32HashAdapter : IHashAdapter<Int32HashAdapter, int>
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x00020A68 File Offset: 0x0001EC68
		public bool IsDefault2(int v)
		{
			return v == 0;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00020A6E File Offset: 0x0001EC6E
		public bool Equals(int x, int y)
		{
			return x == y;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00020A74 File Offset: 0x0001EC74
		public bool Equals3(int x, int y)
		{
			return x == y;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00020A7A File Offset: 0x0001EC7A
		public int GetBucket2(int v, int mask)
		{
			return v & mask;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00020A7F File Offset: 0x0001EC7F
		public void Serialize(Stream s)
		{
			StreamUtilities.WriteInt32(s, 0);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00020A88 File Offset: 0x0001EC88
		public Int32HashAdapter Deserialize(Stream s)
		{
			return default(Int32HashAdapter);
		}
	}
}
