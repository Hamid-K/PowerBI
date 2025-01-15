using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000008 RID: 8
	internal sealed class RSDataShapingConnectionPoolAdapter : IConnectionPool
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002729 File Offset: 0x00000929
		internal RSDataShapingConnectionPoolAdapter(IDbConnectionPool connectionPool)
		{
			this._connectionPool = connectionPool;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002738 File Offset: 0x00000938
		public Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection Get(IDataSourceInfo dataSourceInfo)
		{
			ConnectionKey connectionKey = RSDataShapingConnectionPoolAdapter.CreatePoolKey(dataSourceInfo);
			return this.GetConnectionFromPool(connectionKey);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002754 File Offset: 0x00000954
		public bool Put(Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection connection, IDataSourceInfo dataSourceInfo)
		{
			ConnectionKey connectionKey = RSDataShapingConnectionPoolAdapter.CreatePoolKey(dataSourceInfo);
			return this.PoolConnection(connection, connectionKey);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002770 File Offset: 0x00000970
		private Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection GetConnectionFromPool(ConnectionKey poolKey)
		{
			Microsoft.ReportingServices.DataProcessing.IDbConnection connection = this._connectionPool.GetConnection(poolKey);
			if (connection == null)
			{
				return null;
			}
			return ((DataShapingConnectionAdapter)connection).Connection;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000279C File Offset: 0x0000099C
		private bool PoolConnection(Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbConnection connection, ConnectionKey poolKey)
		{
			if (this._connectionPool == null)
			{
				return false;
			}
			DataShapingConnectionAdapter dataShapingConnectionAdapter = new DataShapingConnectionAdapter(connection);
			return this._connectionPool.PoolConnection(dataShapingConnectionAdapter, poolKey);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027C8 File Offset: 0x000009C8
		private static ConnectionKey CreatePoolKey(IDataSourceInfo dataSourceInfo)
		{
			RSDataShapingDataSourceInfo rsdataShapingDataSourceInfo = (RSDataShapingDataSourceInfo)dataSourceInfo;
			return new ConnectionKey(rsdataShapingDataSourceInfo.Extension + "-Managed", rsdataShapingDataSourceInfo.ConnectionString, rsdataShapingDataSourceInfo.ConnectionSecurity, null, rsdataShapingDataSourceInfo.UserName, false, null);
		}

		// Token: 0x04000032 RID: 50
		private readonly IDbConnectionPool _connectionPool;
	}
}
