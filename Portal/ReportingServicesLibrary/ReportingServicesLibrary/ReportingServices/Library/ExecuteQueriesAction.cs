using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ProgressivePackaging;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000181 RID: 385
	internal sealed class ExecuteQueriesAction : ModelActionBase
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x00033560 File Offset: 0x00031760
		public ExecuteQueriesAction(IRenderEditSession session, string itemPath, string dataSourceName, CatalogItemContext itemContext, Stream inputStream, Stream outputStream, IList<string> responseFlags, RSService service, string jobId, bool isClientCancelable)
			: base(itemPath, itemContext, outputStream, responseFlags, service)
		{
			RSTrace.CatalogTrace.Assert(session != null, "ExecuteQueriesAction.ctor: session != null");
			RSTrace.CatalogTrace.Assert(inputStream != null, "ExecuteQueriesAction.ctor: inputStream != null");
			this.m_session = session;
			this.m_inputStream = inputStream;
			this.m_jobId = jobId;
			this.m_dataSourceName = dataSourceName;
			this.m_isClientCancelable = isClientCancelable;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000335C9 File Offset: 0x000317C9
		protected override string OperationName
		{
			get
			{
				return "ExecuteQueries";
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000335D0 File Offset: 0x000317D0
		protected override bool InitializeAction()
		{
			return base.TryGetProgressivePackageReader(this.m_inputStream, out this.m_reader);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000335E4 File Offset: 0x000317E4
		protected override void FinalCleanup(ErrorCode status)
		{
			if (this.m_reader != null)
			{
				this.m_reader.Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00033600 File Offset: 0x00031800
		protected override void InternalExecute()
		{
			ProgressiveCacheEntry progressiveCacheEntry;
			if (!base.EnsureValidSessionExists(this.m_session, out progressiveCacheEntry))
			{
				return;
			}
			RSTrace.CatalogTrace.Assert(this.m_reader != null, "ProgressivePackageReader is null");
			IDbConnectionPool dbConnectionPool = ProgressiveExecutionCacheManager.LoadConnectionPool(progressiveCacheEntry);
			DataSourceInfo dataSourceInfo = this.m_dataSourceResolver.LoadDataSourceInfoForItem(progressiveCacheEntry, this.m_dataSourceName, false);
			RSTrace.CatalogTrace.Assert(dataSourceInfo != null, "Failed to load DataSourceInfo for item");
			this.m_dataSourceResolver.ProcessDataSourceInfoForSecureStoreCredentials(dataSourceInfo);
			Stream stream = this.m_reader.ConsumeRequiredValue<Stream>("eqr");
			if (stream == null)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "ExecuteQueriesRequestStream is missing");
				base.MessageWriter.WriteMessage("serverErrorCode", "MissingExecuteQueriesRequest");
				return;
			}
			using (this.m_service.SetStreamFactory(new PowerViewNestedStreamFactory(base.MessageWriter)))
			{
				progressiveCacheEntry.ConcurrentProgressiveActionStarted();
				try
				{
					using (ServerDataExtensionConnectionWrapper serverDataExtensionConnectionWrapper = ProgressiveDataExtensionConnection.Open(this.m_service, this.m_itemContext, dataSourceInfo, dbConnectionPool))
					{
						ExecuteQueriesContext executeQueriesContext = new ExecuteQueriesContext(serverDataExtensionConnectionWrapper.Connection, serverDataExtensionConnectionWrapper.ConnectionFactory, dataSourceInfo, new CreateAndRegisterStream(this.m_provider.StreamManager.GetNewStream), ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext));
						IDataSegmentRenderer dataSegmentRenderer = DataSegmentRendererFactory.CreateDataSegmentRenderer();
						if (this.m_isClientCancelable)
						{
							progressiveCacheEntry.AddJob(this.m_jobId, false);
						}
						try
						{
							dataSegmentRenderer.ExecuteQueries(stream, executeQueriesContext);
						}
						catch (ReportRenderingException ex)
						{
							if (ex.Unexpected)
							{
								throw;
							}
							throw new ExecuteQueriesFailureException(this.m_itemPath, ex.ErrorCode, ex);
						}
						finally
						{
							if (this.m_isClientCancelable)
							{
								progressiveCacheEntry.RemoveJob(this.m_jobId, false);
							}
						}
					}
				}
				finally
				{
					progressiveCacheEntry.ConcurrentProgressiveActionCompleted();
				}
			}
		}

		// Token: 0x040005C5 RID: 1477
		private readonly IRenderEditSession m_session;

		// Token: 0x040005C6 RID: 1478
		private readonly Stream m_inputStream;

		// Token: 0x040005C7 RID: 1479
		private ProgressivePackageReader m_reader;

		// Token: 0x040005C8 RID: 1480
		private readonly string m_jobId;

		// Token: 0x040005C9 RID: 1481
		private readonly string m_dataSourceName;

		// Token: 0x040005CA RID: 1482
		private readonly bool m_isClientCancelable;
	}
}
