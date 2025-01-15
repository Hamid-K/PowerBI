using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039A RID: 922
	[Flags]
	public enum EventsQueryOptions
	{
		// Token: 0x04000991 RID: 2449
		None = 0,
		// Token: 0x04000992 RID: 2450
		RetainDownloadedFiles = 1,
		// Token: 0x04000993 RID: 2451
		SwallowDeleteFilesErrors = 2,
		// Token: 0x04000994 RID: 2452
		ReconstructMessage = 4,
		// Token: 0x04000995 RID: 2453
		UseDeployTimeManifests = 8,
		// Token: 0x04000996 RID: 2454
		SkipCorruptFiles = 16
	}
}
