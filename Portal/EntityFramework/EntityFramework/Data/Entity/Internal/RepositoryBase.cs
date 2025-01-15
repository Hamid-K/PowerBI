using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000129 RID: 297
	internal abstract class RepositoryBase
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x00035C99 File Offset: 0x00033E99
		protected RepositoryBase(InternalContext usersContext, string connectionString, DbProviderFactory providerFactory)
		{
			this._usersContext = usersContext;
			this._connectionString = connectionString;
			this._providerFactory = providerFactory;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00035CB8 File Offset: 0x00033EB8
		protected DbConnection CreateConnection()
		{
			DbConnection dbConnection;
			if (!this._usersContext.IsDisposed && (dbConnection = this._usersContext.Connection) != null)
			{
				if (dbConnection.State == ConnectionState.Open)
				{
					return dbConnection;
				}
				dbConnection = DbProviderServices.GetProviderServices(dbConnection).CloneDbConnection(dbConnection, this._providerFactory);
			}
			else
			{
				dbConnection = this._providerFactory.CreateConnection();
			}
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(this._connectionString));
			return dbConnection;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00035D2E File Offset: 0x00033F2E
		protected void DisposeConnection(DbConnection connection)
		{
			if (connection != null && (this._usersContext.IsDisposed || connection != this._usersContext.Connection))
			{
				DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
			}
		}

		// Token: 0x040009AA RID: 2474
		private readonly InternalContext _usersContext;

		// Token: 0x040009AB RID: 2475
		private readonly string _connectionString;

		// Token: 0x040009AC RID: 2476
		private readonly DbProviderFactory _providerFactory;
	}
}
