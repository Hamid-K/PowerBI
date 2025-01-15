using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000038 RID: 56
	internal sealed class ExecuteExportDataQueryFlow : SemanticQueryHandlerBaseFlow
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000596D File Offset: 0x00003B6D
		internal ExecuteExportDataQueryFlow(string jsonRequest, string databaseID, ExploreClientHandlerContext context, Stream output, ASQueryLimits asQueryLimits)
			: base(context, databaseID)
		{
			this.jsonRequest = jsonRequest;
			this.outputStream = output;
			this.asQueryLimits = asQueryLimits;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000598E File Offset: 0x00003B8E
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00005996 File Offset: 0x00003B96
		public ExportDataMetadata QueryResultMetadata { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000599F File Offset: 0x00003B9F
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

		// Token: 0x060001D4 RID: 468 RVA: 0x000059C0 File Offset: 0x00003BC0
		protected override void InternalRun()
		{
			DataQueryRequest dataQueryRequest = base.DeserializeExecuteSemanticQueryRequest(this.jsonRequest).Queries.First<DataQueryRequest>();
			EngineDataModel engineDataModel;
			ServiceError serviceError;
			IDataSourceInfo dataSourceInfo;
			if (!ExploreHostUtils.TryGetEngineDataModel(this.Context, 0L, this.ServiceErrorExtractor, base.DatabaseID, "2.0", null, out engineDataModel, out serviceError, out dataSourceInfo))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("An error occurred getting the engine data model. Details: {0}", new object[] { serviceError.Message.MarkAsPrivate() })));
			}
			QueryBindingDescriptor queryBindingDescriptor = this.ProcessAndWriteSemanticQueryCommands(dataQueryRequest, engineDataModel);
			this.QueryResultMetadata = ExportDataMetadataBuilder.Build(dataQueryRequest, queryBindingDescriptor);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005A54 File Offset: 0x00003C54
		protected override void InternalValidateRequest(ExecuteSemanticQueryRequest request)
		{
			if (request.Queries.Count != 1)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("The supplied request has an unexpected number of queries. Expected exactly 1 and received {0}.", new object[] { request.Queries.Count })));
			}
			base.InternalValidateRequest(request);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005AA4 File Offset: 0x00003CA4
		private QueryBindingDescriptor ProcessAndWriteSemanticQueryCommands(DataQueryRequest queryRequest, EngineDataModel engineDataModel)
		{
			QueryBindingDescriptor descriptor;
			try
			{
				CapturingExecuteSemanticQueryResultWriter capturingExecuteSemanticQueryResultWriter = new CapturingExecuteSemanticQueryResultWriter(this.outputStream, StreamFormat.RawData);
				this.ExecuteDataQuery(capturingExecuteSemanticQueryResultWriter, engineDataModel, queryRequest.Query, 0, CancellationToken.None);
				descriptor = capturingExecuteSemanticQueryResultWriter.Descriptor;
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreHostUtils.TraceSemanticQueryException(ex);
				throw;
			}
			return descriptor;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005B10 File Offset: 0x00003D10
		private void ExecuteDataQuery(IExecuteSemanticQueryResultWriter queryResultDataWriter, EngineDataModel engineDataModel, DataQuery query, int queryId, CancellationToken cancelToken)
		{
			SemanticQueryDataShapeCommand semanticQueryDataShapeCommand = DataQuery.GetSemanticQueryDataShapeCommand(query);
			DataShapeEngineHost.ExecuteSemanticQueryRawData(this.Context.DataShapeEngine, semanticQueryDataShapeCommand, engineDataModel, queryResultDataWriter, this.Context.PowerViewHandler.GetConnectionPool(base.DatabaseID), this.Context.PowerViewHandler.GetDataSourceInfo(base.DatabaseID), DataReductionConfiguration.DefaultForCompositeDataQuery, this.Context.PowerViewHandler.GetConnectionUserImpersonator(base.DatabaseID), this.Context.PowerViewHandler.CreateTelemetryServiceForRequest(base.DatabaseID), this.Context.PowerViewHandler.GetDSEQueryExecutionOptions(base.DatabaseID), queryId, cancelToken, this.Context.FeatureSwitchProvider, this.Context.AnalyticsFeatureSwitchProvider, this.Context.FeatureSwitches.MsolapTracingEnabled, this.asQueryLimits);
		}

		// Token: 0x040000A5 RID: 165
		private readonly string jsonRequest;

		// Token: 0x040000A6 RID: 166
		private readonly Stream outputStream;

		// Token: 0x040000A7 RID: 167
		private readonly ASQueryLimits asQueryLimits;
	}
}
