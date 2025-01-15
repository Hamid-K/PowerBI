using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006B RID: 107
	internal sealed class ConnectionWrapper
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00007480 File Offset: 0x00005680
		internal ConnectionWrapper(ITelemetryService telemetryService, ITracer tracer, IDataShapingDataSourceInfo dataSourceInfo, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, QueryExecutionStatistics queryStats, IConnectionUserImpersonator connectionUserImpersonator, IDataShapingExecutionMetricsService executionMetricsService)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._dataSourceInfo = dataSourceInfo;
			this._connectionPool = connectionPool;
			this._connectionStringResolver = connectionStringResolver;
			this._queryStats = queryStats;
			this._connectionUserImpersonator = connectionUserImpersonator;
			this._executionMetricsService = executionMetricsService;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000074D0 File Offset: 0x000056D0
		internal async Task<IDbConnection> OpenNewConnection(IConnectionFactory factory, bool performStringResolution)
		{
			ConnectionWrapper.<>c__DisplayClass12_0 CS$<>8__locals1 = new ConnectionWrapper.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.performStringResolution = performStringResolution;
			CS$<>8__locals1.factory = factory;
			await this._telemetryService.RunInAsyncActivity(ActivityKind.OpenConnection, delegate
			{
				ConnectionWrapper.<>c__DisplayClass12_0.<<OpenNewConnection>b__0>d <<OpenNewConnection>b__0>d;
				<<OpenNewConnection>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OpenNewConnection>b__0>d.<>4__this = CS$<>8__locals1;
				<<OpenNewConnection>b__0>d.<>1__state = -1;
				<<OpenNewConnection>b__0>d.<>t__builder.Start<ConnectionWrapper.<>c__DisplayClass12_0.<<OpenNewConnection>b__0>d>(ref <<OpenNewConnection>b__0>d);
				return <<OpenNewConnection>b__0>d.<>t__builder.Task;
			});
			return this._connection;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007523 File Offset: 0x00005723
		internal IDbConnection GetConnectionFromPool()
		{
			if (this._connectionPool == null)
			{
				return null;
			}
			this._telemetryService.RunInActivity(ActivityKind.OpenConnection, delegate
			{
				this._connection = this._connectionPool.Get(this._dataSourceInfo);
				bool? flag = null;
				if (this._connection != null)
				{
					flag = new bool?(true);
				}
				this._telemetryService.FireSanitizedEvent(DataShapingEvents.OpenConnectionInfo, new object[]
				{
					"CCAT",
					this._dataSourceInfo.Category,
					"POLC",
					flag
				});
				if (this._connection != null)
				{
					this._queryStats.RegisterOpenConnection(true);
				}
			});
			return this._connection;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007550 File Offset: 0x00005750
		private async Task<IDbConnection> OpenConnectionWithoutRetry(IConnectionFactory factory)
		{
			try
			{
				this._connection = factory.CreateConnection(this._dataSourceInfo.Extension, this._dataSourceInfo.ConnectionString);
				await this.OpenConnectionAsyncCore(this._connection);
			}
			catch (DataExtensionException ex)
			{
				this._tracer.TraceSanitizedError(ex, "OpenConnectionWithoutRetry failed. Tracing the failure.");
				throw;
			}
			return this._connection;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000759C File Offset: 0x0000579C
		private async Task<IDbConnection> OpenConnectionWithRetry(IConnectionFactory factory)
		{
			int num = 0;
			try
			{
				this._connection = factory.CreateConnection(this._dataSourceInfo.Extension, this._dataSourceInfo.ConnectionString);
				await this.OpenConnectionAsyncCore(this._connection);
			}
			catch (DataExtensionException obj)
			{
				num = 1;
			}
			object obj;
			if (num == 1)
			{
				DataExtensionException ex = (DataExtensionException)obj;
				this._tracer.SanitizedTrace(TraceLevel.Warning, "Error on connection creation or open. Attempting to re-resolve the connection string.");
				TaskAwaiter<bool> taskAwaiter = this.TryResolveConnectionString(factory, ex).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					try
					{
						await this.OpenConnectionAsyncCore(this._connection);
						goto IL_0219;
					}
					catch (DataExtensionException ex2)
					{
						this._tracer.TraceSanitizedError(ex2, "Failed to open a new connection after connection string resolution.");
						throw;
					}
				}
				this._tracer.TraceSanitizedError(ex, "Connection string resolution failed. Tracing original failure");
				Exception ex3 = obj as Exception;
				if (ex3 == null)
				{
					throw obj;
				}
				ExceptionDispatchInfo.Capture(ex3).Throw();
				IL_0219:
				ex = null;
			}
			obj = null;
			return this._connection;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000075E8 File Offset: 0x000057E8
		private async Task OpenConnectionAsyncCore(IDbConnection connection)
		{
			Func<Task> func = new Func<Task>(this._connection.OpenAsync);
			if (this._connectionUserImpersonator != null)
			{
				await this._connectionUserImpersonator.ExecuteInContextAsync(func);
			}
			else
			{
				await func();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000762C File Offset: 0x0000582C
		private async Task<bool> TryResolveConnectionString(IConnectionFactory factory, DataExtensionException dataExtensionException)
		{
			bool flag;
			if (this._connectionStringResolver == null || dataExtensionException == null)
			{
				flag = false;
			}
			else
			{
				string updatedConnectionString;
				TaskAwaiter<bool> taskAwaiter = this._connectionStringResolver.TryResolveConnectionStringAsync(dataExtensionException.ProviderErrorCode, this._dataSourceInfo.Name, out updatedConnectionString).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					await QueryExecutionUtils.ExecuteInTryCatch(this._tracer, async delegate
					{
						await this._connection.CloseAsync();
						this._connection.Dispose();
					}, true, "An error occurred closing the connection with a broken connection");
					this._connection = factory.CreateConnection(this._dataSourceInfo.Extension, updatedConnectionString);
					flag = true;
				}
				else
				{
					this._tracer.SanitizedTrace(TraceLevel.Error, "Error on connection creation or open. Attempt to re-resolve the connection string failed.");
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007680 File Offset: 0x00005880
		internal async Task CloseAsync(bool poolConnections, bool shouldNotThrow)
		{
			if (!this.IsClosed())
			{
				await this.CloseImpl(poolConnections, shouldNotThrow);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000076D3 File Offset: 0x000058D3
		internal bool IsClosed()
		{
			return this._connection == null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000076E0 File Offset: 0x000058E0
		private async Task CloseImpl(bool poolConnections, bool shouldNotThrow)
		{
			if (this._connection != null)
			{
				bool closeConnection = true;
				if (poolConnections && this._connectionPool != null)
				{
					QueryExecutionUtils.ExecuteInTryCatch(this._tracer, delegate
					{
						closeConnection = !this._connectionPool.Put(this._connection, this._dataSourceInfo);
					}, shouldNotThrow, "An error occurred pooling the connection");
				}
				if (closeConnection)
				{
					await QueryExecutionUtils.ExecuteInTryCatch(this._tracer, async delegate
					{
						await this._connection.CloseAsync();
						this._connection.Dispose();
					}, shouldNotThrow, "An error occurred closing the connection");
				}
				this._connection = null;
			}
		}

		// Token: 0x04000186 RID: 390
		private const string IsPoolConnectionProp = "POLC";

		// Token: 0x04000187 RID: 391
		internal const string ConnectionCategoryProp = "CCAT";

		// Token: 0x04000188 RID: 392
		private readonly ITelemetryService _telemetryService;

		// Token: 0x04000189 RID: 393
		private readonly IConnectionPool _connectionPool;

		// Token: 0x0400018A RID: 394
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;

		// Token: 0x0400018B RID: 395
		private readonly IConnectionStringResolver _connectionStringResolver;

		// Token: 0x0400018C RID: 396
		private readonly ITracer _tracer;

		// Token: 0x0400018D RID: 397
		private readonly QueryExecutionStatistics _queryStats;

		// Token: 0x0400018E RID: 398
		private readonly IConnectionUserImpersonator _connectionUserImpersonator;

		// Token: 0x0400018F RID: 399
		private readonly IDataShapingExecutionMetricsService _executionMetricsService;

		// Token: 0x04000190 RID: 400
		private IDbConnection _connection;
	}
}
