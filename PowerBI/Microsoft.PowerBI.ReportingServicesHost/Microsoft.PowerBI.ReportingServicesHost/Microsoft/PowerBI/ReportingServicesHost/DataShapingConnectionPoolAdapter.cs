using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000035 RID: 53
	internal sealed class DataShapingConnectionPoolAdapter : IConnectionPool
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000044DA File Offset: 0x000026DA
		internal DataShapingConnectionPoolAdapter(IDbConnectionPool connectionPool)
		{
			this.m_connectionPool = connectionPool;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000044EC File Offset: 0x000026EC
		public IDbConnection Get(IDataSourceInfo dataSourceInfo)
		{
			ConnectionKey connectionKey = this.CreatePoolKey(dataSourceInfo);
			return this.GetConnectionFromPool(connectionKey);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004508 File Offset: 0x00002708
		public bool Put(IDbConnection connection, IDataSourceInfo dataSourceInfo)
		{
			ConnectionKey connectionKey = this.CreatePoolKey(dataSourceInfo);
			return this.PoolConnection(connection, connectionKey);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004528 File Offset: 0x00002728
		private IDbConnection GetConnectionFromPool(ConnectionKey poolKey)
		{
			if (this.m_connectionPool == null)
			{
				return null;
			}
			DataShapingConnectionAdapter dataShapingConnectionAdapter = (DataShapingConnectionAdapter)this.m_connectionPool.GetConnection(poolKey);
			if (dataShapingConnectionAdapter == null)
			{
				return null;
			}
			return dataShapingConnectionAdapter.Connection;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000455C File Offset: 0x0000275C
		private bool PoolConnection(IDbConnection connection, ConnectionKey poolKey)
		{
			if (this.m_connectionPool == null)
			{
				return false;
			}
			DataShapingConnectionAdapter dataShapingConnectionAdapter = new DataShapingConnectionAdapter(connection);
			return this.m_connectionPool.PoolConnection(dataShapingConnectionAdapter, poolKey);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004587 File Offset: 0x00002787
		private ConnectionKey CreatePoolKey(IDataSourceInfo dataSourceInfo)
		{
			return new ConnectionKey(dataSourceInfo.Extension + "-Managed", dataSourceInfo.ConnectionString, ConnectionSecurity.None, null, null, false, null);
		}

		// Token: 0x040000E7 RID: 231
		private readonly IDbConnectionPool m_connectionPool;
	}
}
