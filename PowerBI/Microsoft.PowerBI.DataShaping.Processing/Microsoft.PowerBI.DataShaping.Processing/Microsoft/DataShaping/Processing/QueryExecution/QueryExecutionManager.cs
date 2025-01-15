using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000064 RID: 100
	internal sealed class QueryExecutionManager
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00006D9C File Offset: 0x00004F9C
		internal QueryExecutionManager(ITelemetryService telemetryService, ITracer tracer, IDataShapingDataSourceInfo dataSourceInfo, QueryCommandOptions commandOptions, IList<DataSet> dataSets, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, IQueryExecutionStrategy executionStrategy, IConnectionUserImpersonator connectionUserImpersonator, QueryExecutionOptions queryExecutionOptions, IDataShapingExecutionMetricsService executionMetricsService)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._dataSourceInfo = dataSourceInfo;
			this._commandOptions = commandOptions;
			this._connectionPool = connectionPool;
			this._connectionStringResolver = connectionStringResolver;
			this._dataSets = dataSets;
			this._queryExecutors = new List<QueryExecutor<CommandExecutor>>(this._dataSets.Count);
			this._executionStrategy = executionStrategy;
			this._queryStats = new QueryExecutionStatistics();
			this._connectionUserImpersonator = connectionUserImpersonator;
			this._queryExecutionOptions = queryExecutionOptions;
			this._executionMetricsService = executionMetricsService;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006E28 File Offset: 0x00005028
		internal async Task RunQueriesAsync(IConnectionFactory connectionFactory, IList<ResultTableLookupInfo> resultTableInfos, CancellationToken token)
		{
			QueryExecutionManager.<>c__DisplayClass15_0 CS$<>8__locals1 = new QueryExecutionManager.<>c__DisplayClass15_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.connectionFactory = connectionFactory;
			CS$<>8__locals1.token = token;
			await this._telemetryService.RunInAsyncActivity(ActivityKind.QueryExecution, delegate
			{
				QueryExecutionManager.<>c__DisplayClass15_0.<<RunQueriesAsync>b__0>d <<RunQueriesAsync>b__0>d;
				<<RunQueriesAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunQueriesAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunQueriesAsync>b__0>d.<>1__state = -1;
				<<RunQueriesAsync>b__0>d.<>t__builder.Start<QueryExecutionManager.<>c__DisplayClass15_0.<<RunQueriesAsync>b__0>d>(ref <<RunQueriesAsync>b__0>d);
				return <<RunQueriesAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006E7C File Offset: 0x0000507C
		private void CreateRowSources()
		{
			this._rowSources = new List<IRowSource>(this._queryExecutors.Count);
			foreach (QueryExecutor<CommandExecutor> queryExecutor in this._queryExecutors)
			{
				this._rowSources.Add(queryExecutor.CommandExecutor.DataReader);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal void CreateQueryExecutors()
		{
			for (int i = 0; i < this._dataSets.Count; i++)
			{
				CommandExecutor commandExecutor = new CommandExecutor(new CommandExecutionContext(this._dataSets[i], this._commandOptions, this._dataSourceInfo.Category), this._telemetryService, this._tracer, this._connectionUserImpersonator, this._executionMetricsService);
				QueryExecutor<CommandExecutor> queryExecutor = new QueryExecutor<CommandExecutor>(this._telemetryService, this._tracer, this._dataSourceInfo, this._connectionPool, this._connectionStringResolver, commandExecutor, this._queryStats, this._connectionUserImpersonator, this._queryExecutionOptions, this._executionMetricsService);
				this._queryExecutors.Add(queryExecutor);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006FA4 File Offset: 0x000051A4
		public async Task CloseAsync(bool poolConnections, bool shouldNotThrow)
		{
			foreach (QueryExecutor<CommandExecutor> queryExecutor in this._queryExecutors)
			{
				await queryExecutor.CloseAsync(poolConnections, shouldNotThrow);
			}
			IEnumerator<QueryExecutor<CommandExecutor>> enumerator = null;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00006FF7 File Offset: 0x000051F7
		public long RowCount
		{
			get
			{
				return this._queryExecutors.Sum((QueryExecutor<CommandExecutor> qe) => qe.CommandExecutor.DataReader.RowCount);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007023 File Offset: 0x00005223
		public IList<IRowSource> RowSources
		{
			get
			{
				return this._rowSources;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000702B File Offset: 0x0000522B
		internal QueryExecutionStatistics QueryStats
		{
			get
			{
				return this._queryStats;
			}
		}

		// Token: 0x04000171 RID: 369
		private readonly ITelemetryService _telemetryService;

		// Token: 0x04000172 RID: 370
		private readonly IList<DataSet> _dataSets;

		// Token: 0x04000173 RID: 371
		private readonly ITracer _tracer;

		// Token: 0x04000174 RID: 372
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;

		// Token: 0x04000175 RID: 373
		private readonly QueryCommandOptions _commandOptions;

		// Token: 0x04000176 RID: 374
		private readonly IConnectionPool _connectionPool;

		// Token: 0x04000177 RID: 375
		private readonly IConnectionStringResolver _connectionStringResolver;

		// Token: 0x04000178 RID: 376
		private readonly IList<QueryExecutor<CommandExecutor>> _queryExecutors;

		// Token: 0x04000179 RID: 377
		private readonly IQueryExecutionStrategy _executionStrategy;

		// Token: 0x0400017A RID: 378
		private readonly QueryExecutionStatistics _queryStats;

		// Token: 0x0400017B RID: 379
		private readonly IConnectionUserImpersonator _connectionUserImpersonator;

		// Token: 0x0400017C RID: 380
		private readonly QueryExecutionOptions _queryExecutionOptions;

		// Token: 0x0400017D RID: 381
		private readonly IDataShapingExecutionMetricsService _executionMetricsService;

		// Token: 0x0400017E RID: 382
		private List<IRowSource> _rowSources;
	}
}
