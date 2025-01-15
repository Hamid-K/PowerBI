using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Common.DaxComparer;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.DataPreparation;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Reconciliation;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000006 RID: 6
	internal sealed class DataShapeProcessor : IDataShapeProcessor
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		public Task Process(DataShapeProcessorContext context, CancellationToken cancelToken)
		{
			return context.TelemetryService.RunInAsyncActivity(ActivityKind.DataShapeProcessing, () => this.ProcessImpl(context, cancelToken));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
		private async Task ProcessImpl(DataShapeProcessorContext context, CancellationToken cancelToken)
		{
			ProcessingTelemetry processingTelemetry = context.TelemetryInfo;
			try
			{
				DataShapeDefinition dataShapeDefinition = DataShapeDefinitionReconciler.Reconcile(context.Dsd, context.DataSourceInfo);
				this.ReportInitialStats(processingTelemetry, context);
				Collation collation = dataShapeDefinition.DataSource.Collation;
				CompareOptions compareOptions = CompareUtils.CreateCompareOptions(collation.IgnoreCase, collation.IgnoreNonSpace, collation.IgnoreKanaType, collation.IgnoreWidth, collation.PreferOrdinalStringEquality);
				ProcessingCompareInfo processingCompareInfo = new ProcessingCompareInfo(collation.Culture, compareOptions, true, collation.UseOrdinalStringKeyGeneration);
				QueryExecutionManager queryExecutionManager = this.SetupQueryExecution(context, dataShapeDefinition, processingCompareInfo, cancelToken);
				await this.ExecuteQueryAndWriteResult(context.TelemetryService, context.Tracer, context.Writer, context.ConnectionFactory, cancelToken, processingTelemetry, dataShapeDefinition, processingCompareInfo, queryExecutionManager, context.TransformFactory, context.ConnectionUserImpersonator, context.FeatureSwitchProvider, context.DsrWriterOptions);
				processingTelemetry.DsrSize = context.Writer.BytesWritten;
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				if (cancelToken.IsCancellationRequested)
				{
					DataShapeEngineCanceledException.ThrowForCancel(ex);
				}
				throw;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		internal async Task ExecuteQueryAndWriteResult(Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IStreamingStructureEncodedWriter writer, IConnectionFactory connectionFactory, CancellationToken cancelToken, ProcessingTelemetry processingTelemetry, DataShapeDefinition reconciledDsd, ProcessingCompareInfo compareInfo, QueryExecutionManager activeQueryExecutor, IDataTransformPluginFactory dataTransformPluginFactory, IConnectionUserImpersonator connectionUserImpersonator, IFeatureSwitchProvider featureSwitchProvider, DsrWriterOptions dsrWriterOptions)
		{
			DsrWriterOptions options = dsrWriterOptions ?? DsrWriterOptions.V2;
			processingTelemetry.DsrVersion = (int)options.Version;
			object obj = null;
			try
			{
				int num = 0;
				try
				{
					await activeQueryExecutor.RunQueriesAsync(connectionFactory, reconciledDsd.ResultTableInfos, cancelToken);
					processingTelemetry.QueryExecutionStats = activeQueryExecutor.QueryStats;
					IRowSourceManager rowSourceManager = DataPreparer.CreateRowSourceManager(activeQueryExecutor.RowSources, reconciledDsd.DataSets, reconciledDsd.DataTransforms, reconciledDsd.ResultTableInfos, tracer, telemetryService, dataTransformPluginFactory, compareInfo, cancelToken);
					new DataShapeResultGenerator(telemetryService, tracer, reconciledDsd, writer, compareInfo, processingTelemetry, rowSourceManager, options).Generate();
					processingTelemetry.RowCount = activeQueryExecutor.RowCount;
					await activeQueryExecutor.CloseAsync(true, false);
				}
				catch (Exception obj3) when (!ErrorUtils.IsStoppingException((Exception)obj3))
				{
					num = 1;
				}
				object obj3;
				if (num == 1)
				{
					await activeQueryExecutor.CloseAsync(false, true);
					Exception ex = obj3 as Exception;
					if (ex == null)
					{
						throw obj3;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj3 = null;
			}
			catch (object obj)
			{
			}
			await activeQueryExecutor.CloseAsync(false, false);
			object obj4 = obj;
			if (obj4 != null)
			{
				Exception ex2 = obj4 as Exception;
				if (ex2 == null)
				{
					throw obj4;
				}
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			obj = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A8 File Offset: 0x000003A8
		private QueryExecutionManager SetupQueryExecution(DataShapeProcessorContext context, DataShapeDefinition reconciledDsd, ProcessingCompareInfo compareInfo, CancellationToken cancelToken)
		{
			IList<DataSet> dataSets = reconciledDsd.DataSets;
			DataSource dataSource = reconciledDsd.DataSource;
			return new QueryExecutionManager(context.TelemetryService, context.Tracer, dataSource.DataSourceInfo, context.CommandOptions, dataSets, context.ConnectionPool, context.ConnectionStringResolver, QueryExecutionStrategyFactory.CreateStrategy(true), context.ConnectionUserImpersonator, context.QueryExecutionOptions, context.ExecutionMetricsService);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002208 File Offset: 0x00000408
		private void ReportInitialStats(ProcessingTelemetry processingStats, DataShapeProcessorContext context)
		{
			processingStats.QueryPattern = context.PatternKind;
			processingStats.DataShapeId = context.Dsd.DataShape.Id;
			if (context.DataSourceInfo.Category != null)
			{
				processingStats.ConnectionCategory = context.DataSourceInfo.Category;
			}
		}
	}
}
