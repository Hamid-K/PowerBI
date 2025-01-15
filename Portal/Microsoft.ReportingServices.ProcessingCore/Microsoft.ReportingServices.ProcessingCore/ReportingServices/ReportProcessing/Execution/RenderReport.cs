using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007D1 RID: 2001
	internal abstract class RenderReport
	{
		// Token: 0x060070D7 RID: 28887 RVA: 0x001D5DFF File Offset: 0x001D3FFF
		public RenderReport(ProcessingContext pc, RenderingContext rc)
		{
			this.m_publicProcessingContext = pc;
			this.m_publicRenderingContext = rc;
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x001D5E18 File Offset: 0x001D4018
		public OnDemandProcessingResult Execute(IRenderingExtension newRenderer)
		{
			ExecutionLogContext executionLogContext = new ExecutionLogContext(this.m_publicProcessingContext.JobContext);
			executionLogContext.StartProcessingTimer();
			ProcessingErrorContext processingErrorContext = new ProcessingErrorContext();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			OnDemandProcessingResult onDemandProcessingResult;
			try
			{
				bool flag = false;
				bool flag2 = false;
				UserProfileState userProfileState = UserProfileState.None;
				Hashtable renderProperties = this.PublicRenderingContext.GetRenderProperties(this.IsSnapshotReprocessing);
				NameValueCollection nameValueCollection = this.FormServerParameterCollection(this.PublicRenderingContext.ReportContext.RSRequestParameters.CatalogParameters);
				ProcessingContext.DelayUntilResourcesAvailableBlocking();
				this.PrepareForExecution();
				ProcessingContext.DelayUntilResourcesAvailableBlocking();
				this.ProcessReport(processingErrorContext, executionLogContext, ref userProfileState);
				RenderingContext renderingContext = null;
				try
				{
					ProcessingContext.DelayUntilResourcesAvailableBlocking();
					Microsoft.ReportingServices.OnDemandReportRendering.Report report = this.PrepareROM(out renderingContext);
					executionLogContext.StartRenderingTimer();
					ProcessingContext.DelayUntilResourcesAvailableBlocking();
					flag2 = this.InvokeRenderer(newRenderer, report, nameValueCollection, this.RenderingParameters, this.PublicRenderingContext.ReportContext.RSRequestParameters.BrowserCapabilities, ref renderProperties, this.PublicProcessingContext.CreateStreamCallback);
					this.UpdateServerTotalPages(newRenderer, ref renderProperties);
					this.UpdateEventInfo(renderingContext, this.PublicRenderingContext, ref flag);
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
				onDemandProcessingResult = this.ConstructProcessingResult(flag, renderProperties, processingErrorContext, userProfileState, flag2, executionLogContext);
			}
			catch (ReportProcessing.DataCacheUnavailableException)
			{
				throw;
			}
			catch (RSException)
			{
				this.CleanupForException();
				throw;
			}
			catch (Exception ex3)
			{
				this.CleanupForException();
				throw new ReportProcessingException(ex3, processingErrorContext.Messages);
			}
			finally
			{
				this.FinalCleanup();
				ReportProcessing.UpdateHostingEnvironment(processingErrorContext, this.PublicProcessingContext.ReportContext, executionLogContext, this.RunningProcessingEngine, this.PublicProcessingContext.JobContext);
				if (currentCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = currentCulture;
				}
			}
			return onDemandProcessingResult;
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x001D6084 File Offset: 0x001D4284
		protected virtual void CleanupSuccessfulProcessing(ProcessingErrorContext errorContext)
		{
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x001D6086 File Offset: 0x001D4286
		protected virtual bool InvokeRenderer(IRenderingExtension renderer, Microsoft.ReportingServices.OnDemandReportRendering.Report report, NameValueCollection reportServerParameters, NameValueCollection deviceInfo, NameValueCollection clientCapabilities, ref Hashtable renderProperties, CreateAndRegisterStream createAndRegisterStream)
		{
			return renderer.Render(report, reportServerParameters, deviceInfo, clientCapabilities, ref renderProperties, createAndRegisterStream);
		}

		// Token: 0x060070DB RID: 28891
		protected abstract void ProcessReport(ProcessingErrorContext errorContext, ExecutionLogContext executionLogContext, ref UserProfileState userProfileState);

		// Token: 0x060070DC RID: 28892
		protected abstract void PrepareForExecution();

		// Token: 0x060070DD RID: 28893
		protected abstract Microsoft.ReportingServices.OnDemandReportRendering.Report PrepareROM(out RenderingContext odpRenderingContext);

		// Token: 0x060070DE RID: 28894
		protected abstract OnDemandProcessingResult ConstructProcessingResult(bool eventInfoChanged, Hashtable renderProperties, ProcessingErrorContext errorContext, UserProfileState userProfileState, bool renderingInfoChanged, ExecutionLogContext executionLogContext);

		// Token: 0x060070DF RID: 28895
		protected abstract void FinalCleanup();

		// Token: 0x060070E0 RID: 28896 RVA: 0x001D6098 File Offset: 0x001D4298
		protected virtual void CleanupForException()
		{
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x060070E1 RID: 28897
		protected abstract ProcessingEngine RunningProcessingEngine { get; }

		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x060070E2 RID: 28898 RVA: 0x001D609A File Offset: 0x001D429A
		protected virtual bool IsSnapshotReprocessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x001D609D File Offset: 0x001D429D
		private void UpdateEventInfo(RenderingContext odpRenderingContext, RenderingContext rc, ref bool eventInfoChanged)
		{
			this.UpdateEventInfoInSnapshot();
			eventInfoChanged |= odpRenderingContext.EventInfoChanged;
			if (eventInfoChanged)
			{
				rc.EventInfo = odpRenderingContext.EventInfo;
			}
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x001D60C0 File Offset: 0x001D42C0
		protected virtual void UpdateEventInfoInSnapshot()
		{
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x001D60C2 File Offset: 0x001D42C2
		private void FinallyBlockForProcessingAndRendering(RenderingContext odpRenderingContext, ExecutionLogContext executionLogContext)
		{
			if (odpRenderingContext != null)
			{
				odpRenderingContext.CloseRenderingChunkManager();
				if (odpRenderingContext.OdpContext != null)
				{
					odpRenderingContext.OdpContext.LogMetrics();
				}
			}
			executionLogContext.StopRenderingTimer();
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x001D60E8 File Offset: 0x001D42E8
		protected int GetNumberOfPages(Hashtable renderProperties)
		{
			int num = 0;
			if (renderProperties != null)
			{
				object obj = renderProperties["TotalPages"];
				if (obj != null && obj is int)
				{
					num = (int)obj;
				}
			}
			return num;
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x001D611C File Offset: 0x001D431C
		protected PaginationMode GetUpdatedPaginationMode(Hashtable renderProperties, PaginationMode clientPaginationMode)
		{
			try
			{
				if (renderProperties != null)
				{
					object obj = renderProperties["UpdatedPaginationMode"];
					if (obj != null)
					{
						return (PaginationMode)obj;
					}
				}
			}
			catch (InvalidCastException)
			{
			}
			return PaginationMode.Estimate;
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x001D615C File Offset: 0x001D435C
		protected void UpdateServerTotalPages(IRenderingExtension renderer, ref Hashtable renderProperties)
		{
			if (renderProperties != null && renderer != null && !(renderer is ITotalPages))
			{
				renderProperties["TotalPages"] = 0;
				renderProperties["UpdatedPaginationMode"] = PaginationMode.Estimate;
			}
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x001D6194 File Offset: 0x001D4394
		private NameValueCollection FormServerParameterCollection(NameValueCollection serverParams)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (serverParams == null)
			{
				return nameValueCollection;
			}
			this.CheckAndAddServerParam(serverParams, "Command", nameValueCollection);
			this.CheckAndAddServerParam(serverParams, "Format", nameValueCollection);
			this.CheckAndAddServerParam(serverParams, "SessionID", nameValueCollection);
			this.CheckAndAddServerParam(serverParams, "ShowHideToggle", nameValueCollection);
			this.CheckAndAddServerParam(serverParams, "ImageID", nameValueCollection);
			this.CheckAndAddServerParam(serverParams, "Snapshot", nameValueCollection);
			return nameValueCollection;
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x001D61FC File Offset: 0x001D43FC
		private void CheckAndAddServerParam(NameValueCollection src, string paramName, NameValueCollection dest)
		{
			string[] values = src.GetValues(paramName);
			if (values == null || values.Length == 0)
			{
				return;
			}
			dest.Add(paramName, values[0]);
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x001D6224 File Offset: 0x001D4424
		protected void ValidateReportParameters()
		{
			bool flag = true;
			if (!this.PublicProcessingContext.Parameters.ValuesAreValid(out flag, true))
			{
				throw new ReportProcessingException(ErrorCode.rsParametersNotSpecified);
			}
		}

		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x060070EC RID: 28908 RVA: 0x001D6253 File Offset: 0x001D4453
		internal ProcessingContext PublicProcessingContext
		{
			get
			{
				return this.m_publicProcessingContext;
			}
		}

		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x060070ED RID: 28909 RVA: 0x001D625B File Offset: 0x001D445B
		internal RenderingContext PublicRenderingContext
		{
			get
			{
				return this.m_publicRenderingContext;
			}
		}

		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x060070EE RID: 28910 RVA: 0x001D6263 File Offset: 0x001D4463
		protected string ReportName
		{
			get
			{
				return this.PublicProcessingContext.ReportContext.ItemName;
			}
		}

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x060070EF RID: 28911 RVA: 0x001D6275 File Offset: 0x001D4475
		protected NameValueCollection RenderingParameters
		{
			get
			{
				return this.PublicRenderingContext.ReportContext.RSRequestParameters.RenderingParameters;
			}
		}

		// Token: 0x04003A44 RID: 14916
		private readonly ProcessingContext m_publicProcessingContext;

		// Token: 0x04003A45 RID: 14917
		private readonly RenderingContext m_publicRenderingContext;
	}
}
