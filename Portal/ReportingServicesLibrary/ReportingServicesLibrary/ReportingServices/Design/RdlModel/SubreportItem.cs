using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000418 RID: 1048
	public class SubreportItem : ReportItem
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x00080EFC File Offset: 0x0007F0FC
		// (set) Token: 0x06002160 RID: 8544 RVA: 0x00080F04 File Offset: 0x0007F104
		public string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x00080F0D File Offset: 0x0007F10D
		// (set) Token: 0x06002162 RID: 8546 RVA: 0x00080F15 File Offset: 0x0007F115
		public Parameters Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00080F1E File Offset: 0x0007F11E
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x00080F26 File Offset: 0x0007F126
		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x00080F2F File Offset: 0x0007F12F
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x00080F37 File Offset: 0x0007F137
		public bool MergeTransactions
		{
			get
			{
				return this.m_mergeTransactions;
			}
			set
			{
				this.m_mergeTransactions = value;
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private string m_reportName = "";

		// Token: 0x04000EA1 RID: 3745
		private Parameters m_parameters;

		// Token: 0x04000EA2 RID: 3746
		private string m_noRows;

		// Token: 0x04000EA3 RID: 3747
		private bool m_mergeTransactions;
	}
}
