using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DA RID: 730
	internal abstract class PageEvaluation
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x0006CA2E File Offset: 0x0006AC2E
		protected PageEvaluation(Report report)
		{
			this.m_romReport = report;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0006CA59 File Offset: 0x0006AC59
		internal virtual void Reset(ReportSection section, int newPageNumber, int newTotalPages, int newOverallPageNumber, int newOverallTotalPages)
		{
			this.m_currentPageNumber = newPageNumber;
			this.m_totalPages = newTotalPages;
			this.m_currentOverallPageNumber = newOverallPageNumber;
			this.m_overallTotalPages = newOverallTotalPages;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006CA79 File Offset: 0x0006AC79
		internal virtual void SetPageName(string pageName)
		{
			this.m_pageName = pageName;
		}

		// Token: 0x06001B4A RID: 6986
		internal abstract void Add(string textboxName, object textboxValue);

		// Token: 0x06001B4B RID: 6987
		internal abstract void UpdatePageSections(ReportSection section);

		// Token: 0x04000D7B RID: 3451
		protected int m_currentPageNumber = 1;

		// Token: 0x04000D7C RID: 3452
		protected int m_totalPages = 1;

		// Token: 0x04000D7D RID: 3453
		protected int m_overallTotalPages = 1;

		// Token: 0x04000D7E RID: 3454
		protected int m_currentOverallPageNumber = 1;

		// Token: 0x04000D7F RID: 3455
		protected Report m_romReport;

		// Token: 0x04000D80 RID: 3456
		protected string m_pageName;
	}
}
