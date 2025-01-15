using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000173 RID: 371
	internal class CancelProgressiveSessionJobsAction : ProgressivePackageActionBase
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00031A6F File Offset: 0x0002FC6F
		internal CancelProgressiveSessionJobsAction(IRenderEditSession session, Stream outputStream, IList<string> responseFlags, RSService service)
			: base(outputStream, responseFlags, service)
		{
			RSTrace.CatalogTrace.Assert(session != null, "CancelProgressiveSessionJobsAction.ctor: session != null");
			this.m_session = session;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool InitializeAction()
		{
			return true;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00031A98 File Offset: 0x0002FC98
		protected override void ExecuteAction()
		{
			try
			{
				int num;
				int num2;
				CancelProgressiveSessionJobsAction.CancelSessionJobs(this.m_session, this.m_service.UserName, this.OperationName, out num, out num2);
				base.MessageWriter.WriteMessage("numCancellableJobs", num);
				base.MessageWriter.WriteMessage("numCancelledJobs", num2);
			}
			catch (SessionNotFoundException)
			{
				base.WriteSessionNotFoundError();
			}
			catch (InvalidSessionIdException)
			{
				this.WriteErrorMessage("serverErrorCode", "InvalidSessionId");
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00031B2C File Offset: 0x0002FD2C
		public static void CancelSessionJobs(IRenderEditSession session, string userName, string operationName, out int numCancellableJobs, out int numCancelSuccess)
		{
			numCancellableJobs = 0;
			numCancelSuccess = 0;
			ProgressiveCacheEntry progressiveCacheEntry;
			session.EnsureValidSessionExists(userName, operationName, out progressiveCacheEntry);
			IList<string> runningJobs = progressiveCacheEntry.RunningJobs;
			numCancellableJobs = runningJobs.Count;
			foreach (string text in runningJobs)
			{
				try
				{
					if (RunningJobList.Current.CancelJob(text, ReportServerAbortInfo.AbortReason.JobCanceled))
					{
						numCancelSuccess++;
					}
				}
				catch (Exception ex)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Error occurred while cancelling job {1}: {2}", new object[] { operationName, text, ex.Message });
				}
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void FinalCleanup(ErrorCode status)
		{
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00031BE0 File Offset: 0x0002FDE0
		protected override string OperationName
		{
			get
			{
				return "CancelProgressiveSessionJobs";
			}
		}

		// Token: 0x0400059F RID: 1439
		private readonly IRenderEditSession m_session;
	}
}
