using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000046 RID: 70
	internal sealed class TableHeaderFooterRows : TableRowCollection
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00013130 File Offset: 0x00011330
		internal TableHeaderFooterRows(Table owner, bool repeatOnNewPage, TableRowList rowDefs, TableRowInstance[] rowInstances)
			: base(owner, rowDefs, rowInstances)
		{
			this.m_repeatOnNewPage = repeatOnNewPage;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00013143 File Offset: 0x00011343
		public bool RepeatOnNewPage
		{
			get
			{
				return this.m_repeatOnNewPage;
			}
		}

		// Token: 0x04000155 RID: 341
		private bool m_repeatOnNewPage;
	}
}
