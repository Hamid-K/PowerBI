using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000653 RID: 1619
	internal sealed class DataShapePublishingResult
	{
		// Token: 0x060057C9 RID: 22473 RVA: 0x0016FF68 File Offset: 0x0016E168
		internal DataShapePublishingResult(Report report, DataSourceInfoCollection dataSourceInfos, ProcessingMessageList warnings)
		{
			this.m_report = report;
			this.m_dataSourceInfos = dataSourceInfos;
			this.m_warnings = warnings;
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0016FF85 File Offset: 0x0016E185
		public Report Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x0016FF8D File Offset: 0x0016E18D
		public DataSourceInfoCollection DataSourceInfos
		{
			get
			{
				return this.m_dataSourceInfos;
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x0016FF95 File Offset: 0x0016E195
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x04002E76 RID: 11894
		private readonly Report m_report;

		// Token: 0x04002E77 RID: 11895
		private readonly ProcessingMessageList m_warnings;

		// Token: 0x04002E78 RID: 11896
		private readonly DataSourceInfoCollection m_dataSourceInfos;
	}
}
