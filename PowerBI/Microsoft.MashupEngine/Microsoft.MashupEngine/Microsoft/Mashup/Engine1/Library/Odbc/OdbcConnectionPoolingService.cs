using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005D2 RID: 1490
	internal sealed class OdbcConnectionPoolingService : OdbcDelegatingService
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x0008DAB5 File Offset: 0x0008BCB5
		public OdbcConnectionPoolingService(IEngineHost host, IOdbcService odbc, IPool pool, IResource resource, IOdbcStatementRegistrar statementRegistrar)
			: base(odbc)
		{
			this.odbc = odbc;
			this.pool = pool;
			this.tracer = new Tracer(host, "Engine/IO/Odbc/", resource, null, null);
			this.statementRegistrar = statementRegistrar;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0008DAEC File Offset: 0x0008BCEC
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return this.tracer.Trace<OdbcConnectionPoolingService.PooledOdbcConnection>("PoolingService/CreateConnection", delegate(IHostTrace trace)
			{
				string cacheKey = args.CacheContext.GetStructuredCacheKey(Array.Empty<string>()).GetCacheKey(true);
				IPoolable poolable;
				bool flag = this.pool.TryGet(cacheKey, out poolable);
				trace.Add("UsePooled", flag, false);
				if (flag)
				{
					OdbcConnectionPoolingService.PoolableOdbcConnection poolableOdbcConnection = (OdbcConnectionPoolingService.PoolableOdbcConnection)poolable;
					if (poolableOdbcConnection.setUserQuery != null || poolableOdbcConnection.clearUserQuery != null || args.SetUserQuery != null || args.ClearUserQuery != null)
					{
						if ((poolableOdbcConnection.setUserQuery != null && poolableOdbcConnection.clearUserQuery != null) || (poolableOdbcConnection.setUserQuery == null && poolableOdbcConnection.clearUserQuery != null) || (poolableOdbcConnection.setUserQuery == null && poolableOdbcConnection.clearUserQuery == null))
						{
							if (!StringComparer.Ordinal.Equals(args.SetUserQuery, poolableOdbcConnection.setUserQuery))
							{
								if (poolableOdbcConnection.clearUserQuery != null)
								{
									poolableOdbcConnection.Connection.ExecuteNonQueryDirect(poolableOdbcConnection.clearUserQuery, EmptyArray<OdbcParameter>.Instance, this.statementRegistrar);
								}
								if (args.SetUserQuery != null)
								{
									poolableOdbcConnection.Connection.ExecuteNonQueryDirect(args.SetUserQuery, EmptyArray<OdbcParameter>.Instance, this.statementRegistrar);
								}
							}
						}
						else if (!StringComparer.Ordinal.Equals(args.SetUserQuery, poolableOdbcConnection.setUserQuery))
						{
							this.pool.Add(poolableOdbcConnection);
							return new OdbcConnectionPoolingService.PooledOdbcConnection(this.odbc.CreateConnection(args), this.pool, false, cacheKey, args.SetUserQuery, args.ClearUserQuery, this.statementRegistrar);
						}
					}
					return new OdbcConnectionPoolingService.PooledOdbcConnection(poolableOdbcConnection.Connection, this.pool, true, cacheKey, args.SetUserQuery, args.ClearUserQuery, this.statementRegistrar);
				}
				return new OdbcConnectionPoolingService.PooledOdbcConnection(this.odbc.CreateConnection(args), this.pool, false, cacheKey, args.SetUserQuery, args.ClearUserQuery, this.statementRegistrar);
			});
		}

		// Token: 0x04001480 RID: 5248
		private readonly IOdbcService odbc;

		// Token: 0x04001481 RID: 5249
		private readonly IPool pool;

		// Token: 0x04001482 RID: 5250
		private readonly Tracer tracer;

		// Token: 0x04001483 RID: 5251
		private readonly IOdbcStatementRegistrar statementRegistrar;

		// Token: 0x020005D3 RID: 1491
		private class PoolableOdbcConnection : IPoolable, IDisposable
		{
			// Token: 0x06002E87 RID: 11911 RVA: 0x0008DB29 File Offset: 0x0008BD29
			public PoolableOdbcConnection(IOdbcConnection connection, string poolKey, string setUserQuery, string clearUserQuery, IOdbcStatementRegistrar statementRegistrar)
			{
				this.connection = connection;
				this.poolKey = poolKey;
				this.pooledTime = DateTime.Now;
				this.setUserQuery = setUserQuery;
				this.clearUserQuery = clearUserQuery;
				this.statementRegistrar = statementRegistrar;
			}

			// Token: 0x17001104 RID: 4356
			// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0008DB61 File Offset: 0x0008BD61
			public IOdbcConnection Connection
			{
				get
				{
					return this.connection;
				}
			}

			// Token: 0x17001105 RID: 4357
			// (get) Token: 0x06002E89 RID: 11913 RVA: 0x0008DB69 File Offset: 0x0008BD69
			public string Key
			{
				get
				{
					return this.poolKey;
				}
			}

			// Token: 0x17001106 RID: 4358
			// (get) Token: 0x06002E8A RID: 11914 RVA: 0x00002139 File Offset: 0x00000339
			public bool IsValid
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002E8B RID: 11915 RVA: 0x0008DB74 File Offset: 0x0008BD74
			public void Dispose()
			{
				if (this.connection != null)
				{
					if (this.clearUserQuery != null)
					{
						this.connection.ExecuteNonQueryDirect(this.clearUserQuery, EmptyArray<OdbcParameter>.Instance, this.statementRegistrar);
					}
					this.connection.Dispose();
					this.connection = null;
				}
			}

			// Token: 0x04001484 RID: 5252
			private readonly DateTime pooledTime;

			// Token: 0x04001485 RID: 5253
			private readonly string poolKey;

			// Token: 0x04001486 RID: 5254
			private IOdbcConnection connection;

			// Token: 0x04001487 RID: 5255
			internal readonly string setUserQuery;

			// Token: 0x04001488 RID: 5256
			internal readonly string clearUserQuery;

			// Token: 0x04001489 RID: 5257
			internal readonly IOdbcStatementRegistrar statementRegistrar;
		}

		// Token: 0x020005D4 RID: 1492
		private class PooledOdbcConnection : OdbcDelegatingConnection
		{
			// Token: 0x06002E8C RID: 11916 RVA: 0x0008DBC0 File Offset: 0x0008BDC0
			public PooledOdbcConnection(IOdbcConnection connection, IPool pool, bool isOpen, string poolKey, string setUserQuery, string clearUserQuery, IOdbcStatementRegistrar statementRegistrar)
				: base(connection)
			{
				this.poolKey = poolKey;
				this.connection = connection;
				this.pool = pool;
				this.isOpen = isOpen;
				this.setUserQuery = setUserQuery;
				this.clearUserQuery = clearUserQuery;
				this.statementRegistrar = statementRegistrar;
			}

			// Token: 0x06002E8D RID: 11917 RVA: 0x0008DC00 File Offset: 0x0008BE00
			public override void Open()
			{
				if (!this.isOpen)
				{
					this.connection.Open();
					this.isOpen = true;
					if (this.setUserQuery != null)
					{
						this.connection.ExecuteNonQueryDirect(this.setUserQuery, EmptyArray<OdbcParameter>.Instance, this.statementRegistrar);
					}
				}
			}

			// Token: 0x06002E8E RID: 11918 RVA: 0x0008DC4C File Offset: 0x0008BE4C
			public override void Dispose()
			{
				if (this.isOpen && this.connection != null)
				{
					this.pool.Add(new OdbcConnectionPoolingService.PoolableOdbcConnection(this.connection, this.poolKey, this.setUserQuery, this.clearUserQuery, this.statementRegistrar));
					this.connection = null;
				}
			}

			// Token: 0x0400148A RID: 5258
			private bool isOpen;

			// Token: 0x0400148B RID: 5259
			private readonly string poolKey;

			// Token: 0x0400148C RID: 5260
			private IOdbcConnection connection;

			// Token: 0x0400148D RID: 5261
			private readonly IPool pool;

			// Token: 0x0400148E RID: 5262
			internal readonly string setUserQuery;

			// Token: 0x0400148F RID: 5263
			internal readonly string clearUserQuery;

			// Token: 0x04001490 RID: 5264
			internal readonly IOdbcStatementRegistrar statementRegistrar;
		}
	}
}
