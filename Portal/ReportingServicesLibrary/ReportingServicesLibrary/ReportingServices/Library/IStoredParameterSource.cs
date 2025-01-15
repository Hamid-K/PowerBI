using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A3 RID: 163
	internal interface IStoredParameterSource
	{
		// Token: 0x060007A0 RID: 1952
		ParameterInfoCollection RetrieveParameters(ReportProcessing reportProcessing);

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060007A1 RID: 1953
		ReportSnapshot CompiledParameterSource { get; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060007A2 RID: 1954
		bool IsSnapshotExecution { get; }
	}
}
