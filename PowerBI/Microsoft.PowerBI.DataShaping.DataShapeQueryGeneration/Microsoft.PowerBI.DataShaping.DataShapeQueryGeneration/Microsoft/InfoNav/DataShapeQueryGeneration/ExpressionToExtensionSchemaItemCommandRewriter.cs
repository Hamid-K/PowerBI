using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000094 RID: 148
	internal sealed class ExpressionToExtensionSchemaItemCommandRewriter
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00014EF0 File Offset: 0x000130F0
		internal static bool TryRewrite(DataShapeGenerationInternalContext internalContext, ResolvedSemanticQueryDataShapeCommand command, DataShapeGenerationErrorContext errorContext, IExpressionToExtensionSchemaItemQueryRewriter expressionToExtensionSchemaItemQueryRewriter, out ResolvedSemanticQueryDataShapeCommand newCommand, out DataShapeGenerationInternalContext newInternalContext, out SparklineDataStatistics sparklineStatistics)
		{
			newCommand = command;
			newInternalContext = internalContext;
			sparklineStatistics = SparklineDataStatistics.Empty;
			IFederatedConceptualSchema federatedConceptualSchema = internalContext.FederatedConceptualSchema;
			DataShapeGenerationErrorContext errorContext2 = internalContext.ErrorContext;
			QueryExtensionSchemaContext extension = command.Extension;
			NamingContext namingContext = ((extension != null) ? extension.NamingContext : null);
			QueryExtensionSchemaContext extension2 = command.Extension;
			QuerySchemaExtender querySchemaExtender = QuerySchemaExtender.Create(federatedConceptualSchema, errorContext2, namingContext, (extension2 != null) ? extension2.ExtensionSchemas : null);
			if (!internalContext.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.SparklineData))
			{
				return true;
			}
			ResolvedQueryDefinition resolvedQueryDefinition;
			IReadOnlyList<EngineMessageBase> readOnlyList;
			if (!expressionToExtensionSchemaItemQueryRewriter.TryRewrite(command.QueryDataShape.Query, querySchemaExtender, out resolvedQueryDefinition, out readOnlyList, out sparklineStatistics))
			{
				ExpressionToExtensionSchemaItemCommandRewriter.RegisterErrorMessages(errorContext, readOnlyList);
				return false;
			}
			internalContext.Telemetry.NumSparklineDataExpression = sparklineStatistics.SparklineCount;
			internalContext.Telemetry.NumSparklineDataPoints = sparklineStatistics.SparklinesTotalPointsCount;
			if (resolvedQueryDefinition != command.QueryDataShape.Query)
			{
				newCommand = command.Clone(command.QueryDataShape.Clone(resolvedQueryDefinition), null);
				global::System.ValueTuple<QueryExtensionSchemaContext, IFederatedConceptualSchema> valueTuple = querySchemaExtender.Extend(command.Extension, internalContext.FederatedConceptualSchema);
				if (valueTuple.Item1 != command.Extension)
				{
					newCommand = newCommand.Clone(null, valueTuple.Item1);
				}
				if (valueTuple.Item2 != internalContext.FederatedConceptualSchema)
				{
					newInternalContext = internalContext.Clone(valueTuple.Item2);
				}
			}
			return true;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00015014 File Offset: 0x00013214
		private static void RegisterErrorMessages(DataShapeGenerationErrorContext errorContext, IReadOnlyList<EngineMessageBase> errorMessages)
		{
			if (errorMessages.IsEmpty<EngineMessageBase>())
			{
				errorContext.Register(DataShapeGenerationMessages.ExpressionToExtensionSchemaItemRewriterFailed(EngineMessageSeverity.Error));
			}
			foreach (EngineMessageBase engineMessageBase in errorMessages)
			{
				errorContext.Register(DataShapeGenerationMessages.CouldNotRewriteExpressionToExtensionSchemaItemCommand(engineMessageBase.Message, engineMessageBase.Severity, engineMessageBase.Source, engineMessageBase.AffectedItems));
			}
		}
	}
}
