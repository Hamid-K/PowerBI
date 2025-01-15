using System;
using System.Collections.Specialized;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000091 RID: 145
	internal sealed class RenderReportAction
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00018939 File Offset: 0x00016B39
		public static RenderReportAction CreateWithCatalogItemContext(ClientRequest session, RSService service, CatalogItemContext reportContext)
		{
			return new RenderReportAction(session, service, reportContext);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00018944 File Offset: 0x00016B44
		public static RenderReportAction CreateWithFormatDeviceInfo(ClientRequest session, RSService service, string format, string deviceInfo, PageCountMode pageCountMode)
		{
			RSTrace.CatalogTrace.Assert(service != null, "service");
			RSTrace.CatalogTrace.Assert(session != null, "session");
			CatalogItemContext catalogItemContext = SessionReportItem.CreateContextFromSession(service, session);
			catalogItemContext.RSRequestParameters.FormatParamValue = format;
			catalogItemContext.RSRequestParameters.SetRenderingParameters(deviceInfo);
			catalogItemContext.RSRequestParameters.PaginationModeValue = ((pageCountMode == PageCountMode.Actual) ? "Actual" : "Estimate");
			return RenderReportAction.CreateWithCatalogItemContext(session, service, catalogItemContext);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000189BC File Offset: 0x00016BBC
		private RenderReportAction(ClientRequest session, RSService service, CatalogItemContext reportContext)
		{
			RSTrace.CatalogTrace.Assert(session != null, "session");
			RSTrace.CatalogTrace.Assert(service != null, "service");
			RSTrace.CatalogTrace.Assert(reportContext != null, "reportContext");
			this.m_session = session;
			this.m_service = service;
			this.m_reportContext = reportContext;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00018A28 File Offset: 0x00016C28
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00018A30 File Offset: 0x00016C30
		public bool UsePersistedStreams
		{
			get
			{
				return this.m_usePersistedStreams;
			}
			set
			{
				this.m_usePersistedStreams = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00018A39 File Offset: 0x00016C39
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00018A41 File Offset: 0x00016C41
		public bool WaitPersistStreamCompletion
		{
			get
			{
				return this.m_WaitPersistStreamCompletion;
			}
			set
			{
				this.m_WaitPersistStreamCompletion = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00018A4A File Offset: 0x00016C4A
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00018A52 File Offset: 0x00016C52
		public JobType JobType
		{
			get
			{
				return this.m_jobType;
			}
			set
			{
				this.m_jobType = value;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00018A5C File Offset: 0x00016C5C
		public void Render()
		{
			try
			{
				this.PerformExecution();
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00018A94 File Offset: 0x00016C94
		public void RenderStream(string streamId)
		{
			RSTrace.CatalogTrace.Assert(!this.UsePersistedStreams, "Cannot use persisted streams when rendering a stream");
			try
			{
				this.m_reportContext.RSRequestParameters.ImageIDParamValue = streamId;
				if (this.m_session.SessionReport.Report.SnapshotData == null)
				{
					throw new ReportNotReadyException();
				}
				this.PerformExecution();
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00018B10 File Offset: 0x00016D10
		public byte[] Result
		{
			get
			{
				this.m_result.Seek(0L, SeekOrigin.Begin);
				byte[] array = StreamSupport.ReadToEndNotUsingLength(this.m_result, Global.ResponseBufferSizeBytes);
				this.m_result.Close();
				return array;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00018B3C File Offset: 0x00016D3C
		public RSStream ResultStream
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00018B44 File Offset: 0x00016D44
		public string MimeType
		{
			get
			{
				if (this.m_result != null)
				{
					return this.m_result.MimeType;
				}
				return null;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00018B5B File Offset: 0x00016D5B
		public string Encoding
		{
			get
			{
				if (this.m_result != null && this.m_result.Encoding != null)
				{
					return this.m_result.Encoding.EncodingName;
				}
				return null;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00018B84 File Offset: 0x00016D84
		public string Extension
		{
			get
			{
				if (this.m_result != null)
				{
					return this.m_result.Extension;
				}
				return null;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00018B9B File Offset: 0x00016D9B
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00018BA3 File Offset: 0x00016DA3
		public string[] StreamIds
		{
			get
			{
				return this.m_streamIds;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00018BAB File Offset: 0x00016DAB
		public ParameterInfoCollection EffectiveParameters
		{
			get
			{
				return this.m_effectiveParameters;
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018BB4 File Offset: 0x00016DB4
		private void PerformExecution()
		{
			using (MonitoredScope.New("RenderReportAction.PerformExecution"))
			{
				this.SetBrowserCapabilities();
				ExecutionParameters executionParameters = new ExecutionParameters();
				executionParameters.JobType = this.JobType;
				executionParameters.ReportContext = this.m_reportContext;
				executionParameters.Session = this.m_session;
				ExecutionFactory executionFactory = new ExecutionFactory(this.m_service, executionParameters);
				ExecutionResult executionResult = (this.UsePersistedStreams ? executionFactory.CreatePersistedStreamReportExecution(this.WaitPersistStreamCompletion) : executionFactory.CreateReportExecution()).ExecuteReport();
				this.m_result = executionResult.OutputStream;
				this.m_warnings = executionResult.Warnings;
				this.m_effectiveParameters = executionResult.EffectiveParameters;
				this.m_streamIds = executionResult.StreamIds;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00018C78 File Offset: 0x00016E78
		private void SetBrowserCapabilities()
		{
			RequestContext reqContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext;
			RSTrace.CatalogTrace.Assert(reqContext != null, "reqContext");
			if (this.m_reportContext != null)
			{
				if (reqContext.HasBrowserCapabilities)
				{
					this.m_reportContext.RSRequestParameters.SetBrowserCapabilities(reqContext.BrowserCapabilities);
				}
				if (this.m_reportContext.RSRequestParameters.BrowserCapabilities == null)
				{
					this.m_reportContext.RSRequestParameters.SetBrowserCapabilities(new NameValueCollection());
				}
			}
		}

		// Token: 0x0400032A RID: 810
		private readonly ClientRequest m_session;

		// Token: 0x0400032B RID: 811
		private readonly RSService m_service;

		// Token: 0x0400032C RID: 812
		private readonly CatalogItemContext m_reportContext;

		// Token: 0x0400032D RID: 813
		private JobType m_jobType = JobType.UserJobType;

		// Token: 0x0400032E RID: 814
		private bool m_usePersistedStreams;

		// Token: 0x0400032F RID: 815
		private bool m_WaitPersistStreamCompletion;

		// Token: 0x04000330 RID: 816
		private RSStream m_result;

		// Token: 0x04000331 RID: 817
		private Warning[] m_warnings;

		// Token: 0x04000332 RID: 818
		private string[] m_streamIds;

		// Token: 0x04000333 RID: 819
		private ParameterInfoCollection m_effectiveParameters;
	}
}
