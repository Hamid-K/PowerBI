using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000034 RID: 52
	internal sealed class NewToOldReportSizeCollection : ReportSizeCollection
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		internal NewToOldReportSizeCollection(ReportSizeCollection col)
		{
			this.m_col = col;
		}

		// Token: 0x170003E1 RID: 993
		public override ReportSize this[int index]
		{
			get
			{
				return new ReportSize(this.m_col[index]);
			}
			set
			{
				this.m_col[index] = new ReportSize(value);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000EDFA File Offset: 0x0000CFFA
		public override int Count
		{
			get
			{
				return this.m_col.Count;
			}
		}

		// Token: 0x040000F5 RID: 245
		private ReportSizeCollection m_col;
	}
}
