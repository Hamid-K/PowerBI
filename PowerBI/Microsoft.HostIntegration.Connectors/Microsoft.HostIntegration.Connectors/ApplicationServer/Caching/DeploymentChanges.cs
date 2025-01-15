using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E8 RID: 232
	[Flags]
	internal enum DeploymentChanges
	{
		// Token: 0x04000422 RID: 1058
		NoChange = 0,
		// Token: 0x04000423 RID: 1059
		DeploymentModeChange = 1,
		// Token: 0x04000424 RID: 1060
		GracefulShutdownModeChange = 2,
		// Token: 0x04000425 RID: 1061
		ChangeAll = 15
	}
}
