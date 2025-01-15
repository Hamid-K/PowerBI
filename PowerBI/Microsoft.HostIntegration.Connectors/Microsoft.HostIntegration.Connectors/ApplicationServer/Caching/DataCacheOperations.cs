using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200025B RID: 603
	[Flags]
	public enum DataCacheOperations
	{
		// Token: 0x04000C1C RID: 3100
		AddItem = 1,
		// Token: 0x04000C1D RID: 3101
		ReplaceItem = 2,
		// Token: 0x04000C1E RID: 3102
		RemoveItem = 4,
		// Token: 0x04000C1F RID: 3103
		CreateRegion = 8,
		// Token: 0x04000C20 RID: 3104
		RemoveRegion = 16,
		// Token: 0x04000C21 RID: 3105
		ClearRegion = 32
	}
}
