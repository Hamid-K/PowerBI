using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.DataExtension
{
	// Token: 0x02000094 RID: 148
	internal sealed class ConnectionProvider
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000C023 File Offset: 0x0000A223
		public ConnectionProvider(IConnectionFactory connectionFactory, IConnectionPool connectionPool, ITracer tracer)
		{
			this.m_connectionFactory = connectionFactory;
			this.m_connectionPool = connectionPool;
			this.m_tracer = tracer;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C040 File Offset: 0x0000A240
		internal async Task<IDbConnection> CreateOpenedConnectionAsync(IDataSourceInfo dataSourceInfo)
		{
			IDbConnection dbConnection = null;
			IDbConnection dbConnection2;
			try
			{
				dbConnection = this.m_connectionPool.Get(dataSourceInfo);
				bool flag = dbConnection != null;
				if (flag)
				{
					TaskAwaiter<bool> taskAwaiter = dbConnection.IsAliveAsync().GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					flag = !taskAwaiter.GetResult();
				}
				if (flag)
				{
					dbConnection.Dispose();
					dbConnection = null;
				}
				if (dbConnection == null)
				{
					dbConnection = this.m_connectionFactory.CreateConnection(dataSourceInfo.Extension, dataSourceInfo.ConnectionString);
					await dbConnection.OpenAsync();
				}
				dbConnection2 = dbConnection;
			}
			catch (DataExtensionException ex)
			{
				this.m_tracer.TraceDataExtensionError(TraceLevel.Error, ex, "Error getting and/or creating an open connection");
				if (dbConnection != null)
				{
					dbConnection.Dispose();
				}
				dbConnection2 = null;
			}
			return dbConnection2;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C08C File Offset: 0x0000A28C
		internal async Task ReleaseConnectionAsync(IDbConnection connection, IDataSourceInfo dataSourceInfo)
		{
			bool connectionPooled = false;
			try
			{
				connectionPooled = this.m_connectionPool.Put(connection, dataSourceInfo);
				if (!connectionPooled)
				{
					await connection.CloseAsync();
				}
			}
			catch (DataExtensionException ex)
			{
				this.m_tracer.TraceDataExtensionError(TraceLevel.Error, ex, "Error closing connection");
			}
			finally
			{
				if (connection != null && !connectionPooled)
				{
					connection.Dispose();
				}
			}
		}

		// Token: 0x040001BF RID: 447
		private readonly IConnectionFactory m_connectionFactory;

		// Token: 0x040001C0 RID: 448
		private readonly IConnectionPool m_connectionPool;

		// Token: 0x040001C1 RID: 449
		private readonly ITracer m_tracer;
	}
}
