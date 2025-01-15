using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.DaxQueryExecution;
using Microsoft.DataShaping.Engine.DaxQueryExecution;
using Microsoft.DataShaping.Engine.QueryTranslation;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.RawDataProcessing;
using Microsoft.DataShaping.SemanticQueryTranslation;
using Microsoft.DataShaping.SemanticQueryTranslation.SparklineData;
using Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000006 RID: 6
	public sealed class DataShapeEngine : IDataShapeEngine
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public Task ExecuteSemanticQueryAsync(ExecuteSemanticQueryContext context)
		{
			return this.ExecuteSemanticQueryWithResultAsync(context);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		public Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryWithResultAsync(ExecuteSemanticQueryContext context)
		{
			return context.HostServices.TelemetryService.RunInAsyncActivity<ExecuteSemanticQueryResult>(ActivityKind.ExecuteSemanticQuery, () => this.ExecuteSemanticQueryAsyncImpl(context));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		public void ExecuteSemanticQuery(ExecuteSemanticQueryContext context)
		{
			this.ExecuteSemanticQueryWithResult(context);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C6 File Offset: 0x000002C6
		public ExecuteSemanticQueryResult ExecuteSemanticQueryWithResult(ExecuteSemanticQueryContext context)
		{
			return this.ExecuteSemanticQueryWithResultAsync(context).WaitAndUnwrapResult<ExecuteSemanticQueryResult>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		public Task ExecuteSemanticQueryRawDataAsync(ExecuteSemanticQueryContext context)
		{
			return this.ExecuteSemanticQueryRawDataWithResultAsync(context);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		public Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryRawDataWithResultAsync(ExecuteSemanticQueryContext context)
		{
			return context.HostServices.TelemetryService.RunInAsyncActivity<ExecuteSemanticQueryResult>(ActivityKind.ExecuteSemanticQueryRawData, () => this.ExecuteSemanticQueryRawDataAsyncImpl(context));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public void ExecuteSemanticQueryRawData(ExecuteSemanticQueryContext context)
		{
			this.ExecuteSemanticQueryRawDataWithResult(context);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000212E File Offset: 0x0000032E
		public ExecuteSemanticQueryResult ExecuteSemanticQueryRawDataWithResult(ExecuteSemanticQueryContext context)
		{
			return this.ExecuteSemanticQueryRawDataWithResultAsync(context).WaitAndUnwrapResult<ExecuteSemanticQueryResult>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213C File Offset: 0x0000033C
		public Task<ExecuteDaxQueryResult> ExecuteDaxQueryAsync(ExecuteDaxQueryContext context)
		{
			return context.HostServices.TelemetryService.RunInAsyncActivity<ExecuteDaxQueryResult>(ActivityKind.ExecuteDaxQuery, () => this.ExecuteDaxQueryAsyncImpl(context));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002180 File Offset: 0x00000380
		public ExecuteDaxQueryResult ExecuteDaxQuery(ExecuteDaxQueryContext context)
		{
			return this.ExecuteDaxQueryAsync(context).WaitAndUnwrapResult<ExecuteDaxQueryResult>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public TranslateSemanticQueryResult TranslateSemanticQuery(TranslateSemanticQueryContext context)
		{
			SemanticQueryTranslatorContext semanticQueryTranslatorContext = this.CreateSemanticQueryTranslatorContext(context);
			TranslateQueryCommandProcessorResult translateQueryCommandProcessorResult = TranslateQueryCommandProcessor.Process(context.Command, semanticQueryTranslatorContext, context.ConfigKind, context.EnableRemoteErrors);
			if (translateQueryCommandProcessorResult.Succeeded)
			{
				return TranslateSemanticQueryResult.ForSuccess(translateQueryCommandProcessorResult.TranslatedQuery);
			}
			return TranslateSemanticQueryResult.ForError(translateQueryCommandProcessorResult.TranslatedQuery, translateQueryCommandProcessorResult.ErrorInfo);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E4 File Offset: 0x000003E4
		public SemanticQueryToDaxTranslationResult TranslateGroupingQuery(TranslateGroupingQueryContext context)
		{
			SemanticQueryTranslatorContext semanticQueryTranslatorContext = this.CreateSemanticQueryTranslatorContext(context);
			return GroupingDefinitionToDaxTranslator.Instance.Translate(context.Command, semanticQueryTranslatorContext);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220C File Offset: 0x0000040C
		public SemanticQueryToDaxTranslationResult TranslatePartitionColumn(TranslateGroupingQueryContext context)
		{
			SemanticQueryTranslatorContext semanticQueryTranslatorContext = this.CreateSemanticQueryTranslatorContext(context);
			return GroupingDefinitionToDaxTranslator.Instance.TranslatePartitionColumn(context.Command, semanticQueryTranslatorContext);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		private async Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryRawDataAsyncImpl(ExecuteSemanticQueryContext context)
		{
			if (context.HostServices.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QueryExecutionMetrics) && context.Command.ExecutionMetricsKind != ExecutionMetricsKind.None)
			{
				context.HostServices.Tracer.SanitizedTrace(TraceLevel.Warning, "The raw data request asks for execution metrics. The raw data pipeline does not support execution metrics, so the request ExecutionMetricsKind will be ignored.");
			}
			ExecuteSemanticQueryRawTelemetry telemetryInfo = new ExecuteSemanticQueryRawTelemetry
			{
				QueryId = context.QueryId.ToString(CultureInfo.InvariantCulture)
			};
			ExecuteSemanticQueryResult executeSemanticQueryResult;
			try
			{
				DataShapeQueryTranslationResult dataShapeQueryTranslationResult;
				DataShapeEngineErrorInfo dataShapeEngineErrorInfo;
				RawDataDefinition rawDataDefinition;
				if (!DataShapeEngine.TryGenerateAndTranslateQuery(context, telemetryInfo, new DataShapeEngine.HandleKnownError(this.HandleKnownErrorToRawResult), out dataShapeQueryTranslationResult, out dataShapeEngineErrorInfo))
				{
					executeSemanticQueryResult = ExecuteSemanticQueryResult.ForError(dataShapeEngineErrorInfo);
				}
				else if (!this.TryConvertToRawDataDefinition(context, dataShapeQueryTranslationResult, telemetryInfo, out rawDataDefinition, out dataShapeEngineErrorInfo))
				{
					executeSemanticQueryResult = ExecuteSemanticQueryResult.ForError(dataShapeEngineErrorInfo);
				}
				else
				{
					telemetryInfo.RawDataProcessing = new RawDataProcessingTelemetry();
					executeSemanticQueryResult = await DataShapeProcessingHost.InvokeRawDataProcessingAsync(context.HostServices, context.ResultWriter, context.DataSourceInfo, rawDataDefinition, context.EnableRemoteErrors, telemetryInfo, context.QueryExecutionOptions, context.CancelToken, context.DbCommandOptions.ToQueryCommandOptions(RequestExecutionMetricsKind.None, null));
				}
			}
			catch (Exception ex)
			{
				telemetryInfo.RegisterException(ex);
				throw;
			}
			finally
			{
				telemetryInfo.SetCancelStatus(context.CancelToken);
				telemetryInfo.Write(context.HostServices.TelemetryService, context.HostServices.Tracer);
			}
			return executeSemanticQueryResult;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002280 File Offset: 0x00000480
		private Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryAsyncImpl(ExecuteSemanticQueryContext context)
		{
			DataShapeEngine.<ExecuteSemanticQueryAsyncImpl>d__16 <ExecuteSemanticQueryAsyncImpl>d__;
			<ExecuteSemanticQueryAsyncImpl>d__.<>t__builder = AsyncTaskMethodBuilder<ExecuteSemanticQueryResult>.Create();
			<ExecuteSemanticQueryAsyncImpl>d__.<>4__this = this;
			<ExecuteSemanticQueryAsyncImpl>d__.context = context;
			<ExecuteSemanticQueryAsyncImpl>d__.<>1__state = -1;
			<ExecuteSemanticQueryAsyncImpl>d__.<>t__builder.Start<DataShapeEngine.<ExecuteSemanticQueryAsyncImpl>d__16>(ref <ExecuteSemanticQueryAsyncImpl>d__);
			return <ExecuteSemanticQueryAsyncImpl>d__.<>t__builder.Task;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022CC File Offset: 0x000004CC
		private async Task<ExecuteDaxQueryResult> ExecuteDaxQueryAsyncImpl(ExecuteDaxQueryContext context)
		{
			DaxQueryExecutionTelemetry telemetryInfo = new DaxQueryExecutionTelemetry();
			ExecuteDaxQueryResult executeDaxQueryResult;
			try
			{
				QueryCommandOptions queryCommandOptions = context.DbCommandOptions.ToQueryCommandOptions(RequestExecutionMetricsKind.None, null);
				await new DaxQueryExecutor(context.HostServices.TelemetryService, context.HostServices.Tracer, context.HostServices.ConnectionFactory, queryCommandOptions, context.HostServices.ConnectionPool, context.HostServices.ConnectionStringResolver, context.DaxQuery, context.DataSourceInfo, context.MaxRowCount, context.MaxNumberOfValues, context.MaxNumberOfBytes, context.ResultWriter, context.HostServices.ConnectionUserImpersonator, telemetryInfo, new DaxQueryResultWriterSettings(context.SerializerSettings.IncludeNulls)).ExecuteAsync();
				telemetryInfo.ResultSize = context.ResultWriter.BytesWritten;
				executeDaxQueryResult = ExecuteDaxQueryResult.ForSuccess();
			}
			catch (Exception ex)
			{
				telemetryInfo.RegisterException(ex);
				throw;
			}
			finally
			{
				telemetryInfo.Write(context.HostServices.TelemetryService, context.HostServices.Tracer);
			}
			return executeDaxQueryResult;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
		private void WriteExecutionMetrics(IExecuteSemanticQueryResultWriter writer, IDataShapingExecutionMetricsService executionMetricsService, ExecutionMetricsTelemetry telemetry)
		{
			ExecutionMetrics executionMetrics = executionMetricsService.ToExecutionMetrics();
			if (executionMetrics != null)
			{
				writer.WriteExecutionMetrics(executionMetrics);
				List<ExecutionEvent> events = executionMetrics.Events;
				telemetry.EventCount = ((events != null) ? events.Count : 0);
				telemetry.Truncated = executionMetricsService.ExceededMaxEventCount;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002354 File Offset: 0x00000554
		private static bool TryGenerateAndTranslateQuery(ExecuteSemanticQueryContext context, ExecuteSemanticQueryTelemetry telemetryInfo, DataShapeEngine.HandleKnownError errorHandler, out DataShapeQueryTranslationResult dsqtResult, out DataShapeEngineErrorInfo errorInfo)
		{
			DataShapeGenerationResult dataShapeGenerationResult = null;
			bool flag3;
			try
			{
				telemetryInfo.DataShapeGeneration = new DataShapeGenerationTelemetry();
				EngineDataModel dataModel = context.DataModel;
				bool flag = context.HostServices.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
				telemetryInfo.Model = ((dataModel.Model != null) ? dataModel.Model.GetModelTelemetry(flag ? dataModel.Schema : null) : ModelTelemetryBuilder.BuildModelTelemetry(dataModel.Model, dataModel.Schema));
				SemanticQueryTranslatorContext semanticQueryTranslatorContext = new SemanticQueryTranslatorContext(context.HostServices.TelemetryService, context.HostServices.Tracer, context.HostServices.FeatureSwitchProvider, context.HostServices.Dumper, context.DataModel.Schema, context.DataModel.Model, context.DataSourceInfo.Name, context.TransformMetadataFactory, context.QueryId, new CancellationToken?(context.CancelToken));
				DataShapeGenerationContext dataShapeGenerationContext = new DataShapeGenerationContext(context.HostServices.Tracer, context.HostServices.TelemetryService, context.HostServices.FeatureSwitchProvider, context.HostServices.Dumper, dataModel.Schema, DateTimeProviderFactory.Instance, context.QueryId, context.UseDynamicLimits, telemetryInfo.DataShapeGeneration, new ExpressionToExtensionSchemaItemQueryRewriter(semanticQueryTranslatorContext), false, false, false);
				DataReductionConfiguration dataReductionConfiguration = context.DataReductionConfig ?? DataReductionConfiguration.Default;
				dataShapeGenerationResult = DataShapeQueryGeneratorAdapter.Instance.GenerateDataShapeFromCommand(dataShapeGenerationContext, context.Command, dataReductionConfiguration, DataReductionConfiguration.DefaultForLegacyLimits);
				context.ResultWriter.WriteQueryBindingDescriptor(dataShapeGenerationResult.BindingDescriptor);
				dataShapeGenerationResult.DataShape.DataSourceId = context.DataSourceId;
				DataShapeQueryTranslationOptions dataShapeQueryTranslationOptions = context.DsqtOptions;
				if (context.DsqtOptions.ApplyTransformsInQuery == null)
				{
					bool flag2 = TransformTypeSelector.Select(dataShapeGenerationResult.DataShape, context.TransformMetadataFactory, context.HostServices.DataTransformPluginFactory) == TransformType.Query;
					dataShapeQueryTranslationOptions = dataShapeQueryTranslationOptions.CloneWithOverrides(flag2);
				}
				DataSourceContext dataSourceContext = new DataSourceContext(context.DataSourceInfo.Name, dataModel.Model, dataShapeGenerationResult.FederatedConceptualSchema);
				telemetryInfo.DataShapeQueryTranslation = new DataShapeQueryTranslationTelemetry();
				DataShapeQueryTranslationContext dataShapeQueryTranslationContext = new DataShapeQueryTranslationContext(dataShapeGenerationResult.DataShape, context.HostServices.Tracer, context.HostServices.TelemetryService, context.HostServices.FeatureSwitchProvider, context.HostServices.Dumper, dataSourceContext, context.TestOnlyQueryPatternOverride, telemetryInfo.DataShapeQueryTranslation, context.CancelToken, dataShapeQueryTranslationOptions, context.TransformMetadataFactory);
				dsqtResult = context.DataShapeQueryTranslator.Translate(dataShapeQueryTranslationContext);
				errorInfo = null;
				flag3 = true;
			}
			catch (DataShapeEngineException ex)
			{
				telemetryInfo.RegisterException(ex);
				context.HostServices.Tracer.SanitizedTrace(TraceLevel.Info, "Writing error DSR");
				string text;
				if (dataShapeGenerationResult == null)
				{
					text = null;
				}
				else
				{
					DataShape dataShape = dataShapeGenerationResult.DataShape;
					text = ((dataShape != null) ? dataShape.Id.Value : null);
				}
				string text2 = text;
				errorHandler(ex, context, text2);
				dsqtResult = null;
				errorInfo = ex.ToErrorInfo();
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002630 File Offset: 0x00000830
		private bool TryConvertToRawDataDefinition(ExecuteSemanticQueryContext context, DataShapeQueryTranslationResult dsqtResult, ExecuteSemanticQueryTelemetry telemetryInfo, out RawDataDefinition rawDataDefinition, out DataShapeEngineErrorInfo errorInfo)
		{
			bool flag;
			try
			{
				rawDataDefinition = dsqtResult.UnifiedDataShapeDefinition.ToRawDataDefinition();
				errorInfo = null;
				flag = true;
			}
			catch (DataShapeEngineException ex)
			{
				telemetryInfo.RegisterException(ex);
				context.HostServices.Tracer.SanitizedTrace(TraceLevel.Info, "Error converting dsd to raw data definition");
				DataShapeProcessingHost.WriteRawDataError(context.ResultWriter, ex);
				rawDataDefinition = null;
				errorInfo = ex.ToErrorInfo();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026A0 File Offset: 0x000008A0
		private void HandleKnownErrorToDsr(DataShapeEngineException ex, ExecuteSemanticQueryContext context, string dataShapeId)
		{
			DataShapeResultWriterUtilities.WriteErrorDsr(context.ResultWriter.GetDataShapeResultStream(), context.EnableRemoteErrors, ex, dataShapeId, context.HostServices.FeatureSwitchProvider);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026CA File Offset: 0x000008CA
		private void HandleKnownErrorToRawResult(DataShapeEngineException ex, ExecuteSemanticQueryContext context, string dataShapeId)
		{
			DataShapeProcessingHost.WriteRawDataError(context.ResultWriter, ex);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026D8 File Offset: 0x000008D8
		private SemanticQueryTranslatorContext CreateSemanticQueryTranslatorContext(TranslateQueryContextBase context)
		{
			return new SemanticQueryTranslatorContext(context.TelemetryService, context.Tracer, context.FeatureSwitchProvider, context.Dumper, context.DataModel.Schema, context.DataModel.Model, "DataSourceName", context.TransformMetadataFactory, context.QueryId, new CancellationToken?(context.CancelToken));
		}

		// Token: 0x04000029 RID: 41
		public static readonly DataShapeEngine Instance = new DataShapeEngine();

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060000F4 RID: 244
		private delegate void HandleKnownError(DataShapeEngineException ex, ExecuteSemanticQueryContext context, string dataShapeId);
	}
}
