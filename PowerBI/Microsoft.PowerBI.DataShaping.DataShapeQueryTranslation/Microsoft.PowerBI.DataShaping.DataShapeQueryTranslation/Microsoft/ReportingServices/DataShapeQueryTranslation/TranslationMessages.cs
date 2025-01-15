using System;
using System.Globalization;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005F RID: 95
	internal static class TranslationMessages
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public static TranslationMessage AggregateWithMultipleInputScopes(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier scopeId1, Identifier scopeId2)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an aggregate function that refers to an item in scope '{3}' and an item in scope '{4}'. Aggregate functions must only refer to items from a single scope.", TranslationErrorCode.AggregateWithMultipleInputScopes, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { scopeId1, scopeId2 });
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		public static TranslationMessage AllMandatoryConstraintsInDynamicLimits(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("Cannot have {2} on {0} '{1}' with all mandatory constraints.", TranslationErrorCode.AllMandatoryConstraintsInDynamicLimits, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		public static TranslationMessage ComplexSlicerNotAllowedWithMeasures(EngineMessageSeverity severity, Identifier objectId)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.ComplexSlicerNotAllowedWithMeasures), TranslationErrorCode.ComplexSlicerNotAllowedWithMeasures, severity, ErrorSourceCategory.UserInput, ObjectType.DataShape, objectId, null, Array.Empty<object>());
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DDED File Offset: 0x0000BFED
		public static TranslationMessage ComplexSlicerNotAllowed(EngineMessageSeverity severity, Identifier objectId)
		{
			return TranslationMessages.CreateMessage("The query contains an unsupported filter operator involving tuples of values for columns from different tables. This filter is not allowed with this model. Try changing the filter to only use columns from a single table.", TranslationErrorCode.ComplexSlicerNotAllowed, severity, ErrorSourceCategory.UserInput, ObjectType.FilterCondition, objectId, null, Array.Empty<object>());
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public static TranslationMessage CompoundFilterOperatorNotSupportedWithRemove(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier compoundFilterConditionId, Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator compoundFilterOperator)
		{
			return TranslationMessages.CreateMessage("The compound filter operator '{4}' on filter condition '{3}' is not supported when removing filter conditions.", TranslationErrorCode.CompoundFilterOperatorNotSupportedWithRemove, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { compoundFilterConditionId, compoundFilterOperator });
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000DE44 File Offset: 0x0000C044
		public static TranslationMessage ComplexHighlightsNotAllowed(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier calculationId)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.ComplexHighlightsNotAllowed), TranslationErrorCode.ComplexHighlightsNotAllowed, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { calculationId });
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000DE74 File Offset: 0x0000C074
		public static TranslationMessage ConflictingQueryPatternRequirements(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string regularPatternFeatures, string batchPatternFeatures)
		{
			return TranslationMessages.CreateMessage("The DataShapeQuery cannot be translated because it contains features that require conflicting query patterns. Regular pattern only features: '{3}'. Super DAX pattern only features: '{4}'.", TranslationErrorCode.ConflictingQueryPatternRequirements, severity, ErrorSourceCategory.UnsupportedFeature, objectType, objectId, propertyName, new object[] { regularPatternFeatures, batchPatternFeatures });
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000DEA5 File Offset: 0x0000C0A5
		public static TranslationMessage ContextFilterDataShapeIntersectionMustBeEmpty(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {2} in the {0} '{1}' are expected to be empty.", TranslationErrorCode.ContextFilterDataShapeIntersectionMustBeEmpty, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public static TranslationMessage ContextFilterDataShapeCannotBeMerged(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier dataShapeId, Identifier otherDataShapeId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' in {2} '{3}' and the {0} in its filter context data shape ('{4}') do not have matching members. Hierarchies in '{1}' and '{4}' cannot be merged.", TranslationErrorCode.ContextFilterDataShapeCannotBeMerged, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { dataShapeId, otherDataShapeId });
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public static TranslationMessage ContextFilterDataShapeDoesNotAllowLimits(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier dataSetId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is not allowed in {2} '{3}'. Filter context data shapes don't support limits.", TranslationErrorCode.ContextFilterDataShapeDoesNotAllowLimits, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { dataSetId });
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000DF24 File Offset: 0x0000C124
		public static TranslationMessage ContextFilterDataShapeDoesNotAllowNestedDataShapes(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier dataSetId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is not allowed in {2} '{3}'. Filter context data shapes don't allow nested data shapes.", TranslationErrorCode.ContextFilterDataShapeDoesNotAllowNestedDataShapes, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { dataSetId });
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DF51 File Offset: 0x0000C151
		public static TranslationMessage ContextFilterDataShapeMustHaveHierarchyMembers(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' does not have hierarchy members. Filter context data shapes must have at least one non-empty hierarchy.", TranslationErrorCode.ContextFilterDataShapeMustHaveHierarchyMembers, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000DF6D File Offset: 0x0000C16D
		public static TranslationMessage ContextFilterDoesNotAllowScopeFilterPeer(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} with {2} '{1}' is a scope filter which is not allowed if there is a context filter in the same data shape. The scope filter must be moved to the filter context data shape.", TranslationErrorCode.ContextFilterDoesNotAllowScopeFilterPeer, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000DF89 File Offset: 0x0000C189
		public static TranslationMessage ContextFilterOnlyAllowsScopeFilterInContextDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} for {2} '{1}' is not scope filter. ContextFilterCondition only allows scope filter for context data shape.", TranslationErrorCode.ContextFilterOnlyAllowsScopeFilterInContextDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		public static TranslationMessage DataShapeWithContextFilterMustHaveHierarchyMembers(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' does not have hierarchy members. Context filters can only be applied to data shapes that have at least one dynamic hierarchy member.", TranslationErrorCode.DataShapeWithContextFilterMustHaveHierarchyMembers, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public static TranslationMessage ModelUnavailable(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string details)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.ModelUnavailable), TranslationErrorCode.ModelUnavailable, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, new object[] { details });
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DFF3 File Offset: 0x0000C1F3
		public static TranslationMessage DetailWithoutGroup(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' references a field outside of a group.", TranslationErrorCode.DetailWithoutGroup, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000E00F File Offset: 0x0000C20F
		public static TranslationMessage DuplicateId(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' has the same Id value as another object. Id values must be unique.", TranslationErrorCode.DuplicateId, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E02C File Offset: 0x0000C22C
		public static TranslationMessage ExpressionLexer_DecimalWithExponent(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. An decimal value with an exponent was found at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_DecimalWithExponent, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E064 File Offset: 0x0000C264
		public static TranslationMessage ExpressionLexer_DigitExpected(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. A digit was expected at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_DigitExpected, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000E09C File Offset: 0x0000C29C
		public static TranslationMessage ExpressionLexer_Int64WithExponentOrDot(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. An Int64 value with an exponent or decimal point was found at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_Int64WithExponentOrDot, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public static TranslationMessage ExpressionLexer_InvalidCharacterDetected(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. An invalid character was found at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_InvalidCharacterDetected, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000E10C File Offset: 0x0000C30C
		public static TranslationMessage ExpressionLexer_NumericValueWithoutSuffix(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. A numeric value without identifying suffix character was found at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_NumericValueWithoutSuffix, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000E144 File Offset: 0x0000C344
		public static TranslationMessage ExpressionLexer_UnterminatedLiteral(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. An unterminated literal was found at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_UnterminatedLiteral, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E17C File Offset: 0x0000C37C
		public static TranslationMessage ExpressionLexer_UnterminatedStringLiteral(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int position, IContainsTelemetryMarkup expression)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' specifies an invalid expression. A digit was expected at position {3} in '{4}'.", TranslationErrorCode.ExpressionLexer_UnterminatedStringLiteral, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { position, expression });
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public static TranslationMessage ExpressionParser_CloseParenExpected(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Expected a ')' token but found a {4} token with text '{3}'.", TranslationErrorCode.ExpressionParser_CloseParenExpected, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static TranslationMessage ExpressionParser_InvalidModelReferenceSyntax(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Expected a token of kind '{5}' when parsing a model reference but found a token with text '{3}' and kind '{4}'.", TranslationErrorCode.ExpressionParser_InvalidModelReferenceSyntax, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E23C File Offset: 0x0000C43C
		public static TranslationMessage ExpressionParser_InvalidStructureReferenceSyntax(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Expected a token of kind '{5}' when parsing a structure reference but found a token with text '{3}' and kind '{4}'.", TranslationErrorCode.ExpressionParser_InvalidStructureReferenceSyntax, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E280 File Offset: 0x0000C480
		public static TranslationMessage ExpressionParser_InvalidTokenKind(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. The token with text '{3}' has an unsupported token kind '{4}'.", TranslationErrorCode.ExpressionParser_InvalidTokenKind, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind });
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public static TranslationMessage ExpressionParser_NotAllTokensConsumed(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. After parsing an expression, not all tokens have been consumed. First non-consumed token has text '{3}' and is of kind '{4}'.", TranslationErrorCode.ExpressionParser_NotAllTokensConsumed, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		public static TranslationMessage ExpressionParser_OpenParenExpected(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Expected a '(' token but found a {4} token with text '{3}'.", TranslationErrorCode.ExpressionParser_OpenParenExpected, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000E340 File Offset: 0x0000C540
		public static TranslationMessage ExpressionParser_DaxTextExpected(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ExpressionNodeKind? exprNodeKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Expected 'DaxTextExpressionNode' but found '{3}'.", TranslationErrorCode.ExpressionParser_DaxTextExpected, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { exprNodeKind });
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000E374 File Offset: 0x0000C574
		public static TranslationMessage ExpressionParser_PrematureEndOfExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. Prematurely reached the end of the expression while parsing the token with text '{3}'.", TranslationErrorCode.ExpressionParser_PrematureEndOfExpression, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind });
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public static TranslationMessage ExpressionParser_UnexpectedTokenKind(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("Unexpected token kind '{4}' for token '{3}' found. Expected a token of kind '{5}'.", TranslationErrorCode.ExpressionParser_UnexpectedTokenKind, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public static TranslationMessage ExpressionParser_UnrecognizedLiteral(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' contains an invalid expression. The string '{3}' could not be parsed into a literal of type kind '{5}'.", TranslationErrorCode.ExpressionParser_UnrecognizedLiteral, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000E434 File Offset: 0x0000C634
		public static TranslationMessage ExpressionParser_UnsupportedUnaryOperator(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup tokenText, ExpressionTokenKind tokenKind, ExpressionTokenKind expectedTokenKind)
		{
			return TranslationMessages.CreateMessage("The unary operator of kind '{5}' for token '{3}' of kind '{4}' is not supported for that token kind.", TranslationErrorCode.ExpressionParser_UnsupportedUnaryOperator, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { tokenText, tokenKind, expectedTokenKind });
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E478 File Offset: 0x0000C678
		public static TranslationMessage ImageOrBinaryFieldReferenceNotAllowed(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup fieldName)
		{
			return TranslationMessages.CreateMessage("The expression for {2} on {0} contains a field reference to {3} '{1}' which is an Image or binary field. References to Image or binary fields are not supported for {2} on {0}.", TranslationErrorCode.ImageOrBinaryFieldReferenceNotAllowed, severity, ErrorSourceCategory.InputDoesNotMatchModel, objectType, objectId, propertyName, new object[] { fieldName });
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		public static TranslationMessage InconsistentFilterEmptyGroups(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' has inconsistent '{2}' behaviour with the parent {0}.", TranslationErrorCode.InconsistentFilterEmptyGroups, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public static TranslationMessage InconsistentShowItemsWithNoDataValue(EngineMessageSeverity severity, ObjectType object1Type, Identifier object1Id, string propertyName, ObjectType propertyOwnerType, ObjectType object2Type, Identifier object2Id)
		{
			return TranslationMessages.CreateMessage("The value of {3} property '{2}' on {0} '{1}' is inconsistent with {4} '{5}'. A group key must have the same ShowItemsWithNoData value as all ancestor group keys with the same expression.", TranslationErrorCode.InconsistentShowItemsWithNoDataValue, severity, ErrorSourceCategory.MalformedInternalInput, object1Type, object1Id, propertyName, new object[] { propertyOwnerType, object2Type, object2Id });
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000E505 File Offset: 0x0000C705
		public static TranslationMessage InconsistentSortDirectionForBinnedLineSampleSeries(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The secondary hierarchy containing {0} '{1}' has multiple sort keys with inconsistent sort direction. When using BinnedLineSample, series need to have consistent sort direction.", TranslationErrorCode.InconsistentSortDirectionForBinnedLineSampleSeries, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000E521 File Offset: 0x0000C721
		public static TranslationMessage InconsistentSortDirectionForSubtotal(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' contains multiple subtotal calculations with inconsistent positions relative to the detail data.", TranslationErrorCode.InconsistentSortDirectionForSubtotal, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000E53D File Offset: 0x0000C73D
		public static TranslationMessage InTableFilterNotSupportedForModel(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InTableFilterNotSupportedForModel), TranslationErrorCode.InTableFilterNotSupportedForModel, severity, ErrorSourceCategory.UnsupportedFeature, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000E55C File Offset: 0x0000C75C
		public static TranslationMessage InvalidApplyFilterConditionTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter with ApplyFilterCondition on {0} '{1}' references the {3} '{4}', which is an invalid target for this type of filter. A filter with a condition of type ApplyFilter condition must target a DataShape object.", TranslationErrorCode.InvalidApplyFilterConditionTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E593 File Offset: 0x0000C793
		public static TranslationMessage InvalidApplyFilterDataShapeReference(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("A filter with ApplyFilterCondition on {0} '{1}' contains an invalid DataShapeReference. DataShapeReference must refer to an independent, subquery DataShape.", TranslationErrorCode.InvalidApplyFilterDataShapeReference, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public static TranslationMessage InvalidBatchSubtotalAnnotation(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string error)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' has an invalid subtotal annotation: '{3}'.", TranslationErrorCode.InvalidBatchSubtotalAnnotation, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { error });
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E5DD File Offset: 0x0000C7DD
		public static TranslationMessage InvalidCalculationInSyncGroup(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' does not have equivalent calculations as the {2} reference. Calculations in a {0} must be equivalent to the calculations in its {2} reference.", TranslationErrorCode.InvalidCalculationInSyncGroup, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		public static TranslationMessage InvalidConflictingLimits(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} has a conflict with corresponding '{2}' on another {0} defined for the DataShape.", TranslationErrorCode.InvalidConflictingLimits, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E618 File Offset: 0x0000C818
		public static TranslationMessage InvalidContextFilterConditionTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter with ContextFilterCondition on {0} '{1}' references the {3} '{4}', which is an invalid target for this type of filter. A filter with a condition of type ContextFilter condition must target a DataShape object.", TranslationErrorCode.InvalidContextFilterConditionTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E64F File Offset: 0x0000C84F
		public static TranslationMessage InvalidContextOnlyDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' is a top level {0} and is not allowed to be {2}. The property {2} must not be specified on a top level {0} or must be set to false.", TranslationErrorCode.InvalidContextOnlyDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E66C File Offset: 0x0000C86C
		public static TranslationMessage InvalidContextOnlyDataMemberParentIsDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("Data member '{0}' is marked as context only, which is not allowed for data members whose parent is a data shape.", TranslationErrorCode.InvalidContextOnlyDataMemberParentIsDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { objectId });
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000E698 File Offset: 0x0000C898
		public static TranslationMessage InvalidContextOnlyDataMemberParentIsNotContextOnly(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("Data member '{0}' is marked as context only, but its parent data member is not, which is not allowed.", TranslationErrorCode.InvalidContextOnlyDataMemberParentIsNotContextOnly, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { objectId });
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		public static TranslationMessage InvalidContextOnlyFlagForFilterContextDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter with ContextFilterCondition on {0} '{1}' must have a context only data shape. The property {2} must be set to true for '{3}'.", TranslationErrorCode.InvalidContextOnlyFlagForFilterContextDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectId });
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000E6F1 File Offset: 0x0000C8F1
		public static TranslationMessage InvalidDataShapeNoOutputData(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' does not require any output data. Data shapes must contain at least one group or calculation used to output data.", TranslationErrorCode.InvalidDataShapeNoOutputData, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000E70D File Offset: 0x0000C90D
		public static TranslationMessage InvalidDataShapeStructure(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The structure of {0} '{1}' is invalid. DataIntersections are required if the DataShape has both a PrimaryHierarchy and a SecondaryHierarchy. DataIntersections must not be specified if the PrimaryHierarchy or SecondaryHierarchy is omitted.", TranslationErrorCode.InvalidDataShapeStructure, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000E72C File Offset: 0x0000C92C
		public static TranslationMessage InvalidDefaultFilterContextConditionTargetExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ExpressionNodeKind nodeKind)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' contains a {3} expression element which is not allowed. {2} expressions must refer to an Entity Column and must not use other expression features. ", TranslationErrorCode.InvalidDefaultFilterContextConditionTargetExpression, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { nodeKind });
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000E760 File Offset: 0x0000C960
		public static TranslationMessage InvalidDeepComplexSlicer(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int limit)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidDeepComplexSlicer), TranslationErrorCode.InvalidDeepComplexSlicer, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { limit });
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000E794 File Offset: 0x0000C994
		public static TranslationMessage InvalidDetailFieldReference(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup fieldName)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' refers to the field '{3}' which is not to one with any containing group key. Expressions may only refer to fields that have a single value when evaluated within all containing groups.", TranslationErrorCode.InvalidDetailFieldReference, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { fieldName });
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		public static TranslationMessage InvalidDetailTableExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ExpressionNodeKind nodeKind)
		{
			return TranslationMessages.CreateMessage("The detail table expression in property '{2}' on {0} '{1}' cannot be a '{3}'. Only aggregates and property references are allowed.", TranslationErrorCode.InvalidDetailTableExpression, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { nodeKind });
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public static TranslationMessage InvalidDynamicLimitBlockType(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string typeName)
		{
			return TranslationMessages.CreateMessage("The query has an unsupported DynamicLimitBlock of type {3}.", TranslationErrorCode.InvalidDynamicLimitBlockType, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { typeName });
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000E825 File Offset: 0x0000CA25
		public static TranslationMessage InvalidDynamicLimitsStructure(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The DynamicLimits mixes both structural and block based dynamic limits. When Blocks are specified the property '{2}' must not be specified.", TranslationErrorCode.InvalidDynamicLimitsStructure, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000E844 File Offset: 0x0000CA44
		public static TranslationMessage InvalidExistsFilter(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string detailedMessage)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' contains an ExistsFilterCondition with an error. {3}", TranslationErrorCode.InvalidExistsFilter, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { detailedMessage });
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000E874 File Offset: 0x0000CA74
		public static TranslationMessage InvalidExistsFilterExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ExpressionNodeKind actualNodeType)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' contains a {3} expression element which is not allowed. {2} expressions must refer to an EntitySet and must not use other expression features.", TranslationErrorCode.InvalidExistsFilterExpression, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { actualNodeType });
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		public static TranslationMessage InvalidExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase details)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' contains an error: '{3}'.", TranslationErrorCode.InvalidExpression, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { details });
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public static TranslationMessage InvalidExtensionDax_UnclosedBracketIdentifier(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken"), TranslationErrorCode.InvalidExtensionDax_UnclosedBracketIdentifier, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, "]", errorLine, errorPosition });
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000E93C File Offset: 0x0000CB3C
		public static TranslationMessage InvalidExtensionDax_UnclosedMultiLineComment(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken"), TranslationErrorCode.InvalidExtensionDax_UnclosedMultiLineComment, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, "*/", errorLine, errorPosition });
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public static TranslationMessage InvalidExtensionDax_UnclosedParenthesis(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken"), TranslationErrorCode.InvalidExtensionDax_UnclosedParenthesis, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, ")", errorLine, errorPosition });
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public static TranslationMessage InvalidExtensionDax_UnclosedQuoteIdentifier(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken"), TranslationErrorCode.InvalidExtensionDax_UnclosedQuoteIdentifier, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, "'", errorLine, errorPosition });
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public static TranslationMessage InvalidExtensionDax_UnclosedStringLiteral(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken"), TranslationErrorCode.InvalidExtensionDax_UnclosedStringLiteral, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, "\"", errorLine, errorPosition });
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public static TranslationMessage InvalidExtensionDax_UnexpectedCloseParenthesis(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, TranslationMessagePhrase itemDescription, int errorLine, int errorPosition, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage(DsqtStrings.Keys.GetString("InvalidExtensionDax_UnexpectedToken"), TranslationErrorCode.InvalidExtensionDax_UnexpectedCloseParenthesis, severity, ErrorSourceCategory.UserInput, new int?(errorLine), new int?(errorPosition), TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, new object[] { itemDescription, ")", errorLine, errorPosition });
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public static TranslationMessage InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int maxNumber)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter), TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { maxNumber });
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public static TranslationMessage InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int maxNumber)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite), TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { maxNumber });
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public static TranslationMessage InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int maxNumber)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite), TranslationErrorCode.InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { maxNumber });
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public static TranslationMessage InvalidInFilterWithDuplicateColumns(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidInFilterWithDuplicateColumns), TranslationErrorCode.InvalidInFilterWithDuplicateColumns, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000EBEA File Offset: 0x0000CDEA
		public static TranslationMessage InvalidInFilterValuesAndTable(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The query contains an In filter {1} that specifies both Values and Table. An In filter must specify either Values or Table.", TranslationErrorCode.InvalidInFilterValuesAndTable, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000EC06 File Offset: 0x0000CE06
		public static TranslationMessage InvalidInFilterTableWithoutIdentityComparison(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The query contains an In filter {1} that specifies Table without specifying IdentityComparison. IdentityComparison must be true for an In table filter.", TranslationErrorCode.InvalidInFilterTableWithoutIdentityComparison, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public static TranslationMessage InvalidFilterConditionIncompatibleDataType(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup filteredItem = null)
		{
			if (filteredItem == null)
			{
				return TranslationMessages.CreateMessage("The query contains a filter {1} with a comparison with incompatible data types. This often happens when the data types do not match. Please update update the data type or value.", TranslationErrorCode.InvalidFilterConditionIncompatibleDataType, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, Array.Empty<object>());
			}
			return TranslationMessages.CreateMessage("The query contains a filter with a comparison against {3} with incompatible data types. This often happens when the data types do not match. Please update update the data type or value.", TranslationErrorCode.InvalidFilterConditionIncompatibleDataType, severity, ErrorSourceCategory.UserInput, TranslationMessages.ToAffectedItems(filteredItem), objectType, objectId, propertyName, new object[] { filteredItem });
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000EC77 File Offset: 0x0000CE77
		public static TranslationMessage InvalidFilterConditionMultipleEntitySets(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is invalid; multiple entity sets are referenced in the same condition, which is not allowed.", TranslationErrorCode.InvalidFilterConditionMultipleEntitySets, severity, ErrorSourceCategory.UserInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000EC93 File Offset: 0x0000CE93
		public static TranslationMessage InvalidFilterConditionNonHierarchicalEntitySets(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is invalid; the entity sets referenced from the filter condition are not hierarchically related, which is not allowed.", TranslationErrorCode.InvalidFilterConditionNonHierarchicalEntitySets, severity, ErrorSourceCategory.UserInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public static TranslationMessage InvalidFilterEmptyGroupsTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter with FilterEmptyGroupsCondition on {0} '{1}' references the {3} '{4}', which is an invalid target for this type of filter. A filter with a condition of type FilterEmptyGroups condition must target a DataShape object.", TranslationErrorCode.InvalidFilterEmptyGroupsTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public static TranslationMessage InvalidFilterTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter of {0} '{1}' references the {3} '{4}', which is an invalid filter target.", TranslationErrorCode.InvalidFilterTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public static TranslationMessage InvalidFilterTargetScope(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("A filter of {0} '{1}' references the {3} '{4}', which is not in the same data shape. A filter can only reference constructs within the scope of its parent data shape.", TranslationErrorCode.InvalidFilterTargetScope, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000ED57 File Offset: 0x0000CF57
		public static TranslationMessage InvalidGroupExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' specifies an invalid group expression. A group expression must only contain a reference to a field, must not refer to a measure, and must not refer to an image.", TranslationErrorCode.InvalidGroupExpression, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public static TranslationMessage InvalidLimitInNestedDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string propertyName2)
		{
			return TranslationMessages.CreateMessage("The properties {2} and {3} on {0} '{1}' do not reference objects in the same DataShape. Properties {2} and {3} of a {0} must reference objects in the same DataShape.", TranslationErrorCode.InvalidLimitInNestedDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { propertyName2 });
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public static TranslationMessage InvalidLimitOperator(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' contains an operator.  A limit targetting intersection is not allowed with the specified operator.", TranslationErrorCode.InvalidLimitOperator, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000EDBD File Offset: 0x0000CFBD
		public static TranslationMessage InvalidLimitScopes(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' contains an invalid scope reference.  The Target must refer to a group that is contained inside the Within scope.  The Within scope must be a data shape or group. The AppliesTo scope must be a data shape.", TranslationErrorCode.InvalidLimitScopes, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public static TranslationMessage InvalidLimitTargetNotInnermostGroup(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId, ObjectType operatorType)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' references the {3} '{4}', which is an invalid limit target. Limits using operators of type '{5}' must target the innermost group.", TranslationErrorCode.InvalidLimitTargetNotInnermostGroup, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId, operatorType });
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000EE1D File Offset: 0x0000D01D
		public static TranslationMessage InvalidLimitTargets(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' is not valid. Targets should not be null or empty and Targets should be contiguous parents of the last Targets node.", TranslationErrorCode.InvalidLimitTargets, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000EE39 File Offset: 0x0000D039
		public static TranslationMessage InvalidLimitTargetsScopeType(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' is not valid. The first node in the Targets should be DataMember. A subsequent node can only be a DataMember when the within scope is a DataMember. When the within scope is a DataShape, subsequent nodes can be DataMembers or DataIntersections.", TranslationErrorCode.InvalidLimitTargetsScopeType, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000EE55 File Offset: 0x0000D055
		public static TranslationMessage InvalidIntersectionLimitNotInnerMostScope(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' targets a {2} which has a nested DataShape. Limits are not allowed to target {2} that are not the innermost scope.", TranslationErrorCode.InvalidIntersectionLimitNotInnerMostScope, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000EE74 File Offset: 0x0000D074
		public static TranslationMessage InvalidInstanceFilters(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup affectedItem)
		{
			return TranslationMessages.CreateMessage("The InstanceFilters have a column expression that cannot be matched to any existing grouping expression. Instance filters need to match the grouping columns.", TranslationErrorCode.InvalidInstanceFilters, severity, ErrorSourceCategory.MalformedExternalInput, TranslationMessages.ToAffectedItems(affectedItem), objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		public static TranslationMessage InvalidLimitWithinDataShapeRequired(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId, ObjectType operatorType)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' references the {3} '{4}', which is an invalid limit Within expression. Limits using operators of type '{5}' must be Within the owning data shape.", TranslationErrorCode.InvalidLimitWithinDataShapeRequired, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId, operatorType });
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public static TranslationMessage InvalidLiteralDataType(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string actualDataType, string expectedDataType)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' contains a value with data type '{3}' which is not compatible with the expected data type '{4}'.", TranslationErrorCode.InvalidLiteralDataType, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { actualDataType, expectedDataType });
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000EF1A File Offset: 0x0000D11A
		public static TranslationMessage InvalidMandatoryLimitCountProduct(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The product of all mandatory constraint limit counts should not be greater than {2} on {0}.", TranslationErrorCode.InvalidMandatoryLimitCountProduct, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000EF36 File Offset: 0x0000D136
		public static TranslationMessage InvalidMandatoryLimitCountProductInBlock(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The product of all mandatory constraint limit counts on {0} should not be greater than the containing block's Count.Max.", TranslationErrorCode.InvalidMandatoryLimitCountProductInBlock, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000EF54 File Offset: 0x0000D154
		public static TranslationMessage InvalidMeasureInContextFilterDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier calculationId)
		{
			return TranslationMessages.CreateMessage("The {2} {3} on {0} '{1}' contains a measure. Measures are not allowed in {2} on {0} '{1}'.", TranslationErrorCode.InvalidMeasureInContextFilterDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { calculationId });
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000EF81 File Offset: 0x0000D181
		public static TranslationMessage InvalidMixedCompoundFilterCondition(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The filter targeting {0} '{1}' is not valid. Scope filters must not be combined with other kinds of filters in the same CompoundFilterCondition.", TranslationErrorCode.InvalidMixedCompoundFilterCondition, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public static TranslationMessage InvalidMultipleFiltersSameTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType)
		{
			return TranslationMessages.CreateMessage("The {0} with {2} '{1}' is not the only {0} targeting {3} '{1}'. Multiple filters with the same target are not allowed.", TranslationErrorCode.InvalidMultipleFiltersSameTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType });
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000EFD2 File Offset: 0x0000D1D2
		public static TranslationMessage InvalidHierarchyLimitGap(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("If limits are specified on a hierarchy, the whole hierarchy must be covered by the limits. {0} '{1}' does not have a limit.", TranslationErrorCode.InvalidHierarchyLimitGap, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000EFEE File Offset: 0x0000D1EE
		public static TranslationMessage InvalidMultipleContextFilters(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string count)
		{
			return TranslationMessages.CreateMessage("The {0} in {2} '{1}' is not the only {0} with ContextFilterCondition in the data shape tree. Multiple context filters are not allowed in the data shape tree.", TranslationErrorCode.InvalidMultipleContextFilters, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, count, Array.Empty<object>());
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000F00A File Offset: 0x0000D20A
		public static TranslationMessage InvalidMultipleScopeFilters(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string count)
		{
			return TranslationMessages.CreateMessage("The {0} with {2} '{1}' is not the only {0} targeting a DataShape, a DataMember or a DataIntersection. Multiple scope filters are not allowed.", TranslationErrorCode.InvalidMultipleScopeFilters, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, count, Array.Empty<object>());
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000F028 File Offset: 0x0000D228
		public static TranslationMessage InvalidMultiplePostRegroupLimit(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType targetObjectType, Identifier targetObjectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' defines a limit that spans multiple groups. {3} '{4}' also spans multiple groups and is defined on the same DataShape. Only one such {0} is allowed on a DataShape.", TranslationErrorCode.InvalidMultiplePostRegroupLimit, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { targetObjectType, targetObjectId });
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F05F File Offset: 0x0000D25F
		public static TranslationMessage InvalidNestedContextFilter(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string count)
		{
			return TranslationMessages.CreateMessage("The {0} with {2} '{1}' is nested within another filter condition. Context filters nested within filter conditions are not allowed.", TranslationErrorCode.InvalidNestedContextFilter, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, count, Array.Empty<object>());
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F07C File Offset: 0x0000D27C
		public static TranslationMessage InvalidNestedFilterCondition(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, ObjectType nestedFilterType)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' on DataShape '{1}' is invalid. {2} is not allowed as a condition in a CompoundFilterCondition.", TranslationErrorCode.InvalidNestedFilterCondition, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, null, new object[] { nestedFilterType });
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public static TranslationMessage InvalidParent(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType parentObjectType, Identifier parentObjectId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is nested inside the {3} '{4}', which is invalid.", TranslationErrorCode.InvalidParent, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { parentObjectType, parentObjectId });
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		public static TranslationMessage InvalidPeerDataShapes(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' contains peer data shapes. Peer data shapes are not supported.", TranslationErrorCode.InvalidPeerDataShapes, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F103 File Offset: 0x0000D303
		public static TranslationMessage InvalidPeerGroups(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' contains peer groups. All groups within a hierarchy must be an ancestor or descendent of every other group in that hierarchy.", TranslationErrorCode.InvalidPeerGroups, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000F11F File Offset: 0x0000D31F
		public static TranslationMessage InvalidSortOnMeasureInSyncTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {2} for {0} '{1}' references a group with a sort key targeting a measure. A {2} reference cannot have sort on measure.", TranslationErrorCode.InvalidSortOnMeasureInSyncTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000F13B File Offset: 0x0000D33B
		public static TranslationMessage InvalidSyncDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage(" The Sync {0} '{1}' contains unsupported features. A Sync data shape can only have a primary member with a sync target and the same calculations as on the target group and FilterEmptyGroups filter. It is not supported to have other filters, limits, calculations or a secondary hierarchy.", TranslationErrorCode.InvalidSyncDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000F157 File Offset: 0x0000D357
		public static TranslationMessage InvalidSyncGroup(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId)
		{
			return TranslationMessages.CreateMessage(" The {0} '{1}' containing a Sync group is invalid. Sync groups cannot be Secondary Hierarchy members, children of other data members and contain children data members. A data shape with a group sync must be nested under a top level data shape.", TranslationErrorCode.InvalidSyncGroup, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, null, Array.Empty<object>());
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000F173 File Offset: 0x0000D373
		public static TranslationMessage InvalidSyncTargetScope(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(" {0} '{1}' contains a {2} that references an invalid scope. {2} must only reference another {0}.", TranslationErrorCode.InvalidSyncTargetScope, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000F18F File Offset: 0x0000D38F
		public static TranslationMessage InvalidUnconstrainedJoin(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.InvalidUnconstrainedJoin), TranslationErrorCode.InvalidUnconstrainedJoin, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		public static TranslationMessage InvalidUnlimitedBlock(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("At most one block is allowed to be unlimited on {0}.{2} and it can only be the last one.", TranslationErrorCode.InvalidUnlimitedBlock, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		public static TranslationMessage InvalidUsageOfEvaluateFunction(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The usage of the Evaluate function is invalid. An Evaluate function can only be the top level expression or inside an Aggregate function.", TranslationErrorCode.InvalidUsageOfEvaluateFunction, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F1E5 File Offset: 0x0000D3E5
		public static TranslationMessage InvalidValueFilterOnContextOnlyDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("Invalid value filter on context DataShape '{0}'. Value filters can only be on context data shapes that are part of a context filter.", TranslationErrorCode.InvalidValueFilterOnContextOnlyDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F204 File Offset: 0x0000D404
		public static TranslationMessage IsRelatedToManyNotSupportedForDetailTable(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup property)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.IsRelatedToManyNotSupportedForDetailTable), TranslationErrorCode.IsRelatedToManyNotSupportedForDetailTable, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { property });
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F233 File Offset: 0x0000D433
		public static TranslationMessage MissingContainingMemberStartPosition(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' specifies a {2} but one or more containing Groups or their subtotals do not specify a {2}. All containing Groups and their subtotals must specify a {2}.", TranslationErrorCode.MissingContainingMemberStartPosition, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F24F File Offset: 0x0000D44F
		public static TranslationMessage MissingOrEmptyGroupKeys(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property GroupKeys on {0} '{1}' is missing or is empty. A {0} must specify a GroupKeys collection with at least one GroupKey.", TranslationErrorCode.MissingOrEmptyGroupKeys, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F26B File Offset: 0x0000D46B
		public static TranslationMessage MissingOrInvalidPropertyValue(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' is missing or has an invalid value.", TranslationErrorCode.MissingOrInvalidPropertyValue, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F288 File Offset: 0x0000D488
		public static TranslationMessage ModelGroupingInstructionsIgnored(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup itemName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.ModelGroupingInstructionsIgnored), TranslationErrorCode.ModelGroupingInstructionsIgnored, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { itemName });
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		public static TranslationMessage ModelMeasuresNotSupportedForDetailTable(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, IContainsTelemetryMarkup measureName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.ModelMeasuresNotSupportedForDetailTable), TranslationErrorCode.ModelMeasuresNotSupportedForDetailTable, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[] { measureName });
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F2E7 File Offset: 0x0000D4E7
		public static TranslationMessage MoreThanOnePrimarySecondaryBlock(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("At most one PrimarySecondaryBlock is allowed in {0}.{2}.", TranslationErrorCode.MoreThanOnePrimarySecondaryBlock, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F303 File Offset: 0x0000D503
		public static TranslationMessage NaNLiteralNotSupported(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.NaNLiteralNotSupported), TranslationErrorCode.NaNLiteralNotSupported, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000F324 File Offset: 0x0000D524
		public static TranslationMessage NestedDataShapeWithSubtotal(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, Identifier nestedDataShapeId)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' is nested in data shape '{3}' and both data shapes use subtotal functions. Subtotal functions in a parent and a nested data shape are not supported.", TranslationErrorCode.NestedDataShapeWithSubtotal, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { nestedDataShapeId });
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000F354 File Offset: 0x0000D554
		public static TranslationMessage NonNegativeIntegerValueRequired(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' has an invalid value. Only non-negative integer values are allowed.", TranslationErrorCode.NonNegativeIntegerValueRequired, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000F373 File Offset: 0x0000D573
		public static TranslationMessage NoUniqueKeyForDetailTable(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.NoUniqueKeyForDetailTable), TranslationErrorCode.NoUniqueKeyForDetailTable, severity, ErrorSourceCategory.InputDoesNotMatchModel, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000F397 File Offset: 0x0000D597
		public static TranslationMessage OverlappingKeysOnOppositeHierarchies(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.OverlappingKeysOnOppositeHierarchies), TranslationErrorCode.OverlappingKeysOnOppositeHierarchies, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public static TranslationMessage PositiveIntegerValueRequired(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' has an invalid value. Only positive integer values are allowed.", TranslationErrorCode.PositiveIntegerValueRequired, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000F3DA File Offset: 0x0000D5DA
		public static TranslationMessage RestartTokensOnNestedDataShape(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property {2} on {0} '{1}' is not allowed. RestartTokens are not allowed on nested data shapes.", TranslationErrorCode.RestartTokensOnNestedDataShape, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		public static TranslationMessage ShowAllWithDataTransform(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, ObjectType propertyOwnerType)
		{
			return TranslationMessages.CreateMessage("The value of {3} property '{2}' on {0} '{1}' is incompatible with Data Transform. Combination of Data Transforms and group keys with ShowItemsWithNoData set to true is prohibited.", TranslationErrorCode.ShowAllWithDataTransform, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { propertyOwnerType });
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F431 File Offset: 0x0000D631
		public static TranslationMessage SortKeysDuplicateExpressions(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' contains more than one SortKey with the same expression in SortKeys.", TranslationErrorCode.SortKeysDuplicateExpressions, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000F450 File Offset: 0x0000D650
		public static TranslationMessage SortKeysInconsistentStartPosition(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' contains a {2} which does not have consistent value with the corresponding {2} in other DataMembers.", TranslationErrorCode.SortKeysInconsistentStartPosition, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F46F File Offset: 0x0000D66F
		public static TranslationMessage StartPositionNoSortKeys(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' specifies a {2}, but no SortKeys are specified.", TranslationErrorCode.StartPositionNoSortKeys, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F48E File Offset: 0x0000D68E
		public static TranslationMessage StartPositionInSecondaryHierarchy(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' specifies a {2} in the SecondaryHierarchy. {2} may only be specified in the PrimaryHierarchy.", TranslationErrorCode.StartPositionInSecondaryHierarchy, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000F4AD File Offset: 0x0000D6AD
		public static TranslationMessage StartPositionNotSupportedForSyncTarget(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' contains a {2} property. {2} is not supported for group synchronization.", TranslationErrorCode.StartPositionNotSupportedForSyncTarget, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		public static TranslationMessage StartPositionRequiresSortKeys(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' does not contain a {2} property. {2} is required when a StartPosition is specified.", TranslationErrorCode.StartPositionRequiresSortKeys, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F4EB File Offset: 0x0000D6EB
		public static TranslationMessage StartPositionWithRestartTokens(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' contains a {2} property. {2} is not supported when a RestartToken is specified.", TranslationErrorCode.StartPositionWithRestartTokens, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000F50A File Offset: 0x0000D70A
		public static TranslationMessage SubtotalAndNonSubtotalCalculations(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.SubtotalAndNonSubtotalCalculations), TranslationErrorCode.SubtotalAndNonSubtotalCalculations, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000F52E File Offset: 0x0000D72E
		public static TranslationMessage SubtotalStartPositionOnNonSubtotal(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' contains a {2} property. {2} is not supported on non subtotal member.", TranslationErrorCode.SubtotalStartPositionOnNonSubtotal, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000F54D File Offset: 0x0000D74D
		public static TranslationMessage SuppressJoinPredicateOnNonMeasure(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.SuppressJoinPredicateOnNonMeasure), TranslationErrorCode.SuppressJoinPredicateOnNonMeasure, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000F571 File Offset: 0x0000D771
		public static TranslationMessage SyncTargetNotAllowedWithGroupKeys(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("{0} '{1}' contains {2} property. {2} is not allowed with GroupKey, SortKeys and ScopeIdDefinition.", TranslationErrorCode.SyncTargetNotAllowedWithGroupKeys, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F590 File Offset: 0x0000D790
		public static TranslationMessage TopNPerLevelLevelNotPresentOnPrimary(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("Level expression is not present in the primary member's sort key list", TranslationErrorCode.TopNPerLevelLevelNotPresentOnPrimary, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static TranslationMessage VisualAxisWithoutSort(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string axisName)
		{
			return TranslationMessages.CreateMessage("The query specifies a visual axis '{3}' where no group has a sort key. Each visual axis must have at least one sorted group.", TranslationErrorCode.VisualAxisWithoutSort, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, new object[] { axisName });
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public static TranslationMessage UnexpectedDataTransformParameter(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The query specifies a DataTransform Parameter '{1}' which could not be mapped to the transform implementation.", TranslationErrorCode.UnexpectedDataTransformParameter, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000F600 File Offset: 0x0000D800
		public static TranslationMessage UnexpectedQueryGenerationError(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string queryName, string details)
		{
			return TranslationMessages.CreateMessage("An unexpected error occurred while generating query '{3}'. Details: {4}", TranslationErrorCode.UnexpectedQueryGenerationError, severity, ErrorSourceCategory.UnexpectedError, objectType, objectId, propertyName, new object[] { queryName, details });
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000F638 File Offset: 0x0000D838
		public static TranslationMessage UnknownDataTransformAlgorithm(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, string algorithmName)
		{
			return TranslationMessages.CreateMessage("The query contains a DataTransform using an unknown algorithm '{3}'.", TranslationErrorCode.UnknownDataTransformAlgorithm, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, new object[] { algorithmName });
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000F668 File Offset: 0x0000D868
		public static TranslationMessage UnsupportedAggregateOverShowItemsWithNoData(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The expression in property '{2}' on {0} '{1}' uses an aggregate function that is not supported over group keys with ShowItemsWithNoData. Remove the ShowItemsWithNoData setting on the group or remove the aggregate that is incompatible with it.", TranslationErrorCode.UnsupportedAggregateOverShowItemsWithNoData, severity, ErrorSourceCategory.UnsupportedFeature, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000F688 File Offset: 0x0000D888
		public static TranslationMessage UnsupportedDateTimeLiteral(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, DateTime userSuppliedDateTime, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			string[] array = TranslationMessages.UnsupportedDateTimeLiteralArgs(userSuppliedDateTime, daxCapabilitiesAnnotation);
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.UnsupportedDateTimeLiteral), TranslationErrorCode.UnsupportedDateTimeLiteral, severity, ErrorSourceCategory.UserInput, objectType, objectId, propertyName, new object[]
			{
				array[0],
				array[1]
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public static string[] UnsupportedDateTimeLiteralArgs(DateTime userSuppliedDateTime, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			string text = userSuppliedDateTime.ToString(CultureInfo.InvariantCulture).MarkAsCustomerContent();
			string text2 = QueryConstants.GetEarliestSupportedDateTime(daxCapabilitiesAnnotation).ToString(CultureInfo.InvariantCulture);
			return new string[] { text, text2 };
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000F711 File Offset: 0x0000D911
		public static TranslationMessage UnsupportedNegatedTuplesFilter(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.UnsupportedNegatedTuplesFilter), TranslationErrorCode.UnsupportedNegatedTuplesFilter, severity, ErrorSourceCategory.UnsupportedFeature, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000F735 File Offset: 0x0000D935
		public static TranslationMessage UnsupportedStringMinMaxColumn(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.UnsupportedStringMinMaxColumn), TranslationErrorCode.UnsupportedStringMinMaxColumn, severity, ErrorSourceCategory.InputDoesNotMatchModel, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000F759 File Offset: 0x0000D959
		public static TranslationMessage UnsupportedStringMinMaxExpression(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage(TranslationMessages.GetLocalizedTemplate(TranslationErrorCode.UnsupportedStringMinMaxExpression), TranslationErrorCode.UnsupportedStringMinMaxExpression, severity, ErrorSourceCategory.InputDoesNotMatchModel, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F77D File Offset: 0x0000D97D
		public static TranslationMessage WrongNumberOfDataRows(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' has an incorrect number of DataRows. There must be one DataRow for each innermost DataMember in the PrimaryHierarchy.", TranslationErrorCode.WrongNumberOfDataRows, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000F79C File Offset: 0x0000D99C
		public static TranslationMessage WrongNumberOfFiltersWithContextFilterCondition(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The {0} '{1}' has an incorrect number of Filters with ContextFilterCondition. There must not be more than one Filter with ContextFilterCondition for each {0}.", TranslationErrorCode.WrongNumberOfFiltersWithContextFilterCondition, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		public static TranslationMessage WrongNumberOfIntersections(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName, int rowIndex)
		{
			return TranslationMessages.CreateMessage("The DataRow at index {3} in {0} '{1}' has an incorrect number of DataIntersections. There must be one DataIntersection for each innermost DataMember in the SecondaryHierarchy.", TranslationErrorCode.WrongNumberOfIntersections, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, new object[] { rowIndex });
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000F7F1 File Offset: 0x0000D9F1
		public static TranslationMessage WrongNumberOfScopeValueDefinitions(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' has an incorrect number of Values. The ScopeIdDefinition must contain one ScopeValueDefinition for each SortKey in SortKeys.", TranslationErrorCode.WrongNumberOfScopeValueDefinitions, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000F810 File Offset: 0x0000DA10
		public static TranslationMessage WrongNumberOfStartPositionValues(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' has an incorrect number of Values. The StartPosition must contain one ScopeValue for each SortKey in the SortKeys.", TranslationErrorCode.WrongNumberOfStartPositionValues, severity, ErrorSourceCategory.MalformedExternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F82F File Offset: 0x0000DA2F
		public static TranslationMessage WrongOrderOfScopeValueDefinitions(EngineMessageSeverity severity, ObjectType objectType, Identifier objectId, string propertyName)
		{
			return TranslationMessages.CreateMessage("The property '{2}' on {0} '{1}' has an incorrect order of Values. The ScopeIdDefinition must contain the ScopeValueDefinitions in the same order as the SortKeys.", TranslationErrorCode.WrongOrderOfScopeValueDefinitions, severity, ErrorSourceCategory.MalformedInternalInput, objectType, objectId, propertyName, Array.Empty<object>());
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000F850 File Offset: 0x0000DA50
		private static TranslationMessage CreateMessage(string messageTemplate, TranslationErrorCode code, EngineMessageSeverity severity, ErrorSource source, ObjectType objectType, Identifier objectId, string propertyName, params object[] args)
		{
			return TranslationMessages.CreateMessage(messageTemplate, code, severity, source, null, objectType, objectId, propertyName, args);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F870 File Offset: 0x0000DA70
		private static TranslationMessage CreateMessage(string messageTemplate, TranslationErrorCode code, EngineMessageSeverity severity, ErrorSource source, string[] affectedItems, ObjectType objectType, Identifier objectId, string propertyName, params object[] args)
		{
			return TranslationMessages.CreateMessage(messageTemplate, code, severity, source, null, null, affectedItems, objectType, objectId, propertyName, args);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		private static TranslationMessage CreateMessage(string messageTemplate, TranslationErrorCode code, EngineMessageSeverity severity, ErrorSource source, int? line, int? position, string[] affectedItems, ObjectType objectType, Identifier objectId, string propertyName, params object[] args)
		{
			object[] messageArguments = TranslationMessages.GetMessageArguments(objectType, objectId, propertyName, args);
			string text = TranslationMessageUtils.FormatMessage(messageTemplate, messageArguments);
			return new TranslationMessage(text, text, code, severity, source, objectType, TranslationMessageUtils.GetDisplayString(objectId), propertyName, line, position, affectedItems);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		private static object[] GetMessageArguments(ObjectType objectType, Identifier objectId, string propertyName, object[] arguments)
		{
			object[] array = new object[3 + ((arguments == null) ? 0 : arguments.Length)];
			array[0] = objectType;
			array[1] = TranslationMessageUtils.GetDisplayString(objectId);
			array[2] = propertyName;
			if (arguments != null)
			{
				Array.Copy(arguments, 0, array, 3, arguments.Length);
			}
			return array;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F928 File Offset: 0x0000DB28
		private static string GetLocalizedTemplate(TranslationErrorCode errorCode)
		{
			return DsqtStrings.Keys.GetString(errorCode.ToString());
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F93C File Offset: 0x0000DB3C
		private static string[] ToAffectedItems(IContainsTelemetryMarkup item)
		{
			if (item == null)
			{
				return null;
			}
			return new string[] { item.ToCustomerContentString() };
		}
	}
}
