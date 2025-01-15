using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.ExploreServiceCommon;
using Microsoft.PowerBI.ExploreServiceCommon.Interfaces;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000037 RID: 55
	internal sealed class EditScriptVisualCommandFlow : SemanticQueryHandlerBaseFlow
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00005621 File Offset: 0x00003821
		internal EditScriptVisualCommandFlow(string jsonRequest, string databaseID, ExploreClientHandlerContext context, ASQueryLimits asQueryLimits)
			: base(context, databaseID)
		{
			this.jsonRequest = jsonRequest;
			this.asQueryLimits = asQueryLimits;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000563C File Offset: 0x0000383C
		protected override void InternalValidateRequest(ExecuteSemanticQueryRequest request)
		{
			if (request.Queries.Count != 1 || request.Queries.Single<DataQueryRequest>().Query.Commands.Count != 2 || request.Queries.Single<DataQueryRequest>().Query.Commands[1].ScriptVisualCommand == null)
			{
				throw new ArgumentException("The supplied request is invalid.");
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000056A8 File Offset: 0x000038A8
		protected override void InternalRun()
		{
			try
			{
				ExecuteSemanticQueryRequest executeSemanticQueryRequest = base.DeserializeExecuteSemanticQueryRequest(this.jsonRequest);
				string text = "2.0";
				EngineDataModel engineDataModel = ExploreHostUtils.GetEngineDataModel(this.Context.PowerViewHandler, base.DatabaseID, text, new TranslationsBehavior?(TranslationsBehavior.Default));
				DataQuery query = executeSemanticQueryRequest.Queries.Single<DataQueryRequest>().Query;
				SemanticQueryDataShapeCommand semanticQueryDataShapeCommand = query.Commands[0].SemanticQueryDataShapeCommand;
				DataReductionConfiguration dataReductionConfiguration = ((query.Commands.Count > 1) ? DataReductionConfiguration.DefaultForCompositeDataQuery : null);
				using (FileStream fileStream = new FileStream(Path.GetTempFileName(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 10000, FileOptions.DeleteOnClose))
				{
					QueryBindingDescriptor queryBindingDescriptor = this.ExecuteQuery(engineDataModel, semanticQueryDataShapeCommand, dataReductionConfiguration, fileStream, CancellationToken.None);
					QueryCommand queryCommand = query.Commands[1];
					IScriptEditor scriptEditor = this.Context.ScriptEditors[queryCommand.ScriptVisualCommand.RenderingEngine];
					using (ExecuteSemanticQueryResultStreamReader dsrDataReader = this.GetDsrDataReader(semanticQueryDataShapeCommand, queryCommand.ScriptVisualCommand, fileStream, queryBindingDescriptor))
					{
						if (dsrDataReader.IsODataError)
						{
							throw new Exception("Failed to read input data for the script.");
						}
						string text2 = queryCommand.ScriptVisualCommand.Script.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
						scriptEditor.OpenEditor(text2, dsrDataReader);
					}
				}
			}
			catch (ScriptHandlerException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new ScriptHandlerDeferredException(ScriptHandlerDeferredException.ExceptionType.failedToMaterilaizeInputData, ex);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005878 File Offset: 0x00003A78
		private QueryBindingDescriptor ExecuteQuery(EngineDataModel engineDataModel, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig, FileStream dataShapeResultStream, CancellationToken cancelToken)
		{
			CapturingExecuteSemanticQueryResultWriter capturingExecuteSemanticQueryResultWriter = new CapturingExecuteSemanticQueryResultWriter(dataShapeResultStream, StreamFormat.DataShapeResult);
			DataShapeEngineHost.ExecuteSemanticQuery(this.Context.DataShapeEngine, command, engineDataModel, capturingExecuteSemanticQueryResultWriter, this.Context.PowerViewHandler.GetConnectionPool(base.DatabaseID), this.Context.PowerViewHandler.GetDataSourceInfo(base.DatabaseID), dataReductionConfig, this.Context.PowerViewHandler.GetConnectionUserImpersonator(base.DatabaseID), this.Context.PowerViewHandler.CreateTelemetryServiceForRequest(base.DatabaseID), this.Context.PowerViewHandler.GetDSEQueryExecutionOptions(base.DatabaseID), 0, cancelToken, this.Context.FeatureSwitchProvider, this.Context.AnalyticsFeatureSwitchProvider, this.Context.FeatureSwitches.MsolapTracingEnabled, true, this.asQueryLimits);
			return capturingExecuteSemanticQueryResultWriter.Descriptor;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005946 File Offset: 0x00003B46
		private ExecuteSemanticQueryResultStreamReader GetDsrDataReader(SemanticQueryDataShapeCommand command, ScriptVisualCommand scriptVisualCommand, FileStream dataShapeResultStream, QueryBindingDescriptor bindingDescriptor)
		{
			IEnumerable<Tuple<string, string>> enumerable = ExecuteSemanticQueryResultStreamReader.CreatePrimarySelectsMap(bindingDescriptor, scriptVisualCommand.ScriptInput, command);
			dataShapeResultStream.Position = 0L;
			long length = dataShapeResultStream.Length;
			return new ExecuteSemanticQueryResultStreamReader(enumerable, bindingDescriptor, dataShapeResultStream);
		}

		// Token: 0x040000A3 RID: 163
		private readonly string jsonRequest;

		// Token: 0x040000A4 RID: 164
		private readonly ASQueryLimits asQueryLimits;
	}
}
