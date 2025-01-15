using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039E RID: 926
	[Flags]
	public enum EtwEventsReaderOptions
	{
		// Token: 0x0400099A RID: 2458
		None = 0,
		// Token: 0x0400099B RID: 2459
		CreateEtwEventMessageField = 1,
		// Token: 0x0400099C RID: 2460
		SwallowDeleteFilesErrors = 2
	}
}
