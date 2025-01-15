using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005E RID: 94
	internal static class TranslationMessagePhrases
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000D966 File Offset: 0x0000BB66
		public static TranslationMessagePhrase CalculationReferenceNotAllowed()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a calculation reference outside an aggregate function.", Array.Empty<object>());
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D977 File Offset: 0x0000BB77
		public static TranslationMessagePhrase CalculationRefersToAncestorScope()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a calculation reference to an ancestor scope.", Array.Empty<object>());
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000D988 File Offset: 0x0000BB88
		public static TranslationMessagePhrase CannotInvokeFunction(string functionName)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' cannot be invoked with the specified arguments.", new object[] { functionName });
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D99E File Offset: 0x0000BB9E
		public static TranslationMessagePhrase CircularReference()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a reference to itself. Circular references are not allowed.", Array.Empty<object>());
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D9AF File Offset: 0x0000BBAF
		public static TranslationMessagePhrase DaxExternalContent_ExtensionMeasure(IContainsTelemetryMarkup itemName)
		{
			return TranslationMessagePhrases.CreatePhrase(DsqtStrings.Keys.GetString("DaxExternalContent_ExtensionMeasure"), new object[] { itemName });
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		public static TranslationMessagePhrase DaxExternalContent_ExtensionColumn(IContainsTelemetryMarkup itemName)
		{
			return TranslationMessagePhrases.CreatePhrase(DsqtStrings.Keys.GetString("DaxExternalContent_ExtensionColumn"), new object[] { itemName });
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		public static TranslationMessagePhrase DaxExternalContent_Inline()
		{
			return TranslationMessagePhrases.CreatePhrase(DsqtStrings.Keys.GetString("DaxExternalContent_Inline"), Array.Empty<object>());
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000D9FB File Offset: 0x0000BBFB
		public static TranslationMessagePhrase DuplicateArgumentToScopeFunction(IContainsTelemetryMarkup argument)
		{
			return TranslationMessagePhrases.CreatePhrase("The argument {0} was passed more than once to the Scope function; this is not supported.", new object[] { argument });
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000DA11 File Offset: 0x0000BC11
		public static TranslationMessagePhrase EntitySetReferenceNotAllowed()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a reference to an entity set outside an aggregate function that supports entity set references.", Array.Empty<object>());
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DA22 File Offset: 0x0000BC22
		public static TranslationMessagePhrase EntitySetNotFound(IContainsTelemetryMarkup entitySetFullName)
		{
			return TranslationMessagePhrases.CreatePhrase(SR.Keys.GetString("EntitySetNotFound"), new object[] { entitySetFullName });
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DA3D File Offset: 0x0000BC3D
		public static TranslationMessagePhrase FilterConditionReferenceInWrongContext(Identifier filterConditionId)
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a filter condition reference to filter condition '{0}' in a context where it is not allowed.", new object[] { filterConditionId });
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000DA53 File Offset: 0x0000BC53
		public static TranslationMessagePhrase FilterConditionReferenceNotAllowed()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains a filter condition reference outside a calculation value.", Array.Empty<object>());
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public static TranslationMessagePhrase FilterConditionReferenceToInvalidScope(Identifier filterConditionId)
		{
			return TranslationMessagePhrases.CreatePhrase("The reference to filter condition '{0}' references a filter condition in a nested data shape or in a non-data shape scope. Only references to filter conditions of top-level data shapes are allowed.", new object[] { filterConditionId });
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DA7A File Offset: 0x0000BC7A
		public static TranslationMessagePhrase GeneralQueryError(string error)
		{
			return TranslationMessagePhrases.CreatePhrase(error, Array.Empty<object>());
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DA87 File Offset: 0x0000BC87
		public static TranslationMessagePhrase InvalidArgumentCountForFunction(string functionName, int argCount)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' requires {1} arguments.", new object[] { functionName, argCount });
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
		public static TranslationMessagePhrase InvalidArgumentCountForFunction_VarArg(string functionName, int argCount)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' requires at least {1} arguments.", new object[] { functionName, argCount });
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DAC5 File Offset: 0x0000BCC5
		public static TranslationMessagePhrase InvalidArgumentKindForFunction(string functionName, string argKind, int argPosition)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' does not support arguments of kind '{1}' at position '{2}'.", new object[] { functionName, argKind, argPosition });
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		internal static TranslationMessagePhrase InvalidDetailGroupIdentity(ExpressionNodeKind expressionNodeKind)
		{
			return TranslationMessagePhrases.CreatePhrase("The detail group identity expression cannot be a '{0}'. Only Entity Set references are allowed.", new object[] { expressionNodeKind });
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DB03 File Offset: 0x0000BD03
		internal static TranslationMessagePhrase InvalidDetailGroupIdentityExtensionEntity()
		{
			return TranslationMessagePhrases.CreatePhrase("The detail group identity expression cannot be an entity with extension columns.", Array.Empty<object>());
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public static TranslationMessagePhrase InvalidExpressionSyntax(string token)
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains invalid syntax. The token '{0}' was unexpected.", new object[] { token });
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000DB2A File Offset: 0x0000BD2A
		public static TranslationMessagePhrase InvalidFieldReference(IContainsTelemetryMarkup fieldName)
		{
			return TranslationMessagePhrases.CreatePhrase(SR.Keys.GetString("InvalidFieldReference"), new object[] { fieldName });
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000DB45 File Offset: 0x0000BD45
		public static TranslationMessagePhrase InvalidFilter(ObjectType objectType, Identifier objectId)
		{
			return TranslationMessagePhrases.CreatePhrase("The filter targeting {0} '{1}' is not valid. Only scope filters can target a DataMember or DataIntersection in the DataShape. A scope filter can only target a top-level DataShape that has no groups.", new object[] { objectType, objectId });
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public static TranslationMessagePhrase InvalidFilterCondition()
		{
			return TranslationMessagePhrases.CreatePhrase("The filter condition is invalid; the types of the arguments cannot be determined.", Array.Empty<object>());
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DB75 File Offset: 0x0000BD75
		public static TranslationMessagePhrase InvalidFunctionForExpression(string functionName)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' cannot be used in this type of expression.", new object[] { functionName });
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DB8B File Offset: 0x0000BD8B
		public static TranslationMessagePhrase InvalidPercentileExcKValue(string functionName, int argPosition)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' has an invalid argument at position '{1}'.  The value must be a double literal between 0.0 and 1.0 exclusive.", new object[] { functionName, argPosition });
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000DBAA File Offset: 0x0000BDAA
		public static TranslationMessagePhrase InvalidPercentileIncKValue(string functionName, int argPosition)
		{
			return TranslationMessagePhrases.CreatePhrase("The function '{0}' has an invalid argument at position '{1}'.  The value must be a double literal between 0.0 and 1.0 inclusive.", new object[] { functionName, argPosition });
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DBC9 File Offset: 0x0000BDC9
		public static TranslationMessagePhrase InvalidOperator(string operatorName)
		{
			return TranslationMessagePhrases.CreatePhrase("The operator '{0}' is not supported.", new object[] { operatorName });
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DBDF File Offset: 0x0000BDDF
		public static TranslationMessagePhrase InvalidQueryParameterName(string name)
		{
			return TranslationMessagePhrases.CreatePhrase("Could not locate query parameter with name '{0}'.", new object[] { name.MarkAsModelInfo() });
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		public static TranslationMessagePhrase InvalidRemoveFilterConditionReference(Identifier filterConditionId)
		{
			return TranslationMessagePhrases.CreatePhrase("The reference to a filter condition '{0}' when used as an argument to a RemoveFilter function needs to reference a top-level singleton filter condition or a condition of a top-level compound filter condition.", new object[] { filterConditionId });
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public static TranslationMessagePhrase InvalidRollupScopes(string start, string end)
		{
			return TranslationMessagePhrases.CreatePhrase("Invalid usage of Rollup scopes. Rollup end scope must be a parent scope of the Rollup start scope. Scopes '{0}' and '{1}' do not meet this condition.", new object[] { start, end });
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		public static TranslationMessagePhrase InvalidRollupScopesInTopLevelEvaluate(string start, string end)
		{
			return TranslationMessagePhrases.CreatePhrase("Invalid usage of a Rollup function in a top level Evaluate. The containing scope of a Rollup function must be equal to the end scope. Scopes '{0}' and '{1}' do not meet this condition.", new object[] { start, end });
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public static TranslationMessagePhrase InvalidRollupStartScope(string scope)
		{
			return TranslationMessagePhrases.CreatePhrase("Invalid start scope for a Rollup function. The start scope of a Rollup function must be equal to the query scope. Scope '{0}' does not meet this condition.", new object[] { scope });
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DC5A File Offset: 0x0000BE5A
		public static TranslationMessagePhrase InvalidSubQueryScopes(Identifier outerId, Identifier innerId)
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains an invalid reference to '{1}' from within '{0}'.", new object[] { outerId, innerId });
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000DC74 File Offset: 0x0000BE74
		public static TranslationMessagePhrase InvalidSubQueryTarget()
		{
			return TranslationMessagePhrases.CreatePhrase("The expression contains an aggregate with an invalid structure reference. The target of a structure reference inside an aggregate must be a Calculation with a measure value or a Calculation with a group key expression.", Array.Empty<object>());
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000DC85 File Offset: 0x0000BE85
		public static TranslationMessagePhrase InvalidSubtotalTarget(Identifier targetId)
		{
			return TranslationMessagePhrases.CreatePhrase("'{0}' is not a valid target for the Subtotal function.", new object[] { targetId });
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		public static TranslationMessagePhrase InvalidUsageOfFunction(string functionName)
		{
			return TranslationMessagePhrases.CreatePhrase("The usage of function {0} is invalid.", new object[] { functionName });
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		public static TranslationMessagePhrase MissingOrInvalidStructuralReferenceTarget(Identifier targetId)
		{
			return TranslationMessagePhrases.CreatePhrase("The target of structural reference '[{0}]' was missing or had an invalid data type.", new object[] { targetId });
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DCC7 File Offset: 0x0000BEC7
		public static TranslationMessagePhrase ModelReferenceNotAllowed(ObjectType objectType, Identifier objectId)
		{
			return TranslationMessagePhrases.CreatePhrase("A model reference is used in {0} '{1}'. Model reference is not allowed in this context.", new object[] { objectType, objectId });
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public static TranslationMessagePhrase MultipleFilterContextChangingFunctions()
		{
			return TranslationMessagePhrases.CreatePhrase("Multiple filter context changing functions were found in a single expression; this is not supported.", Array.Empty<object>());
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		public static TranslationMessagePhrase SchemaNotFound(IContainsTelemetryMarkup schemaName)
		{
			return TranslationMessagePhrases.CreatePhrase(SR.Keys.GetString("ConceptualSchemaNotFound"), new object[] { schemaName });
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000DD12 File Offset: 0x0000BF12
		public static TranslationMessagePhrase ScopeFunctionArgumentsNotInCurrentScope(string argument)
		{
			return TranslationMessagePhrases.CreatePhrase("The argument {0} to the Scope function is not a grouping key, sort key, or detail in the current context; this is not supported.", new object[] { argument });
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000DD28 File Offset: 0x0000BF28
		public static TranslationMessagePhrase StructureReferenceNotAllowed(ObjectType objectType, Identifier objectId)
		{
			return TranslationMessagePhrases.CreatePhrase("A structure reference is used in '{0}' {1}. Structure references are not allowed in this context.", new object[] { objectType, objectId });
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000DD47 File Offset: 0x0000BF47
		public static TranslationMessagePhrase UnknownFunctionName(string functionName)
		{
			return TranslationMessagePhrases.CreatePhrase("The expression uses an invalid function name '{0}'.", new object[] { functionName });
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public static TranslationMessagePhrase UnknownKeyword(string keyword)
		{
			return TranslationMessagePhrases.CreatePhrase("The expression uses an invalid keyword or string literal '{0}'.", new object[] { keyword });
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000DD73 File Offset: 0x0000BF73
		private static TranslationMessagePhrase CreatePhrase(string template, params object[] args)
		{
			return new TranslationMessagePhrase(TranslationMessageUtils.FormatMessage(template, args));
		}
	}
}
