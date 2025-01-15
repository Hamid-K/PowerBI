using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E9 RID: 233
	[Flags]
	internal enum CacheChanges
	{
		// Token: 0x04000427 RID: 1063
		NoChange = 0,
		// Token: 0x04000428 RID: 1064
		WBChange = 1,
		// Token: 0x04000429 RID: 1065
		RTChange = 2,
		// Token: 0x0400042A RID: 1066
		ChangeAll = 3
	}
}
