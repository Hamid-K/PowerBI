using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportServer.ExploreHost.DataSource;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200000C RID: 12
	internal interface IRSPowerViewDataSourceProvider
	{
		// Token: 0x0600004A RID: 74
		ExploreHostDataSourceInfo GetDataSourceInfo(string modelId, out RSDataSourceConnection rsDataSourceConnection, out IASConnectionInfo asConnectionInfo);

		// Token: 0x0600004B RID: 75
		Task<ExploreHostDataSourceConnectionInfo> GetDataSourceInfoAsync(string modelId);
	}
}
