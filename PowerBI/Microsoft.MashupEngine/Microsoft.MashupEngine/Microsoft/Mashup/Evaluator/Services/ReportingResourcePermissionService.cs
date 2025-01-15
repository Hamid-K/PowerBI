using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA3 RID: 7587
	internal sealed class ReportingResourcePermissionService : IResourcePermissionService
	{
		// Token: 0x0600BC25 RID: 48165 RVA: 0x00261307 File Offset: 0x0025F507
		public ReportingResourcePermissionService(IEngineHost engineHost)
		{
			this.resourcePermissionService = engineHost.QueryService<IResourcePermissionService>();
			this.reportResourceAccess = engineHost.QueryService<IReportResourceAccess>();
		}

		// Token: 0x0600BC26 RID: 48166 RVA: 0x00261327 File Offset: 0x0025F527
		public bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
		{
			if (this.resourcePermissionService.IsResourceAccessPermitted(resource, out credentials))
			{
				if (this.reportResourceAccess != null)
				{
					this.reportResourceAccess.ResourceAccessed(resource);
				}
				return true;
			}
			credentials = null;
			return false;
		}

		// Token: 0x04005FC2 RID: 24514
		private readonly IResourcePermissionService resourcePermissionService;

		// Token: 0x04005FC3 RID: 24515
		private readonly IReportResourceAccess reportResourceAccess;
	}
}
