using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000136 RID: 310
	internal sealed class ListRunningJobsAction : RSSoapAction<ListRunningJobsActionParameters>
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0002E339 File Offset: 0x0002C539
		internal ListRunningJobsAction(RSService service)
			: base("", service)
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002E348 File Offset: 0x0002C548
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.Service.PropertyProvider.GetSystemUrl());
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.ListJobs, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.ActionParameters.Jobs = RunningJobsDb.ListRunningJobs();
		}
	}
}
