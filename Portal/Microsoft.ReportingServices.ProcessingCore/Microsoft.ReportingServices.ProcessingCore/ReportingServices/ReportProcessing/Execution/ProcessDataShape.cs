using System;
using Microsoft.ReportingServices.DataShapeResultRenderer;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CA RID: 1994
	internal sealed class ProcessDataShape : ProcessReportOdpStreaming
	{
		// Token: 0x060070B4 RID: 28852 RVA: 0x001D58F8 File Offset: 0x001D3AF8
		public ProcessDataShape(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, DateTime executionTime, IAbortHelper abortHelper, bool useParallelQueryExecution)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, executionTime, abortHelper)
		{
			this.m_useParallelQueryExecution = useParallelQueryExecution;
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x001D5922 File Offset: 0x001D3B22
		protected override void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			this.ParallelPreloadQueries(odpContext);
			base.SetupInitialOdpState(odpContext, reportInstance, reportSnapshot);
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x001D5938 File Offset: 0x001D3B38
		private void ParallelPreloadQueries(OnDemandProcessingContext odpContext)
		{
			if (!this.m_useParallelQueryExecution || odpContext.JobContext == null)
			{
				return;
			}
			try
			{
				new DataShapeDataPrefetchManager(odpContext).ExecuteQueries();
			}
			catch (Exception innerException)
			{
				ProcessingAbortedException ex = innerException as ProcessingAbortedException;
				if (ex != null && ex.InnerException != null)
				{
					innerException = ex.InnerException;
				}
				throw new DataShapeResultRenderingException(innerException, null, true);
			}
		}

		// Token: 0x04003A3A RID: 14906
		private readonly bool m_useParallelQueryExecution;
	}
}
