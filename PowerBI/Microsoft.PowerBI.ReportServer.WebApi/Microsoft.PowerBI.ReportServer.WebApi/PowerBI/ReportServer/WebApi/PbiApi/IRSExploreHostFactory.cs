using System;
using Microsoft.PowerBI.ReportServer.ExploreHost;
using Microsoft.PowerBI.ReportServer.ExploreHost.Logging;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000027 RID: 39
	public interface IRSExploreHostFactory
	{
		// Token: 0x06000099 RID: 153
		IRSExploreHost CreateRSExploreHost(IReportServerLogger reportServerLogger, IRSDataSourceProvider dataSourceProvider, string requestId, string clientSessionId);
	}
}
