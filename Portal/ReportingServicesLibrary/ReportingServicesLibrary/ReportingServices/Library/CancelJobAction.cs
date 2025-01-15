using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000134 RID: 308
	internal sealed class CancelJobAction : RSSoapAction<CancelJobActionParameters>
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x0002E29B File Offset: 0x0002C49B
		internal CancelJobAction(RSService service)
			: base("CancelJobAction", service)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.Service.PropertyProvider.GetSystemUrl());
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.CancelJobs, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.ActionParameters.Cancelled = RunningJobList.Current.CancelJob(base.ActionParameters.JobID, ReportServerAbortInfo.AbortReason.JobCanceled);
		}
	}
}
