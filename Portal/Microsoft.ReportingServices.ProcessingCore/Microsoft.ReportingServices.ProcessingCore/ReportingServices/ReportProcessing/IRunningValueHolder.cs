using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C0 RID: 1728
	internal interface IRunningValueHolder
	{
		// Token: 0x06005CF5 RID: 23797
		RunningValueInfoList GetRunningValueList();

		// Token: 0x06005CF6 RID: 23798
		void ClearIfEmpty();
	}
}
