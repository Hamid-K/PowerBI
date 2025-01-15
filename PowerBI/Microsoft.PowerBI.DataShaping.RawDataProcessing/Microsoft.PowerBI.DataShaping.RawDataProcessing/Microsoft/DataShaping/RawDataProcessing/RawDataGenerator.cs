using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x0200000B RID: 11
	internal sealed class RawDataGenerator : IRawDataGenerator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002488 File Offset: 0x00000688
		internal RawDataGenerator(ITelemetryService telemetryService, ITracer tracer, IDataShapingDataSourceInfo dataSourceInfo, RawDataDefinition rawDataDefinition, IConnectionFactory factory, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, IExecuteSemanticQueryResultWriter resultWriter, IConnectionUserImpersonator connectionUserImpersonator, RawDataProcessingTelemetry telemetryInfo, QueryExecutionOptions connectionConfig, QueryCommandOptions queryCommandOptions)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._dataSourceInfo = dataSourceInfo;
			this._rawDataDefinition = rawDataDefinition;
			this._factory = factory;
			this._connectionPool = connectionPool;
			this._connectionStringResolver = connectionStringResolver;
			this._resultWriter = resultWriter;
			this._connectionUserImpersonator = connectionUserImpersonator;
			this._telemetryInfo = telemetryInfo;
			this._queryExecutionOptions = connectionConfig;
			this._queryCommandOptions = queryCommandOptions;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024F8 File Offset: 0x000006F8
		internal static void WriteRawDataError(IExecuteSemanticQueryResultWriter resultWriter, Exception ex)
		{
			PowerBIRawDataServerPipeline.WriteRawDataError(RawDataGenerator.GetAndVerifyStream(resultWriter), ex);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002506 File Offset: 0x00000706
		public Task Generate()
		{
			return this._telemetryService.RunInAsyncActivity(ActivityKind.RawDataGeneration, () => this.GenerateImpl());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002524 File Offset: 0x00000724
		private async Task GenerateImpl()
		{
			object obj = null;
			try
			{
				int num = 0;
				try
				{
					QueryExecutionStatistics queryExecutionStats = new QueryExecutionStatistics();
					await this.SetupQueryExecution(queryExecutionStats);
					this._telemetryInfo.QueryExecutionStats = queryExecutionStats;
					queryExecutionStats = null;
				}
				catch (DataShapeEngineException obj2)
				{
					num = 1;
				}
				object obj2;
				if (num == 1)
				{
					await this._activeQueryExecutor.CloseAsync(true, true);
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj2 = null;
			}
			catch (object obj)
			{
			}
			await this._activeQueryExecutor.CloseAsync(true, false);
			object obj3 = obj;
			if (obj3 != null)
			{
				Exception ex2 = obj3 as Exception;
				if (ex2 == null)
				{
					throw obj3;
				}
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			obj = null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002568 File Offset: 0x00000768
		private async Task SetupQueryExecution(QueryExecutionStatistics executionStats)
		{
			RawDataCommandExecutor rawDataCommandExecutor = new RawDataCommandExecutor(this._telemetryService, this._tracer, this._dataSourceInfo, this._rawDataDefinition, this._queryCommandOptions, new RawConnectionExtractor(), new PageReaderFactory());
			this._activeQueryExecutor = new QueryExecutor<RawDataCommandExecutor>(this._telemetryService, this._tracer, this._dataSourceInfo, this._connectionPool, this._connectionStringResolver, rawDataCommandExecutor, executionStats, this._connectionUserImpersonator, this._queryExecutionOptions, NoOpDataShapingExecutionMetricsService.Instance);
			await this._activeQueryExecutor.RunQueryAsync(this._factory, CancellationToken.None, CancellationToken.None);
			PowerBIRawDataServerPipeline powerBIRawDataServerPipeline = new PowerBIRawDataServerPipeline(this._activeQueryExecutor.CommandExecutor.GetDataReader(), null);
			Stream andVerifyStream = RawDataGenerator.GetAndVerifyStream(this._resultWriter);
			await powerBIRawDataServerPipeline.FillStreamAsync(andVerifyStream);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025B3 File Offset: 0x000007B3
		private static Stream GetAndVerifyStream(IExecuteSemanticQueryResultWriter resultWriter)
		{
			return resultWriter.GetRawDataStream();
		}

		// Token: 0x04000032 RID: 50
		private const string DataShapeIdProp = "DSID";

		// Token: 0x04000033 RID: 51
		private const string RowCountProp = "ROWC";

		// Token: 0x04000034 RID: 52
		private const string CachedRowCountProp = "CCHR";

		// Token: 0x04000035 RID: 53
		private readonly ITelemetryService _telemetryService;

		// Token: 0x04000036 RID: 54
		private readonly ITracer _tracer;

		// Token: 0x04000037 RID: 55
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;

		// Token: 0x04000038 RID: 56
		private readonly RawDataDefinition _rawDataDefinition;

		// Token: 0x04000039 RID: 57
		private readonly IConnectionFactory _factory;

		// Token: 0x0400003A RID: 58
		private readonly IConnectionPool _connectionPool;

		// Token: 0x0400003B RID: 59
		private readonly IConnectionStringResolver _connectionStringResolver;

		// Token: 0x0400003C RID: 60
		private readonly IExecuteSemanticQueryResultWriter _resultWriter;

		// Token: 0x0400003D RID: 61
		private readonly IConnectionUserImpersonator _connectionUserImpersonator;

		// Token: 0x0400003E RID: 62
		private readonly QueryExecutionOptions _queryExecutionOptions;

		// Token: 0x0400003F RID: 63
		private readonly RawDataProcessingTelemetry _telemetryInfo;

		// Token: 0x04000040 RID: 64
		private readonly QueryCommandOptions _queryCommandOptions;

		// Token: 0x04000041 RID: 65
		private QueryExecutor<RawDataCommandExecutor> _activeQueryExecutor;
	}
}
