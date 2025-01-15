using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.InfoNav.Experimental.Insights;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.DataExtension;
using Microsoft.PowerBI.Insights.Hosting;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000082 RID: 130
	internal sealed class InsightsConnectionFactory : IUserScopedConnectionFactory, IConnectionFactory
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public string DatabaseName
		{
			get
			{
				return this.m_catalogName;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000AEE8 File Offset: 0x000090E8
		public InsightsConnectionFactory(IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, ITracer tracer)
		{
			this.m_connectionProvider = new ConnectionProvider(connectionFactory, connectionPool, tracer);
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_catalogName = InsightsConnectionFactory.GetCatalogName(dataSourceInfo.ConnectionString);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000AF18 File Offset: 0x00009118
		public async Task<ValueOrError<IDbConnectionWrapper, BaseError>> CreateOpenConnectionAsync(int timeoutSeconds)
		{
			IDbConnection dbConnection = await this.m_connectionProvider.CreateOpenedConnectionAsync(this.m_dataSourceInfo);
			ValueOrError<IDbConnectionWrapper, BaseError> valueOrError;
			if (dbConnection == null)
			{
				valueOrError = InsightsConnectionFactory.GenericOpenConnectionError;
			}
			else
			{
				valueOrError = new DbConnectionWrapper(dbConnection);
			}
			return valueOrError;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000AF5B File Offset: 0x0000915B
		public Task ReleaseConnectionAsync(IDbConnectionWrapper connectionWrapper)
		{
			return this.m_connectionProvider.ReleaseConnectionAsync(connectionWrapper.Connection, this.m_dataSourceInfo);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000AF74 File Offset: 0x00009174
		private static string GetCatalogName(string connectionString)
		{
			return (string)new DbConnectionStringBuilder
			{
				ConnectionString = connectionString
			}["Initial Catalog"];
		}

		// Token: 0x0400019A RID: 410
		private static readonly BaseError GenericOpenConnectionError = new BaseError(1, "Unknown error while attempting to create an open connection", null);

		// Token: 0x0400019B RID: 411
		private readonly ConnectionProvider m_connectionProvider;

		// Token: 0x0400019C RID: 412
		private readonly IDataSourceInfo m_dataSourceInfo;

		// Token: 0x0400019D RID: 413
		private readonly string m_catalogName;
	}
}
