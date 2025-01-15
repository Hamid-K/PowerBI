using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing;
using Microsoft.DataShaping.RawDataProcessing;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000007 RID: 7
	internal sealed class DataShapeProcessingHost
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002748 File Offset: 0x00000948
		internal static Task<ExecuteSemanticQueryResult> InvokeProcessingAsync(HostServices hostServices, IDataShapeProcessor processor, DataShapeDefinition dsd, IExecuteSemanticQueryResultWriter resultWriter, IDataShapingDataSourceInfo dataSourceInfo, bool enableRemoteErrors, QueryPatternKind patternKind, ExecuteSemanticQueryTelemetry sqTelemetryInfo, QueryExecutionOptions queryExecutionOptions, CancellationToken cancelToken, IDataShapingExecutionMetricsService executionMetricsService, QueryCommandOptions queryCommandOptions, DsrWriterOptions dsrWriterOptions)
		{
			return DataShapeProcessingHost.InvokeProcessingAsync(processor, hostServices.TelemetryService, hostServices.Tracer, hostServices.ConnectionFactory, dsd, resultWriter, dataSourceInfo, enableRemoteErrors, hostServices.ConnectionPool, hostServices.ConnectionStringResolver, hostServices.DataTransformPluginFactory, patternKind, hostServices.ConnectionUserImpersonator, hostServices.FeatureSwitchProvider, sqTelemetryInfo, queryExecutionOptions, cancelToken, executionMetricsService, queryCommandOptions, dsrWriterOptions);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027A0 File Offset: 0x000009A0
		internal static Task<ExecuteSemanticQueryResult> InvokeRawDataProcessingAsync(HostServices hostServices, IExecuteSemanticQueryResultWriter resultWriter, IDataShapingDataSourceInfo dataSource, RawDataDefinition rawDataDefinition, bool enableRemoteErrors, ExecuteSemanticQueryRawTelemetry sqTelemetryInfo, QueryExecutionOptions queryExecutionOptions, CancellationToken cancelToken, QueryCommandOptions queryCommandOptions)
		{
			return DataShapeProcessingHost.InvokeRawDataProcessingAsync(new RawDataGenerator(hostServices.TelemetryService, hostServices.Tracer, dataSource, rawDataDefinition, hostServices.ConnectionFactory, hostServices.ConnectionPool, hostServices.ConnectionStringResolver, resultWriter, hostServices.ConnectionUserImpersonator, sqTelemetryInfo.RawDataProcessing, queryExecutionOptions, queryCommandOptions), hostServices.Tracer, resultWriter, enableRemoteErrors, sqTelemetryInfo, cancelToken);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027F8 File Offset: 0x000009F8
		internal static async Task<ExecuteSemanticQueryResult> InvokeProcessingAsync(IDataShapeProcessor processor, Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IConnectionFactory connectionFactory, DataShapeDefinition dsd, IExecuteSemanticQueryResultWriter resultWriter, IDataShapingDataSourceInfo dataSourceInfo, bool enableRemoteErrors, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, IDataTransformPluginFactory transformFactory, QueryPatternKind patternKind, IConnectionUserImpersonator connectionUserImpersonator, IFeatureSwitchProvider featureSwitchProvider, ExecuteSemanticQueryTelemetry sqTelemetryInfo, QueryExecutionOptions queryExecutionOptions, CancellationToken cancelToken, IDataShapingExecutionMetricsService executionMetricsService, QueryCommandOptions commandOptions, DsrWriterOptions dsrWriterOptions)
		{
			ExecuteSemanticQueryResult executeSemanticQueryResult;
			using (MemoryStream bufferStream = new MemoryStream())
			{
				JsonStreamingStructureWriter writer = new JsonStreamingStructureWriter(bufferStream);
				DataShapeProcessorContext dataShapeProcessorContext = new DataShapeProcessorContext(telemetryService, tracer, dsd, writer, connectionFactory, dataSourceInfo, commandOptions, connectionPool, connectionStringResolver, patternKind, transformFactory, connectionUserImpersonator, featureSwitchProvider, sqTelemetryInfo.Processing, queryExecutionOptions, executionMetricsService, dsrWriterOptions);
				try
				{
					await processor.Process(dataShapeProcessorContext, cancelToken);
					writer.Flush();
					bufferStream.Seek(0L, SeekOrigin.Begin);
					Stream andVerifyStream = DataShapeProcessingHost.GetAndVerifyStream(resultWriter);
					await bufferStream.CopyToAsync(andVerifyStream);
					executeSemanticQueryResult = ExecuteSemanticQueryResult.ForSuccess();
				}
				catch (Exception ex) when (ex is DataExtensionException || ex is DataShapeEngineException)
				{
					sqTelemetryInfo.RegisterException(ex);
					tracer.SanitizedTrace(TraceLevel.Info, "Writing error DSR");
					string text;
					if (dsd == null)
					{
						text = null;
					}
					else
					{
						DataShape dataShape = dsd.DataShape;
						text = ((dataShape != null) ? dataShape.Id : null);
					}
					string text2 = text;
					HandledExceptionWrapper handledExceptionWrapper = new HandledExceptionWrapper(ex as DataExtensionException, ex as DataShapeEngineException);
					DataShapeResultWriterUtilities.WriteErrorDsr(DataShapeProcessingHost.GetAndVerifyStream(resultWriter), enableRemoteErrors, handledExceptionWrapper, text2, featureSwitchProvider);
					executeSemanticQueryResult = ExecuteSemanticQueryResult.ForError(handledExceptionWrapper.ToErrorInfo());
				}
				catch (Exception ex2) when (!ErrorUtils.IsStoppingException(ex2))
				{
					tracer.TraceSanitizedError(ex2, "An unexpected exception occured in Data Shape Engine Processing.");
					throw;
				}
			}
			return executeSemanticQueryResult;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028E4 File Offset: 0x00000AE4
		internal static async Task<ExecuteSemanticQueryResult> InvokeRawDataProcessingAsync(IRawDataGenerator rawDataGenerator, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IExecuteSemanticQueryResultWriter resultWriter, bool enableRemoteErrors, ExecuteSemanticQueryRawTelemetry sqTelemetryInfo, CancellationToken cancelToken)
		{
			ExecuteSemanticQueryResult executeSemanticQueryResult;
			try
			{
				await rawDataGenerator.Generate();
				executeSemanticQueryResult = ExecuteSemanticQueryResult.ForSuccess();
			}
			catch (Exception ex) when (ex is DataExtensionException || ex is DataShapeEngineException)
			{
				sqTelemetryInfo.RegisterException(ex);
				tracer.SanitizedTrace(TraceLevel.Info, "Writing raw data error");
				DataShapeProcessingHost.WriteRawDataError(resultWriter, ex);
				HandledExceptionWrapper handledExceptionWrapper = new HandledExceptionWrapper(ex as DataExtensionException, ex as DataShapeEngineException);
				executeSemanticQueryResult = ExecuteSemanticQueryResult.ForError(handledExceptionWrapper.ToErrorInfo());
			}
			return executeSemanticQueryResult;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002940 File Offset: 0x00000B40
		internal static void WriteRawDataError(IExecuteSemanticQueryResultWriter resultWriter, Exception ex)
		{
			RawDataGenerator.WriteRawDataError(resultWriter, ex);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002949 File Offset: 0x00000B49
		private static Stream GetAndVerifyStream(IExecuteSemanticQueryResultWriter resultWriter)
		{
			return resultWriter.GetDataShapeResultStream();
		}
	}
}
