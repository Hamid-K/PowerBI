using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000185 RID: 389
	internal abstract class GetModelActionBase : ModelActionBase
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x000341FC File Offset: 0x000323FC
		public GetModelActionBase(IRenderEditSession session, string itemPath, string dataSourceName, string modelMetadataVersion, Stream inputStream, Stream outputStream, IList<string> responseFlags, RSService service, CatalogItemContext itemContext)
			: base(itemPath, itemContext, outputStream, responseFlags, service, true)
		{
			RSTrace.CatalogTrace.Assert(session != null, "GetModelActionBase.ctor: session != null");
			this.m_session = session;
			this.m_dataSourceName = dataSourceName;
			this.m_modelMetadataVersion = modelMetadataVersion;
			this.m_inputStream = inputStream;
		}

		// Token: 0x06000E3A RID: 3642
		protected abstract bool PopulateCacheWithInput(ProgressiveCacheEntry entry);

		// Token: 0x06000E3B RID: 3643
		protected abstract string ResolveModel(ProgressiveCacheEntry entry, bool isDataSourcePresent);

		// Token: 0x06000E3C RID: 3644 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool InitializeAction()
		{
			return true;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void FinalCleanup(ErrorCode status)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003424C File Offset: 0x0003244C
		protected override void InternalExecute()
		{
			if (!base.ValidateSession(this.m_session))
			{
				return;
			}
			using (MonitoredScope.NewFormat("GetModelActionBase.InternalExecute[Session ID={0}]", this.m_session.SessionId))
			{
				bool flag = false;
				ProgressiveCacheEntry progressiveCacheEntry;
				bool flag3;
				bool flag2 = ProgressiveExecutionCacheManager.TryGetOrCreateCacheEntry(this.m_session, this.m_service.UserName, this.OperationName, out progressiveCacheEntry, out flag3);
				progressiveCacheEntry.ConcurrentProgressiveActionStarted();
				try
				{
					if (this.m_session.IsNewSession || !flag2)
					{
						this.m_session.GenerateSession();
						ProgressiveExecutionCacheManager.PutCacheEntry(this.m_session, progressiveCacheEntry);
						flag = true;
					}
					else
					{
						ProgressiveReportCounters.Current.ExistingSessionTotal.Increment();
					}
					bool flag4 = this.PopulateCacheWithInput(progressiveCacheEntry);
					string text = this.ResolveModel(progressiveCacheEntry, flag4);
					base.MessageWriter.WriteMessage("sessionId", this.m_session.SessionId);
					this.WriteModelToOutput(text);
				}
				finally
				{
					progressiveCacheEntry.ConcurrentProgressiveActionCompleted();
					if (flag3 && !flag)
					{
						progressiveCacheEntry.Dispose();
					}
				}
			}
		}

		// Token: 0x040005DA RID: 1498
		protected IRenderEditSession m_session;

		// Token: 0x040005DB RID: 1499
		protected IDbConnectionPool m_connectionPool;

		// Token: 0x040005DC RID: 1500
		protected readonly string m_dataSourceName;

		// Token: 0x040005DD RID: 1501
		protected readonly string m_modelMetadataVersion;

		// Token: 0x040005DE RID: 1502
		protected readonly Stream m_inputStream;
	}
}
