using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x0200011C RID: 284
	internal static class DefinitionGenerationUtils
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x000291FC File Offset: 0x000273FC
		internal static ExpressionNode GetExpressionNode(ExpressionId? exprId, ExpressionTable primaryExpressionTable, ExpressionTable fallbackExpressionTable, bool canUseFallbackExpressionTable, out ExpressionTable usedExpressionTable)
		{
			usedExpressionTable = primaryExpressionTable;
			ExpressionNode expressionNode;
			if (!canUseFallbackExpressionTable)
			{
				expressionNode = usedExpressionTable.GetNode(exprId.Value);
			}
			else
			{
				expressionNode = usedExpressionTable.GetNodeOrDefault(exprId.Value);
				if (expressionNode == null)
				{
					expressionNode = fallbackExpressionTable.GetNode(exprId.Value);
					usedExpressionTable = fallbackExpressionTable;
				}
			}
			return expressionNode;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00029248 File Offset: 0x00027448
		internal static Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection ToDsdSortDirection(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection.Ascending)
			{
				return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Ascending;
			}
			if (sortDirection != Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection.Descending)
			{
				Contract.RetailFail("QDM value type " + sortDirection.ToString() + " is not supported.");
				throw new InvalidOperationException();
			}
			return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Descending;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00029280 File Offset: 0x00027480
		internal static IList<ItemSourceLocation> BuildQuerySourceMap(IReadOnlyList<QueryItemSourceLocation> qdmSourceMap)
		{
			if (qdmSourceMap.IsNullOrEmpty<QueryItemSourceLocation>())
			{
				return null;
			}
			List<ItemSourceLocation> list = new List<ItemSourceLocation>(qdmSourceMap.Count);
			for (int i = 0; i < qdmSourceMap.Count; i++)
			{
				QueryItemSourceLocation queryItemSourceLocation = qdmSourceMap[i];
				IContainsTelemetryMarkup itemSource = queryItemSourceLocation.ItemSource;
				list.Add(new ItemSourceLocation
				{
					WrapperLineStart = queryItemSourceLocation.WrapperLineStart,
					SourceLineStart = queryItemSourceLocation.SourceLineStart,
					SourceLineEnd = queryItemSourceLocation.SourceLineEnd,
					WrapperLineEnd = queryItemSourceLocation.WrapperLineEnd,
					SourceIndent = queryItemSourceLocation.SourceIndent,
					QuerySourceName = itemSource.ToCustomerContentString(),
					SourceType = DefinitionGenerationUtils.ToDSDItemSourceType(queryItemSourceLocation.SourceType)
				});
			}
			return list;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00029328 File Offset: 0x00027528
		internal static IList<QueryParameter> BuildQueryParameters(IReadOnlyList<QueryParameterDeclaration> queryParameterDeclarations, GeneratedQueryParameterMap queryParameterMap)
		{
			if (queryParameterDeclarations.IsNullOrEmpty<QueryParameterDeclaration>() || queryParameterMap.Count == 0)
			{
				return null;
			}
			List<QueryParameter> list = new List<QueryParameter>(queryParameterMap.Count);
			for (int i = 0; i < queryParameterDeclarations.Count; i++)
			{
				string name = queryParameterDeclarations[i].Name;
				QueryParameterReferenceExpression queryParameterReferenceExpression;
				if (queryParameterMap.TryGetParameterReference(name, out queryParameterReferenceExpression))
				{
					list.Add(new QueryParameter
					{
						Name = name,
						QueryName = queryParameterReferenceExpression.Name
					});
				}
			}
			return list;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002939C File Offset: 0x0002759C
		internal static void HandleCommandTreeTranslationException(CommandTreeTranslationException e, TranslationErrorContext errorContext, string dataSetId)
		{
			TranslationMessage treeTranslationExceptionMessage = DefinitionGenerationUtils.GetTreeTranslationExceptionMessage(e, dataSetId);
			if (treeTranslationExceptionMessage != null)
			{
				errorContext.Register(treeTranslationExceptionMessage);
				throw new DefinitionGenerationException("Query translation failed.");
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000293C6 File Offset: 0x000275C6
		internal static EngineMessageBase GetTreeTranslationExceptionBaseMessage(CommandTreeTranslationException e, string dataSetId)
		{
			return DefinitionGenerationUtils.GetTreeTranslationExceptionMessage(e, dataSetId);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000293D0 File Offset: 0x000275D0
		private static TranslationMessage GetTreeTranslationExceptionMessage(CommandTreeTranslationException e, string dataSetId)
		{
			TranslationMessage translationMessage;
			if (!QueryGenerationUtils.TryGetTreeTranslationExceptionMessage(e, dataSetId, out translationMessage) && e.ErrorCode == CommandTreeTranslationErrorCode.InvalidDaxExternalContent)
			{
				return DefinitionGenerationUtils.CreateMessageForInvalidDaxExternalContent((DaxInvalidExternalContentException)e, dataSetId);
			}
			return translationMessage;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000293FF File Offset: 0x000275FF
		private static Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ItemSourceType ToDSDItemSourceType(Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.ItemSourceType sourceType)
		{
			if (sourceType == Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.ItemSourceType.QueryExtensionMeasure)
			{
				return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ItemSourceType.QueryExtensionMeasure;
			}
			if (sourceType != Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.ItemSourceType.QueryExtensionColumn)
			{
				throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected ItemSourceType '{0}'", new object[] { sourceType }));
			}
			return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ItemSourceType.QueryExtensionColumn;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002942C File Offset: 0x0002762C
		private static TranslationMessage CreateMessageForInvalidDaxExternalContent(DaxInvalidExternalContentException e, string dataSetId)
		{
			ScrubbedEntityPropertyReference itemReference = e.ItemReference;
			ObjectType objectType;
			Identifier identifier;
			string text;
			TranslationMessagePhrase identifiersForInvalidDaxExternalContent = DefinitionGenerationUtils.GetIdentifiersForInvalidDaxExternalContent(e, dataSetId, out objectType, out identifier, out text);
			switch (e.ContentErrorCode)
			{
			case DaxInvalidExternalContentErrorCode.UnclosedBracketIdentifier:
				return TranslationMessages.InvalidExtensionDax_UnclosedBracketIdentifier(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			case DaxInvalidExternalContentErrorCode.UnclosedMultiLineComment:
				return TranslationMessages.InvalidExtensionDax_UnclosedMultiLineComment(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			case DaxInvalidExternalContentErrorCode.UnclosedParenthesis:
				return TranslationMessages.InvalidExtensionDax_UnclosedParenthesis(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			case DaxInvalidExternalContentErrorCode.UnclosedQuoteIdentifier:
				return TranslationMessages.InvalidExtensionDax_UnclosedQuoteIdentifier(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			case DaxInvalidExternalContentErrorCode.UnclosedStringLiteral:
				return TranslationMessages.InvalidExtensionDax_UnclosedStringLiteral(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			case DaxInvalidExternalContentErrorCode.UnexpectedCloseParenthesis:
				return TranslationMessages.InvalidExtensionDax_UnexpectedCloseParenthesis(EngineMessageSeverity.Error, objectType, identifier, text, identifiersForInvalidDaxExternalContent, e.ErrorLine, e.ErrorPosition, itemReference);
			default:
				Contract.RetailFail("Unexpected DaxInvalidExternalContentErrorCode '{0}'", e.ContentErrorCode);
				throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected DaxInvalidExternalContentErrorCode '{0}'", new object[] { e.ContentErrorCode }));
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002954C File Offset: 0x0002774C
		private static TranslationMessagePhrase GetIdentifiersForInvalidDaxExternalContent(DaxInvalidExternalContentException e, string dataSetId, out ObjectType objectType, out Identifier objectId, out string propertyName)
		{
			switch (e.ContentType)
			{
			case DaxInvalidExternalContentType.Inline:
				objectType = ObjectType.DataSet;
				objectId = dataSetId;
				propertyName = "CommandText";
				return TranslationMessagePhrases.DaxExternalContent_Inline();
			case DaxInvalidExternalContentType.MeasureDeclaration:
				objectType = ObjectType.ExtensionMeasure;
				objectId = e.ItemReference.ToCustomerContentString();
				propertyName = "Expression";
				return TranslationMessagePhrases.DaxExternalContent_ExtensionMeasure(e.ItemReference);
			case DaxInvalidExternalContentType.FieldDeclaration:
				objectType = ObjectType.ExtensionColumn;
				objectId = e.ItemReference.ToCustomerContentString();
				propertyName = "Expression";
				return TranslationMessagePhrases.DaxExternalContent_ExtensionColumn(e.ItemReference);
			default:
				Contract.RetailFail("Unexpected DaxInvalidExternalContentType '{0}'", e.ContentType);
				throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected DaxInvalidExternalContentType '{0}'", new object[] { e.ContentType }));
			}
		}
	}
}
