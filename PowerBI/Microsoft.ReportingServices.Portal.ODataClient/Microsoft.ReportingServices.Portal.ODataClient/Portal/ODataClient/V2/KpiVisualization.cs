using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200009D RID: 157
	[OriginalName("KpiVisualization")]
	public enum KpiVisualization
	{
		// Token: 0x0400033F RID: 831
		[OriginalName("None")]
		None,
		// Token: 0x04000340 RID: 832
		[OriginalName("Bar")]
		Bar,
		// Token: 0x04000341 RID: 833
		[OriginalName("Line")]
		Line,
		// Token: 0x04000342 RID: 834
		[OriginalName("Step")]
		Step,
		// Token: 0x04000343 RID: 835
		[OriginalName("Area")]
		Area
	}
}
