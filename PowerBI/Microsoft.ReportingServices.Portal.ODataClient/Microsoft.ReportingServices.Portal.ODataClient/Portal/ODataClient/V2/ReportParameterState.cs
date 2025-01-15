using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200009B RID: 155
	[OriginalName("ReportParameterState")]
	public enum ReportParameterState
	{
		// Token: 0x04000332 RID: 818
		[OriginalName("HasValidValue")]
		HasValidValue,
		// Token: 0x04000333 RID: 819
		[OriginalName("MissingValidValue")]
		MissingValidValue,
		// Token: 0x04000334 RID: 820
		[OriginalName("HasOutstandingDependencies")]
		HasOutstandingDependencies,
		// Token: 0x04000335 RID: 821
		[OriginalName("DynamicValuesUnavailable")]
		DynamicValuesUnavailable
	}
}
