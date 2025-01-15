using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000634 RID: 1588
	internal enum DataFieldStatus
	{
		// Token: 0x04002DE8 RID: 11752
		None,
		// Token: 0x04002DE9 RID: 11753
		Overflow,
		// Token: 0x04002DEA RID: 11754
		UnSupportedDataType,
		// Token: 0x04002DEB RID: 11755
		IsMissing = 4,
		// Token: 0x04002DEC RID: 11756
		IsError = 8
	}
}
