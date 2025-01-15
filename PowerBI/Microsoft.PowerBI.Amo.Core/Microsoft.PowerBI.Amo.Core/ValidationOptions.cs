using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000CE RID: 206
	[Flags]
	public enum ValidationOptions
	{
		// Token: 0x0400070A RID: 1802
		None = 0,
		// Token: 0x0400070B RID: 1803
		DoNotValidateBindings = 1,
		// Token: 0x0400070C RID: 1804
		AddDetails = 2,
		// Token: 0x0400070D RID: 1805
		AddWarnings = 4,
		// Token: 0x0400070E RID: 1806
		AddMessages = 8
	}
}
