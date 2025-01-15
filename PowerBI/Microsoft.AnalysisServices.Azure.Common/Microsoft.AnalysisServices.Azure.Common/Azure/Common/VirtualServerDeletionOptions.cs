using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000079 RID: 121
	[Flags]
	public enum VirtualServerDeletionOptions
	{
		// Token: 0x040001ED RID: 493
		None = 0,
		// Token: 0x040001EE RID: 494
		IgnorePersistentStateStore = 1,
		// Token: 0x040001EF RID: 495
		SkipVirtualServerDatabaseMappingExistsCheck = 2
	}
}
