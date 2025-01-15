using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	public sealed class DocumentMapNode : OnDemandDocumentMapNode
	{
		// Token: 0x06001A8F RID: 6799 RVA: 0x0006B00E File Offset: 0x0006920E
		internal DocumentMapNode(string aLabel, string aId, int aLevel)
			: base(aLabel, aId, aLevel)
		{
		}
	}
}
