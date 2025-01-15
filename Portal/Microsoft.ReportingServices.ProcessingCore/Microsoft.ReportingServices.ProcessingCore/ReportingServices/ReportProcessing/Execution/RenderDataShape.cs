using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007C6 RID: 1990
	internal sealed class RenderDataShape : RenderStreamingOdp
	{
		// Token: 0x0600708A RID: 28810 RVA: 0x001D4FF3 File Offset: 0x001D31F3
		public RenderDataShape(DataShapeProcessingContext progProcessingContext, ICatalogItemContext reportContext, Microsoft.ReportingServices.ReportIntermediateFormat.Report rifReport, DataSourceInfoCollection dataSourceInfos, IDataShapeResultRenderer renderer)
			: base(progProcessingContext, reportContext, new ParameterInfoCollection(), rifReport, dataSourceInfos, null)
		{
			this.m_renderer = renderer;
			this.m_processingContext = progProcessingContext;
		}

		// Token: 0x0600708B RID: 28811 RVA: 0x001D5018 File Offset: 0x001D3218
		protected override RenderingContext Render(ExecutionLogContext executionLogContext)
		{
			RenderingContext renderingContext;
			DataShapeResult dataShapeResult = this.PrepareROM(out renderingContext);
			if (this.m_renderer == null)
			{
				this.m_renderer = DataShapeResultRendererFactory.CreateDataShapeResultRenderer();
			}
			executionLogContext.StartRenderingTimer();
			this.m_renderer.RenderDataShapeResult(dataShapeResult, this.m_processingContext.CreateJsonStreamWriterCallback);
			return renderingContext;
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x001D5060 File Offset: 0x001D3260
		protected override void UpdateExecutionLog(ErrorContext errorContext, ExecutionLogContext executionLogContext, IJobContext jobContext)
		{
			Microsoft.ReportingServices.Diagnostics.Internal.DataShape dataShapeAdditionalInfo = this.m_processingContext.DataShapeAdditionalInfo;
			if (jobContext != null)
			{
				Global.Tracer.Assert(executionLogContext != null, "ExecutionLogContext must not be null");
				executionLogContext.StopAllRunningTimers();
				dataShapeAdditionalInfo.TimeDataRetrieval = new long?(executionLogContext.DataProcessingDurationMsNormalized);
				dataShapeAdditionalInfo.TimeProcessing = new long?(executionLogContext.ReportProcessingDurationMsNormalized);
				dataShapeAdditionalInfo.TimeRendering = new long?(executionLogContext.ReportRenderingDurationMsNormalized);
				if (dataShapeAdditionalInfo.ScalabilityTime == null)
				{
					dataShapeAdditionalInfo.ScalabilityTime = new ScaleTimeCategory();
				}
				dataShapeAdditionalInfo.ScalabilityTime.Processing = new long?(executionLogContext.ProcessingScalabilityDurationMsNormalized);
				if (dataShapeAdditionalInfo.EstimatedMemoryUsageKB == null)
				{
					dataShapeAdditionalInfo.EstimatedMemoryUsageKB = new EstimatedMemoryUsageKBCategory();
				}
				dataShapeAdditionalInfo.EstimatedMemoryUsageKB.Processing = new long?(executionLogContext.PeakProcesssingMemoryUsage);
				if (dataShapeAdditionalInfo.Connections == null)
				{
					dataShapeAdditionalInfo.Connections = executionLogContext.GetConnectionMetrics();
				}
				else
				{
					dataShapeAdditionalInfo.Connections.AddRange(executionLogContext.GetConnectionMetrics());
				}
			}
			ReportProcessing.TraceProcessingMessages(errorContext, base.ReportContext);
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x001D5150 File Offset: 0x001D3350
		private DataShapeResult PrepareROM(out RenderingContext odpRenderingContext)
		{
			odpRenderingContext = new RenderingContext("DSR", this.m_odpReportSnapshot, null, this.m_odpContext);
			return new DataShapeResult(this.m_odpReportSnapshot.Report, odpRenderingContext);
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x001D5180 File Offset: 0x001D3380
		protected override void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			ReportProcessingContext reportProcessingContext = new ReportProcessingContext(base.ReportContext, this.m_processingContext.RequestUserName, this.m_parameters, this.m_runtimeDataSources, null, null, this.m_processingContext.GetResourceCallback, null, ReportProcessing.ExecutionType.Live, this.m_processingContext.UserLanguage, UserProfileState.InQuery | UserProfileState.InReport | UserProfileState.OnDemandExpressions, UserProfileState.None, this.m_processingContext.CreateDataExtensionInstanceFunction, this.m_processingContext.ReportRuntimeSetup, this.m_processingContext.CreateStreamCallback, false, this.m_processingContext.JobContext, null, this.m_processingContext.DataProtection, null);
			ProcessDataShape processDataShape = new ProcessDataShape(this.m_processingContext.Configuration, reportProcessingContext, this.m_rifReport, errorContext, null, this.m_globalIDOwnerCollection, executionLogContext, this.m_processingContext.ExecutionTimeStamp, this.m_processingContext.GetAbortHelper(), this.m_processingContext.UseParallelQueryExecution);
			this.m_odpReportSnapshot = processDataShape.Execute(out this.m_odpContext);
		}

		// Token: 0x04003A2E RID: 14894
		internal const string DataShapeRendererFormatString = "DSR";

		// Token: 0x04003A2F RID: 14895
		private IDataShapeResultRenderer m_renderer;

		// Token: 0x04003A30 RID: 14896
		private DataShapeProcessingContext m_processingContext;
	}
}
