using System;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.Engine.QueryTranslation;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Analytics;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Msolap;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.ReportingServices.Library;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000026 RID: 38
	internal static class DataShapeEngineHost
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00003AF4 File Offset: 0x00001CF4
		internal static void ExecuteSemanticQuery(IDataShapeEngine engine, SemanticQueryDataShapeCommand command, EngineDataModel engineDataModel, IExecuteSemanticQueryResultWriter writer, IConnectionPool connectionPool, IDataShapingDataSourceInfo dsDataSourceInfo, DataReductionConfiguration dataReductionConfig, IConnectionUserImpersonator connectionUserImpersonator, ITelemetryService telemetryService, QueryExecutionOptions queryExecutionOptions, int queryId, CancellationToken cancelToken, IFeatureSwitchProvider featureSwitchProvider, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider, bool enableMsolapTracing, bool writeDsrV1, ASQueryLimits asQueryLimits)
		{
			DbCommandOptions dbCommandOptions = asQueryLimits.ToDBCommandOptions(RequestPriorityKind.Normal);
			ExecutionMetricsOptions executionMetricsOptions = new ExecutionMetricsOptions(ExecutionMetricsKind.All, new int?(300), new int?(200));
			ExecuteSemanticQueryContext executeSemanticQueryContext = new ExecuteSemanticQueryContext(DataShapeEngineHost.CreateHostServices(connectionPool, connectionUserImpersonator, featureSwitchProvider, telemetryService, analyticsFeatureSwitchProvider, enableMsolapTracing), command, writer, engineDataModel, dsDataSourceInfo, DaxDataTransformMetadataFactoryHost.Create(analyticsFeatureSwitchProvider), dataReductionConfig, queryId, dbCommandOptions, true, queryExecutionOptions, new CancellationToken?(cancelToken), executionMetricsOptions, writeDsrV1);
			engine.ExecuteSemanticQuery(executeSemanticQueryContext);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003B64 File Offset: 0x00001D64
		internal static void ExecuteSemanticQueryRawData(IDataShapeEngine engine, SemanticQueryDataShapeCommand command, EngineDataModel engineDataModel, IExecuteSemanticQueryResultWriter writer, IConnectionPool connectionPool, IDataShapingDataSourceInfo dsDataSourceInfo, DataReductionConfiguration dataReductionConfig, IConnectionUserImpersonator connectionUserImpersonator, ITelemetryService telemetryService, QueryExecutionOptions queryExecutionOptions, int queryId, CancellationToken cancelToken, IFeatureSwitchProvider featureSwitchProvider, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider, bool enableMsolapTracing, ASQueryLimits asQueryLimits)
		{
			DbCommandOptions dbCommandOptions = asQueryLimits.ToDBCommandOptions(RequestPriorityKind.Normal);
			ExecuteSemanticQueryContext executeSemanticQueryContext = new ExecuteSemanticQueryContext(DataShapeEngineHost.CreateHostServices(connectionPool, connectionUserImpersonator, featureSwitchProvider, telemetryService, analyticsFeatureSwitchProvider, enableMsolapTracing), command, writer, engineDataModel, dsDataSourceInfo, DaxDataTransformMetadataFactoryHost.Create(analyticsFeatureSwitchProvider), dataReductionConfig, queryId, dbCommandOptions, true, queryExecutionOptions, new CancellationToken?(cancelToken), null, false);
			engine.ExecuteSemanticQueryRawData(executeSemanticQueryContext);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003BB8 File Offset: 0x00001DB8
		internal static TranslateSemanticQueryResult TranslateDataViewQuery(IDataShapeEngine engine, TranslateQueryCommand command, IFeatureSwitchProvider featureSwitchProvider, EngineDataModel engineDataModel, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			TranslateSemanticQueryContext translateSemanticQueryContext = new TranslateSemanticQueryContext(DataShapingTracer.Instance, DataShapingDumper.Instance, DataShapingTelemetryService.Instance, featureSwitchProvider, command, engineDataModel, DaxDataTransformMetadataFactoryHost.Create(analyticsFeatureSwitchProvider), TranslateSemanticQueryConfigKind.ForDataView, true, 0, new CancellationToken?(CancellationToken.None));
			return engine.TranslateSemanticQuery(translateSemanticQueryContext);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003BF8 File Offset: 0x00001DF8
		internal static SemanticQueryToDaxTranslationResult TranslateGroupingQuery(IDataShapeEngine engine, TranslateGroupingQueryCommand command, IFeatureSwitchProvider featureSwitchProvider, EngineDataModel engineDataModel, CancellationToken cancelToken, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			TranslateGroupingQueryContext translateGroupingQueryContext = new TranslateGroupingQueryContext(DataShapingTracer.Instance, DataShapingDumper.Instance, DataShapingTelemetryService.Instance, featureSwitchProvider, command, engineDataModel, DaxDataTransformMetadataFactoryHost.Create(analyticsFeatureSwitchProvider), true, new CancellationToken?(cancelToken));
			return engine.TranslateGroupingQuery(translateGroupingQueryContext);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003C34 File Offset: 0x00001E34
		internal static SemanticQueryToDaxTranslationResult TranslatePartitionColumn(IDataShapeEngine engine, TranslateGroupingQueryCommand command, IFeatureSwitchProvider featureSwitchProvider, EngineDataModel engineDataModel, CancellationToken cancelToken, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			TranslateGroupingQueryContext translateGroupingQueryContext = new TranslateGroupingQueryContext(DataShapingTracer.Instance, DataShapingDumper.Instance, DataShapingTelemetryService.Instance, featureSwitchProvider, command, engineDataModel, DaxDataTransformMetadataFactoryHost.Create(analyticsFeatureSwitchProvider), true, new CancellationToken?(cancelToken));
			return engine.TranslatePartitionColumn(translateGroupingQueryContext);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003C70 File Offset: 0x00001E70
		private static HostServices CreateHostServices(IConnectionPool connectionPool, IConnectionUserImpersonator impersonator, IFeatureSwitchProvider featureSwitchProvider, ITelemetryService telemetryService, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider, bool enableMsolapTracing)
		{
			return new HostServices(DataShapingTracer.Instance, DataShapingDumper.Instance, telemetryService, DataExtensionFactory.CreateDefaultConnectionFactory(DataShapingTracer.Instance, DataExtensionPrivateInformationService.Instance, enableMsolapTracing), connectionPool, null, DataTransformPluginFactoryHost.Create(analyticsFeatureSwitchProvider), impersonator, featureSwitchProvider);
		}

		// Token: 0x0400008B RID: 139
		private const RequestPriorityKind DefaultRequestPriorityKind = RequestPriorityKind.Normal;

		// Token: 0x0400008C RID: 140
		private const bool EnableRemoteErrors = true;
	}
}
