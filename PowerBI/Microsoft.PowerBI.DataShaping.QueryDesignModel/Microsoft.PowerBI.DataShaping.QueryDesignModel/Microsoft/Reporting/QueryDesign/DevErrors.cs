using System;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Reporting.QueryDesign
{
	// Token: 0x020000CE RID: 206
	internal static class DevErrors
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x00021FD5 File Offset: 0x000201D5
		internal static string UnexpectedEdmPropertySubtype(string typeName)
		{
			return StringUtil.FormatInvariant("Unexpected EdmProperty subtype: {0}", new object[] { typeName });
		}

		// Token: 0x020002D9 RID: 729
		internal static class AddMissingItemsTableBuilder
		{
			// Token: 0x06001CCE RID: 7374 RVA: 0x0004FADF File Offset: 0x0004DCDF
			internal static string ColumnIsNotInInputTable(string columnName)
			{
				return StringUtil.FormatInvariant("The column '{0}' is not in the input table.", new object[] { columnName });
			}

			// Token: 0x0400104F RID: 4175
			internal static readonly string DuplicateRollup = "Only one Rollup is allowed in AddMissingItems.";

			// Token: 0x04001050 RID: 4176
			internal static readonly string GroupMustHaveAtLeastOneKey = "An AddMissingItems group must have at least one key.";

			// Token: 0x04001051 RID: 4177
			internal static readonly string RollupMustHaveAtLeastOneGroup = "An AddMissingItems rollup must have at least one group.";
		}

		// Token: 0x020002DA RID: 730
		internal static class BatchQueryBuilder
		{
			// Token: 0x06001CD0 RID: 7376 RVA: 0x0004FB15 File Offset: 0x0004DD15
			internal static string InvalidDeclarationTypeInComposableQuery(string typeName)
			{
				return StringUtil.FormatInvariant("Declaration of type {0} is not allowed in a composable batch query.", new object[] { typeName });
			}

			// Token: 0x04001052 RID: 4178
			internal static readonly string MultipleOutputTablesInComposableQuery = "Only one OutputTable is allowed in a composable batch query.";
		}

		// Token: 0x020002DB RID: 731
		internal static class EdmField
		{
			// Token: 0x06001CD2 RID: 7378 RVA: 0x0004FB37 File Offset: 0x0004DD37
			internal static string GroupByIsNotKeysWhenGroupOnEntityKey(string entityName, string fieldName)
			{
				return StringUtil.FormatInvariant("EntityKey is specified but EdmField.GroupBy contains non-key fields: {0}.{1}.", new object[] { entityName, fieldName });
			}

			// Token: 0x06001CD3 RID: 7379 RVA: 0x0004FB51 File Offset: 0x0004DD51
			internal static string CycleDetectedInAttributeRelationship(string entityName, string fieldName)
			{
				return StringUtil.FormatInvariant("A cycle has been detected in an EdmField.RelatedTo reference starting at field {0}.{1}.", new object[] { entityName, fieldName });
			}
		}

		// Token: 0x020002DC RID: 732
		internal static class EntityDataModelManager
		{
			// Token: 0x04001053 RID: 4179
			internal const string OperationFromWrongThread = "EntityDataModelManager operation invoked from the wrong thread.";
		}

		// Token: 0x020002DD RID: 733
		internal static class EntityType
		{
			// Token: 0x06001CD4 RID: 7380 RVA: 0x0004FB6B File Offset: 0x0004DD6B
			internal static string UnsupportedElementType(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported Element Type: {0}", new object[] { typeName });
			}

			// Token: 0x06001CD5 RID: 7381 RVA: 0x0004FB81 File Offset: 0x0004DD81
			internal static string UnknownMemberReference(string memberRef)
			{
				return StringUtil.FormatInvariant("Unknown Member Reference: {0}", new object[] { memberRef });
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x0004FB97 File Offset: 0x0004DD97
			internal static string UnknownHierarchyReference(string hierarchyRef)
			{
				return StringUtil.FormatInvariant("Unknown Hierarchy Reference: {0}", new object[] { hierarchyRef });
			}
		}

		// Token: 0x020002DE RID: 734
		internal static class TypeUsage
		{
			// Token: 0x06001CD7 RID: 7383 RVA: 0x0004FBAD File Offset: 0x0004DDAD
			internal static string UnsupportedEdmType(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported EdmType: {0}", new object[] { typeName });
			}
		}

		// Token: 0x020002DF RID: 735
		internal static class DefaultExpressionVisitor
		{
			// Token: 0x06001CD8 RID: 7384 RVA: 0x0004FBC3 File Offset: 0x0004DDC3
			internal static string UnsupportedAddMissingItemsGroupItem(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported IAddMissingItemsGroupItem type: {0}", new object[] { typeName });
			}

			// Token: 0x06001CD9 RID: 7385 RVA: 0x0004FBD9 File Offset: 0x0004DDD9
			internal static string UnsupportedExpression(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported QueryExpression type: {0}", new object[] { typeName });
			}

			// Token: 0x06001CDA RID: 7386 RVA: 0x0004FBEF File Offset: 0x0004DDEF
			internal static string UnsupportedGroupItem(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported IGroupItem type: {0}", new object[] { typeName });
			}
		}

		// Token: 0x020002E0 RID: 736
		internal static class QueryTableUtils
		{
			// Token: 0x04001054 RID: 4180
			internal const string NoCompatibleGroupForDetail = "Did not find compatible group builder for query expression.";
		}

		// Token: 0x020002E1 RID: 737
		internal static class GroupAndJoinTableBuilder
		{
			// Token: 0x04001055 RID: 4181
			internal const string TryingToAddGroupKeyAsColumn = "Trying to add an existing group key as a table column is not allowed.";

			// Token: 0x04001056 RID: 4182
			internal const string DuplicateRollupGroupNames = "Detected duplicate rollup group names in the query.";

			// Token: 0x04001057 RID: 4183
			internal const string DetailsButNoKeysInTopLevelGroup = "A top level group with group details but without group keys was found.";

			// Token: 0x04001058 RID: 4184
			internal const string ContextTablesInEmptyGroup = "A group without group details or group keys still has context tables that cannot be reconciled against other groups.";
		}

		// Token: 0x020002E2 RID: 738
		internal static class QueryDefinitionBuilder
		{
			// Token: 0x06001CDB RID: 7387 RVA: 0x0004FC05 File Offset: 0x0004DE05
			internal static string UnknownGroup(string groupName)
			{
				return StringUtil.FormatInvariant("Group '{0}' was not recognized by the QueryBuilder. Add the Group to the QueryBuilder before attempting the current operation.", new object[] { groupName });
			}

			// Token: 0x06001CDC RID: 7388 RVA: 0x0004FC1B File Offset: 0x0004DE1B
			internal static string TryingToAddIncompatibleDetail(string groupName, string detailExpr)
			{
				return StringUtil.FormatInvariant("Detail expression '{0}' is incompatible with group '{1}'", new object[] { detailExpr, groupName });
			}

			// Token: 0x06001CDD RID: 7389 RVA: 0x0004FC35 File Offset: 0x0004DE35
			internal static string TooManyStartAtValues(int startAtValueCount, int sortFieldCount)
			{
				return StringUtil.FormatInvariant("The query contains too many StartAt values. {0} StartAt values were specified but the query only contains {1} sort fields.", new object[] { startAtValueCount, sortFieldCount });
			}

			// Token: 0x04001059 RID: 4185
			internal const string TryingToAddSameDetailInconsistentState = "Trying to add a query detail that is inconsistent with an existing query detail.";

			// Token: 0x0400105A RID: 4186
			internal const string TryingToAddSameGroupInconsistentState = "Trying to add a query group that is inconsistent with an existing query group.";

			// Token: 0x0400105B RID: 4187
			internal const string TryingToAddSameMeasureInconsistentState = "Trying to add a query measure that is inconsistent with an existing query measure.";

			// Token: 0x0400105C RID: 4188
			internal const string TryingToAddMultipleLimitsToSameGroup = "Trying to add multiple limits to the same group is not allowed.";

			// Token: 0x0400105D RID: 4189
			internal const string TryingToAddLimitAcrossMultipleGroups = "Trying to add a Limit across multiple groups is only allowed for post-regroup Limits.";

			// Token: 0x0400105E RID: 4190
			internal const string TryingToAddLimitSortOnUnrelatedGroup = "Trying to add a Limit sort expression that is not on the groups in the limit";

			// Token: 0x0400105F RID: 4191
			internal const string TryingtoAddMultiplePostRegroupLimits = "Trying to add multiple post-regroup Limits is not allowed. A query may only contain one post-regroup Limit.";

			// Token: 0x04001060 RID: 4192
			internal const string TryingtoAddMultipleTopLevelLimits = "Trying to add multiple top-level Limits is not allowed. A query may only contain one top-level Limit.";

			// Token: 0x04001061 RID: 4193
			internal const string TryingToAddSameRollupWithDifferentGroups = "Trying to add a rollup with the same indicator name but different groups.";

			// Token: 0x04001062 RID: 4194
			internal const string TryingToAddMultipleRollupsToSameGroup = "Trying to add multiple rollups with different aggregate indicator names to the same group.";

			// Token: 0x04001063 RID: 4195
			internal const string TryingToAddSecondGroupFilter = "Trying to add a group filter when a group filter has already been added. A query may only contain one group filter.";

			// Token: 0x04001064 RID: 4196
			internal const string TopLevelLimitWithoutSorting = "A top-level Limit may only be used when the query has sorting.";
		}

		// Token: 0x020002E3 RID: 739
		internal static class QueryExpressionBuilder
		{
			// Token: 0x04001065 RID: 4197
			internal const string QueryFunctionInvocationFailedMessage = "The query function invocation failed.";

			// Token: 0x04001066 RID: 4198
			internal const string GroupAndJoinRequiresAtLeastOneColumn = "At least one column must be specified when constructing a GroupAndJoin.";

			// Token: 0x04001067 RID: 4199
			internal const string DataTableInvalidColumnReference = "Referenced column '{0}' doesn't exist.";

			// Token: 0x04001068 RID: 4200
			internal const string DataTableInvalidExpressionResultType = "The data table must contain an expression yielding a table valued result.";
		}

		// Token: 0x020002E4 RID: 740
		internal static class ExpressionExtensions
		{
			// Token: 0x06001CDE RID: 7390 RVA: 0x0004FC59 File Offset: 0x0004DE59
			internal static string FunctionNameNotLocalized(string functionName)
			{
				return StringUtil.FormatInvariant("The function {0} has not been localized", new object[] { functionName });
			}

			// Token: 0x04001069 RID: 4201
			internal const string NonComposableExpression = "The specified operation may not be composed with other expressions.";
		}

		// Token: 0x020002E5 RID: 741
		internal static class ExpressionSerialization
		{
			// Token: 0x06001CDF RID: 7391 RVA: 0x0004FC6F File Offset: 0x0004DE6F
			internal static string UnsupportedComparisonKind(string comparisonKind)
			{
				return StringUtil.FormatInvariant("Unsupported Comparison Kind: {0}", new object[] { comparisonKind });
			}

			// Token: 0x06001CE0 RID: 7392 RVA: 0x0004FC85 File Offset: 0x0004DE85
			internal static string UnsupportedFunction(string functionName)
			{
				return StringUtil.FormatInvariant("Unsupported Function: {0}", new object[] { functionName });
			}

			// Token: 0x06001CE1 RID: 7393 RVA: 0x0004FC9B File Offset: 0x0004DE9B
			internal static string UnsupportedAllKind(string allKind)
			{
				return StringUtil.FormatInvariant("Unsupported All Kind: {0}", new object[] { allKind });
			}

			// Token: 0x06001CE2 RID: 7394 RVA: 0x0004FCB1 File Offset: 0x0004DEB1
			internal static string UnsupportedGenerateKind(string generateKind)
			{
				return StringUtil.FormatInvariant("Unsupported generate Kind: {0}", new object[] { generateKind });
			}

			// Token: 0x0400106A RID: 4202
			internal const string VariableScopePoppedOutOfOrder = "Popped variable scope does not match pushed scope.";
		}

		// Token: 0x020002E6 RID: 742
		internal static class DaxTranslation
		{
			// Token: 0x06001CE3 RID: 7395 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
			internal static string CouldNotResolveLineageForField(string name)
			{
				return StringUtil.FormatInvariant("Could not locate column {0} in the specified input table.", new object[] { name });
			}

			// Token: 0x06001CE4 RID: 7396 RVA: 0x0004FCDD File Offset: 0x0004DEDD
			internal static string DuplicateUnqualifiedColumnName(string name)
			{
				return StringUtil.FormatInvariant("Multiple columns have the unqualified name '{0}'. The current operation requires unique unqualified column names.", new object[] { name.MarkAsCustomerContent() });
			}

			// Token: 0x06001CE5 RID: 7397 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
			internal static string UnsupportedExtensionFunctionResultType(string type)
			{
				return StringUtil.FormatInvariant("The specified result type '{0}' is not supported for an extension function.", new object[] { type });
			}

			// Token: 0x06001CE6 RID: 7398 RVA: 0x0004FD0E File Offset: 0x0004DF0E
			internal static string UnsupportedQueryParameterResultType(string type)
			{
				return StringUtil.FormatInvariant("The specified result type '{0}' is not supported for a query parameter.", new object[] { type });
			}

			// Token: 0x06001CE7 RID: 7399 RVA: 0x0004FD24 File Offset: 0x0004DF24
			internal static string QueryParameterNotInScope(string name)
			{
				return StringUtil.FormatInvariant("The specified query parameter '{0}' is not in scope.", new object[] { name });
			}

			// Token: 0x06001CE8 RID: 7400 RVA: 0x0004FD3A File Offset: 0x0004DF3A
			internal static string VariableNotInScope(string varName)
			{
				return StringUtil.FormatInvariant("The specified variable '{0}' is not in scope.", new object[] { varName });
			}

			// Token: 0x06001CE9 RID: 7401 RVA: 0x0004FD50 File Offset: 0x0004DF50
			internal static string UnexpectedFunction(string functionName)
			{
				return StringUtil.FormatInvariant("The specified function '{0}' is not supported.", new object[] { functionName });
			}

			// Token: 0x06001CEA RID: 7402 RVA: 0x0004FD66 File Offset: 0x0004DF66
			internal static string UnexpectedOperator(string operatorName)
			{
				return StringUtil.FormatInvariant("The specified operator '{0}' is not supported.", new object[] { operatorName });
			}

			// Token: 0x06001CEB RID: 7403 RVA: 0x0004FD7C File Offset: 0x0004DF7C
			internal static string UnexpectedProjectSubsetStrategy(string projectSubsetStrategy)
			{
				return StringUtil.FormatInvariant("The specified ProjectSubsetStrategy '{0}' is not supported.", new object[] { projectSubsetStrategy });
			}

			// Token: 0x06001CEC RID: 7404 RVA: 0x0004FD92 File Offset: 0x0004DF92
			internal static string UnexpectedComparisonKind(string comparisonKind)
			{
				return StringUtil.FormatInvariant("The specified comparison kind '{0}' is not supported.", new object[] { comparisonKind });
			}

			// Token: 0x06001CED RID: 7405 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
			internal static string UnexpectedLiteralResultType(string literalType)
			{
				return StringUtil.FormatInvariant("The specified literal result type '{0}' is not supported.", new object[] { literalType });
			}

			// Token: 0x06001CEE RID: 7406 RVA: 0x0004FDBE File Offset: 0x0004DFBE
			internal static string UnexpectedDecisionTreeActionType(string actionType)
			{
				return StringUtil.FormatInvariant("The specified decision tree action type '{0}' is not supported.", new object[] { actionType });
			}

			// Token: 0x06001CEF RID: 7407 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
			internal static string UnexpectedAllKind(string allKind)
			{
				return StringUtil.FormatInvariant("The specified all kind '{0}' is not supported.", new object[] { allKind });
			}

			// Token: 0x06001CF0 RID: 7408 RVA: 0x0004FDEA File Offset: 0x0004DFEA
			internal static string UnexpectedGenerateKind(string generateKind)
			{
				return StringUtil.FormatInvariant("The specified generate kind '{0}' is not supported.", new object[] { generateKind });
			}

			// Token: 0x06001CF1 RID: 7409 RVA: 0x0004FE00 File Offset: 0x0004E000
			internal static string UnexpectedDeclarationKind(string declarationKind)
			{
				return StringUtil.FormatInvariant("The specified declaration kind '{0}' is not supported.", new object[] { declarationKind });
			}

			// Token: 0x06001CF2 RID: 7410 RVA: 0x0004FE16 File Offset: 0x0004E016
			internal static string UnexpectedLimitKind(string limitKind)
			{
				return StringUtil.FormatInvariant("The specified limit kind '{0}' is not supported.", new object[] { limitKind });
			}

			// Token: 0x06001CF3 RID: 7411 RVA: 0x0004FE2C File Offset: 0x0004E02C
			internal static string UnexpectedNaturalJoinKind(string joinKind)
			{
				return StringUtil.FormatInvariant("The specified natural join kind '{0}' is not supported.", new object[] { joinKind });
			}

			// Token: 0x06001CF4 RID: 7412 RVA: 0x0004FE42 File Offset: 0x0004E042
			internal static string UnexpectedBatchRoot(string rootType)
			{
				return StringUtil.FormatInvariant("The root of the batch query expression tree has an unexpected type: {0}", new object[] { rootType });
			}

			// Token: 0x0400106B RID: 4203
			internal const string EnsureUniqueUnqualifiedNamesWithoutSelectColumns = "EnsureUniqueUnqualifiedNames cannot be used when SelectColumns is not supported by the target model.";

			// Token: 0x0400106C RID: 4204
			internal const string ExpectedVariableReference = "A variable reference was expected.";

			// Token: 0x0400106D RID: 4205
			internal const string ExpressionNotField = "Unexpected expression encountered, expected to be field expression.";

			// Token: 0x0400106E RID: 4206
			internal const string InvalidDaxExternalContent = "The query contains an invalid external DAX string.";

			// Token: 0x0400106F RID: 4207
			internal const string InvalidGroupKeyExpression = "The specified group key expression is not valid for DAX translation.";

			// Token: 0x04001070 RID: 4208
			internal const string GroupOnRowNumber = "Grouping on RowNumber is not allowed.";

			// Token: 0x04001071 RID: 4209
			internal const string LimitWithoutSort = "Encountered QueryLimitExpression without sort order.";

			// Token: 0x04001072 RID: 4210
			internal const string UnexpectedQueryGroupAndJoinExpression = "QueryGroupAndJoinExpression cannot be used when SummarizeColumns is not supported.";

			// Token: 0x04001073 RID: 4211
			internal const string UnexpectedCurrentGroupExpression = "The function 'CurrentGroup' can only be used with 'GroupBy' which is not supported.";

			// Token: 0x04001074 RID: 4212
			internal const string UnexpectedSortExpression = "Unexpected sort expression encountered. Sort expressions must consist of simple column references on the input table.";

			// Token: 0x04001075 RID: 4213
			internal const string DuplicateColumnsOnDaxExpression = "DaxExpression specifies duplicate columns. Table expression columns must be unique.";

			// Token: 0x04001076 RID: 4214
			internal const string UnexpectedAggregateFunctionArguments = "Unexpected arguments were specified for an aggregate function.";

			// Token: 0x04001077 RID: 4215
			internal const string UnexpectedBinaryFunctionArguments = "Unexpected arguments were specified for a binary function.";

			// Token: 0x04001078 RID: 4216
			internal const string UnexpectedTriaryFunctionArguments = "Unexpected arguments were specified for a triary function.";

			// Token: 0x04001079 RID: 4217
			internal const string UnexpectedUnaryFunctionArguments = "Unexpected arguments were specified for a unary function.";

			// Token: 0x0400107A RID: 4218
			internal const string UnexpectedNaryOperatorArguments = "Unexpected arguments were specified for a N-ary operator.";

			// Token: 0x0400107B RID: 4219
			internal const string UnexpectedDateTimeEqualToSecondFunctionArguments = "Unexpected arguments were specified for the DateTimeEqualToSecond function.";

			// Token: 0x0400107C RID: 4220
			internal const string UnexpectedApplyExpression = "Unexpected QueryApplyExpression encountered.";

			// Token: 0x0400107D RID: 4221
			internal const string UnexpectedGroupItem = "Unexpected group item encountered.";

			// Token: 0x0400107E RID: 4222
			internal const string UnexpectedIsAggregateExpression = "Unexpected QueryIsAggregateExpression encountered.";

			// Token: 0x0400107F RID: 4223
			internal const string UnexpectedProjectExpression = "Unexpected QueryProjectExpression encountered.";

			// Token: 0x04001080 RID: 4224
			internal const string UnexpectedCrossJoinExpression = "Unexpected QueryCrossJoinExpression encountered.";

			// Token: 0x04001081 RID: 4225
			internal const string UnexpectedBatchRootExpression = "Unexpected QueryBatchRootExpression encountered.";

			// Token: 0x04001082 RID: 4226
			internal const string UnexpectedQueryParameterDeclaration = "Unexpected QueryParameterDeclarationExpression encountered.";

			// Token: 0x04001083 RID: 4227
			internal const string UnexpectedTableDeclarationExpression = "Unexpected QueryTableDeclarationExpression encountered. Table declarations must be at the root of the query.";

			// Token: 0x04001084 RID: 4228
			internal const string UnexpectedStartAt = "Unexpected QueryStartAtExpression encountered.";

			// Token: 0x04001085 RID: 4229
			internal const string UnexpectedScalarEntityReference = "Unexpected QueryScalarEntityReferenceExpression encountered.";

			// Token: 0x04001086 RID: 4230
			internal const string UnexpectedNewInstance = "Unexpected QueryNewInstanceExpression encountered.";

			// Token: 0x04001087 RID: 4231
			internal const string UnsupportedFormatByLocale = "The query uses format by locale expression. This is not supported by the underlying model.";

			// Token: 0x04001088 RID: 4232
			internal const string UnsupportedMinMaxOverStringColumn = "The query uses min or max over a string column. This is not supported by the underlying model.";

			// Token: 0x04001089 RID: 4233
			internal const string UnsupportedMinMaxOverStringExpression = "The query uses min or max over a string expression. This is not supported by the underlying model.";
		}

		// Token: 0x020002E7 RID: 743
		internal static class Filter
		{
			// Token: 0x0400108A RID: 4234
			internal const string CannotNestFilter = "Cannot nest a Filter in another Filter.";

			// Token: 0x0400108B RID: 4235
			internal const string UnhandledFilterCondition = "Cannot generate Predicate for this FilterCondition.";
		}

		// Token: 0x020002E8 RID: 744
		internal static class Groups
		{
			// Token: 0x0400108C RID: 4236
			internal const string SortInfoMustReferToValidGroupDetail = "The GroupDetail referenced by the GroupDetailSortInfo must be in the Group's GroupDetails collection.";
		}

		// Token: 0x020002E9 RID: 745
		internal static class QdmExpressions
		{
			// Token: 0x0400108D RID: 4237
			internal const string NoEntityPlaceholders = "The specified expression does not contain any entity placeholders.";

			// Token: 0x0400108E RID: 4238
			internal const string UnsupportedMultiEntityFilterConfiguration = "The query filters contain multiple multi-entity filters, and one of those has a subset of the expressions filtered in the other.";

			// Token: 0x0400108F RID: 4239
			internal const string UnsupportedMultiEntityFilter = "The query contains multi-entity filters which are not supported with the current model.";

			// Token: 0x04001090 RID: 4240
			internal const string InvalidFilterExceedsMaxNumberOfValuesForInFilter = "The count of expressions in the Values argument to the In operator exceeds the maximum number allowed.";

			// Token: 0x04001091 RID: 4241
			internal const string InvalidFilterExceedsMaxNumberOfValuesForInFilterTreeRewrite = "The count of expressions in the Values argument to the In operator exceeds the maximum number allowed when rewriting to a binary tree of ORs and ANDs.";

			// Token: 0x04001092 RID: 4242
			internal const string InvalidInFilterWithDuplicateColumns = "The duplicate expressions in the In filter do not have identical values. They cannot be deduped.";
		}

		// Token: 0x020002EA RID: 746
		internal static class QdmCommandTreeTranslation
		{
			// Token: 0x06001CF5 RID: 7413 RVA: 0x0004FE58 File Offset: 0x0004E058
			internal static string RollupNotConsistentWithGrouping(string groupName, string expectedGroupNameFromRollup)
			{
				return StringUtil.FormatInvariant("The query contains a Rollup structure that is not consistent with the grouping structure.  Rollup expected group '{0}' but the grouping contained '{1}'", new object[] { expectedGroupNameFromRollup, groupName });
			}

			// Token: 0x06001CF6 RID: 7414 RVA: 0x0004FE72 File Offset: 0x0004E072
			internal static string UnsupportedLimitOperator(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported LimitOperator type: {0}", new object[] { typeName });
			}

			// Token: 0x04001093 RID: 4243
			internal const string RollupOnMultipleNonContiguousGroups = "Rollup was specified on multiple non-contiguous groups. Rollup may only be specified on a single set of contiguous groups.";

			// Token: 0x04001094 RID: 4244
			internal const string RollupOnNonProjectedGroup = "Rollup was specified on a group that is not projected. Rollup is not allowed on non-projected groups.";

			// Token: 0x04001095 RID: 4245
			internal const string InvalidPostRegroupLimit = "The query contains a grouping structure that is not compatible with the post-regroup limit. The limit must cover all groups after the first limited group. Non-projected groups must be contiguous with the first limited group.";

			// Token: 0x04001096 RID: 4246
			internal const string RollupOutsidePostRegroupLimit = "The query contains a Rollup that refers to a group not covered by the post-regroup Limit. When the query has a post-regroup limit, all Rollups may only refer to groups inside that Limit.";
		}

		// Token: 0x020002EB RID: 747
		internal static class QdmSerialization
		{
			// Token: 0x06001CF7 RID: 7415 RVA: 0x0004FE88 File Offset: 0x0004E088
			internal static string UnsupportedObjectType(string typeName)
			{
				return StringUtil.FormatInvariant("Unsupported type for serialization: {0}", new object[] { typeName });
			}
		}

		// Token: 0x020002EC RID: 748
		internal static class BatchableQueryExecutionProvider
		{
			// Token: 0x04001097 RID: 4247
			internal const string QueryExecutionContextsMustBeUnique = "The specified QueryExecutionContext collection cannot contain any duplicate.";

			// Token: 0x04001098 RID: 4248
			internal const string QueryExecutionContextAlreadyExistsInBatch = "One or more of the specified QueryExecutionContext already exists in this batch of queries.";

			// Token: 0x04001099 RID: 4249
			internal const string CannotCloseBatchWhenThereIsNoOpenBatch = "Cannot CloseBatch() when there is no open batch.";
		}
	}
}
