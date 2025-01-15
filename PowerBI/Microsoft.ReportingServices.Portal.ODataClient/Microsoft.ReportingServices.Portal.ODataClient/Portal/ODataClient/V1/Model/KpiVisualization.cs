using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200012B RID: 299
	[OriginalName("KpiVisualization")]
	public enum KpiVisualization
	{
		// Token: 0x04000605 RID: 1541
		[OriginalName("None")]
		None,
		// Token: 0x04000606 RID: 1542
		[OriginalName("Bar")]
		Bar,
		// Token: 0x04000607 RID: 1543
		[OriginalName("Line")]
		Line,
		// Token: 0x04000608 RID: 1544
		[OriginalName("Step")]
		Step,
		// Token: 0x04000609 RID: 1545
		[OriginalName("Area")]
		Area
	}
}
