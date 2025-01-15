using System;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000411 RID: 1041
	public struct RawCountKey
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0007EFA4 File Offset: 0x0007D1A4
		public RawCountKey(int hashId, long hashValue)
		{
			this.HashId = hashId;
			this.HashValue = hashValue;
		}

		// Token: 0x04000D5B RID: 3419
		public readonly int HashId;

		// Token: 0x04000D5C RID: 3420
		public readonly long HashValue;
	}
}
