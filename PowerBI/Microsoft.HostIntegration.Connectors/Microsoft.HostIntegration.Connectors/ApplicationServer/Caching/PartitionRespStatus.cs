using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000268 RID: 616
	internal enum PartitionRespStatus : byte
	{
		// Token: 0x04000C4D RID: 3149
		Success,
		// Token: 0x04000C4E RID: 3150
		NotPrimary,
		// Token: 0x04000C4F RID: 3151
		CacheDoesNotExist
	}
}
