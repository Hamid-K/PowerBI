using System;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007E0 RID: 2016
	internal abstract class RenderStreamingOdp
	{
		// Token: 0x06007131 RID: 28977 RVA: 0x001D70F1 File Offset: 0x001D52F1
		protected RenderStreamingOdp(StreamingOdpProcessingContextBase pc, ICatalogItemContext reportContext, ParameterInfoCollection parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Report rifReport, DataSourceInfoCollection dataSourceInfos, GlobalIDOwnerCollection globalIDOwnerCollection)
		{
			this.m_publicProcessingContext = pc;
			this.m_reportContext = reportContext;
			this.m_parameters = parameters;
			this.m_rifReport = rifReport;
			this.m_globalIDOwnerCollection = globalIDOwnerCollection;
			this.m_runtimeDataSources = this.BuildRuntimeDataSources(dataSourceInfos);
		}

		// Token: 0x06007132 RID: 28978
		protected abstract RenderingContext Render(ExecutionLogContext executionLogContext);

		// Token: 0x06007133 RID: 28979 RVA: 0x001D712C File Offset: 0x001D532C
		protected virtual void UpdateExecutionLog(ErrorContext errorContext, ExecutionLogContext executionLogContext, IJobContext jobContext)
		{
			ReportProcessing.UpdateHostingEnvironment(errorContext, this.ReportContext, executionLogContext, this.RunningProcessingEngine, jobContext);
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x001D7144 File Offset: 0x001D5344
		public ProgressiveProcessingResult Execute()
		{
			ExecutionLogContext executionLogContext = new ExecutionLogContext(this.m_publicProcessingContext.JobContext);
			executionLogContext.StartProcessingTimer();
			ProcessingErrorContext processingErrorContext = new ProcessingErrorContext();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			ProgressiveProcessingResult progressiveProcessingResult;
			try
			{
				UserProfileState userProfileState = UserProfileState.None;
				this.PrepareForExecution();
				this.ProcessReport(processingErrorContext, executionLogContext, ref userProfileState);
				this.m_odpContext.CheckAndThrowIfAborted();
				RenderingContext renderingContext = null;
				try
				{
					renderingContext = this.Render(executionLogContext);
				}
				catch (ReportProcessing.DataCacheUnavailableException)
				{
					throw;
				}
				catch (ReportRenderingException ex)
				{
					ReportProcessing.HandleRenderingException(ex);
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex2)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex2))
					{
						throw;
					}
					throw new UnhandledReportRenderingException(ex2);
				}
				finally
				{
					this.FinallyBlockForProcessingAndRendering(renderingContext, executionLogContext);
				}
				this.CleanupSuccessfulProcessing(processingErrorContext);
				progressiveProcessingResult = this.ConstructProcessingResult(processingErrorContext, executionLogContext);
			}
			catch (ReportProcessing.DataCacheUnavailableException)
			{
				throw;
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex3)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex3))
				{
					throw;
				}
				throw new ReportProcessingException(ex3, processingErrorContext.Messages);
			}
			finally
			{
				this.FinalCleanup();
				if (this.m_odpContext != null && this.m_odpContext.ContextMode == OnDemandProcessingContext.Mode.Streaming)
				{
					this.m_odpContext.UnregisterAbortInfo();
				}
				this.UpdateExecutionLog(processingErrorContext, executionLogContext, this.PublicProcessingContext.JobContext);
				if (currentCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = currentCulture;
				}
			}
			return progressiveProcessingResult;
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x001D72B8 File Offset: 0x001D54B8
		protected void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
			ReportProcessing.CleanupOnDemandProcessing(this.m_odpContext, true);
		}

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x06007136 RID: 28982 RVA: 0x001D72C6 File Offset: 0x001D54C6
		private UserProfileState AllowedUserProfileState
		{
			get
			{
				return UserProfileState.InQuery | UserProfileState.InReport | UserProfileState.OnDemandExpressions;
			}
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x001D72CC File Offset: 0x001D54CC
		protected virtual void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState)
		{
			ReportProcessingContext reportProcessingContext = new ReportProcessingContext(this.m_reportContext, this.m_publicProcessingContext.RequestUserName, this.m_parameters, this.m_runtimeDataSources, null, null, this.m_publicProcessingContext.GetResourceCallback, null, ReportProcessing.ExecutionType.Live, this.m_publicProcessingContext.UserLanguage, this.AllowedUserProfileState, UserProfileState.None, this.m_publicProcessingContext.CreateDataExtensionInstanceFunction, this.m_publicProcessingContext.ReportRuntimeSetup, this.m_publicProcessingContext.CreateStreamCallback, false, this.m_publicProcessingContext.JobContext, null, this.m_publicProcessingContext.DataProtection, null);
			ProcessReportOdpStreaming processReportOdpStreaming = new ProcessReportOdpStreaming(this.m_publicProcessingContext.Configuration, reportProcessingContext, this.m_rifReport, errorContext, null, this.m_globalIDOwnerCollection, executionLogContext, this.m_publicProcessingContext.ExecutionTimeStamp, this.m_publicProcessingContext.GetAbortHelper());
			this.m_odpReportSnapshot = processReportOdpStreaming.Execute(out this.m_odpContext);
		}

		// Token: 0x06007138 RID: 28984 RVA: 0x001D73A0 File Offset: 0x001D55A0
		protected void PrepareForExecution()
		{
			this.ValidateReportParameters();
			ReportProcessing.CheckReportCredentialsAndConnectionUserDependency(this.m_runtimeDataSources, this.AllowedUserProfileState, this.m_reportContext.ItemName);
		}

		// Token: 0x06007139 RID: 28985 RVA: 0x001D73C4 File Offset: 0x001D55C4
		protected ProgressiveProcessingResult ConstructProcessingResult(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext)
		{
			SerializableValues serializableValues = new SerializableValues();
			serializableValues.AddProcessingMessages("processingWarnings", errorContext.Messages);
			return new ProgressiveProcessingResult(serializableValues, executionLogContext, errorContext.Messages);
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x001D73E8 File Offset: 0x001D55E8
		protected void FinalCleanup()
		{
			if (this.m_odpContext != null)
			{
				this.m_odpContext.FreeAllResources();
			}
		}

		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x0600713B RID: 28987 RVA: 0x001D73FD File Offset: 0x001D55FD
		protected ProcessingEngine RunningProcessingEngine
		{
			get
			{
				return ProcessingEngine.OnDemandEngine;
			}
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x001D7400 File Offset: 0x001D5600
		private void FinallyBlockForProcessingAndRendering(RenderingContext odpRenderingContext, ExecutionLogContext executionLogContext)
		{
			if (odpRenderingContext != null)
			{
				odpRenderingContext.CloseRenderingChunkManager();
			}
			if (executionLogContext.IsRenderingTimerRunning)
			{
				executionLogContext.StopRenderingTimer();
			}
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x001D741C File Offset: 0x001D561C
		protected void ValidateReportParameters()
		{
			bool flag = true;
			if (!this.m_parameters.ValuesAreValid(out flag, true))
			{
				throw new ReportProcessingException(ErrorCode.rsParametersNotSpecified);
			}
		}

		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x0600713E RID: 28990 RVA: 0x001D7446 File Offset: 0x001D5646
		internal StreamingOdpProcessingContextBase PublicProcessingContext
		{
			get
			{
				return this.m_publicProcessingContext;
			}
		}

		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x0600713F RID: 28991 RVA: 0x001D744E File Offset: 0x001D564E
		protected string ReportName
		{
			get
			{
				return this.m_reportContext.ItemName;
			}
		}

		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x001D745B File Offset: 0x001D565B
		protected ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x001D7464 File Offset: 0x001D5664
		private RuntimeDataSourceInfoCollection BuildRuntimeDataSources(DataSourceInfoCollection dataSourceInfos)
		{
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection = new RuntimeDataSourceInfoCollection();
			ReportProcessing.CheckAndConvertDataSources(this.m_reportContext, dataSourceInfos, null, true, this.m_publicProcessingContext.ServerDataSourceSettings, runtimeDataSourceInfoCollection, null);
			return runtimeDataSourceInfoCollection;
		}

		// Token: 0x04003A58 RID: 14936
		private readonly StreamingOdpProcessingContextBase m_publicProcessingContext;

		// Token: 0x04003A59 RID: 14937
		private readonly ICatalogItemContext m_reportContext;

		// Token: 0x04003A5A RID: 14938
		protected readonly ParameterInfoCollection m_parameters;

		// Token: 0x04003A5B RID: 14939
		protected readonly RuntimeDataSourceInfoCollection m_runtimeDataSources;

		// Token: 0x04003A5C RID: 14940
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.Report m_rifReport;

		// Token: 0x04003A5D RID: 14941
		protected readonly GlobalIDOwnerCollection m_globalIDOwnerCollection;

		// Token: 0x04003A5E RID: 14942
		protected OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A5F RID: 14943
		protected ReportSnapshot m_odpReportSnapshot;
	}
}
