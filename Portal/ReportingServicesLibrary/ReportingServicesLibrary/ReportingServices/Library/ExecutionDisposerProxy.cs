using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000111 RID: 273
	internal sealed class ExecutionDisposerProxy : IReportExecution
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00028AC0 File Offset: 0x00026CC0
		public ExecutionDisposerProxy(bool isRenderFirst, IExecutionDataProvider provider, ExecutionParameters requestInfo)
		{
			this.m_isRenderFirst = isRenderFirst;
			this.m_provider = provider;
			this.m_requestInfo = requestInfo;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public ExecutionResult ExecuteReport()
		{
			ExecutionResult executionResult;
			using (ReportExecutionBase reportExecutionBase = this.BuildReportExecution())
			{
				executionResult = reportExecutionBase.ExecuteReport();
			}
			return executionResult;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00028B18 File Offset: 0x00026D18
		private ReportExecutionBase BuildReportExecution()
		{
			ReportExecutionBase reportExecutionBase;
			if (this.m_isRenderFirst)
			{
				if (this.m_requestInfo.ReportContext.RSRequestParameters.SnapshotParamValue != null)
				{
					reportExecutionBase = new RenderForHistory(this.m_provider, this.m_requestInfo);
				}
				else if (this.m_requestInfo.ReportContext.ItemPath.IsEditSession)
				{
					reportExecutionBase = new RenderForEditSession(this.m_provider, this.m_requestInfo);
				}
				else
				{
					reportExecutionBase = new RenderForNewSession(this.m_provider, this.m_requestInfo);
				}
			}
			else if (this.m_requestInfo.ReportContext.RSRequestParameters.ImageIDParamValue != null)
			{
				reportExecutionBase = new RenderForStream(this.m_provider, this.m_requestInfo);
			}
			else
			{
				reportExecutionBase = new RenderForExistingSession(this.m_provider, this.m_requestInfo);
			}
			RSTrace.CatalogTrace.Assert(reportExecutionBase != null, "reportExecutionContext");
			return reportExecutionBase;
		}

		// Token: 0x040004A1 RID: 1185
		private readonly bool m_isRenderFirst;

		// Token: 0x040004A2 RID: 1186
		private readonly IExecutionDataProvider m_provider;

		// Token: 0x040004A3 RID: 1187
		private readonly ExecutionParameters m_requestInfo;
	}
}
