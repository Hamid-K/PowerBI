using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E6 RID: 230
	[Flags]
	internal enum StoreVersionChanges
	{
		// Token: 0x0400040C RID: 1036
		NoChange = 0,
		// Token: 0x0400040D RID: 1037
		ClusterConfigStoreVersionChange = 1,
		// Token: 0x0400040E RID: 1038
		ChangeAll = 15
	}
}
