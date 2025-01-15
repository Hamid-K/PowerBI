using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200076C RID: 1900
	[Flags]
	public enum UserLocationFlags
	{
		// Token: 0x040033EE RID: 13294
		None = 1,
		// Token: 0x040033EF RID: 13295
		ReportBody = 2,
		// Token: 0x040033F0 RID: 13296
		ReportPageSection = 4,
		// Token: 0x040033F1 RID: 13297
		ReportQueries = 8
	}
}
