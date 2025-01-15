using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.ExploreServiceCommon;
using Microsoft.PowerBI.ExploreServiceCommon.Interfaces;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;
using Microsoft.PowerBI.Telemetry.PIIUtils;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000039 RID: 57
	internal sealed class ExecuteSemanticQueryFlow : SemanticQueryHandlerBaseFlow
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00005BDA File Offset: 0x00003DDA
		internal ExecuteSemanticQueryFlow(string jsonRequest, string databaseID, ExploreClientHandlerContext context, ASQueryLimits asQueryLimits)
			: base(context, databaseID)
		{
			this.jsonRequest = jsonRequest;
			this.serializedResult = null;
			this.asQueryLimits = asQueryLimits;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00005BFA File Offset: 0x00003DFA
		public string SerializedSemanticQueryResult
		{
			get
			{
				return this.serializedResult;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005C02 File Offset: 0x00003E02
		private ServiceErrorExtractor ServiceErrorExtractor
		{
			get
			{
				if (this.serviceErrorExtractor == null)
				{
					this.serviceErrorExtractor = ExploreHostServiceErrorExtractorFactory.Instance.Create();
				}
				return this.serviceErrorExtractor;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005C22 File Offset: 0x00003E22
		private IQueryCancellationManager CancellationManager
		{
			get
			{
				return this.Context.RunningQueriesCancellationManager;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005C30 File Offset: 0x00003E30
		private static ServiceError CreateServiceErrorFromException(Exception e, ServiceErrorExtractor extractor, ServiceErrorStatusCode statusCode)
		{
			ServiceError serviceError;
			if (e != null && extractor.TryExtractServiceError(e, out serviceError))
			{
				serviceError.StatusCode = statusCode;
			}
			else
			{
				serviceError = new ServiceError
				{
					StatusCode = statusCode
				};
			}
			return serviceError;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005C64 File Offset: 0x00003E64
		public static string CreateSemanticQueryResultFromException(Exception ex, ServiceErrorStatusCode statusCode, string powerBIErrorCode)
		{
			string result;
			using (IExploreExecuteSemanticQueryResultWriter exploreExecuteSemanticQueryResultWriter = ExploreExecuteSemanticQueryResultWriter.Create())
			{
				ServiceError serviceError = ExecuteSemanticQueryFlow.CreateServiceErrorFromException(ex, ExploreHostServiceErrorExtractorFactory.Instance.Create(), statusCode);
				serviceError.PowerBIErrorCode = powerBIErrorCode;
				exploreExecuteSemanticQueryResultWriter.WriteRequestError(serviceError);
				result = exploreExecuteSemanticQueryResultWriter.GetResult();
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005CBC File Offset: 0x00003EBC
		protected override void InternalRun()
		{
			using (IExploreExecuteSemanticQueryResultWriter exploreExecuteSemanticQueryResultWriter = ExploreExecuteSemanticQueryResultWriter.Create())
			{
				ExecuteSemanticQueryRequest executeSemanticQueryRequest;
				try
				{
					executeSemanticQueryRequest = base.DeserializeExecuteSemanticQueryRequest(this.jsonRequest);
				}
				catch (ArgumentException ex)
				{
					ServiceError serviceError = ExecuteSemanticQueryFlow.CreateServiceErrorFromException(ex, this.ServiceErrorExtractor, ServiceErrorStatusCode.ExecuteSemanticQueryInvalidStreamFormat);
					ExploreHostUtils.TraceClientRequestStreamException(ex);
					exploreExecuteSemanticQueryResultWriter.WriteRequestError(serviceError);
					this.serializedResult = exploreExecuteSemanticQueryResultWriter.GetResult();
					return;
				}
				IQueryResultsWriter queryResultsWriter = exploreExecuteSemanticQueryResultWriter.BeginResults(executeSemanticQueryRequest);
				HashSet<string> hashSet = null;
				if (!executeSemanticQueryRequest.CancelQueries.IsNullOrEmptyCollection<CancelQueryRequest>())
				{
					this.ProcessCancelCommands(executeSemanticQueryRequest.CancelQueries, executeSemanticQueryRequest.Queries, out hashSet);
				}
				if (!executeSemanticQueryRequest.Queries.IsNullOrEmptyCollection<DataQueryRequest>())
				{
					EngineDataModel engineDataModel;
					ServiceError serviceError2;
					IDataSourceInfo dataSourceInfo;
					if (!ExploreHostUtils.TryGetEngineDataModel(this.Context, 0L, this.ServiceErrorExtractor, base.DatabaseID, "2.0", null, out engineDataModel, out serviceError2, out dataSourceInfo))
					{
						for (int i = 0; i < executeSemanticQueryRequest.Queries.Count; i++)
						{
							queryResultsWriter.WriteFailedQueryResult(serviceError2);
						}
					}
					else
					{
						this.ProcessAndWriteSemanticQueryCommands(queryResultsWriter, executeSemanticQueryRequest.Queries, hashSet, engineDataModel);
					}
				}
				this.serializedResult = exploreExecuteSemanticQueryResultWriter.GetResult();
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005DDC File Offset: 0x00003FDC
		private void ProcessCancelCommands(IList<CancelQueryRequest> cancels, IList<DataQueryRequest> queries, out HashSet<string> pendingQueriesToCancel)
		{
			pendingQueriesToCancel = null;
			bool flag = !queries.IsNullOrEmptyCollection<DataQueryRequest>();
			DataShapingTracer instance = DataShapingTracer.Instance;
			for (int i = 0; i < cancels.Count; i++)
			{
				try
				{
					string queryId = cancels[i].QueryId;
					bool flag2 = this.CancellationManager.CancelRunningQuery(queryId);
					if (!flag2 && flag && queries.Any((DataQueryRequest q) => string.CompareOrdinal(q.QueryId, queryId) == 0))
					{
						Util.AddToLazy(ref pendingQueriesToCancel, queryId);
						flag2 = true;
					}
					ExploreHostUtils.AccumulatedCancelTelemetry(!flag2);
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					ExploreHostUtils.TraceSemanticQueryException(ex);
				}
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005E90 File Offset: 0x00004090
		private void ProcessAndWriteSemanticQueryCommands(IQueryResultsWriter queryResultsWriter, IList<DataQueryRequest> queries, HashSet<string> pendingQueriesToCancel, EngineDataModel engineDataModel)
		{
			DataShapingTracer instance = DataShapingTracer.Instance;
			for (int i = 0; i < queries.Count; i++)
			{
				IQueryResultDataWriter queryResultDataWriter = queryResultsWriter.BeginQueryResultData();
				ServiceErrorStatusCode serviceErrorStatusCode = ServiceErrorStatusCode.ExecuteSemanticQueryError;
				DataQueryRequest dataQueryRequest = queries[i];
				try
				{
					using (QueryCancelTokenWrapper queryCancelTokenWrapper = this.CancellationManager.CreateTokenForQuery(dataQueryRequest.QueryId, instance))
					{
						if (pendingQueriesToCancel != null && pendingQueriesToCancel.Contains(dataQueryRequest.QueryId))
						{
							this.CancellationManager.CancelRunningQuery(dataQueryRequest.QueryId);
						}
						this.ExecuteDataQuery(queryResultDataWriter, engineDataModel, dataQueryRequest.Query, i, ref serviceErrorStatusCode, queryCancelTokenWrapper.Token);
						queryResultDataWriter.EndQueryResultData();
					}
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					if (ex.ContainsPII())
					{
						ExploreHostUtils.TraceSemanticQueryException(ex.ToTraceString(), ex.Message.MarkAsCustomerContent());
					}
					else
					{
						ExploreHostUtils.TraceSemanticQueryException(ex);
					}
					ServiceError serviceError = ExecuteSemanticQueryFlow.CreateServiceErrorFromException(ex, this.ServiceErrorExtractor, serviceErrorStatusCode);
					queryResultDataWriter.DiscardQueryResultDataProgress();
					queryResultsWriter.WriteFailedQueryResult(serviceError);
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005FAC File Offset: 0x000041AC
		private void ExecuteDataQuery(IQueryResultDataWriter queryResultDataWriter, EngineDataModel engineDataModel, DataQuery query, int queryId, ref ServiceErrorStatusCode serviceErrorStatusCode, CancellationToken cancelToken)
		{
			SemanticQueryDataShapeCommand semanticQueryDataShapeCommand = query.Commands[0].SemanticQueryDataShapeCommand;
			DataReductionConfiguration dataReductionConfiguration = ((query.Commands.Count > 1) ? DataReductionConfiguration.DefaultForCompositeDataQuery : null);
			if (query.Commands.Count == 1)
			{
				DataShapeEngineHost.ExecuteSemanticQuery(this.Context.DataShapeEngine, semanticQueryDataShapeCommand, engineDataModel, queryResultDataWriter, this.Context.PowerViewHandler.GetConnectionPool(base.DatabaseID), this.Context.PowerViewHandler.GetDataSourceInfo(base.DatabaseID), dataReductionConfiguration, this.Context.PowerViewHandler.GetConnectionUserImpersonator(base.DatabaseID), this.Context.PowerViewHandler.CreateTelemetryServiceForRequest(base.DatabaseID), this.Context.PowerViewHandler.GetDSEQueryExecutionOptions(base.DatabaseID), queryId, cancelToken, this.Context.FeatureSwitchProvider, this.Context.AnalyticsFeatureSwitchProvider, this.Context.FeatureSwitches.MsolapTracingEnabled, false, this.asQueryLimits);
				return;
			}
			using (FileStream fileStream = new FileStream(Path.GetTempFileName(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 10000, FileOptions.DeleteOnClose))
			{
				CapturingExecuteSemanticQueryResultWriter capturingExecuteSemanticQueryResultWriter = new CapturingExecuteSemanticQueryResultWriter(fileStream, StreamFormat.DataShapeResult);
				DataShapeEngineHost.ExecuteSemanticQuery(this.Context.DataShapeEngine, semanticQueryDataShapeCommand, engineDataModel, capturingExecuteSemanticQueryResultWriter, this.Context.PowerViewHandler.GetConnectionPool(base.DatabaseID), this.Context.PowerViewHandler.GetDataSourceInfo(base.DatabaseID), dataReductionConfiguration, this.Context.PowerViewHandler.GetConnectionUserImpersonator(base.DatabaseID), this.Context.PowerViewHandler.CreateTelemetryServiceForRequest(base.DatabaseID), this.Context.PowerViewHandler.GetDSEQueryExecutionOptions(base.DatabaseID), queryId, cancelToken, this.Context.FeatureSwitchProvider, this.Context.AnalyticsFeatureSwitchProvider, this.Context.FeatureSwitches.MsolapTracingEnabled, true, this.asQueryLimits);
				QueryBindingDescriptor descriptor = capturingExecuteSemanticQueryResultWriter.Descriptor;
				ExecutionMetrics metrics = capturingExecuteSemanticQueryResultWriter.Metrics;
				serviceErrorStatusCode = ServiceErrorStatusCode.ExecuteSemanticQueryTransformError;
				int num = 1;
				while (num < query.Commands.Count && this.TransformDataShapeResult(query.Commands[num], semanticQueryDataShapeCommand, fileStream, ref descriptor))
				{
					num++;
				}
				queryResultDataWriter.WriteQueryBindingDescriptor(descriptor);
				Stream dataShapeResultStream = queryResultDataWriter.GetDataShapeResultStream();
				fileStream.Position = 0L;
				fileStream.CopyTo(dataShapeResultStream);
				if (metrics != null)
				{
					queryResultDataWriter.WriteExecutionMetrics(metrics);
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006218 File Offset: 0x00004418
		private bool TransformDataShapeResult(QueryCommand transformCommand, SemanticQueryDataShapeCommand command, Stream dataShapeResultStream, ref QueryBindingDescriptor bindingDescriptor)
		{
			ITransformFlow transformFlow = null;
			if (transformCommand.ScriptVisualCommand != null)
			{
				IScriptHandler scriptHandler;
				if (!this.Context.ScriptHandlers.TryGetValue(transformCommand.ScriptVisualCommand.RenderingEngine, out scriptHandler))
				{
					throw new ArgumentException("The supplied rendering engine is not supported");
				}
				transformFlow = new ScriptVisualCommandFlow(command, transformCommand.ScriptVisualCommand, scriptHandler);
			}
			return transformFlow != null && transformFlow.Run(dataShapeResultStream, ref bindingDescriptor);
		}

		// Token: 0x040000A9 RID: 169
		private readonly string jsonRequest;

		// Token: 0x040000AA RID: 170
		private readonly ASQueryLimits asQueryLimits;

		// Token: 0x040000AB RID: 171
		private string serializedResult;
	}
}
