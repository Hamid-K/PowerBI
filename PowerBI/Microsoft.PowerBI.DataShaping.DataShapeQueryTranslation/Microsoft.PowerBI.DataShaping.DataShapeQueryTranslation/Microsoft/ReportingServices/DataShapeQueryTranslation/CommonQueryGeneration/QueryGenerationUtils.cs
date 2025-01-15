using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x02000119 RID: 281
	internal static class QueryGenerationUtils
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00028F88 File Offset: 0x00027188
		internal static bool TryHandleCommandTreeTranslationException(CommandTreeTranslationException e, TranslationErrorContext errorContext, string dataSetId)
		{
			TranslationMessage translationMessage;
			if (QueryGenerationUtils.TryGetTreeTranslationExceptionMessage(e, dataSetId, out translationMessage))
			{
				errorContext.Register(translationMessage);
				return true;
			}
			return false;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00028FAC File Offset: 0x000271AC
		internal static bool TryGetTreeTranslationExceptionMessage(CommandTreeTranslationException e, string dataSetId, out TranslationMessage result)
		{
			TranslationMessage translationMessage;
			switch (e.ErrorCode)
			{
			case CommandTreeTranslationErrorCode.UnsupportedStringMinMaxColumn:
				translationMessage = TranslationMessages.UnsupportedStringMinMaxColumn(EngineMessageSeverity.Error, ObjectType.DataSet, dataSetId, "CommandText");
				goto IL_00A3;
			case CommandTreeTranslationErrorCode.UnsupportedStringMinMaxExpression:
				translationMessage = TranslationMessages.UnsupportedStringMinMaxExpression(EngineMessageSeverity.Error, ObjectType.DataSet, dataSetId, "CommandText");
				goto IL_00A3;
			case CommandTreeTranslationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilter:
				translationMessage = TranslationMessages.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter(EngineMessageSeverity.Error, ObjectType.DataSet, dataSetId, "CommandText", 30000);
				goto IL_00A3;
			case CommandTreeTranslationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilterTreeRewrite:
				translationMessage = TranslationMessages.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite(EngineMessageSeverity.Error, ObjectType.DataSet, dataSetId, "CommandText", 500);
				goto IL_00A3;
			case CommandTreeTranslationErrorCode.InvalidInFilterWithDuplicateColumns:
				translationMessage = TranslationMessages.InvalidInFilterWithDuplicateColumns(EngineMessageSeverity.Error, ObjectType.DataSet, dataSetId, "CommandText");
				goto IL_00A3;
			}
			translationMessage = null;
			IL_00A3:
			result = translationMessage;
			return result != null;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00029064 File Offset: 0x00027264
		internal static ConceptualPrimitiveResultType GetConceptualResultType(this ExtensionProperty extensionProperty)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = ConceptualPrimitiveResultType.Variant;
			if (extensionProperty.DataType.IsValid)
			{
				conceptualPrimitiveResultType = ConceptualPrimitiveResultType.FromPrimitive(extensionProperty.DataType.Value);
			}
			return conceptualPrimitiveResultType;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00029098 File Offset: 0x00027298
		internal static QueryMParameterDeclarationExpression ConvertToQueryMParameterDeclarationExpression(ModelParameter modelParameter, TranslationErrorContext translationErrorContext, QueryExpressionGeneratorBase queryExpressionGeneratorBase)
		{
			if (!modelParameter.IsListType)
			{
				ExpressionContext expressionContext = new ExpressionContext(translationErrorContext, ObjectType.ModelParameter, null, "Values");
				QueryExpression queryExpression = modelParameter.Values[0].ToQueryExpression(queryExpressionGeneratorBase, expressionContext);
				return new QueryMParameterDeclarationExpression(modelParameter.Name, queryExpression);
			}
			DataTableBuilder dataTableBuilder = new DataTableBuilder(1);
			dataTableBuilder.AddColumn(ConceptualPrimitiveResultType.Variant);
			foreach (Expression expression in modelParameter.Values)
			{
				ExpressionContext expressionContext2 = new ExpressionContext(translationErrorContext, ObjectType.ModelParameter, null, "Values");
				QueryExpression queryExpression2 = expression.ToQueryExpression(queryExpressionGeneratorBase, expressionContext2);
				dataTableBuilder.AddRow(new List<QueryExpression>(1) { queryExpression2 });
			}
			return new QueryMParameterDeclarationExpression(modelParameter.Name, dataTableBuilder.ToQueryTable().Expression);
		}
	}
}
