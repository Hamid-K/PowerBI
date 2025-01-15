using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000279 RID: 633
	public interface ICustomReportItem
	{
		// Token: 0x060018B2 RID: 6322
		void GenerateReportItemDefinition(CustomReportItem cri);

		// Token: 0x060018B3 RID: 6323
		void EvaluateReportItemInstance(CustomReportItem cri);
	}
}
