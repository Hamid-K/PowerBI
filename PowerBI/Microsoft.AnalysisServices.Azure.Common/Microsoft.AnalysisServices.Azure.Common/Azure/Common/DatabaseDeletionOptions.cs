using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006A RID: 106
	[Flags]
	public enum DatabaseDeletionOptions
	{
		// Token: 0x040001A2 RID: 418
		None = 0,
		// Token: 0x040001A3 RID: 419
		IgnorePersistentStateStore = 1
	}
}
