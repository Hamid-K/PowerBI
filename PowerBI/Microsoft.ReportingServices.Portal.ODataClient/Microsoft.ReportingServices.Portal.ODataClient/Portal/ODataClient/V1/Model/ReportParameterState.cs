using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000129 RID: 297
	[OriginalName("ReportParameterState")]
	public enum ReportParameterState
	{
		// Token: 0x040005F8 RID: 1528
		[OriginalName("HasValidValue")]
		HasValidValue,
		// Token: 0x040005F9 RID: 1529
		[OriginalName("MissingValidValue")]
		MissingValidValue,
		// Token: 0x040005FA RID: 1530
		[OriginalName("HasOutstandingDependencies")]
		HasOutstandingDependencies,
		// Token: 0x040005FB RID: 1531
		[OriginalName("DynamicValuesUnavailable")]
		DynamicValuesUnavailable
	}
}
