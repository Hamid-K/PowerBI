using System;
using System.Data;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200103A RID: 4154
	internal sealed class ConnectionPoolingDbProviderFactory : DelegatingDbProviderFactory
	{
		// Token: 0x06006C59 RID: 27737 RVA: 0x00175607 File Offset: 0x00173807
		public ConnectionPoolingDbProviderFactory(IPool pool, string providerName, DbProviderFactory factory)
			: base(factory)
		{
			this.pool = pool;
			this.providerName = providerName;
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x0017561E File Offset: 0x0017381E
		public override DbConnection CreateConnection()
		{
			return this.CreateConnection(false);
		}

		// Token: 0x06006C5B RID: 27739 RVA: 0x00175627 File Offset: 0x00173827
		public DbConnection CreateConnection(bool newConnection)
		{
			return new ConnectionPoolingDbProviderFactory.ConnectionPoolingConnection(this, newConnection);
		}

		// Token: 0x06006C5C RID: 27740 RVA: 0x00175630 File Offset: 0x00173830
		public static ConnectionPoolingDbProviderFactory New(IEngineHost host, string providerName, DbProviderFactory factory)
		{
			IPool pool = null;
			ILifetimeService lifetimeService = host.QueryService<ILifetimeService>();
			if (lifetimeService != null)
			{
				pool = new Pool();
				lifetimeService.Register(pool);
			}
			return new ConnectionPoolingDbProviderFactory(pool, providerName, factory);
		}

		// Token: 0x06006C5D RID: 27741 RVA: 0x00175660 File Offset: 0x00173860
		public static DbConnection GetUnwrappedConnection(DbConnection connection)
		{
			ConnectionPoolingDbProviderFactory.ConnectionPoolingConnection connectionPoolingConnection = connection as ConnectionPoolingDbProviderFactory.ConnectionPoolingConnection;
			if (connectionPoolingConnection == null)
			{
				return connection;
			}
			return connectionPoolingConnection.InnerConnection;
		}

		// Token: 0x04003C4F RID: 15439
		private readonly IPool pool;

		// Token: 0x04003C50 RID: 15440
		private readonly string providerName;

		// Token: 0x0200103B RID: 4155
		private sealed class ConnectionPoolingConnection : DbConnection
		{
			// Token: 0x06006C5E RID: 27742 RVA: 0x0017567F File Offset: 0x0017387F
			public ConnectionPoolingConnection(ConnectionPoolingDbProviderFactory factory, bool newConnection)
			{
				this.factory = factory;
				this.newConnection = newConnection;
			}

			// Token: 0x17001ED7 RID: 7895
			// (get) Token: 0x06006C5F RID: 27743 RVA: 0x00175695 File Offset: 0x00173895
			public DbConnection InnerConnection
			{
				get
				{
					this.EnsureInnerConnection();
					return this.innerConnection.Connection;
				}
			}

			// Token: 0x06006C60 RID: 27744 RVA: 0x001756A8 File Offset: 0x001738A8
			public override void Close()
			{
				if (this.innerConnection != null)
				{
					if (this.factory.pool != null)
					{
						this.factory.pool.Add(this.innerConnection);
					}
					this.innerConnection = null;
				}
			}

			// Token: 0x17001ED8 RID: 7896
			// (get) Token: 0x06006C61 RID: 27745 RVA: 0x001756DC File Offset: 0x001738DC
			// (set) Token: 0x06006C62 RID: 27746 RVA: 0x00175700 File Offset: 0x00173900
			public override string ConnectionString
			{
				get
				{
					if (this.innerConnection == null)
					{
						return this.connectionString;
					}
					return this.innerConnection.Connection.ConnectionString;
				}
				set
				{
					if (this.innerConnection == null)
					{
						this.connectionString = value;
						return;
					}
					DbConnection connection = this.innerConnection.Connection;
					this.connectionString = value;
					connection.ConnectionString = value;
				}
			}

			// Token: 0x17001ED9 RID: 7897
			// (get) Token: 0x06006C63 RID: 27747 RVA: 0x00175737 File Offset: 0x00173937
			public override string DataSource
			{
				get
				{
					this.EnsureInnerConnection();
					return this.innerConnection.Connection.DataSource;
				}
			}

			// Token: 0x17001EDA RID: 7898
			// (get) Token: 0x06006C64 RID: 27748 RVA: 0x0017574F File Offset: 0x0017394F
			public override string Database
			{
				get
				{
					this.EnsureInnerConnection();
					return this.innerConnection.Connection.Database;
				}
			}

			// Token: 0x17001EDB RID: 7899
			// (get) Token: 0x06006C65 RID: 27749 RVA: 0x00175767 File Offset: 0x00173967
			public override string ServerVersion
			{
				get
				{
					this.EnsureInnerConnection();
					return this.innerConnection.Connection.ServerVersion;
				}
			}

			// Token: 0x17001EDC RID: 7900
			// (get) Token: 0x06006C66 RID: 27750 RVA: 0x0017577F File Offset: 0x0017397F
			public override ConnectionState State
			{
				get
				{
					if (this.innerConnection == null)
					{
						return ConnectionState.Closed;
					}
					return this.innerConnection.Connection.State;
				}
			}

			// Token: 0x06006C67 RID: 27751 RVA: 0x0017579B File Offset: 0x0017399B
			public override void Open()
			{
				this.EnsureInnerConnection();
				if (this.innerConnection.Connection.State == ConnectionState.Closed)
				{
					this.innerConnection.Connection.Open();
				}
			}

			// Token: 0x06006C68 RID: 27752 RVA: 0x000091AE File Offset: 0x000073AE
			public override void ChangeDatabase(string databaseName)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006C69 RID: 27753 RVA: 0x000091AE File Offset: 0x000073AE
			protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006C6A RID: 27754 RVA: 0x001757C5 File Offset: 0x001739C5
			protected override DbCommand CreateDbCommand()
			{
				this.EnsureInnerConnection();
				return this.innerConnection.Connection.CreateCommand();
			}

			// Token: 0x06006C6B RID: 27755 RVA: 0x001757DD File Offset: 0x001739DD
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.Close();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06006C6C RID: 27756 RVA: 0x001757F0 File Offset: 0x001739F0
			private void EnsureInnerConnection()
			{
				if (this.innerConnection == null)
				{
					string key = ConnectionPoolingDbProviderFactory.PoolableDbConnection.GetKey(this.factory.providerName, this.connectionString);
					IPoolable poolable;
					if (!this.newConnection && this.factory.pool != null && this.factory.pool.TryGet(key, out poolable))
					{
						this.innerConnection = (ConnectionPoolingDbProviderFactory.PoolableDbConnection)poolable;
						return;
					}
					DbConnection dbConnection = this.factory.InnerFactory.CreateConnection();
					dbConnection.ConnectionString = this.connectionString;
					this.innerConnection = new ConnectionPoolingDbProviderFactory.PoolableDbConnection(this.factory.providerName, dbConnection);
				}
			}

			// Token: 0x04003C51 RID: 15441
			private readonly ConnectionPoolingDbProviderFactory factory;

			// Token: 0x04003C52 RID: 15442
			private readonly bool newConnection;

			// Token: 0x04003C53 RID: 15443
			private string connectionString;

			// Token: 0x04003C54 RID: 15444
			private ConnectionPoolingDbProviderFactory.PoolableDbConnection innerConnection;
		}

		// Token: 0x0200103C RID: 4156
		private sealed class PoolableDbConnection : IPoolable, IDisposable
		{
			// Token: 0x06006C6D RID: 27757 RVA: 0x0017588A File Offset: 0x00173A8A
			public PoolableDbConnection(string providerName, DbConnection connection)
			{
				this.providerName = providerName;
				this.connection = connection;
			}

			// Token: 0x17001EDD RID: 7901
			// (get) Token: 0x06006C6E RID: 27758 RVA: 0x001758A0 File Offset: 0x00173AA0
			public string Key
			{
				get
				{
					return ConnectionPoolingDbProviderFactory.PoolableDbConnection.GetKey(this.providerName, this.connection.ConnectionString);
				}
			}

			// Token: 0x17001EDE RID: 7902
			// (get) Token: 0x06006C6F RID: 27759 RVA: 0x001758B8 File Offset: 0x00173AB8
			public bool IsValid
			{
				get
				{
					return this.connection.State == ConnectionState.Open;
				}
			}

			// Token: 0x17001EDF RID: 7903
			// (get) Token: 0x06006C70 RID: 27760 RVA: 0x001758C8 File Offset: 0x00173AC8
			public DbConnection Connection
			{
				get
				{
					return this.connection;
				}
			}

			// Token: 0x06006C71 RID: 27761 RVA: 0x001758D0 File Offset: 0x00173AD0
			public void Dispose()
			{
				if (this.connection != null)
				{
					this.connection.Dispose();
					this.connection = null;
				}
			}

			// Token: 0x06006C72 RID: 27762 RVA: 0x001758EC File Offset: 0x00173AEC
			public static string GetKey(string providerName, string connectionString)
			{
				return providerName + "/" + connectionString.Replace("/", "//");
			}

			// Token: 0x04003C55 RID: 15445
			private readonly string providerName;

			// Token: 0x04003C56 RID: 15446
			private DbConnection connection;
		}
	}
}
