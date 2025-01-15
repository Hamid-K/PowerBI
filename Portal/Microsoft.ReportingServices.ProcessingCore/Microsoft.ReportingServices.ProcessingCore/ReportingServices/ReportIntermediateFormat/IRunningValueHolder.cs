using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DA RID: 1242
	internal interface IRunningValueHolder
	{
		// Token: 0x06003EAD RID: 16045
		List<RunningValueInfo> GetRunningValueList();

		// Token: 0x06003EAE RID: 16046
		void ClearIfEmpty();

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06003EAF RID: 16047
		DataScopeInfo DataScopeInfo { get; }
	}
}
