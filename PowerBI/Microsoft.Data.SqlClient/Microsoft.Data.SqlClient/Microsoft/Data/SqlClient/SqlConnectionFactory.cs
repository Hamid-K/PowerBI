using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E8 RID: 232
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0003F7DE File Offset: 0x0003D9DE
		private SqlConnectionFactory()
			: base(SqlPerformanceCounters.SingletonInstance)
		{
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0003CCA5 File Offset: 0x0003AEA5
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003F7EB File Offset: 0x0003D9EB
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection, null);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003F7FC File Offset: 0x0003D9FC
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlConnectionPoolKey sqlConnectionPoolKey = (SqlConnectionPoolKey)poolKey;
			SessionData sessionData = null;
			SqlConnection sqlConnection = owningConnection as SqlConnection;
			bool flag = sqlConnection != null && sqlConnection._applyTransientFaultHandling;
			SqlConnectionString sqlConnectionString2 = null;
			if (userOptions != null)
			{
				sqlConnectionString2 = (SqlConnectionString)userOptions;
			}
			else if (sqlConnection != null)
			{
				sqlConnectionString2 = (SqlConnectionString)sqlConnection.UserConnectionOptions;
			}
			if (sqlConnection != null)
			{
				sessionData = sqlConnection._recoverySessionData;
			}
			SqlInternalConnection sqlInternalConnection;
			if (sqlConnectionString.ContextConnection)
			{
				sqlInternalConnection = this.GetContextConnection(sqlConnectionString, poolGroupProviderInfo);
			}
			else
			{
				bool flag2 = false;
				DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.UsesCertificate || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					if (pool != null)
					{
						dbConnectionPoolIdentity = pool.Identity;
					}
					else
					{
						dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					}
				}
				if (sqlConnectionString.UserInstance)
				{
					flag2 = true;
					string text;
					if (pool == null || (pool != null && pool.Count <= 0))
					{
						SqlInternalConnectionTds sqlInternalConnectionTds = null;
						try
						{
							SqlConnectionString sqlConnectionString3 = new SqlConnectionString(sqlConnectionString, sqlConnectionString.DataSource, true, new bool?(false));
							sqlInternalConnectionTds = new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString3, sqlConnectionPoolKey.Credential, null, "", null, false, null, null, null, null, null, null, null, flag);
							text = sqlInternalConnectionTds.InstanceName;
							if (!text.StartsWith("\\\\.\\", StringComparison.Ordinal))
							{
								throw SQL.NonLocalSSEInstance();
							}
							if (pool != null)
							{
								SqlConnectionPoolProviderInfo sqlConnectionPoolProviderInfo = (SqlConnectionPoolProviderInfo)pool.ProviderInfo;
								sqlConnectionPoolProviderInfo.InstanceName = text;
							}
							goto IL_0164;
						}
						finally
						{
							if (sqlInternalConnectionTds != null)
							{
								sqlInternalConnectionTds.Dispose();
							}
						}
					}
					SqlConnectionPoolProviderInfo sqlConnectionPoolProviderInfo2 = (SqlConnectionPoolProviderInfo)pool.ProviderInfo;
					text = sqlConnectionPoolProviderInfo2.InstanceName;
					IL_0164:
					sqlConnectionString = new SqlConnectionString(sqlConnectionString, text, false, null);
					poolGroupProviderInfo = null;
				}
				sqlInternalConnection = new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString, sqlConnectionPoolKey.Credential, poolGroupProviderInfo, "", null, flag2, sqlConnectionString2, sessionData, sqlConnectionPoolKey.ServerCertificateValidationCallback, sqlConnectionPoolKey.ClientCertificateRetrievalCallback, pool, sqlConnectionPoolKey.AccessToken, sqlConnectionPoolKey.OriginalNetworkAddressInfo, flag);
			}
			return sqlInternalConnection;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003F9CC File Offset: 0x0003DBCC
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003F9E4 File Offset: 0x0003DBE4
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				dbConnectionPoolProviderInfo = new SqlConnectionPoolProviderInfo();
			}
			return dbConnectionPoolProviderInfo;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003FA08 File Offset: 0x0003DC08
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)connectionOptions;
			DbConnectionPoolGroupOptions dbConnectionPoolGroupOptions = null;
			if (!sqlConnectionString.ContextConnection && sqlConnectionString.Pooling)
			{
				int num = sqlConnectionString.ConnectTimeout;
				if (0 < num && num < 2147483)
				{
					num *= 1000;
				}
				else if (num >= 2147483)
				{
					num = int.MaxValue;
				}
				if (sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive)
				{
					if (num >= 214748364)
					{
						num = int.MaxValue;
					}
					else
					{
						num *= 10;
					}
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlConnectionFactory.CreateConnectionPoolGroupOptions>Set connection pool CreateTimeout={0} when AD Interactive is in use.", num);
				}
				dbConnectionPoolGroupOptions = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity || sqlConnectionString.UsesCertificate || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist);
			}
			return dbConnectionPoolGroupOptions;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003FACC File Offset: 0x0003DCCC
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Data.SqlClient.SqlMetaData.xml");
			cacheMetaDataFactory = true;
			return new SqlMetaDataFactory(manifestResourceStream, internalConnection.ServerVersion, internalConnection.ServerVersion);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003FAFE File Offset: 0x0003DCFE
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003FB0C File Offset: 0x0003DD0C
		internal static SqlConnectionString FindSqlConnectionOptions(SqlConnectionPoolKey key)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(key);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(key.ConnectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003FB48 File Offset: 0x0003DD48
		private SqlInternalConnectionSmi GetContextConnection(SqlConnectionString options, object providerInfo)
		{
			SmiContext currentContext = SmiContextFactory.Instance.GetCurrentContext();
			SqlInternalConnectionSmi sqlInternalConnectionSmi = (SqlInternalConnectionSmi)currentContext.GetContextValue(0);
			if (sqlInternalConnectionSmi == null || sqlInternalConnectionSmi.IsConnectionDoomed)
			{
				if (sqlInternalConnectionSmi != null)
				{
					sqlInternalConnectionSmi.Dispose();
				}
				sqlInternalConnectionSmi = new SqlInternalConnectionSmi(options, currentContext);
				currentContext.SetContextValue(0, sqlInternalConnectionSmi);
			}
			sqlInternalConnectionSmi.Activate();
			return sqlInternalConnectionSmi;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003FB98 File Offset: 0x0003DD98
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		protected override int GetObjectId(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003FC18 File Offset: 0x0003DE18
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003FC38 File Offset: 0x0003DE38
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003FC58 File Offset: 0x0003DE58
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003FC7C File Offset: 0x0003DE7C
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04000739 RID: 1849
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();

		// Token: 0x0400073A RID: 1850
		private const string _metaDataXml = "MetaDataXml";
	}
}
