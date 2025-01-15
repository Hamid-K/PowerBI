using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ProgressivePackaging;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000180 RID: 384
	internal abstract class RenderEditActionBase : ProgressivePackageActionBase
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x00033074 File Offset: 0x00031274
		internal RenderEditActionBase(Stream inputStream, Stream outputStream, IList<string> responseFlags, IRenderEditSession session, RSService service, CatalogItemContext reportContext, string jobId)
			: base(outputStream, responseFlags, service)
		{
			RSTrace.CatalogTrace.Assert(inputStream != null, "RenderEditAction.ctor: inputStream != null");
			RSTrace.CatalogTrace.Assert(session != null, "RenderEditAction.ctor: session != null");
			RSTrace.CatalogTrace.Assert(reportContext != null, "RenderEditAction.ctor: reportContext != null");
			this.m_inputStream = inputStream;
			this.m_session = session;
			this.m_reportContext = reportContext;
			this.m_jobId = jobId;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000330E5 File Offset: 0x000312E5
		protected override bool InitializeAction()
		{
			return base.TryGetProgressivePackageReader(this.m_inputStream, out this.m_reader);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x000053DC File Offset: 0x000035DC
		private bool IsPackagedReportArchive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000330FC File Offset: 0x000312FC
		protected override void FinalCleanup(ErrorCode status)
		{
			RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
			if (jobContext != null)
			{
				ReportExecutionInfo executionInfo = jobContext.ExecutionInfo;
				executionInfo.Status = status;
				executionInfo.Source = ExecutionLogExecType.AdHoc;
				executionInfo.EventType = ReportEventType.RenderEdit;
				executionInfo.Format = "RPDS";
				executionInfo.ExecutionId = this.m_session.SessionId;
				executionInfo.ItemPath = this.m_session.ItemPath;
				executionInfo.ByteCount = this.m_byteCount;
			}
			if (this.m_reader != null)
			{
				this.m_reader.Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00033180 File Offset: 0x00031380
		protected override string OperationName
		{
			get
			{
				return "RenderEdit";
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00033188 File Offset: 0x00031388
		protected void PublishReport(Stream rdlxStream, ProgressiveCacheEntry entry)
		{
			using (MonitoredScope.New("RenderEditAction.PublishReport"))
			{
				ProgressivePublishingContext publishingContext = this.GetPublishingContext(rdlxStream, entry);
				ProgressivePublishingResult progressivePublishingResult;
				if (entry.Report == null)
				{
					progressivePublishingResult = this.CreateProgressiveReport(publishingContext);
				}
				else
				{
					progressivePublishingResult = entry.Report.MergeDefinition(publishingContext);
				}
				this.HandlePublishingResult(progressivePublishingResult, entry);
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000331EC File Offset: 0x000313EC
		private void HandlePublishingResult(ProgressivePublishingResult pubResult, ProgressiveCacheEntry entry)
		{
			RSTrace.CatalogTrace.Assert(pubResult != null, "RenderEditAction: pubResult != null");
			RSTrace.CatalogTrace.Assert(pubResult.Report != null, "RenderEditAction: pubResult.Report != null");
			RSTrace.CatalogTrace.Assert(!entry.IsPowerView, "RenderEditAction: IsPowerView is true");
			this.WriteResults(pubResult.ResultValues);
			DataSourceInfoCollection dataSourceInfoCollection = entry.DataSources;
			if (dataSourceInfoCollection == null)
			{
				dataSourceInfoCollection = pubResult.DataSources;
			}
			else
			{
				dataSourceInfoCollection = dataSourceInfoCollection.CombineOnSetDefinitionWithoutSideEffects(pubResult.DataSources);
			}
			entry.DataSources = dataSourceInfoCollection;
			entry.Report = pubResult.Report;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003327B File Offset: 0x0003147B
		private ProgressivePublishingResult CreateProgressiveReport(ProgressivePublishingContext pubContext)
		{
			return ProgressiveReport.Create(pubContext);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00033284 File Offset: 0x00031484
		private ProgressivePublishingContext GetPublishingContext(Stream rdlxStream, ProgressiveCacheEntry entry)
		{
			RSTrace.CatalogTrace.Assert(!entry.IsPowerView, "RenderEditAction: IsPowerView is true");
			DataSourceInfoCollection dataSourceInfoCollection = entry.DataSources;
			if (dataSourceInfoCollection == null)
			{
				dataSourceInfoCollection = new DataSourceInfoCollection();
			}
			return new ProgressivePublishingContext(this.m_reportContext, rdlxStream, new ReportProcessing.CheckSharedDataSource(this.m_service.CheckDataSourcePublishingCallback), Global.ProcessingConfiguration, DataProtection.Instance, dataSourceInfoCollection, new ReportProcessing.ResolveTemporaryDataSource(RSService.ResolveTemporaryDataSource), this.IsPackagedReportArchive);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000332F4 File Offset: 0x000314F4
		private ProgressiveProcessingContext GetProcessingContext(ReportProcessing.ExecutionType execType, ProgressiveCacheEntry entry)
		{
			RSTrace.CatalogTrace.Assert(!entry.IsPowerView, "RenderEditAction: IsPowerView is true");
			IProcessingDataExtensionConnection processingDataExtensionConnection = new ProgressiveDataExtensionConnection(this.m_provider.DataExtensionCallback, this.m_provider.UserContext, execType, this.m_provider.GetAdditionalTokenInterface(this.m_reportContext), entry.ConnectionPool);
			return new ProgressiveProcessingContext(this.m_provider.UserContext.UserName, this.m_provider.ResourceCallback, Localization.ClientPrimaryCulture, processingDataExtensionConnection, ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(this.m_provider.StreamManager.GetNewStream), ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), DataProtection.Instance, Global.ProcessingConfiguration, new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity), DateTime.Now, entry.DataSources);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000333C4 File Offset: 0x000315C4
		protected void RenderReport(ReportProcessing.ExecutionType execType, ProgressiveCacheEntry entry, Stream dsqStream)
		{
			using (MonitoredScope.New("RenderEditAction.RenderReport"))
			{
				RSTrace.CatalogTrace.Assert(entry.Report != null, "RenderReport requires a ProgressiveReport");
				ProgressiveProcessingResult progressiveProcessingResult = null;
				using (this.m_service.SetStreamFactory(new PowerViewNestedStreamFactory(base.MessageWriter)))
				{
					ProgressiveProcessingContext processingContext = this.GetProcessingContext(execType, entry);
					progressiveProcessingResult = entry.Report.Render(dsqStream, processingContext);
					this.m_byteCount = this.m_provider.StreamManager.PrimaryStream.Length;
				}
				RSTrace.CatalogTrace.Assert(progressiveProcessingResult != null, "RenderEditAction.RenderReport: procResult != null");
				this.WriteResults(progressiveProcessingResult.ResultValues);
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00033494 File Offset: 0x00031694
		private void WriteResults(ISerializableValues results)
		{
			if (results != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in results)
				{
					try
					{
						base.MessageWriter.WriteMessage(keyValuePair.Key, keyValuePair.Value);
					}
					catch (Exception ex)
					{
						this.ProcessWriterException(ex);
					}
				}
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003350C File Offset: 0x0003170C
		private void ProcessWriterException(Exception e)
		{
			RSException ex = new ProgressiveMessageWriteException(this.OperationName, e, null);
			if (!(e is IOException))
			{
				try
				{
					base.WriteServerError(ex);
				}
				catch (Exception ex2)
				{
					ex = new ProgressiveMessageWriteException(this.OperationName, ex, ex2.ToString());
				}
			}
			throw ex;
		}

		// Token: 0x040005BF RID: 1471
		protected readonly Stream m_inputStream;

		// Token: 0x040005C0 RID: 1472
		protected readonly IRenderEditSession m_session;

		// Token: 0x040005C1 RID: 1473
		protected readonly CatalogItemContext m_reportContext;

		// Token: 0x040005C2 RID: 1474
		private long m_byteCount;

		// Token: 0x040005C3 RID: 1475
		protected ProgressivePackageReader m_reader;

		// Token: 0x040005C4 RID: 1476
		protected readonly string m_jobId;
	}
}
