using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000677 RID: 1655
	internal struct DataAggregateObjResult
	{
		// Token: 0x04002F47 RID: 12103
		internal bool ErrorOccurred;

		// Token: 0x04002F48 RID: 12104
		internal object Value;

		// Token: 0x04002F49 RID: 12105
		internal bool HasCode;

		// Token: 0x04002F4A RID: 12106
		internal ProcessingErrorCode Code;

		// Token: 0x04002F4B RID: 12107
		internal Severity Severity;

		// Token: 0x04002F4C RID: 12108
		internal string[] Arguments;

		// Token: 0x04002F4D RID: 12109
		internal DataFieldStatus FieldStatus;
	}
}
