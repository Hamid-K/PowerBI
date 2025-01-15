using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010F RID: 271
	internal static class ModelParametersGenerator
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00023764 File Offset: 0x00021964
		public static void BuildModelParameters(DsqExpressionGenerator dsqExpressionGenerator, ParameterMappings parameterMappings, DataShapeGenerationErrorContext errorContext, DataShapeBuilder dataShapeBuilder)
		{
			if (parameterMappings != null && parameterMappings.Count > 0)
			{
				ModelParametersGenerator.AddParametersToDataShape(dsqExpressionGenerator, dataShapeBuilder, errorContext, parameterMappings);
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00023784 File Offset: 0x00021984
		private static void AddParametersToDataShape(DsqExpressionGenerator dsqExpressionGenerator, DataShapeBuilder dataShapeBuilder, DataShapeGenerationErrorContext errorContext, ParameterMappings parameterMappings)
		{
			foreach (ParameterMapping parameterMapping in parameterMappings)
			{
				if (!parameterMapping.IsListType && parameterMapping.Values.Count > 1)
				{
					errorContext.Register(DataShapeGenerationMessages.SingleValueParameterWithMultipleValues(EngineMessageSeverity.Error));
					break;
				}
				if (parameterMapping.Values.Count == 0)
				{
					errorContext.Register(DataShapeGenerationMessages.ParameterMappingFilterConflict(EngineMessageSeverity.Error, new IContainsTelemetryMarkup[]
					{
						new ScrubbedString(parameterMapping.ParameterName)
					}));
					break;
				}
				List<Expression> list = new List<Expression>(parameterMapping.Values.Count);
				ExpressionContext expressionContext = new ExpressionContext(dataShapeBuilder.Id.Value, SemanticQueryObjectType.ModelParameter, parameterMapping.ParameterName);
				foreach (ResolvedQueryLiteralExpression resolvedQueryLiteralExpression in parameterMapping.Values)
				{
					GeneratedDsqExpression generatedDsqExpression;
					if (!ResolvedQueryExpressionValidator.Validate(resolvedQueryLiteralExpression, errorContext, AllowedExpressionContent.WhereExpression, expressionContext) || !dsqExpressionGenerator.TryGenerate(resolvedQueryLiteralExpression, out generatedDsqExpression))
					{
						errorContext.Register(DataShapeGenerationMessages.CouldNotConvertLiteral(EngineMessageSeverity.Error));
					}
					else
					{
						list.Add(generatedDsqExpression.Expression);
					}
				}
				dataShapeBuilder.WithModelParameter(parameterMapping.ParameterName, list, parameterMapping.IsListType);
			}
		}
	}
}
