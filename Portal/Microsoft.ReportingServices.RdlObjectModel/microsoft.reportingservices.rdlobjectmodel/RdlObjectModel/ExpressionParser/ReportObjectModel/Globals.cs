using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002C4 RID: 708
	internal abstract class Globals
	{
		// Token: 0x170006EB RID: 1771
		internal abstract object this[string key] { get; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060015EB RID: 5611
		internal abstract string ReportName { get; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060015EC RID: 5612
		internal abstract int PageNumber { get; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060015ED RID: 5613
		internal abstract int TotalPages { get; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060015EE RID: 5614
		internal abstract DateTime ExecutionTime { get; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060015EF RID: 5615
		internal abstract string ReportServerUrl { get; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060015F0 RID: 5616
		internal abstract string ReportFolder { get; }
	}
}
