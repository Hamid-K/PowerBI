using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E5 RID: 229
	[Flags]
	internal enum VersionPropertiesChanges
	{
		// Token: 0x04000405 RID: 1029
		NoChange = 0,
		// Token: 0x04000406 RID: 1030
		BeginClientVersionChange = 1,
		// Token: 0x04000407 RID: 1031
		EndClientVersionChange = 2,
		// Token: 0x04000408 RID: 1032
		BeginServerVersionChange = 4,
		// Token: 0x04000409 RID: 1033
		EndServerVersionChange = 8,
		// Token: 0x0400040A RID: 1034
		ChangeAll = 15
	}
}
