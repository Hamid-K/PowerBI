using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.Errors;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.InfoNav.Utils;

namespace Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery
{
	// Token: 0x02000019 RID: 25
	internal static class TranslateQueryCommandProcessor
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004C90 File Offset: 0x00002E90
		internal static TranslateQueryCommandProcessorResult Process(TranslateQueryCommand command, SemanticQueryTranslatorContext context, TranslateSemanticQueryConfigKind config, bool enableRemoteErrors)
		{
			return context.TelemetryService.RunInActivity<TranslateQueryCommandProcessorResult>(ActivityKind.TranslateSemanticQuery, () => TranslateQueryCommandProcessor.ProcessImpl(command, context, config, enableRemoteErrors));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004CE0 File Offset: 0x00002EE0
		private static TranslateQueryCommandProcessorResult ProcessImpl(TranslateQueryCommand command, SemanticQueryTranslatorContext context, TranslateSemanticQueryConfigKind config, bool enableRemoteErrors)
		{
			TranslateSemanticQueryTelemetry translateSemanticQueryTelemetry = new TranslateSemanticQueryTelemetry();
			translateSemanticQueryTelemetry.QueryId = context.QueryId.ToString(CultureInfo.InvariantCulture);
			TranslateQueryCommandProcessorResult translateQueryCommandProcessorResult;
			try
			{
				bool flag = context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
				translateSemanticQueryTelemetry.Model = context.Model.GetModelTelemetry(flag ? context.Schema : null);
				SemanticQueryTranslationUtils.TraceCommand(context, command);
				if (!TranslateQueryCommandValidator.TryValidateCommand(command, context))
				{
					translateQueryCommandProcessorResult = TranslateQueryCommandProcessor.CreateErrorResult(context, enableRemoteErrors, translateSemanticQueryTelemetry);
				}
				else
				{
					QueryDefinitionDaxTableGeneratorResult queryDefinitionDaxTableGeneratorResult;
					if (command.Binding == null)
					{
						if (!SemanticQueryTranslationUtils.TryUpgradeQuery(command.Query, context))
						{
							return TranslateQueryCommandProcessor.CreateErrorResult(context, enableRemoteErrors, translateSemanticQueryTelemetry);
						}
						ResolvedQueryDefinition resolvedQueryDefinition;
						if (!SemanticQueryTranslationUtils.TryResolveQuery(command.Query, context, out resolvedQueryDefinition))
						{
							return TranslateQueryCommandProcessor.CreateErrorResult(context, enableRemoteErrors, translateSemanticQueryTelemetry);
						}
						if (context.ErrorContext.HasError)
						{
							return TranslateQueryCommandProcessor.CreateErrorResult(context, enableRemoteErrors, translateSemanticQueryTelemetry);
						}
						queryDefinitionDaxTableGeneratorResult = QueryDefinitionDaxTableGenerator.Generate(resolvedQueryDefinition, context, TranslateSemanticQueryConfigHandler.GetGenerationOptions(config), translateSemanticQueryTelemetry);
						TranslateQueryCommandProcessor.TraceGeneratedQuery(context, queryDefinitionDaxTableGeneratorResult);
					}
					else
					{
						if (context.ErrorContext.HasError)
						{
							return TranslateQueryCommandProcessor.CreateErrorResult(context, enableRemoteErrors, translateSemanticQueryTelemetry);
						}
						queryDefinitionDaxTableGeneratorResult = QueryDefinitionDaxTableGenerator.Generate(new SemanticQueryDataShapeCommand
						{
							Query = command.Query,
							Binding = command.Binding
						}, context, TranslateSemanticQueryConfigHandler.GetGenerationOptions(config), translateSemanticQueryTelemetry);
					}
					translateQueryCommandProcessorResult = TranslateQueryCommandProcessor.CreateSuccessResult(queryDefinitionDaxTableGeneratorResult);
				}
			}
			catch (DataShapeEngineException ex)
			{
				translateQueryCommandProcessorResult = TranslateQueryCommandProcessor.CreateErrorResult(ex, context, enableRemoteErrors, translateSemanticQueryTelemetry);
			}
			finally
			{
				translateSemanticQueryTelemetry.SetCancelStatus(context.CancellationToken);
				translateSemanticQueryTelemetry.Write(context.TelemetryService, context.Tracer);
			}
			return translateQueryCommandProcessorResult;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004E84 File Offset: 0x00003084
		private static void TraceGeneratedQuery(SemanticQueryTranslatorContext context, QueryDefinitionDaxTableGeneratorResult daxTableGeneratorResult)
		{
			string daxQuery = daxTableGeneratorResult.DaxQuery;
			context.Tracer.SanitizedTrace(TraceLevel.Info, "Generated Dax: (length: {0}) {1}", (daxQuery != null) ? new int?(daxQuery.Length) : null, daxQuery.MarkAsCustomerContent());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004ECD File Offset: 0x000030CD
		private static TranslateQueryCommandProcessorResult CreateErrorResult(SemanticQueryTranslatorContext context, bool enableRemoteErrors, TranslateSemanticQueryTelemetry telemetryInfo)
		{
			Contract.RetailAssert(context.ErrorContext.HasError, "Cannot create an error result without an error.");
			return TranslateQueryCommandProcessor.CreateErrorResult(SemanticQueryTranslationException.Create(context.ErrorContext), context, enableRemoteErrors, telemetryInfo);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004EF8 File Offset: 0x000030F8
		private static TranslateQueryCommandProcessorResult CreateErrorResult(DataShapeEngineException exception, SemanticQueryTranslatorContext context, bool enableRemoteErrors, TranslateSemanticQueryTelemetry telemetryInfo)
		{
			telemetryInfo.RegisterException(exception);
			ODataError odataError = ExceptionToODataErrorConverter.Convert(exception, enableRemoteErrors, context.FeatureSwitchProvider);
			return TranslateQueryCommandProcessorResult.ForError(new TranslatedQuery
			{
				Version = TranslatedQueryVersions.Version0,
				Error = odataError
			}, exception.ToErrorInfo());
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004F41 File Offset: 0x00003141
		private static TranslateQueryCommandProcessorResult CreateSuccessResult(QueryDefinitionDaxTableGeneratorResult tableGeneratorResult)
		{
			return TranslateQueryCommandProcessorResult.ForSuccess(new TranslatedQuery
			{
				Version = TranslatedQueryVersions.Version0,
				Query = tableGeneratorResult.DaxQuery,
				Schema = tableGeneratorResult.QuerySchema,
				Warnings = TranslateQueryCommandProcessor.CreateMessages(tableGeneratorResult.Messages)
			});
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004F84 File Offset: 0x00003184
		private static IList<TranslateQueryMessage> CreateMessages(IReadOnlyList<EngineMessageBase> engineMessages)
		{
			if (engineMessages.IsNullOrEmpty<EngineMessageBase>())
			{
				return null;
			}
			List<TranslateQueryMessage> list = new List<TranslateQueryMessage>(engineMessages.Count);
			foreach (EngineMessageBase engineMessageBase in engineMessages)
			{
				TranslateQueryMessage translateQueryMessage = new TranslateQueryMessage
				{
					Code = engineMessageBase.GetErrorCodeString(),
					Message = engineMessageBase.Message.RemovePrivateAndInternalMarkup()
				};
				list.Add(translateQueryMessage);
			}
			return list;
		}
	}
}
