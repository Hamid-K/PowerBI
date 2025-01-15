using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A0 RID: 928
	[Flags]
	public enum EventsRepositoryOptions
	{
		// Token: 0x0400099E RID: 2462
		None = 0,
		// Token: 0x0400099F RID: 2463
		DecompressOnEventFilesDownload = 1,
		// Token: 0x040009A0 RID: 2464
		SkipCorruptFiles = 2
	}
}
