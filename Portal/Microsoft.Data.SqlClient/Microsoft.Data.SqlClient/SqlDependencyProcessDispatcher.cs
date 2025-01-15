using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Xml;
using Microsoft.Data;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.SqlClient;

// Token: 0x02000005 RID: 5
internal class SqlDependencyProcessDispatcher : MarshalByRefObject
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002058 File Offset: 0x00000258
	internal int ObjectID { get; } = Interlocked.Increment(ref SqlDependencyProcessDispatcher.s_objectTypeCount);

	// Token: 0x06000005 RID: 5 RVA: 0x00002060 File Offset: 0x00000260
	private SqlDependencyProcessDispatcher(object dummyVariable)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependencyProcessDispatcher|DEP> {0}", this.ObjectID);
		try
		{
			this._connectionContainers = new Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer>();
			this._sqlDependencyPerAppDomainDispatchers = new Dictionary<string, SqlDependencyPerAppDomainDispatcher>();
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
	public SqlDependencyProcessDispatcher()
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependencyProcessDispatcher|DEP> {0}", this.ObjectID);
		try
		{
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
	internal static SqlDependencyProcessDispatcher SingletonProcessDispatcher
	{
		get
		{
			return SqlDependencyProcessDispatcher.s_staticInstance;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
	private static SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper GetHashHelper(string connectionString, out SqlConnectionStringBuilder connectionStringBuilder, out DbConnectionPoolIdentity identity, out string user, string queue)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyProcessDispatcher.GetHashString|DEP> {0}, queue: {1}", SqlDependencyProcessDispatcher.s_staticInstance.ObjectID, queue);
		SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper;
		try
		{
			connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
			{
				Pooling = false,
				Enlist = false,
				ConnectRetryCount = 0
			};
			if (queue != null)
			{
				connectionStringBuilder.ApplicationName = queue;
			}
			if (connectionStringBuilder.IntegratedSecurity)
			{
				identity = DbConnectionPoolIdentity.GetCurrent();
				user = null;
			}
			else
			{
				identity = null;
				user = connectionStringBuilder.UserID;
			}
			sqlConnectionContainerHashHelper = new SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper(identity, connectionStringBuilder.ConnectionString, queue, connectionStringBuilder);
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
		return sqlConnectionContainerHashHelper;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000021D8 File Offset: 0x000003D8
	public override object InitializeLifetimeService()
	{
		return null;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021DC File Offset: 0x000003DC
	private void Invalidate(string server, SqlNotification sqlNotification)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyProcessDispatcher.Invalidate|DEP> {0}, server: {1}", this.ObjectID, server);
		try
		{
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher> keyValuePair in this._sqlDependencyPerAppDomainDispatchers)
				{
					SqlDependencyPerAppDomainDispatcher value = keyValuePair.Value;
					try
					{
						value.InvalidateServer(server, sqlNotification);
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
					}
				}
			}
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000022B8 File Offset: 0x000004B8
	internal void QueueAppDomainUnloading(string appDomainKey)
	{
		ThreadPool.QueueUserWorkItem(new WaitCallback(this.AppDomainUnloading), appDomainKey);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022D0 File Offset: 0x000004D0
	private void AppDomainUnloading(object state)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlDependencyProcessDispatcher.AppDomainUnloading|DEP> {0}", this.ObjectID);
		try
		{
			string text = (string)state;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper> list = new List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper>();
				foreach (KeyValuePair<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> keyValuePair in this._connectionContainers)
				{
					SqlDependencyProcessDispatcher.SqlConnectionContainer value = keyValuePair.Value;
					if (value.AppDomainUnload(text))
					{
						list.Add(value.HashHelper);
					}
				}
				foreach (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper in list)
				{
					this._connectionContainers.Remove(sqlConnectionContainerHashHelper);
				}
			}
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				this._sqlDependencyPerAppDomainDispatchers.Remove(text);
			}
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002424 File Offset: 0x00000624
	internal bool StartWithDefault(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string service, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart)
	{
		return this.Start(connectionString, out server, out identity, out user, out database, ref service, appDomainKey, dispatcher, out errorOccurred, out appDomainStart, true);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000244C File Offset: 0x0000064C
	internal bool Start(string connectionString, string queue, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher)
	{
		string text;
		DbConnectionPoolIdentity dbConnectionPoolIdentity;
		string text2;
		string text3;
		bool flag;
		bool flag2;
		return this.Start(connectionString, out text, out dbConnectionPoolIdentity, out text2, out text3, ref queue, appDomainKey, dispatcher, out flag, out flag2, false);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002474 File Offset: 0x00000674
	private bool Start(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart, bool useDefaults)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string, string, int>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, queue: '{1}', appDomainKey: '{2}', perAppDomainDispatcher ID: '{3}'", this.ObjectID, queueService, appDomainKey, dispatcher.ObjectID);
		bool flag4;
		try
		{
			server = null;
			identity = null;
			user = null;
			database = null;
			errorOccurred = false;
			appDomainStart = false;
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				if (!this._sqlDependencyPerAppDomainDispatchers.ContainsKey(appDomainKey))
				{
					this._sqlDependencyPerAppDomainDispatchers[appDomainKey] = dispatcher;
				}
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder;
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
			bool flag2 = false;
			SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = null;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				if (!this._connectionContainers.ContainsKey(hashHelper))
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent<int>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, hashtable miss, creating new container.", this.ObjectID);
					sqlConnectionContainer = new SqlDependencyProcessDispatcher.SqlConnectionContainer(hashHelper, appDomainKey, useDefaults);
					this._connectionContainers.Add(hashHelper, sqlConnectionContainer);
					flag2 = true;
					appDomainStart = true;
				}
				else
				{
					sqlConnectionContainer = this._connectionContainers[hashHelper];
					SqlClientEventSource.Log.TryNotificationTraceEvent<int, int>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, hashtable hit, container: {1}", this.ObjectID, sqlConnectionContainer.ObjectID);
					if (sqlConnectionContainer.InErrorState)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent<int, int>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, container: {1} is in error state!", this.ObjectID, sqlConnectionContainer.ObjectID);
						errorOccurred = true;
					}
					else
					{
						sqlConnectionContainer.IncrementStartCount(appDomainKey, out appDomainStart);
					}
				}
			}
			if (useDefaults && !errorOccurred)
			{
				server = sqlConnectionContainer.Server;
				database = sqlConnectionContainer.Database;
				queueService = sqlConnectionContainer.Queue;
				SqlClientEventSource.Log.TryNotificationTraceEvent<int, string, string, string>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, default service: '{1}', server: '{2}', database: '{3}'", this.ObjectID, queueService, server, database);
			}
			SqlClientEventSource.Log.TryNotificationTraceEvent<int, bool>("<sc.SqlDependencyProcessDispatcher.Start|DEP> {0}, started: {1}", this.ObjectID, flag2);
			flag4 = flag2;
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
		return flag4;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000268C File Offset: 0x0000088C
	internal bool Stop(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, out bool appDomainStop)
	{
		long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlDependencyProcessDispatcher.Stop|DEP> {0}, queue: '{1}'", this.ObjectID, queueService);
		bool flag3;
		try
		{
			server = null;
			identity = null;
			user = null;
			database = null;
			appDomainStop = false;
			SqlConnectionStringBuilder sqlConnectionStringBuilder;
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
			bool flag = false;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				if (this._connectionContainers.ContainsKey(hashHelper))
				{
					SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = this._connectionContainers[hashHelper];
					SqlClientEventSource.Log.TryNotificationTraceEvent<int, int>("<sc.SqlDependencyProcessDispatcher.Stop|DEP> {0}, hashtable hit, container: {1}", this.ObjectID, sqlConnectionContainer.ObjectID);
					server = sqlConnectionContainer.Server;
					database = sqlConnectionContainer.Database;
					queueService = sqlConnectionContainer.Queue;
					if (sqlConnectionContainer.Stop(appDomainKey, out appDomainStop))
					{
						flag = true;
						this._connectionContainers.Remove(hashHelper);
					}
				}
				else
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent<int>("<Sc.SqlDependencyProcessDispatcher.Stop|DEP> {0}, hashtable miss.", this.ObjectID);
				}
			}
			SqlClientEventSource.Log.TryNotificationTraceEvent<int, bool>("<sc.SqlDependencyProcessDispatcher.Stop|DEP> {0}, stopped: {1}", this.ObjectID, flag);
			flag3 = flag;
		}
		finally
		{
			SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
		}
		return flag3;
	}

	// Token: 0x04000001 RID: 1
	private static readonly SqlDependencyProcessDispatcher s_staticInstance = new SqlDependencyProcessDispatcher(null);

	// Token: 0x04000002 RID: 2
	private readonly Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> _connectionContainers;

	// Token: 0x04000003 RID: 3
	private readonly Dictionary<string, SqlDependencyPerAppDomainDispatcher> _sqlDependencyPerAppDomainDispatchers;

	// Token: 0x04000004 RID: 4
	private static int s_objectTypeCount;

	// Token: 0x02000188 RID: 392
	private class SqlConnectionContainer
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00076525 File Offset: 0x00074725
		internal int ObjectID { get; } = Interlocked.Increment(ref SqlDependencyProcessDispatcher.SqlConnectionContainer.s_objectTypeCount);

		// Token: 0x06001D1F RID: 7455 RVA: 0x00076530 File Offset: 0x00074730
		internal SqlConnectionContainer(SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper, string appDomainKey, bool useDefaults)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "<sc.SqlConnectionContainer|DEP> {0}, queue: '{1}'";
			int objectID = this.ObjectID;
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper2 = this.HashHelper;
			long num = log.TryNotificationScopeEnterEvent<int, string>(text, objectID, (hashHelper2 != null) ? hashHelper2.Queue : null);
			bool flag = false;
			try
			{
				this._hashHelper = hashHelper;
				string text2 = null;
				if (useDefaults)
				{
					text2 = Guid.NewGuid().ToString();
					this._queue = "SqlQueryNotificationService-" + text2;
					this._hashHelper.ConnectionStringBuilder.ApplicationName = this._queue;
				}
				else
				{
					this._queue = this._hashHelper.Queue;
				}
				this._con = new SqlConnection(this._hashHelper.ConnectionStringBuilder.ConnectionString);
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this._con.ConnectionOptions;
				sqlConnectionString.CreatePermissionSet().Assert();
				if (sqlConnectionString.LocalDBInstance != null)
				{
					LocalDBAPI.AssertLocalDBPermissions();
				}
				this._con.Open();
				this._cachedServer = this._con.DataSource;
				bool? flag2 = null;
				if (hashHelper.Identity != null)
				{
					this._windowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
				}
				this._escapedQueueName = SqlConnection.FixupDatabaseTransactionName(this._queue);
				this._appDomainKeyHash = new Dictionary<string, int>();
				this._com = new SqlCommand
				{
					Connection = this._con,
					CommandText = "select is_broker_enabled from sys.databases where database_id=db_id()"
				};
				using (SqlDataReader sqlDataReader = this._com.ExecuteReader(CommandBehavior.SingleRow))
				{
					if (sqlDataReader.Read() && sqlDataReader[0] != null)
					{
						flag2 = new bool?(sqlDataReader.GetBoolean(0));
					}
				}
				if (flag2 == null || !flag2.Value)
				{
					throw SQL.SqlDependencyDatabaseBrokerDisabled();
				}
				this._conversationGuidParam = new SqlParameter("@p1", SqlDbType.UniqueIdentifier);
				this._timeoutParam = new SqlParameter("@p2", SqlDbType.Int)
				{
					Value = 0
				};
				this._com.Parameters.Add(this._timeoutParam);
				flag = true;
				this._receiveQuery = "WAITFOR(RECEIVE TOP (1) message_type_name, conversation_handle, cast(message_body AS XML) as message_body from " + this._escapedQueueName + "), TIMEOUT @p2;";
				if (useDefaults)
				{
					this._sprocName = SqlConnection.FixupDatabaseTransactionName("SqlQueryNotificationStoredProcedure-" + text2);
					this.CreateQueueAndService(false);
				}
				else
				{
					this._com.CommandText = this._receiveQuery;
					this._endConversationQuery = "END CONVERSATION @p1; ";
					this._concatQuery = this._endConversationQuery + this._receiveQuery;
				}
				bool flag3;
				this.IncrementStartCount(appDomainKey, out flag3);
				this.SynchronouslyQueryServiceBrokerQueue();
				this._timeoutParam.Value = this._defaultWaitforTimeout;
				this.AsynchronouslyQueryServiceBrokerQueue();
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex);
				if (flag)
				{
					this.TearDownAndDispose();
				}
				else
				{
					if (this._com != null)
					{
						this._com.Dispose();
						this._com = null;
					}
					if (this._con != null)
					{
						this._con.Dispose();
						this._con = null;
					}
				}
				throw;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00076884 File Offset: 0x00074A84
		internal string Database
		{
			get
			{
				if (this._cachedDatabase == null)
				{
					this._cachedDatabase = this._con.Database;
				}
				return this._cachedDatabase;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000768A5 File Offset: 0x00074AA5
		internal SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper HashHelper
		{
			get
			{
				return this._hashHelper;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000768AD File Offset: 0x00074AAD
		internal bool InErrorState
		{
			get
			{
				return this._errorState;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x000768B7 File Offset: 0x00074AB7
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000768BF File Offset: 0x00074ABF
		internal string Server
		{
			get
			{
				return this._cachedServer;
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000768C8 File Offset: 0x00074AC8
		internal bool AppDomainUnload(string appDomainKey)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int, string>("<sc.SqlConnectionContainer.AppDomainUnload|DEP> {0}, AppDomainKey: '{1}'", this.ObjectID, appDomainKey);
			bool stopped;
			try
			{
				Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
				lock (appDomainKeyHash)
				{
					if (this._appDomainKeyHash.ContainsKey(appDomainKey))
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash contained AppDomainKey: '{0}'.", appDomainKey);
						int i = this._appDomainKeyHash[appDomainKey];
						SqlClientEventSource.Log.TryNotificationTraceEvent<string, int>("SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash for AppDomainKey: '{0}' count: '{1}'.", appDomainKey, i);
						bool flag2 = false;
						while (i > 0)
						{
							this.Stop(appDomainKey, out flag2);
							i--;
						}
						if (this._appDomainKeyHash.ContainsKey(appDomainKey))
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent<string, int>("SqlConnectionContainer.AppDomainUnload|DEP|ERR> ERROR - after the Stop() loop, _appDomainKeyHash for AppDomainKey: '{0}' entry not removed from hash.  Count: {1}'", appDomainKey, this._appDomainKeyHash[appDomainKey]);
						}
					}
					else
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent<string>("SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash did not contain AppDomainKey: '{0}'.", appDomainKey);
					}
				}
				SqlClientEventSource.Log.TryNotificationTraceEvent<bool>("SqlConnectionContainer.AppDomainUnload|DEP> Exiting, _stopped: '{0}'.", this._stopped);
				stopped = this._stopped;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return stopped;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000769E8 File Offset: 0x00074BE8
		private void AsynchronouslyQueryServiceBrokerQueue()
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.AsynchronouslyQueryServiceBrokerQueue|DEP> {0}", this.ObjectID);
			try
			{
				AsyncCallback asyncCallback = new AsyncCallback(this.AsyncResultCallback);
				this._com.BeginExecuteReader(asyncCallback, null, CommandBehavior.Default);
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00076A48 File Offset: 0x00074C48
		private void AsyncResultCallback(IAsyncResult asyncResult)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.AsyncResultCallback|DEP> {0}", this.ObjectID);
			try
			{
				using (SqlDataReader sqlDataReader = this._com.EndExecuteReader(asyncResult))
				{
					this.ProcessNotificationResults(sqlDataReader);
				}
				if (!this._stop)
				{
					this.AsynchronouslyQueryServiceBrokerQueue();
				}
				else
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					this._errorState = true;
					throw;
				}
				SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.AsyncResultCallback|DEP> Exception occurred.");
				if (!this._stop)
				{
					ADP.TraceExceptionWithoutRethrow(ex);
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
				else
				{
					this._errorState = true;
					this.Restart(null);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00076B30 File Offset: 0x00074D30
		private void CreateQueueAndService(bool restart)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.CreateQueueAndService|DEP> {0}", this.ObjectID);
			try
			{
				SqlCommand sqlCommand = new SqlCommand
				{
					Connection = this._con
				};
				SqlTransaction sqlTransaction = null;
				try
				{
					sqlTransaction = this._con.BeginTransaction();
					sqlCommand.Transaction = sqlTransaction;
					string text = SqlServerEscapeHelper.MakeStringLiteral(this._queue);
					sqlCommand.CommandText = string.Concat(new string[]
					{
						"CREATE PROCEDURE ", this._sprocName, " AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM ", this._escapedQueueName, "; IF (SELECT COUNT(*) FROM ", this._escapedQueueName, " WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = ", text, ") > 0)   DROP SERVICE ", this._escapedQueueName,
						"; if (OBJECT_ID(", text, ", 'SQ') IS NOT NULL)   DROP QUEUE ", this._escapedQueueName, "; DROP PROCEDURE ", this._sprocName, "; END COMMIT TRANSACTION; END"
					});
					if (!restart)
					{
						sqlCommand.ExecuteNonQuery();
					}
					else
					{
						try
						{
							sqlCommand.ExecuteNonQuery();
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
							try
							{
								if (sqlTransaction != null)
								{
									sqlTransaction.Rollback();
									sqlTransaction = null;
								}
							}
							catch (Exception ex2)
							{
								if (!ADP.IsCatchableExceptionType(ex2))
								{
									throw;
								}
								ADP.TraceExceptionWithoutRethrow(ex2);
							}
						}
						if (sqlTransaction == null)
						{
							sqlTransaction = this._con.BeginTransaction();
							sqlCommand.Transaction = sqlTransaction;
						}
					}
					sqlCommand.CommandText = string.Concat(new string[]
					{
						"IF OBJECT_ID(", text, ", 'SQ') IS NULL BEGIN CREATE QUEUE ", this._escapedQueueName, " WITH ACTIVATION (PROCEDURE_NAME=", this._sprocName, ", MAX_QUEUE_READERS=1, EXECUTE AS OWNER); END; IF (SELECT COUNT(*) FROM sys.services WHERE NAME=", text, ") = 0 BEGIN CREATE SERVICE ", this._escapedQueueName,
						" ON QUEUE ", this._escapedQueueName, " ([http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]); IF (SELECT COUNT(*) FROM sys.database_principals WHERE name='sql_dependency_subscriber' AND type='R') <> 0 BEGIN GRANT SEND ON SERVICE::", this._escapedQueueName, " TO sql_dependency_subscriber; END;  END; BEGIN DIALOG @dialog_handle FROM SERVICE ", this._escapedQueueName, " TO SERVICE ", text
					});
					SqlParameter sqlParameter = new SqlParameter
					{
						ParameterName = "@dialog_handle",
						DbType = DbType.Guid,
						Direction = ParameterDirection.Output
					};
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					this._dialogHandle = ((Guid)sqlParameter.Value).ToString();
					this._beginConversationQuery = "BEGIN CONVERSATION TIMER ('" + this._dialogHandle + "') TIMEOUT = 120; " + this._receiveQuery;
					this._com.CommandText = this._beginConversationQuery;
					this._endConversationQuery = "END CONVERSATION @p1; ";
					this._concatQuery = this._endConversationQuery + this._com.CommandText;
					sqlTransaction.Commit();
					sqlTransaction = null;
					this._serviceQueueCreated = true;
				}
				finally
				{
					if (sqlTransaction != null)
					{
						try
						{
							sqlTransaction.Rollback();
							sqlTransaction = null;
						}
						catch (Exception ex3)
						{
							if (!ADP.IsCatchableExceptionType(ex3))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex3);
						}
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00076EA0 File Offset: 0x000750A0
		internal void IncrementStartCount(string appDomainKey, out bool appDomainStart)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.IncrementStartCount|DEP> {0}", this.ObjectID);
			try
			{
				appDomainStart = false;
				int num2 = Interlocked.Increment(ref this._startCount);
				SqlClientEventSource.Log.TryNotificationTraceEvent<int, int>("SqlConnectionContainer.IncrementStartCount|DEP> {0}, incremented _startCount: {1}", SqlDependencyProcessDispatcher.s_staticInstance.ObjectID, num2);
				Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
				lock (appDomainKeyHash)
				{
					if (this._appDomainKeyHash.ContainsKey(appDomainKey))
					{
						this._appDomainKeyHash[appDomainKey] = this._appDomainKeyHash[appDomainKey] + 1;
						SqlClientEventSource.Log.TryNotificationTraceEvent<string, int>("SqlConnectionContainer.IncrementStartCount|DEP> _appDomainKeyHash contained AppDomainKey: '{0}', incremented count: '{1}'.", appDomainKey, this._appDomainKeyHash[appDomainKey]);
					}
					else
					{
						this._appDomainKeyHash[appDomainKey] = 1;
						appDomainStart = true;
						SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlConnectionContainer.IncrementStartCount|DEP> _appDomainKeyHash did not contain AppDomainKey: '{0}', added to hashtable and value set to 1.", appDomainKey);
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00076F98 File Offset: 0x00075198
		private void ProcessNotificationResults(SqlDataReader reader)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> {0}", this.ObjectID);
			Guid guid = Guid.Empty;
			try
			{
				if (!this._stop)
				{
					while (reader.Read())
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Row read.");
						string @string = reader.GetString(0);
						SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> msgType: '{0}'", @string);
						guid = reader.GetGuid(1);
						if (string.Equals(@string, "http://schemas.microsoft.com/SQL/Notifications/QueryNotification", StringComparison.OrdinalIgnoreCase))
						{
							SqlXml sqlXml = reader.GetSqlXml(2);
							if (sqlXml != null)
							{
								SqlNotification sqlNotification = SqlDependencyProcessDispatcher.SqlNotificationParser.ProcessMessage(sqlXml);
								if (sqlNotification != null)
								{
									string key = sqlNotification.Key;
									SqlClientEventSource.Log.TryNotificationTraceEvent<string>("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Key: '{0}'", key);
									int num2 = key.IndexOf(';');
									if (num2 >= 0)
									{
										string text = key.Substring(0, num2);
										Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = SqlDependencyProcessDispatcher.s_staticInstance._sqlDependencyPerAppDomainDispatchers;
										SqlDependencyPerAppDomainDispatcher sqlDependencyPerAppDomainDispatcher;
										lock (sqlDependencyPerAppDomainDispatchers)
										{
											sqlDependencyPerAppDomainDispatcher = SqlDependencyProcessDispatcher.s_staticInstance._sqlDependencyPerAppDomainDispatchers[text];
										}
										if (sqlDependencyPerAppDomainDispatcher != null)
										{
											try
											{
												sqlDependencyPerAppDomainDispatcher.InvalidateCommandID(sqlNotification);
												continue;
											}
											catch (Exception ex)
											{
												if (!ADP.IsCatchableExceptionType(ex))
												{
													throw;
												}
												ADP.TraceExceptionWithoutRethrow(ex);
												continue;
											}
										}
										SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Received notification but do not have an associated PerAppDomainDispatcher!");
									}
									else
									{
										SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Unexpected ID format received!");
									}
								}
								else
								{
									SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Null notification returned from ProcessMessage!");
								}
							}
							else
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Null payload for QN notification type!");
							}
						}
						else
						{
							guid = Guid.Empty;
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Unexpected message format received!");
						}
					}
				}
			}
			finally
			{
				if (guid == Guid.Empty)
				{
					this._com.CommandText = this._beginConversationQuery ?? this._receiveQuery;
					if (this._com.Parameters.Count > 1)
					{
						this._com.Parameters.Remove(this._conversationGuidParam);
					}
				}
				else
				{
					this._com.CommandText = this._concatQuery;
					this._conversationGuidParam.Value = guid;
					if (this._com.Parameters.Count == 1)
					{
						this._com.Parameters.Add(this._conversationGuidParam);
					}
				}
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00077224 File Offset: 0x00075424
		private void Restart(object unused)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.Restart|DEP> {0}", this.ObjectID);
			try
			{
				lock (this)
				{
					if (!this._stop)
					{
						try
						{
							this._con.Close();
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
						}
					}
				}
				lock (this)
				{
					if (!this._stop)
					{
						if (this._hashHelper.Identity != null)
						{
							WindowsImpersonationContext windowsImpersonationContext = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								windowsImpersonationContext = this._windowsIdentity.Impersonate();
								this._con.Open();
								goto IL_00B7;
							}
							finally
							{
								if (windowsImpersonationContext != null)
								{
									windowsImpersonationContext.Undo();
								}
							}
						}
						this._con.Open();
					}
					IL_00B7:;
				}
				lock (this)
				{
					if (!this._stop && this._serviceQueueCreated)
					{
						bool flag4 = false;
						try
						{
							this.CreateQueueAndService(true);
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex2);
							flag4 = true;
						}
						if (flag4)
						{
							SqlDependencyProcessDispatcher.s_staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
						}
					}
				}
				lock (this)
				{
					if (!this._stop)
					{
						this._timeoutParam.Value = 0;
						this.SynchronouslyQueryServiceBrokerQueue();
						this._timeoutParam.Value = this._defaultWaitforTimeout;
						this.AsynchronouslyQueryServiceBrokerQueue();
						this._errorState = false;
						Timer retryTimer = this._retryTimer;
						if (retryTimer != null)
						{
							this._retryTimer = null;
							retryTimer.Dispose();
						}
					}
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception ex3)
			{
				if (!ADP.IsCatchableExceptionType(ex3))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex3);
				try
				{
					SqlDependencyProcessDispatcher.s_staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
				}
				catch (Exception ex4)
				{
					if (!ADP.IsCatchableExceptionType(ex4))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex4);
				}
				try
				{
					this._con.Close();
				}
				catch (Exception ex5)
				{
					if (!ADP.IsCatchableExceptionType(ex5))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex5);
				}
				if (!this._stop)
				{
					this._retryTimer = new Timer(new TimerCallback(this.Restart), null, this._defaultWaitforTimeout, -1);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000775AC File Offset: 0x000757AC
		internal bool Stop(string appDomainKey, out bool appDomainStop)
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.Stop|DEP> {0}", this.ObjectID);
			bool stopped;
			try
			{
				appDomainStop = false;
				if (appDomainKey != null)
				{
					Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
					lock (appDomainKeyHash)
					{
						if (this._appDomainKeyHash.ContainsKey(appDomainKey))
						{
							int num2 = this._appDomainKeyHash[appDomainKey];
							SqlClientEventSource.Log.TryNotificationTraceEvent<string, int>("<sc.SqlConnectionContainer.Stop|DEP> _appDomainKeyHash contained AppDomainKey: '{0}', pre-decrement Count: '{1}'.", appDomainKey, num2);
							if (num2 > 0)
							{
								this._appDomainKeyHash[appDomainKey] = num2 - 1;
							}
							else
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.Stop|DEP|ERR> ERROR pre-decremented count <= 0!");
							}
							if (1 == num2)
							{
								this._appDomainKeyHash.Remove(appDomainKey);
								appDomainStop = true;
							}
						}
						else
						{
							SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.Stop|DEP|ERR> ERROR appDomainKey not null and not found in hash!");
						}
					}
				}
				if (Interlocked.Decrement(ref this._startCount) == 0)
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlConnectionContainer.Stop|DEP> Reached 0 count, cancelling and waiting.");
					lock (this)
					{
						try
						{
							this._com.Cancel();
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(ex);
						}
						this._stop = true;
					}
					Stopwatch stopwatch = Stopwatch.StartNew();
					for (;;)
					{
						lock (this)
						{
							if (this._stopped)
							{
								break;
							}
							if (this._errorState || stopwatch.Elapsed.Seconds >= 30)
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent<int, bool>("<sc.SqlConnectionContainer.Stop|DEP|ERR> forcing cleanup. elapsedSeconds: '{0}', _errorState: '{1}'.", stopwatch.Elapsed.Seconds, this._errorState);
								Timer retryTimer = this._retryTimer;
								this._retryTimer = null;
								if (retryTimer != null)
								{
									retryTimer.Dispose();
								}
								this.TearDownAndDispose();
								break;
							}
						}
						Thread.Sleep(1);
					}
				}
				else
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent<int>("<sc.SqlConnectionContainer.Stop|DEP> _startCount not 0 after decrement.  _startCount: '{0}'.", this._startCount);
				}
				stopped = this._stopped;
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
			return stopped;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0007782C File Offset: 0x00075A2C
		private void SynchronouslyQueryServiceBrokerQueue()
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.SynchronouslyQueryServiceBrokerQueue|DEP> {0}", this.ObjectID);
			try
			{
				using (SqlDataReader sqlDataReader = this._com.ExecuteReader())
				{
					this.ProcessNotificationResults(sqlDataReader);
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00077898 File Offset: 0x00075A98
		private void TearDownAndDispose()
		{
			long num = SqlClientEventSource.Log.TryNotificationScopeEnterEvent<int>("<sc.SqlConnectionContainer.TearDownAndDispose|DEP> {0}", this.ObjectID);
			try
			{
				lock (this)
				{
					try
					{
						if (this._con.State != ConnectionState.Closed && ConnectionState.Broken != this._con.State)
						{
							if (this._com.Parameters.Count > 1)
							{
								try
								{
									this._com.CommandText = this._endConversationQuery;
									this._com.Parameters.Remove(this._timeoutParam);
									this._com.ExecuteNonQuery();
								}
								catch (Exception ex)
								{
									if (!ADP.IsCatchableExceptionType(ex))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(ex);
								}
							}
							if (this._serviceQueueCreated && !this._errorState)
							{
								this._com.CommandText = string.Concat(new string[] { "BEGIN TRANSACTION; DROP SERVICE ", this._escapedQueueName, "; DROP QUEUE ", this._escapedQueueName, "; DROP PROCEDURE ", this._sprocName, "; COMMIT TRANSACTION;" });
								try
								{
									this._com.ExecuteNonQuery();
								}
								catch (Exception ex2)
								{
									if (!ADP.IsCatchableExceptionType(ex2))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(ex2);
								}
							}
						}
					}
					finally
					{
						this._stopped = true;
						this._con.Dispose();
						WindowsIdentity windowsIdentity = this._windowsIdentity;
						if (windowsIdentity != null)
						{
							windowsIdentity.Dispose();
						}
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryNotificationScopeLeaveEvent(num);
			}
		}

		// Token: 0x04000C40 RID: 3136
		private readonly SqlConnection _con;

		// Token: 0x04000C41 RID: 3137
		private readonly SqlCommand _com;

		// Token: 0x04000C42 RID: 3138
		private readonly SqlParameter _conversationGuidParam;

		// Token: 0x04000C43 RID: 3139
		private readonly SqlParameter _timeoutParam;

		// Token: 0x04000C44 RID: 3140
		private readonly SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper _hashHelper;

		// Token: 0x04000C45 RID: 3141
		private readonly WindowsIdentity _windowsIdentity;

		// Token: 0x04000C46 RID: 3142
		private readonly string _queue;

		// Token: 0x04000C47 RID: 3143
		private readonly string _receiveQuery;

		// Token: 0x04000C48 RID: 3144
		private string _beginConversationQuery;

		// Token: 0x04000C49 RID: 3145
		private string _endConversationQuery;

		// Token: 0x04000C4A RID: 3146
		private string _concatQuery;

		// Token: 0x04000C4B RID: 3147
		private readonly int _defaultWaitforTimeout = 60000;

		// Token: 0x04000C4C RID: 3148
		private readonly string _escapedQueueName;

		// Token: 0x04000C4D RID: 3149
		private readonly string _sprocName;

		// Token: 0x04000C4E RID: 3150
		private string _dialogHandle;

		// Token: 0x04000C4F RID: 3151
		private readonly string _cachedServer;

		// Token: 0x04000C50 RID: 3152
		private string _cachedDatabase;

		// Token: 0x04000C51 RID: 3153
		private volatile bool _errorState;

		// Token: 0x04000C52 RID: 3154
		private volatile bool _stop;

		// Token: 0x04000C53 RID: 3155
		private volatile bool _stopped;

		// Token: 0x04000C54 RID: 3156
		private volatile bool _serviceQueueCreated;

		// Token: 0x04000C55 RID: 3157
		private int _startCount;

		// Token: 0x04000C56 RID: 3158
		private Timer _retryTimer;

		// Token: 0x04000C57 RID: 3159
		private readonly Dictionary<string, int> _appDomainKeyHash;

		// Token: 0x04000C58 RID: 3160
		private static int s_objectTypeCount;
	}

	// Token: 0x02000189 RID: 393
	private class SqlNotificationParser
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x00077A88 File Offset: 0x00075C88
		internal static SqlNotification ProcessMessage(SqlXml xmlMessage)
		{
			SqlNotification sqlNotification;
			using (XmlReader xmlReader = xmlMessage.CreateReader())
			{
				string empty = string.Empty;
				SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes messageAttributes = SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.None;
				SqlNotificationType sqlNotificationType = SqlNotificationType.Unknown;
				SqlNotificationInfo sqlNotificationInfo = SqlNotificationInfo.Unknown;
				SqlNotificationSource sqlNotificationSource = SqlNotificationSource.Unknown;
				string text = string.Empty;
				xmlReader.Read();
				if (XmlNodeType.Element == xmlReader.NodeType && "QueryNotification" == xmlReader.LocalName && 3 <= xmlReader.AttributeCount)
				{
					while (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes && xmlReader.MoveToNextAttribute())
					{
						try
						{
							string localName = xmlReader.LocalName;
							if (!(localName == "type"))
							{
								if (!(localName == "source"))
								{
									if (localName == "info")
									{
										string value = xmlReader.Value;
										if (!(value == "set options"))
										{
											if (!(value == "previous invalid"))
											{
												if (!(value == "query template limit"))
												{
													SqlNotificationInfo sqlNotificationInfo2;
													if (Enum.TryParse<SqlNotificationInfo>(value, true, out sqlNotificationInfo2) && Enum.IsDefined(typeof(SqlNotificationInfo), sqlNotificationInfo2))
													{
														sqlNotificationInfo = sqlNotificationInfo2;
													}
												}
												else
												{
													sqlNotificationInfo = SqlNotificationInfo.TemplateLimit;
												}
											}
											else
											{
												sqlNotificationInfo = SqlNotificationInfo.PreviousFire;
											}
										}
										else
										{
											sqlNotificationInfo = SqlNotificationInfo.Options;
										}
										messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Info;
									}
								}
								else
								{
									try
									{
										SqlNotificationSource sqlNotificationSource2 = (SqlNotificationSource)Enum.Parse(typeof(SqlNotificationSource), xmlReader.Value, true);
										if (Enum.IsDefined(typeof(SqlNotificationSource), sqlNotificationSource2))
										{
											sqlNotificationSource = sqlNotificationSource2;
										}
									}
									catch (Exception ex)
									{
										if (!ADP.IsCatchableExceptionType(ex))
										{
											throw;
										}
										ADP.TraceExceptionWithoutRethrow(ex);
									}
									messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Source;
								}
							}
							else
							{
								try
								{
									SqlNotificationType sqlNotificationType2 = (SqlNotificationType)Enum.Parse(typeof(SqlNotificationType), xmlReader.Value, true);
									if (Enum.IsDefined(typeof(SqlNotificationType), sqlNotificationType2))
									{
										sqlNotificationType = sqlNotificationType2;
									}
								}
								catch (Exception ex2)
								{
									if (!ADP.IsCatchableExceptionType(ex2))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(ex2);
								}
								messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Type;
							}
						}
						catch (ArgumentException ex3)
						{
							ADP.TraceExceptionWithoutRethrow(ex3);
							SqlClientEventSource.Log.TryNotificationTraceEvent<string, string>("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> Exception thrown - Enum.Parse failed to parse the value '{0}' of the attribute '{1}'.", xmlReader.Value, xmlReader.LocalName);
							return null;
						}
					}
					if (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent<int>("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> Not all expected attributes in Message; messageAttributes = '{0}'.", (int)messageAttributes);
						sqlNotification = null;
					}
					else if (!xmlReader.Read())
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<Sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
						sqlNotification = null;
					}
					else if (XmlNodeType.Element != xmlReader.NodeType || string.Compare(xmlReader.LocalName, "Message", StringComparison.OrdinalIgnoreCase) != 0)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
						sqlNotification = null;
					}
					else if (!xmlReader.Read())
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
						sqlNotification = null;
					}
					else if (xmlReader.NodeType != XmlNodeType.Text)
					{
						SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
						sqlNotification = null;
					}
					else
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(xmlReader.Value, XmlNodeType.Element, null)
						{
							DtdProcessing = DtdProcessing.Prohibit
						})
						{
							if (!xmlTextReader.Read())
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
								return null;
							}
							if (xmlTextReader.NodeType != XmlNodeType.Text)
							{
								SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
								return null;
							}
							text = xmlTextReader.Value;
							xmlTextReader.Close();
						}
						sqlNotification = new SqlNotification(sqlNotificationInfo, sqlNotificationSource, sqlNotificationType, text);
					}
				}
				else
				{
					SqlClientEventSource.Log.TryNotificationTraceEvent("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.");
					sqlNotification = null;
				}
			}
			return sqlNotification;
		}

		// Token: 0x04000C5A RID: 3162
		private const string RootNode = "QueryNotification";

		// Token: 0x04000C5B RID: 3163
		private const string MessageNode = "Message";

		// Token: 0x04000C5C RID: 3164
		private const string InfoAttribute = "info";

		// Token: 0x04000C5D RID: 3165
		private const string SourceAttribute = "source";

		// Token: 0x04000C5E RID: 3166
		private const string TypeAttribute = "type";

		// Token: 0x02000290 RID: 656
		[Flags]
		private enum MessageAttributes
		{
			// Token: 0x040017B5 RID: 6069
			None = 0,
			// Token: 0x040017B6 RID: 6070
			Type = 1,
			// Token: 0x040017B7 RID: 6071
			Source = 2,
			// Token: 0x040017B8 RID: 6072
			Info = 4,
			// Token: 0x040017B9 RID: 6073
			All = 7
		}
	}

	// Token: 0x0200018A RID: 394
	private class SqlConnectionContainerHashHelper
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x00077E50 File Offset: 0x00076050
		internal SqlConnectionContainerHashHelper(DbConnectionPoolIdentity identity, string connectionString, string queue, SqlConnectionStringBuilder connectionStringBuilder)
		{
			this._identity = identity;
			this._connectionString = connectionString;
			this._queue = queue;
			this._connectionStringBuilder = connectionStringBuilder;
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00077E75 File Offset: 0x00076075
		internal SqlConnectionStringBuilder ConnectionStringBuilder
		{
			get
			{
				return this._connectionStringBuilder;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00077E7D File Offset: 0x0007607D
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00077E85 File Offset: 0x00076085
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00077E90 File Offset: 0x00076090
		public override bool Equals(object value)
		{
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper = (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper)value;
			bool flag;
			if (sqlConnectionContainerHashHelper == null)
			{
				flag = false;
			}
			else if (this == sqlConnectionContainerHashHelper)
			{
				flag = true;
			}
			else if ((this._identity != null && sqlConnectionContainerHashHelper._identity == null) || (this._identity == null && sqlConnectionContainerHashHelper._identity != null))
			{
				flag = false;
			}
			else if (this._identity == null && sqlConnectionContainerHashHelper._identity == null)
			{
				flag = sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase);
			}
			else
			{
				flag = sqlConnectionContainerHashHelper._identity.Equals(this._identity) && sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase);
			}
			return flag;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00077F60 File Offset: 0x00076160
		public override int GetHashCode()
		{
			int num = 0;
			if (this._identity != null)
			{
				num = this._identity.GetHashCode();
			}
			if (this._queue != null)
			{
				num = this._connectionString.GetHashCode() + this._queue.GetHashCode() + num;
			}
			else
			{
				num = this._connectionString.GetHashCode() + num;
			}
			return num;
		}

		// Token: 0x04000C5F RID: 3167
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x04000C60 RID: 3168
		private readonly string _connectionString;

		// Token: 0x04000C61 RID: 3169
		private readonly string _queue;

		// Token: 0x04000C62 RID: 3170
		private readonly SqlConnectionStringBuilder _connectionStringBuilder;
	}
}
