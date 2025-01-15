using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002E RID: 46
	internal sealed class SubReport : ReportItem
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000E091 File Offset: 0x0000C291
		internal SubReport(int intUniqueName, SubReport reportItemDef, SubReportInstance reportItemInstance, RenderingContext renderingContext, Report innerReport, bool processedWithError)
			: base(null, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
			this.m_report = innerReport;
			this.m_processedWithError = processedWithError;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000E0AF File Offset: 0x0000C2AF
		public Report Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000E0B7 File Offset: 0x0000C2B7
		public bool ProcessedWithError
		{
			get
			{
				return this.m_processedWithError;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000E0BF File Offset: 0x0000C2BF
		public bool NoRows
		{
			get
			{
				if (this.m_processedWithError)
				{
					return false;
				}
				Global.Tracer.Assert(this.m_report != null);
				return this.m_report.ReportInstance == null || this.m_report.InstanceInfo.NoRows;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E100 File Offset: 0x0000C300
		public string NoRowMessage
		{
			get
			{
				ExpressionInfo noRows = ((SubReport)base.ReportItemDef).NoRows;
				if (noRows != null)
				{
					if (ExpressionInfo.Types.Constant == noRows.Type)
					{
						return noRows.Value;
					}
					if (base.InstanceInfo != null)
					{
						return ((SubReportInstanceInfo)base.InstanceInfo).NoRows;
					}
				}
				return null;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E14C File Offset: 0x0000C34C
		internal override bool Search(SearchContext searchContext)
		{
			if (base.SkipSearch || this.NoRows || this.ProcessedWithError)
			{
				return false;
			}
			Report report = this.Report;
			return report != null && report.Body.Search(searchContext);
		}

		// Token: 0x040000E5 RID: 229
		private Report m_report;

		// Token: 0x040000E6 RID: 230
		private bool m_processedWithError;
	}
}
