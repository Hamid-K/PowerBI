using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportServer.ExploreHost.DataSource;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200000F RID: 15
	internal class RSPowerViewDataSourceProvider : IRSPowerViewDataSourceProvider
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002D20 File Offset: 0x00000F20
		internal RSPowerViewDataSourceProvider(IRSDataSourceProvider provider)
		{
			this._dataSourceProvider = provider;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D30 File Offset: 0x00000F30
		public ExploreHostDataSourceInfo GetDataSourceInfo(string modelIdFromClient, out RSDataSourceConnection rsDataSourceConnection, out IASConnectionInfo asConnectionInfo)
		{
			long num = RSPowerViewDataSourceProvider.ValidateModelId(modelIdFromClient);
			rsDataSourceConnection = this._dataSourceProvider.GetDataSource(num);
			asConnectionInfo = RSPowerViewDataSourceProvider.ConvertRSConnection(rsDataSourceConnection);
			return DataShapingHelper.CreateDataSourceInfo("EntityDataSource", asConnectionInfo);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D68 File Offset: 0x00000F68
		public async Task<ExploreHostDataSourceConnectionInfo> GetDataSourceInfoAsync(string modelIdFromClient)
		{
			long num = RSPowerViewDataSourceProvider.ValidateModelId(modelIdFromClient);
			RSDataSourceConnection rsdataSourceConnection = await this._dataSourceProvider.GetDataSourceAsync(num);
			ASConnectionInfoForRS asconnectionInfoForRS = RSPowerViewDataSourceProvider.ConvertRSConnection(rsdataSourceConnection);
			return new ExploreHostDataSourceConnectionInfo(DataShapingHelper.CreateDataSourceInfo("EntityDataSource", asconnectionInfoForRS), rsdataSourceConnection, asconnectionInfoForRS);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DB3 File Offset: 0x00000FB3
		private static ASConnectionInfoForRS ConvertRSConnection(RSDataSourceConnection rsConnection)
		{
			return new ASConnectionInfoForRS(rsConnection);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DBC File Offset: 0x00000FBC
		private static long ValidateModelId(string modelIdFromClient)
		{
			long num = -1L;
			if (!long.TryParse(modelIdFromClient, out num))
			{
				throw new ArgumentException("modelId should be long");
			}
			return num;
		}

		// Token: 0x0400004B RID: 75
		internal const string DefaultDataSourceName = "EntityDataSource";

		// Token: 0x0400004C RID: 76
		private readonly IRSDataSourceProvider _dataSourceProvider;
	}
}
