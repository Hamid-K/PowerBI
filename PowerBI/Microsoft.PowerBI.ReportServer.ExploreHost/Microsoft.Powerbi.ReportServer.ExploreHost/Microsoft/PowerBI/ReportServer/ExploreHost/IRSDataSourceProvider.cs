using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200000B RID: 11
	public interface IRSDataSourceProvider
	{
		// Token: 0x06000048 RID: 72
		RSDataSourceConnection GetDataSource(long modelId);

		// Token: 0x06000049 RID: 73
		Task<RSDataSourceConnection> GetDataSourceAsync(long modelId);
	}
}
