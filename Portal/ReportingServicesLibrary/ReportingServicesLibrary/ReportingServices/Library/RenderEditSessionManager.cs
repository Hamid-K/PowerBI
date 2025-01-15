using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018F RID: 399
	internal sealed class RenderEditSessionManager
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x00035CFC File Offset: 0x00033EFC
		internal static void ExecuteInRenderEditSession(IPowerViewExecution executor, IRenderEditSession session, string jobId, string userName, string operationName)
		{
			bool flag = false;
			bool flag2 = false;
			ProgressiveCacheEntry progressiveCacheEntry;
			bool flag3 = ProgressiveExecutionCacheManager.TryGetOrCreateCacheEntry(session, userName, operationName, out progressiveCacheEntry, out flag2);
			if (!progressiveCacheEntry.AddJob(jobId, true))
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "InvalidConcurrentRenderEditSessionRequest", new object[] { "Invalid concurrent RenderEdit session request" });
				executor.WriteMessage("serverErrorCode", "InvalidConcurrentRenderEditSessionRequest");
				return;
			}
			try
			{
				executor.ProcessInput(flag3, progressiveCacheEntry);
				bool flag4 = false;
				if (session.IsNewSession || !flag3)
				{
					executor.WriteMessage("serverErrorCode", "SessionNotFound");
					if (progressiveCacheEntry.Report == null)
					{
						return;
					}
					flag4 = true;
				}
				if (flag4)
				{
					session.GenerateSession();
					ProgressiveExecutionCacheManager.PutCacheEntry(session, progressiveCacheEntry);
					flag = true;
				}
				else
				{
					ProgressiveReportCounters.Current.ExistingSessionTotal.Increment();
				}
				executor.WriteSessionId(session.SessionId);
				if (!flag4)
				{
					executor.RenderItem();
				}
			}
			finally
			{
				RSTrace.CatalogTrace.Assert(progressiveCacheEntry != null, "RenderEditAction.ExecuteAction: entry != null");
				progressiveCacheEntry.RemoveJob(jobId, true);
				if (flag2 && !flag)
				{
					progressiveCacheEntry.Dispose();
				}
			}
		}
	}
}
