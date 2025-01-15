using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AB RID: 171
	[Flags]
	internal enum RenderingArea
	{
		// Token: 0x0400031A RID: 794
		All = 0,
		// Token: 0x0400031B RID: 795
		PageCreation = 1,
		// Token: 0x0400031C RID: 796
		KeepTogether = 2,
		// Token: 0x0400031D RID: 797
		RepeatOnNewPage = 4,
		// Token: 0x0400031E RID: 798
		RichText = 8
	}
}
