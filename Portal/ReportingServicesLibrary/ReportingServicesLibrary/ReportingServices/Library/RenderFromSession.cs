using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011D RID: 285
	internal class RenderFromSession : RenderWithSeperatePagination
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0002AB7F File Offset: 0x00028D7F
		public RenderFromSession(ReportExecutionBase execContext)
			: base(execContext)
		{
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002AB88 File Offset: 0x00028D88
		protected override void SetSourceSnapshot()
		{
			ClientRequest session = base.ExecutionContext.RequestInfo.Session;
			RSTrace.CatalogTrace.Assert(session != null, "session");
			base.ExecutionContext.ExecutionDateTime = session.SessionReport.ExecutionDateTime;
			base.OriginalSnapshot = session.SessionReport.Report.SnapshotData;
			base.SetPaginationDataFromSnapshot(base.OriginalSnapshot);
			RSTrace.CatalogTrace.Assert(base.OriginalSnapshot != null, "OriginalSnapshot");
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002AC0C File Offset: 0x00028E0C
		protected override bool CheckNeedsReprocessing()
		{
			RSTrace.CatalogTrace.Assert(base.OriginalSnapshot != null, "OriginalSnapshot");
			ClientRequest session = base.ExecutionContext.RequestInfo.Session;
			RSTrace.CatalogTrace.Assert(session != null, "session");
			bool flag = false;
			if (session.SessionReport.Report.SnapshotParametersHaveChanged)
			{
				ExecTrace.TraceVerbose("Parameter change in session requires reprocessing");
				flag = true;
			}
			base.ExecutionContext.EffectiveParameters = session.SessionReport.Report.EffectiveParams;
			return flag;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002AC94 File Offset: 0x00028E94
		protected void VerifySession()
		{
			ClientRequest session = base.ExecutionContext.RequestInfo.Session;
			UserContext userContext = base.ExecutionContext.DataProvider.UserContext;
			if (!Security.IsSameUser(userContext, session.SessionReport.UserContext))
			{
				RSTrace.CatalogTrace.Assert(false, "Failed to validate user name");
				throw new AccessDeniedException(userContext.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		protected override bool IsSharedSnapshot
		{
			get
			{
				return !base.NeedsReprocessing && (base.SnapshotManager.OriginalSnapshot.IsPermanentSnapshot || base.ExecutionContext.FoundInCache);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002AD1F File Offset: 0x00028F1F
		protected override bool UpdateSnapshotOnChange
		{
			get
			{
				return !base.NeedsReprocessing && (this.IsSharedSnapshot || Global.UseLocalFileStore(base.SnapshotManager.OriginalSnapshot.IsPermanentSnapshot));
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002AD4A File Offset: 0x00028F4A
		public override ExecutionLogExecType ExecutionType
		{
			get
			{
				return ExecutionLogExecType.Session;
			}
		}
	}
}
