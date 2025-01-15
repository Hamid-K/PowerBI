using System;
using Microsoft.PowerBI.ReportServer.ExploreHost;
using Microsoft.PowerBI.ReportServer.ExploreHost.Logging;
using Microsoft.PowerBI.ReportServer.WebApi.FeatureSwitches;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002F RID: 47
	public sealed class RSExploreHostFactory : IRSExploreHostFactory
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004745 File Offset: 0x00002945
		public IRSExploreHost CreateRSExploreHost(IReportServerLogger logger, IRSDataSourceProvider dataSourceProvider, string requestId, string clientSessionId)
		{
			return new RSExploreHost(logger, dataSourceProvider, PbiFeatureSwitches.ServerSwitches, requestId, clientSessionId);
		}
	}
}
