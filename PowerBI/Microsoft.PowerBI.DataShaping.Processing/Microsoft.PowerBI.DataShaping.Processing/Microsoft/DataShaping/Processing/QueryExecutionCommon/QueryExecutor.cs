using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006F RID: 111
	internal sealed class QueryExecutor<T> where T : ICommandExecutor
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000078B4 File Offset: 0x00005AB4
		internal QueryExecutor(ITelemetryService telemetryService, ITracer tracer, IDataShapingDataSourceInfo dataSourceInfo, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, T commandExecutor, QueryExecutionStatistics queryStats, IConnectionUserImpersonator connectionUserImpersonator, QueryExecutionOptions queryExecutionOptions, IDataShapingExecutionMetricsService executionMetricsService)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._connectionWrapper = new ConnectionWrapper(telemetryService, tracer, dataSourceInfo, connectionPool, connectionStringResolver, queryStats, connectionUserImpersonator, executionMetricsService);
			this._commandExecutor = commandExecutor;
			this._queryExecutionOptions = queryExecutionOptions;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007900 File Offset: 0x00005B00
		internal async Task RunQueryAsync(IConnectionFactory factory, CancellationToken cancelToken, CancellationToken internalCancelToken)
		{
			TaskAwaiter<bool> taskAwaiter = this.TryRunQueryWithPooledConnectionsAsync(factory, cancelToken, internalCancelToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (!taskAwaiter.GetResult())
			{
				await this.RunQueryWithNewConnectionsAsync(factory, cancelToken, internalCancelToken);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000795C File Offset: 0x00005B5C
		private async Task<bool> TryRunQueryWithPooledConnectionsAsync(IConnectionFactory factory, CancellationToken cancelToken, CancellationToken internalCancelToken)
		{
			int numberOfAttemptsFromPool = this._queryExecutionOptions.PooledConnectionAttempts + this._queryExecutionOptions.PooledConnectionsToDrainOnFailure;
			int remainingPooledConnectionsToDrain = this._queryExecutionOptions.PooledConnectionsToDrainOnFailure;
			int attempt = 0;
			while (attempt < numberOfAttemptsFromPool)
			{
				cancelToken.ThrowIfCancellationRequested();
				IDbConnection connection = this._connectionWrapper.GetConnectionFromPool();
				if (connection != null)
				{
					int num;
					if (attempt >= this._queryExecutionOptions.PooledConnectionAttempts && remainingPooledConnectionsToDrain > 0)
					{
						num = remainingPooledConnectionsToDrain;
						remainingPooledConnectionsToDrain = num - 1;
						await this.CloseAsync(false, false);
					}
					else
					{
						num = 0;
						object obj;
						Exception ex;
						bool flag;
						object obj2;
						Exception ex2;
						try
						{
							T commandExecutor = this._commandExecutor;
							await commandExecutor.ExecuteAsync(connection, cancelToken, internalCancelToken);
							return true;
						}
						catch when (delegate
						{
							// Failed to create a 'catch-when' expression
							ex = obj as Exception;
							if (ex == null)
							{
								flag = false;
							}
							else
							{
								obj2 = ex;
								ex2 = (Exception)obj2;
								flag = (ex2 is DataExtensionException || ex2 is DataShapeEngineException) > false;
							}
							endfilter(flag);
						})
						{
							num = 1;
						}
						if (num == 1)
						{
							if (QueryExecutor<T>.IsKnownNonRetriableError(ex2, cancelToken))
							{
								Exception ex3 = obj2 as Exception;
								if (ex3 == null)
								{
									throw obj2;
								}
								ExceptionDispatchInfo.Capture(ex3).Throw();
							}
							bool flag2 = !this.IsRetriableError(ex2);
							if (flag2)
							{
								flag2 = await this.IsAliveAsync(connection);
							}
							if (flag2)
							{
								Exception ex4 = obj2 as Exception;
								if (ex4 == null)
								{
									throw obj2;
								}
								ExceptionDispatchInfo.Capture(ex4).Throw();
							}
							await this.CloseAsync(false, true);
							this._tracer.SanitizedTrace(TraceLevel.Warning, string.Format("RunQueryWithPooledConnectionsAsync: Got a non-Alive connection from the pool on attempt {0}. Obtaining a new connection and retrying the execution.", attempt));
						}
						obj2 = null;
						connection = null;
					}
					num = attempt + 1;
					attempt = num;
					continue;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000079B0 File Offset: 0x00005BB0
		private async Task RunQueryWithNewConnectionsAsync(IConnectionFactory factory, CancellationToken cancelToken, CancellationToken internalCancelToken)
		{
			int num;
			for (int attempt = 0; attempt < this._queryExecutionOptions.NewConnectionAttempts; attempt = num)
			{
				cancelToken.ThrowIfCancellationRequested();
				num = 0;
				object obj;
				Exception ex;
				bool flag;
				object obj2;
				Exception ex2;
				try
				{
					IDbConnection dbConnection = await this._connectionWrapper.OpenNewConnection(factory, attempt == 0);
					T commandExecutor = this._commandExecutor;
					await commandExecutor.ExecuteAsync(dbConnection, cancelToken, internalCancelToken);
					break;
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					ex = obj as Exception;
					if (ex == null)
					{
						flag = false;
					}
					else
					{
						obj2 = ex;
						ex2 = (Exception)obj2;
						flag = (ex2 is DataExtensionException || ex2 is DataShapeEngineException) > false;
					}
					endfilter(flag);
				})
				{
					num = 1;
				}
				if (num == 1)
				{
					if (attempt == this._queryExecutionOptions.NewConnectionAttempts - 1)
					{
						Exception ex3 = obj2 as Exception;
						if (ex3 == null)
						{
							throw obj2;
						}
						ExceptionDispatchInfo.Capture(ex3).Throw();
					}
					if (QueryExecutor<T>.IsKnownNonRetriableError(ex2, cancelToken))
					{
						Exception ex4 = obj2 as Exception;
						if (ex4 == null)
						{
							throw obj2;
						}
						ExceptionDispatchInfo.Capture(ex4).Throw();
					}
					if (!this.IsRetriableError(ex2))
					{
						Exception ex5 = obj2 as Exception;
						if (ex5 == null)
						{
							throw obj2;
						}
						ExceptionDispatchInfo.Capture(ex5).Throw();
					}
					await this.CloseAsync(false, true);
					this._tracer.SanitizedTrace(TraceLevel.Warning, string.Format("RunQueryWithNewConnectionsAsync: Connection open failed on attempt {0}. Retrying a new connection.", attempt));
				}
				num = attempt + 1;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007A0C File Offset: 0x00005C0C
		private bool IsRetriableError(Exception ex)
		{
			DataExtensionException ex2 = ex as DataExtensionException;
			return ex2 != null && this._queryExecutionOptions.IsRetriableFailure(ex2.ProviderErrorCode);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007A3C File Offset: 0x00005C3C
		private static bool IsKnownNonRetriableError(Exception ex, CancellationToken cancelToken)
		{
			DataExtensionException ex2 = ex as DataExtensionException;
			DataShapeEngineException ex3 = ex as DataShapeEngineException;
			string text = ((ex2 != null) ? ex2.ErrorCode : null) ?? ((ex3 != null) ? ex3.ErrorCode : null);
			return cancelToken.IsCancellationRequested || text == "rsQueryMemoryLimitExceeded" || text == "rsQueryTimeoutExceeded";
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007A98 File Offset: 0x00005C98
		internal Task<bool> IsAliveAsync(IDbConnection connection)
		{
			return this._telemetryService.RunInAsyncActivity<bool>(ActivityKind.CheckConnectionIsAlive, () => connection.IsAliveAsync());
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007ACC File Offset: 0x00005CCC
		internal async Task CloseAsync(bool poolConnections, bool shouldNotThrow)
		{
			QueryExecutor<T>.<>c__DisplayClass12_0 CS$<>8__locals1 = new QueryExecutor<T>.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.shouldNotThrow = shouldNotThrow;
			CS$<>8__locals1.poolConnections = poolConnections;
			if (!this.IsClosed())
			{
				await this._telemetryService.RunInAsyncActivity(ActivityKind.CloseDataExtension, delegate
				{
					QueryExecutor<T>.<>c__DisplayClass12_0.<<CloseAsync>b__0>d <<CloseAsync>b__0>d;
					<<CloseAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<CloseAsync>b__0>d.<>4__this = CS$<>8__locals1;
					<<CloseAsync>b__0>d.<>1__state = -1;
					<<CloseAsync>b__0>d.<>t__builder.Start<QueryExecutor<T>.<>c__DisplayClass12_0.<<CloseAsync>b__0>d>(ref <<CloseAsync>b__0>d);
					return <<CloseAsync>b__0>d.<>t__builder.Task;
				});
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007B20 File Offset: 0x00005D20
		internal bool IsClosed()
		{
			T commandExecutor = this._commandExecutor;
			return commandExecutor.IsClosed() && this._connectionWrapper.IsClosed();
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00007B50 File Offset: 0x00005D50
		internal T CommandExecutor
		{
			get
			{
				return this._commandExecutor;
			}
		}

		// Token: 0x04000195 RID: 405
		private readonly ITelemetryService _telemetryService;

		// Token: 0x04000196 RID: 406
		private readonly ITracer _tracer;

		// Token: 0x04000197 RID: 407
		private readonly ConnectionWrapper _connectionWrapper;

		// Token: 0x04000198 RID: 408
		private readonly T _commandExecutor;

		// Token: 0x04000199 RID: 409
		private readonly QueryExecutionOptions _queryExecutionOptions;
	}
}
