using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000094 RID: 148
	internal class FailedReportItem : ReportItem
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000CDCE File Offset: 0x0000AFCE
		internal FailedReportItem(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext)
			: base("Failed", name, rect, zIndex, diagnosticContext)
		{
		}
	}
}
