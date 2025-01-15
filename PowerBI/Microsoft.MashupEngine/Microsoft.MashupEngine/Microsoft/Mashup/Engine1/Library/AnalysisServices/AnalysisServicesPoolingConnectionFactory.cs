using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F2E RID: 3886
	internal class AnalysisServicesPoolingConnectionFactory : IAnalysisServicesConnectionFactory
	{
		// Token: 0x060066D0 RID: 26320 RVA: 0x00161EA0 File Offset: 0x001600A0
		public AnalysisServicesPoolingConnectionFactory(string additionalCacheKey, IAnalysisServicesConnectionFactory factory, IPool pool)
		{
			this.additionalCacheKey = additionalCacheKey;
			this.pool = pool;
			this.factory = factory;
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x00161EC0 File Offset: 0x001600C0
		public IAnalysisServicesConnection CreateConnection(string connectionString)
		{
			string key = this.GetKey(connectionString);
			IPoolable poolable;
			if (this.pool.TryGet(key, out poolable))
			{
				return new AnalysisServicesPoolingConnectionFactory.ConnectionPoolingConnection(((AnalysisServicesPoolingConnectionFactory.PoolableAnalysisServiceConnection)poolable).Connection, key, this.pool);
			}
			return new AnalysisServicesPoolingConnectionFactory.ConnectionPoolingConnection(this.factory.CreateConnection(connectionString), key, this.pool);
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x00161F15 File Offset: 0x00160115
		private string GetKey(string connectionString)
		{
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add(connectionString);
			persistentCacheKeyBuilder.Add(this.additionalCacheKey);
			return persistentCacheKeyBuilder.ToString();
		}

		// Token: 0x04003889 RID: 14473
		private readonly string additionalCacheKey;

		// Token: 0x0400388A RID: 14474
		private readonly IPool pool;

		// Token: 0x0400388B RID: 14475
		private readonly IAnalysisServicesConnectionFactory factory;

		// Token: 0x02000F2F RID: 3887
		private sealed class ConnectionPoolingConnection : IAnalysisServicesConnection, IDisposable
		{
			// Token: 0x060066D3 RID: 26323 RVA: 0x00161F34 File Offset: 0x00160134
			public ConnectionPoolingConnection(IAnalysisServicesConnection connection, string poolKey, IPool pool)
			{
				this.poolKey = poolKey;
				this.pool = pool;
				this.connection = connection;
			}

			// Token: 0x060066D4 RID: 26324 RVA: 0x00161F51 File Offset: 0x00160151
			public void Open()
			{
				if (this.connection.State == ConnectionState.Closed)
				{
					this.connection.Open();
				}
			}

			// Token: 0x17001DC2 RID: 7618
			// (get) Token: 0x060066D5 RID: 26325 RVA: 0x00161F6B File Offset: 0x0016016B
			public string ServerVersion
			{
				get
				{
					return this.connection.ServerVersion;
				}
			}

			// Token: 0x17001DC3 RID: 7619
			// (get) Token: 0x060066D6 RID: 26326 RVA: 0x00161F78 File Offset: 0x00160178
			public string ProviderVersion
			{
				get
				{
					return this.connection.ProviderVersion;
				}
			}

			// Token: 0x17001DC4 RID: 7620
			// (get) Token: 0x060066D7 RID: 26327 RVA: 0x00161F85 File Offset: 0x00160185
			public ConnectionState State
			{
				get
				{
					return this.connection.State;
				}
			}

			// Token: 0x060066D8 RID: 26328 RVA: 0x00161F92 File Offset: 0x00160192
			public IAnalysisServicesCommand CreateCommand()
			{
				return this.connection.CreateCommand();
			}

			// Token: 0x060066D9 RID: 26329 RVA: 0x00161F9F File Offset: 0x0016019F
			public DataSet GetSchemaDataSet(string name, AdomdRestrictionCollection restrictions)
			{
				return this.connection.GetSchemaDataSet(name, restrictions);
			}

			// Token: 0x060066DA RID: 26330 RVA: 0x00161FAE File Offset: 0x001601AE
			public void Dispose()
			{
				if (this.connection != null && this.connection.State == ConnectionState.Open)
				{
					this.pool.Add(new AnalysisServicesPoolingConnectionFactory.PoolableAnalysisServiceConnection(this.connection, this.poolKey));
					this.connection = null;
				}
			}

			// Token: 0x0400388C RID: 14476
			private readonly string poolKey;

			// Token: 0x0400388D RID: 14477
			private readonly IPool pool;

			// Token: 0x0400388E RID: 14478
			private IAnalysisServicesConnection connection;
		}

		// Token: 0x02000F30 RID: 3888
		private sealed class PoolableAnalysisServiceConnection : IPoolable, IDisposable
		{
			// Token: 0x060066DB RID: 26331 RVA: 0x00161FE9 File Offset: 0x001601E9
			public PoolableAnalysisServiceConnection(IAnalysisServicesConnection connection, string poolKey)
			{
				this.poolKey = poolKey;
				this.connection = connection;
				this.pooledTime = DateTime.Now;
			}

			// Token: 0x17001DC5 RID: 7621
			// (get) Token: 0x060066DC RID: 26332 RVA: 0x0016200A File Offset: 0x0016020A
			public string Key
			{
				get
				{
					return this.poolKey;
				}
			}

			// Token: 0x17001DC6 RID: 7622
			// (get) Token: 0x060066DD RID: 26333 RVA: 0x00002139 File Offset: 0x00000339
			public bool IsValid
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001DC7 RID: 7623
			// (get) Token: 0x060066DE RID: 26334 RVA: 0x00162012 File Offset: 0x00160212
			public IAnalysisServicesConnection Connection
			{
				get
				{
					return this.connection;
				}
			}

			// Token: 0x060066DF RID: 26335 RVA: 0x0016201A File Offset: 0x0016021A
			public void Dispose()
			{
				if (this.connection != null)
				{
					this.connection.Dispose();
					this.connection = null;
				}
			}

			// Token: 0x0400388F RID: 14479
			private readonly DateTime pooledTime;

			// Token: 0x04003890 RID: 14480
			private readonly string poolKey;

			// Token: 0x04003891 RID: 14481
			private IAnalysisServicesConnection connection;
		}
	}
}
