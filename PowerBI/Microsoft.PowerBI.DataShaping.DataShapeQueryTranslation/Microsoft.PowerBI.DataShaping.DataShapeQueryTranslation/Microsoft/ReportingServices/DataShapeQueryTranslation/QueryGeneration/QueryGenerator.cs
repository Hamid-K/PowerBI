using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200008E RID: 142
	internal sealed class QueryGenerator : DataSetPlanElementVisitor
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x00017FCC File Offset: 0x000161CC
		private QueryGenerator(QueryGenerationContext context, DataSetPlan dataSetPlan, DefaultContextManager defaultContextManager)
		{
			this.m_context = context;
			this.m_dataSetPlan = dataSetPlan;
			bool flag = defaultContextManager != null;
			FieldRelationshipAnnotations defaultFieldRelationshipAnnotations = this.m_context.Schema.GetDefaultFieldRelationshipAnnotations();
			ColumnGroupingAnnotations defaultColumnGroupingAnnotations = this.m_context.Schema.GetDefaultColumnGroupingAnnotations();
			DefaultContextManager defaultContextManager2 = defaultContextManager ?? new DefaultContextManager(defaultFieldRelationshipAnnotations, defaultColumnGroupingAnnotations);
			FederatedEntityDataModel model = this.m_context.Model;
			this.m_queryBuilder = new QueryBuilder((model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), defaultContextManager2, flag, false, null);
			this.m_activeGroups = new List<GroupAndSortingContext>();
			this.m_activeSorting = new List<KeyValuePair<IScope, List<SortKeyContext>>>();
			this.m_activeLimits = new List<QueryLimitConstraintContext>();
			this.m_measureJoinPredicates = new List<IJoinPredicate>();
			this.m_expressionTable = this.m_context.ExpressionTable.CreateEmptyWritableTable();
			this.m_queryParameterMap = new WritableGeneratedQueryParameterMap();
			SubQueryGenerator subQueryGenerator = new SubQueryGenerator(context, defaultContextManager2);
			List<IScope> scopesInMeasureContext = dataSetPlan.GetScopesInMeasureContext(this.m_context.ScopeTree);
			this.m_expressionGenerator = new QueryExpressionGenerator(subQueryGenerator, context, this.m_expressionTable, scopesInMeasureContext, this.m_queryParameterMap);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000180F0 File Offset: 0x000162F0
		public static ReadOnlyCollection<QueryGenerationResult> GenerateAll(QueryGenerationContext context, DataSetPlanningResult dataSetPlanningResult)
		{
			List<QueryGenerationResult> list = new List<QueryGenerationResult>(dataSetPlanningResult.DataSetPlans.Count);
			foreach (DataSetPlan dataSetPlan in dataSetPlanningResult.DataSetPlans)
			{
				QueryGenerationResult queryGenerationResult = QueryGenerator.GenerateQueryAndQueryTrimmer(context, dataSetPlan);
				list.Add(queryGenerationResult);
			}
			return list.AsReadOnly();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001815C File Offset: 0x0001635C
		public static QueryGenerationResult GenerateSubQuery(QueryGenerationContext context, DataSetPlan dataSetPlan, DefaultContextManager defaultContextManager)
		{
			return new QueryGenerator(context, dataSetPlan, defaultContextManager).Generate();
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001816C File Offset: 0x0001636C
		public static QueryGenerationResult GenerateQueryAndQueryTrimmer(QueryGenerationContext context, DataSetPlan dataSetPlan)
		{
			QueryGenerator queryGenerator = new QueryGenerator(context, dataSetPlan, null);
			QueryGenerationResult queryGenerationResult = queryGenerator.Generate();
			if (queryGenerationResult == null)
			{
				return null;
			}
			QueryDefinition queryDefinition = queryGenerationResult.QueryDefinition;
			if (queryDefinition != null)
			{
				if (queryDefinition.HasUnconstrainedJoin())
				{
					context.ErrorContext.Register(TranslationMessages.InvalidUnconstrainedJoin(EngineMessageSeverity.Error, ObjectType.DataShape, context.DataShape.Id, "Id"));
					return null;
				}
				QueryGenerator.ValidateLimits(context, queryDefinition.Limits, queryDefinition.Groups);
			}
			QueryTrimmer queryTrimmer = new QueryTrimmer(context.ScopeTree, context.ExpressionTable, queryGenerator.m_activeGroups, dataSetPlan);
			return new QueryGenerationResult(queryGenerationResult.DataSetPlan, queryGenerationResult.QueryDefinition, queryGenerationResult.ExpressionTable, queryGenerationResult.AggregateIndicatorFieldNames, queryGenerationResult.ConstraintStatus, queryGenerationResult.QueryParameterMap, new QueryTrimmer(queryTrimmer.GetNonProjectedGroupsToTrimFromQuery));
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00018228 File Offset: 0x00016428
		private static void ValidateLimits(QueryGenerationContext context, QdmItemCollection<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Limit> limits, QdmNamedItemCollection<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group> allGroups)
		{
			SortItem sortItem = null;
			Func<string, IEnumerable<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group>> <>9__0;
			foreach (Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Limit limit in limits)
			{
				IEnumerable<string> groupRefs = limit.GroupRefs;
				Func<string, IEnumerable<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group>> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (string groupRefName) => allGroups.Where((Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group group) => group.GroupNameEquals(groupRefName)));
				}
				IEnumerable<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group> enumerable = groupRefs.SelectMany(func);
				using (IEnumerator<SortItem> enumerator2 = limit.Sorting.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SortItem sort = enumerator2.Current;
						if (!enumerable.Any((Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group group) => group.HasNamedItem(sort.Name)))
						{
							sortItem = sort;
							break;
						}
					}
				}
			}
			if (sortItem != null)
			{
				context.ErrorContext.Register(TranslationMessages.OverlappingKeysOnOppositeHierarchies(EngineMessageSeverity.Error, context.DataShape.ObjectType, context.DataShape.Id, "Value"));
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00018340 File Offset: 0x00016540
		private QueryGenerationResult Generate()
		{
			QueryGenerationResult queryGenerationResult;
			try
			{
				QueryExtensionSchemaTranslator.Translate(this.m_dataSetPlan.ExtensionSchema, this.m_queryBuilder, this.m_context.Model, this.m_context.Schema, this.m_context.ExpressionTable, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
				this.DeclareQueryParameters(this.m_queryBuilder);
				this.DeclareDataSourceVariables(this.m_queryBuilder);
				this.DeclareMParameters(this.m_queryBuilder);
				this.AddDataSetPlanElementsToQuery();
				ReadOnlyDictionary<DataMember, string> readOnlyDictionary = ((this.m_querySubtotalGenerator != null) ? this.m_querySubtotalGenerator.Generate(this.m_queryBuilder) : QuerySubtotalGenerator.CreateEmptyAggregatorFieldNamesMap());
				if (this.m_context.ErrorContext.HasError)
				{
					queryGenerationResult = null;
				}
				else
				{
					if (!this.m_dataSetPlan.FilterEmptyGroups)
					{
						this.m_queryBuilder.SetAllowBlankRow();
					}
					QueryConstraintStatus? queryConstraintStatus = this.AddWindowingLimit();
					if (this.m_measureJoinPredicates.Count > 0)
					{
						this.m_queryBuilder.SetJoinPredicates(this.m_measureJoinPredicates);
					}
					QueryDefinition queryDefinition = this.m_queryBuilder.GetQueryDefinition(false);
					queryGenerationResult = new QueryGenerationResult(this.m_dataSetPlan, queryDefinition, this.m_expressionTable.AsReadOnly(), readOnlyDictionary, queryConstraintStatus, this.m_queryParameterMap, null);
				}
			}
			catch (QueryGenerationException)
			{
				queryGenerationResult = null;
			}
			catch (CommandTreeTranslationException ex)
			{
				if (!QueryGenerationUtils.TryHandleCommandTreeTranslationException(ex, this.m_context.ErrorContext, this.m_dataSetPlan.Name))
				{
					throw;
				}
				queryGenerationResult = null;
			}
			return queryGenerationResult;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000184C8 File Offset: 0x000166C8
		private void DeclareQueryParameters(QueryBuilder batchBuilder)
		{
			QueryParameterGenerator.Generate(this.m_dataSetPlan.QueryParameters, batchBuilder, this.m_queryParameterMap);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000184E1 File Offset: 0x000166E1
		private void DeclareDataSourceVariables(QueryBuilder queryBuilder)
		{
			if (string.IsNullOrEmpty(this.m_dataSetPlan.DataSourceVariables))
			{
				return;
			}
			queryBuilder.DeclareDataSourceVariables(this.m_dataSetPlan.DataSourceVariables);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00018508 File Offset: 0x00016708
		private void DeclareMParameters(QueryBuilder queryBuilder)
		{
			if (this.m_dataSetPlan.ModelParameters.IsNullOrEmpty<ModelParameter>())
			{
				return;
			}
			foreach (ModelParameter modelParameter in this.m_dataSetPlan.ModelParameters)
			{
				QueryMParameterDeclarationExpression queryMParameterDeclarationExpression = QueryGenerationUtils.ConvertToQueryMParameterDeclarationExpression(modelParameter, this.m_context.ErrorContext, this.m_expressionGenerator);
				queryBuilder.DeclareMParameter(queryMParameterDeclarationExpression);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00018584 File Offset: 0x00016784
		private QueryConstraintStatus? AddWindowingLimit()
		{
			if (this.m_dataSetPlan.SuppressSortingAndLimits)
			{
				return null;
			}
			QueryDataWindowGenerationResult queryDataWindowGenerationResult = QueryDataWindowGenerator.Generate(this.m_context, this.m_activeGroups, this.m_activeLimits, this.m_startAtValues);
			if (queryDataWindowGenerationResult.NeedsWindowLimit)
			{
				this.m_queryBuilder.AddTopLevelLimit(queryDataWindowGenerationResult.WindowLimit);
			}
			if (!queryDataWindowGenerationResult.Status.IsConstrained)
			{
				this.m_context.Tracer.SanitizedTrace(TraceLevel.Info, "Query is unconstrained. Reasons: {0}", queryDataWindowGenerationResult.Status.Reasons);
			}
			return new QueryConstraintStatus?(queryDataWindowGenerationResult.Status);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00018624 File Offset: 0x00016824
		private void AddDataSetPlanElementsToQuery()
		{
			ReadOnlyCollection<ScopePlanElement> scopes = this.m_dataSetPlan.Scopes;
			int innermostProjectedGroupIndex = this.m_dataSetPlan.GetInnermostProjectedGroupIndex();
			for (int i = 0; i < scopes.Count; i++)
			{
				this.m_currentScopeShouldIncludeMeasuresAsDetails = i < innermostProjectedGroupIndex;
				this.m_context.CancellationToken.ThrowIfCancellationRequested();
				ScopePlanElement scopePlanElement = scopes[i];
				scopePlanElement.Accept(this);
				foreach (DataSetPlanElement dataSetPlanElement in scopePlanElement.NestedElements)
				{
					this.m_context.CancellationToken.ThrowIfCancellationRequested();
					dataSetPlanElement.Accept(this);
				}
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000186DC File Offset: 0x000168DC
		private void WriteSortingAndScopeValuesToTable(DataMember member, IEnumerable<string> fieldNames, string aggregateIndicatorFieldName)
		{
			List<ScopeValueDefinition> list = null;
			if (member.Group.ScopeIdDefinition != null)
			{
				list = member.Group.ScopeIdDefinition.Values;
			}
			List<SortKey> sortKeys = member.Group.SortKeys;
			int num = fieldNames.Count<string>();
			Microsoft.DataShaping.Contract.RetailAssert(list == null || list.Count == num || num == 0, "ScopeIdDefinition.Values should have the same count as fieldNames.");
			int num2 = 0;
			foreach (string text in fieldNames)
			{
				if (!(text == aggregateIndicatorFieldName))
				{
					if (list != null)
					{
						ScopeValueDefinition scopeValueDefinition = list[num2];
						DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, text, null);
						this.m_expressionTable.SetNode(scopeValueDefinition.Value, dataSetFieldReferenceExpressionNode);
					}
					if (sortKeys != null)
					{
						SortKey sortKey = sortKeys[num2];
						DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode2 = new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, text, null);
						this.m_expressionTable.SetNode(sortKey.Value, dataSetFieldReferenceExpressionNode2);
					}
					num2++;
				}
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000187E4 File Offset: 0x000169E4
		internal override void Visit(DataShapePlanElement planElement)
		{
			DataShape dataShape = planElement.DataShape;
			if (planElement.SubQueryJoinPredicates != null)
			{
				IReadOnlyList<DataSetPlan> subQueryJoinPredicates = planElement.SubQueryJoinPredicates;
				List<QueryDefinition> list = new List<QueryDefinition>(subQueryJoinPredicates.Count);
				for (int i = 0; i < subQueryJoinPredicates.Count; i++)
				{
					QueryDefinition queryDefinition = QueryGenerator.GenerateSubQuery(this.m_context, subQueryJoinPredicates[i], null).QueryDefinition;
					if (queryDefinition != null)
					{
						list.Add(queryDefinition);
					}
				}
				this.m_queryBuilder.MergeJoinPredicates(list, this.m_context.FeatureSwitchProvider, this.m_context.CancellationToken);
			}
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filterCondition = QueryFilterGenerator.CreateFilter(planElement.FilterCondition, this.m_expressionGenerator, this.m_context.ErrorContext, dataShape.Id, this.m_context.CancellationToken);
			this.m_queryBuilder.AddSlicer(filterCondition);
			foreach (DataSetPlan dataSetPlan in planElement.ApplyFilters.EmptyIfNull<DataSetPlan>())
			{
				QueryGenerationResult queryGenerationResult = QueryGenerator.GenerateSubQuery(this.m_context, dataSetPlan, null);
				if (queryGenerationResult != null)
				{
					this.m_queryBuilder.AddApplyFilter(queryGenerationResult.QueryDefinition);
				}
			}
			if (planElement.ValueFilter != null)
			{
				Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filterCondition2 = QueryFilterGenerator.CreateFilter(planElement.ValueFilter, this.m_expressionGenerator, this.m_context.ErrorContext, dataShape.Id, this.m_context.CancellationToken);
				this.m_queryBuilder.SetTopLevelValueFilter(filterCondition2);
			}
			bool flag = this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			if (!planElement.AnyValueFilters.IsNullOrEmpty<AnyValueFilterCondition>())
			{
				for (int j = 0; j < planElement.AnyValueFilters.Count; j++)
				{
					AnyValueFilterCondition anyValueFilterCondition = planElement.AnyValueFilters[j];
					if (flag)
					{
						Action<IConceptualColumn> action = new Action<IConceptualColumn>(this.m_queryBuilder.AddColumnRequiringClearedContext);
						if (anyValueFilterCondition.DefaultValueOverridesAncestors)
						{
							action = new Action<IConceptualColumn>(this.m_queryBuilder.AddColumnRequiringImplicitGroupingClearedContext);
						}
						this.ProcessTargets(action, dataShape.Id, anyValueFilterCondition.Targets, ObjectType.AnyValueFilterCondition);
					}
					else
					{
						Action<IEdmFieldInstance> action2 = new Action<IEdmFieldInstance>(this.m_queryBuilder.AddFieldRequiringClearedContext);
						if (anyValueFilterCondition.DefaultValueOverridesAncestors)
						{
							action2 = new Action<IEdmFieldInstance>(this.m_queryBuilder.AddFieldRequiringImplicitGroupingClearedContext);
						}
						this.ProcessTargets(action2, dataShape.Id, anyValueFilterCondition.Targets, ObjectType.AnyValueFilterCondition);
					}
				}
			}
			if (planElement.DefaultValueFilter != null)
			{
				if (flag)
				{
					this.ProcessTargets(new Action<IConceptualColumn>(this.m_queryBuilder.AddColumnRequiringDefaultContext), dataShape.Id, planElement.DefaultValueFilter.Targets, ObjectType.DefaultValueFilterCondition);
					return;
				}
				this.ProcessTargets(new Action<IEdmFieldInstance>(this.m_queryBuilder.AddFieldRequiringDefaultContext), dataShape.Id, planElement.DefaultValueFilter.Targets, ObjectType.DefaultValueFilterCondition);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00018AA0 File Offset: 0x00016CA0
		private void ProcessTargets(Action<IEdmFieldInstance> addField, Identifier parentId, List<Expression> targets, ObjectType filterConditionType)
		{
			foreach (Expression expression in targets)
			{
				List<IEdmFieldInstance> list = (this.m_expressionGenerator.TranslateFilterExpression(expression.ExpressionId.Value, new ExpressionContext(this.m_context.ErrorContext, filterConditionType, parentId, "Target")).QueryExpression as QueryFieldExpression).GetReferencedFields().ToList<IEdmFieldInstance>();
				Microsoft.DataShaping.Contract.RetailAssert(list.Count == 1, "Expected 1 referenced field.");
				addField(list[0]);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00018B50 File Offset: 0x00016D50
		private void ProcessTargets(Action<IConceptualColumn> addColumn, Identifier parentId, List<Expression> targets, ObjectType filterConditionType)
		{
			foreach (Expression expression in targets)
			{
				List<IConceptualColumn> list = (this.m_expressionGenerator.TranslateFilterExpression(expression.ExpressionId.Value, new ExpressionContext(this.m_context.ErrorContext, filterConditionType, parentId, "Target")).QueryExpression as QueryFieldExpression).GetReferencedColumns().ToList<IConceptualColumn>();
				Microsoft.DataShaping.Contract.RetailAssert(list.Count == 1, "Expected 1 referenced field.");
				addColumn(list[0]);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00018C00 File Offset: 0x00016E00
		internal override void Visit(DataMemberPlanElement planElement)
		{
			DataMember dataMember = planElement.DataMember;
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey> groupKeys = group.GroupKeys;
			List<GroupReference> list = new List<GroupReference>();
			foreach (GroupCluster groupCluster in GroupClusterBuilder.Build(group, this.m_context.ExpressionTable))
			{
				List<string> list2;
				GroupReference groupReference = this.AddGroupClusterToQuery(planElement, groupCluster, out list2);
				list.Add(groupReference);
				this.WriteGroupClusterToExpressionTable(groupCluster, list2, groupReference);
			}
			string text = null;
			RollupRequirement rollupInfo = planElement.RollupInfo;
			if (rollupInfo != null && rollupInfo.Rollup)
			{
				if (this.m_querySubtotalGenerator == null)
				{
					this.m_querySubtotalGenerator = new QuerySubtotalGenerator(this.m_context.Annotations.SubtotalAnnotations, this.m_context.ScopeTree);
				}
				this.m_querySubtotalGenerator.AddRollupInfo(dataMember, rollupInfo.SortDirection, list);
			}
			KeyValuePair<SortKey, QueryExpressionContext>[] array = null;
			List<QueryBuilder.SortDetail> list3 = null;
			if (!this.m_dataSetPlan.SuppressSortingAndLimits)
			{
				list3 = this.BuildSorting(planElement, dataMember, list, out array);
			}
			this.m_activeGroups.Add(new GroupAndSortingContext(dataMember, list.AsReadOnly(), list3, planElement.IsProjected));
			if (!this.m_dataSetPlan.SuppressSortingAndLimits)
			{
				this.AddSortingAndLimitsToQuery(planElement, list, text, list3, array);
			}
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filterCondition = QueryFilterGenerator.CreateFilter(planElement.FilterCondition, this.m_expressionGenerator, this.m_context.ErrorContext, dataMember.Id, this.m_context.CancellationToken);
			this.m_queryBuilder.AddGroupFilter(filterCondition, null);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018D80 File Offset: 0x00016F80
		private void WriteGroupClusterToExpressionTable(GroupCluster groupCluster, List<string> groupKeyNames, GroupReference qdmGroupRef)
		{
			Microsoft.DataShaping.Contract.RetailAssert(groupKeyNames.Count == groupCluster.GroupKeys.Count, "groupCluster.Names should have the same count as groupCluster.GroupKeys");
			for (int i = 0; i < groupCluster.GroupKeys.Count; i++)
			{
				DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, groupKeyNames[i], null);
				this.m_expressionTable.SetNode(groupCluster.GroupKeys[i].Value, dataSetFieldReferenceExpressionNode);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00018DF4 File Offset: 0x00016FF4
		private GroupReference AddGroupClusterToQuery(DataMemberPlanElement planElement, GroupCluster groupCluster, out List<string> groupKeyNames)
		{
			DataMember dataMember = planElement.DataMember;
			List<QueryExpression> list = new List<QueryExpression>();
			groupKeyNames = new List<string>(groupCluster.GroupKeys.Count);
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in groupCluster.GroupKeys)
			{
				string text;
				QueryExpression queryExpression = QueryBuilderExtensions.AddGroupKeyToQuery(this.m_queryBuilder, this.m_expressionGenerator, this.m_context.ErrorContext, groupKey, dataMember.Id, out text);
				list.Add(queryExpression);
				groupKeyNames.Add(text);
			}
			FollowingJoinBehavior followingJoinBehavior = (groupCluster.ShowItemsWithNoData ? FollowingJoinBehavior.OuterJoin : FollowingJoinBehavior.InnerJoin);
			return this.m_queryBuilder.AddOrReuseGroup(list, null, null, !planElement.IsProjected, followingJoinBehavior);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00018EB8 File Offset: 0x000170B8
		private void AddSortingAndLimitsToQuery(DataMemberPlanElement planElement, List<GroupReference> qdmGroupRefs, string aggregateIndicatorFieldName, List<QueryBuilder.SortDetail> sortDetails, KeyValuePair<SortKey, QueryExpressionContext>[] dsqSorts)
		{
			DataMember dataMember = planElement.DataMember;
			bool requiresReversedSortDirection = planElement.RequiresReversedSortDirection;
			this.m_queryBuilder.AddSorting(qdmGroupRefs, sortDetails);
			List<SortKeyContext> list;
			List<string> sorting = this.GetSorting(dataMember, qdmGroupRefs, dsqSorts, this.m_activeSorting, out list);
			this.m_activeSorting.Add(new KeyValuePair<IScope, List<SortKeyContext>>(dataMember, list));
			this.WriteSortingAndScopeValuesToTable(dataMember, sorting, aggregateIndicatorFieldName);
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit = planElement.Limit;
			this.AddDataMemberLimit(planElement, qdmGroupRefs, sortDetails);
			if (planElement.IsProjected && !planElement.OmitStartAt)
			{
				this.AddStartAtValues(dataMember, list);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00018F38 File Offset: 0x00017138
		private List<QueryBuilder.SortDetail> BuildSorting(DataMemberPlanElement planElement, DataMember dataMember, List<GroupReference> qdmGroupRefs, out KeyValuePair<SortKey, QueryExpressionContext>[] dsqSorts)
		{
			bool requiresReversedSortDirection = planElement.RequiresReversedSortDirection;
			List<SortKey> sortKeys = dataMember.Group.SortKeys;
			List<QueryBuilder.SortDetail> list = new List<QueryBuilder.SortDetail>();
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.SortKey, dataMember.Id, "Value");
			dsqSorts = new KeyValuePair<SortKey, QueryExpressionContext>[(sortKeys != null) ? sortKeys.Count : 0];
			if (sortKeys != null)
			{
				for (int i = 0; i < sortKeys.Count; i++)
				{
					SortKey sortKey = sortKeys[i];
					ExpressionId value = sortKey.Value.ExpressionId.Value;
					QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateSortExpression(value, expressionContext);
					Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection = sortKey.SortDirection.Value;
					if (requiresReversedSortDirection)
					{
						sortDirection = sortDirection.ReverseSortDirection();
					}
					QueryBuilder.SortDetail sortDetail = this.BuildQdmSortDetail(dataMember, sortDirection.ToQdmSortDirection(), queryExpressionContext, qdmGroupRefs, value, expressionContext);
					list.Add(sortDetail);
					dsqSorts[i] = new KeyValuePair<SortKey, QueryExpressionContext>(sortKey, queryExpressionContext);
				}
			}
			return list;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00019030 File Offset: 0x00017230
		private QueryBuilder.SortDetail BuildQdmSortDetail(DataMember dataMember, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection sortDirection, QueryExpressionContext queryExprContext, List<GroupReference> qdmGroupRefs, ExpressionId exprId, ExpressionContext exprContext)
		{
			GroupReference groupReference = null;
			if (!this.TryFindCorrespondingQdmGroupReference(qdmGroupRefs, queryExprContext.QueryExpression, out groupReference))
			{
				groupReference = this.FindInnermostCompatibleGroupReference(qdmGroupRefs, queryExprContext.QueryExpression, exprId, exprContext);
			}
			return this.BuildSortDetail(sortDirection, queryExprContext, groupReference);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00019070 File Offset: 0x00017270
		private List<string> GetSorting(IScope dataMember, List<GroupReference> qdmGroupRefs, KeyValuePair<SortKey, QueryExpressionContext>[] dsqSorts, List<KeyValuePair<IScope, List<SortKeyContext>>> activeSorting, out List<SortKeyContext> sortKeyContexts)
		{
			IList<SortItem> list = this.m_queryBuilder.GetSorting(qdmGroupRefs).Evaluate<SortItem>();
			List<string> list2 = new List<string>();
			new Dictionary<SortKey, string>();
			sortKeyContexts = new List<SortKeyContext>();
			if (list.Count > dsqSorts.Length)
			{
				List<SortItem> list3 = new List<SortItem>();
				foreach (SortItem sortItem in list)
				{
					bool flag = false;
					foreach (KeyValuePair<SortKey, QueryExpressionContext> keyValuePair in dsqSorts)
					{
						if (sortItem.HasSameExpression(keyValuePair.Value.QueryExpression))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						list3.Add(sortItem);
					}
				}
				list = list3;
			}
			int num = 0;
			foreach (KeyValuePair<SortKey, QueryExpressionContext> keyValuePair2 in dsqSorts)
			{
				bool flag2 = false;
				for (int j = num; j < list.Count; j++)
				{
					SortItem sortItem2 = list[j];
					if (sortItem2.HasSameExpression(keyValuePair2.Value.QueryExpression))
					{
						flag2 = true;
						num++;
						bool flag3 = this.IsSortKeyReused(activeSorting, keyValuePair2, sortItem2.Name);
						sortKeyContexts.Add(new SortKeyContext(keyValuePair2.Key, keyValuePair2.Value, flag3, false, sortItem2.Name));
						list2.Add(sortItem2.Name);
						break;
					}
				}
				if (list.Any<SortItem>() && !flag2)
				{
					string text = this.AddGroupDetailToQuery(keyValuePair2.Value.QueryExpression, dataMember.Id.Value, keyValuePair2.Key.Value.ExpressionId.Value, new ExpressionContext(this.m_context.ErrorContext, ObjectType.SortKey, dataMember.Id, "Value"));
					sortKeyContexts.Add(new SortKeyContext(keyValuePair2.Key, keyValuePair2.Value, false, true, text));
					list2.Add(text);
				}
			}
			return list2;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00019278 File Offset: 0x00017478
		private bool IsSortKeyReused(List<KeyValuePair<IScope, List<SortKeyContext>>> activeSorting, KeyValuePair<SortKey, QueryExpressionContext> dsqSort, string sortItemName)
		{
			bool flag = false;
			if (activeSorting.Any<KeyValuePair<IScope, List<SortKeyContext>>>())
			{
				foreach (SortKeyContext sortKeyContext in activeSorting.Select((KeyValuePair<IScope, List<SortKeyContext>> s) => s.Value).SelectMany((List<SortKeyContext> v) => v))
				{
					flag = sortKeyContext.IsSameSorting(dsqSort, sortItemName);
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00019318 File Offset: 0x00017518
		private string AddGroupDetailToQuery(QueryExpression queryExpression, string fallbackCandidateName, ExpressionId detailExprId, ExpressionContext exprContext)
		{
			GroupReference groupReference = this.FindInnermostCompatibleGroupReference(queryExpression, detailExprId, exprContext);
			return this.m_queryBuilder.AddGroupDetailToQuery(groupReference, queryExpression, fallbackCandidateName);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00019340 File Offset: 0x00017540
		private bool TryFindCorrespondingQdmGroupReference(List<GroupReference> qdmGroupRefs, QueryExpression queryExpression, out GroupReference correspondingQdmGroupRef)
		{
			correspondingQdmGroupRef = null;
			foreach (GroupReference groupReference in qdmGroupRefs)
			{
				using (IEnumerator<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.GroupKey> enumerator2 = groupReference.Group.Keys.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Expression == queryExpression)
						{
							correspondingQdmGroupRef = groupReference;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000193D8 File Offset: 0x000175D8
		private void AddDataMemberLimit(DataMemberPlanElement planElement, IEnumerable<GroupReference> qdmGroupRefs, IList<QueryBuilder.SortDetail> sortDetails)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit = planElement.Limit;
			if (limit == null)
			{
				return;
			}
			DataMember dataMember = planElement.DataMember;
			QueryLimitGenerator.Generate(limit.Operator);
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = this.m_context.ExpressionTable.GetNode(limit.Within) as ResolvedScopeReferenceExpressionNode;
			if (this.CanUsePostRegroupLimit(planElement))
			{
				List<DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(this.m_context.ExpressionTable);
				IEnumerable<GroupReference> enumerable = this.TranslateScopesToGroupReferences(groupScopesFromTargets);
				this.AddPostRegroupLimit(limit, enumerable, this.GetSortDetailsForLimit(limit, this.GetSortDetailsFromScopes(groupScopesFromTargets).Evaluate<QueryBuilder.SortDetail>()));
				return;
			}
			this.AddRegularLimit(limit, qdmGroupRefs, resolvedScopeReferenceExpressionNode.Scope, dataMember, sortDetails);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00019470 File Offset: 0x00017670
		private bool CanUsePostRegroupLimit(DataMemberPlanElement planElement)
		{
			if (!planElement.IsProjected)
			{
				return false;
			}
			int count = this.m_activeGroups.Count;
			if (count <= 1)
			{
				return false;
			}
			IList<GroupAndSortingContext> list = this.m_activeGroups.Take(count - 1).Evaluate<GroupAndSortingContext>();
			bool flag = planElement.RollupInfo != null && planElement.RollupInfo.Rollup;
			bool flag2 = list.Any((GroupAndSortingContext g) => !g.IsProjected);
			if (!flag2 && !flag)
			{
				return false;
			}
			if (list.Where((GroupAndSortingContext d) => d.Scope.ContextOnly).Any<GroupAndSortingContext>() && flag)
			{
				return false;
			}
			if (this.m_context.DataShape.GetParentDataShape(this.m_context.ScopeTree, this.m_context.Annotations) == null)
			{
				DataShape dataShape = this.m_context.DataShape;
			}
			return !(from e in this.m_dataSetPlan.Scopes.TakeAfter(planElement)
				where e.IsProjected && e.Limit != null
				select e).Any<ScopePlanElement>() || !flag2 || flag;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000195A0 File Offset: 0x000177A0
		private void AddRegularLimit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit, IEnumerable<GroupReference> qdmGroupRefs, IScope withinScope, IScope targetScope, IList<QueryBuilder.SortDetail> sortDetails)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator topLimitOperator = ((limit != null) ? limit.Operator : null) as Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator;
			if (limit.Operator != null && (topLimitOperator == null || topLimitOperator.Skip == null))
			{
				Microsoft.DataShaping.Contract.RetailAssert(!sortDetails.IsNullOrEmpty<QueryBuilder.SortDetail>(), "SortDetails cannot be empty when any limit operator, except top limit with skip, is used");
			}
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator limitOperator = QueryLimitGenerator.Generate(limit.Operator);
			IEnumerable<GroupReference> enumerable;
			IList<QueryBuilder.SortDetail> list;
			if (limit.Targets.Count > 1)
			{
				List<DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(this.m_context.ExpressionTable);
				enumerable = this.TranslateScopesToGroupReferences(groupScopesFromTargets);
				list = this.GetSortDetailsForLimit(limit, this.GetSortDetailsFromScopes(groupScopesFromTargets).Evaluate<QueryBuilder.SortDetail>());
			}
			else
			{
				enumerable = qdmGroupRefs;
				list = this.GetSortDetailsForLimit(limit, sortDetails);
			}
			this.m_queryBuilder.AddLimit(enumerable, limitOperator, list);
			this.m_activeLimits.Add(new QueryLimitConstraintContext(enumerable, limitOperator, false));
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00019670 File Offset: 0x00017870
		private IList<QueryBuilder.SortDetail> GetSortDetailsForLimit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit, IList<QueryBuilder.SortDetail> sortDetails)
		{
			IList<QueryBuilder.SortDetail> list = null;
			if (limit.Operator.ObjectType == ObjectType.BottomLimitOperator)
			{
				list = sortDetails.Select((QueryBuilder.SortDetail s) => s.ReverseSortDirection()).Evaluate<QueryBuilder.SortDetail>();
			}
			return list;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000196BC File Offset: 0x000178BC
		private void AddPostRegroupLimit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit, IEnumerable<GroupReference> targetGroups, IEnumerable<QueryBuilder.SortDetail> sortDetails)
		{
			if (limit == null)
			{
				return;
			}
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator limitOperator = QueryLimitGenerator.Generate(limit.Operator);
			if (this.m_postRegroupLimit == null)
			{
				this.m_postRegroupLimit = limit;
			}
			if (limit != this.m_postRegroupLimit)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidMultiplePostRegroupLimit(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, null, ObjectType.Limit, this.m_postRegroupLimit.Id));
				return;
			}
			this.m_queryBuilder.AddPostRegroupLimit(targetGroups, limitOperator, sortDetails);
			this.m_activeLimits.Add(new QueryLimitConstraintContext(targetGroups, limitOperator, true));
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001974C File Offset: 0x0001794C
		private IEnumerable<GroupReference> TranslateScopesToGroupReferences(IEnumerable<IScope> scopes)
		{
			List<DataMember> list = scopes.Select((IScope s) => s as DataMember).ToList<DataMember>();
			List<GroupReference> list2 = new List<GroupReference>();
			using (List<DataMember>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataMember d = enumerator.Current;
					list2.AddRange(this.m_activeGroups.Where((GroupAndSortingContext g) => g.Scope == d).SelectMany((GroupAndSortingContext g) => g.GroupReferences));
				}
			}
			return list2;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00019810 File Offset: 0x00017A10
		private IEnumerable<QueryBuilder.SortDetail> GetSortDetailsFromScopes(IEnumerable<IScope> scopes)
		{
			List<DataMember> list = scopes.Select((IScope s) => s as DataMember).ToList<DataMember>();
			List<QueryBuilder.SortDetail> list2 = new List<QueryBuilder.SortDetail>();
			using (List<DataMember>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataMember d = enumerator.Current;
					list2.AddRange(this.m_activeGroups.Where((GroupAndSortingContext g) => g.Scope == d && g.SortDetails != null).SelectMany((GroupAndSortingContext g) => g.SortDetails));
				}
			}
			return list2;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000198D4 File Offset: 0x00017AD4
		private void AddStartAtValues(DataMember dataMember, List<SortKeyContext> sortKeyContexts)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			if (group.StartPosition == null)
			{
				return;
			}
			this.ValidateStartAtValues(dataMember, sortKeyContexts);
			for (int i = 0; i < sortKeyContexts.Count; i++)
			{
				SortKeyContext sortKeyContext = sortKeyContexts[i];
				if (!sortKeyContext.IsReused && !sortKeyContext.IsAddAsDetail)
				{
					ScopeValue scopeValue = group.StartPosition.Values[i];
					this.m_queryBuilder.AddStartAt(scopeValue.Value.Value);
					Microsoft.DataShaping.Util.AddToLazyList<KeyValuePair<SortKeyContext, ScalarValue>>(ref this.m_startAtValues, Microsoft.DataShaping.Util.ToKeyValuePair<SortKeyContext, ScalarValue>(sortKeyContext, scopeValue.Value.Value));
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00019968 File Offset: 0x00017B68
		private void ValidateStartAtValues(DataMember dataMember, List<SortKeyContext> sortKeyContexts)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			Microsoft.DataShaping.Contract.RetailAssert(group.SortKeys != null, "group.SortKeys");
			List<ScopeValue> values = group.StartPosition.Values;
			for (int i = 0; i < sortKeyContexts.Count; i++)
			{
				SortKeyContext sortKeyContext = sortKeyContexts[i];
				ScopeValue scopeValue = values[i];
				this.m_expressionGenerator.ValidateLiteralType(sortKeyContext.QueryExpressionContext.QueryExpression, scopeValue.Value.Value, new ExpressionContext(this.m_context.ErrorContext, ObjectType.Group, dataMember.Id, "StartPosition"), EngineMessageSeverity.Warning);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000199FC File Offset: 0x00017BFC
		internal override void Visit(DataIntersectionPlanElement planElement)
		{
			DataIntersection dataIntersection = planElement.DataIntersection;
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filterCondition = QueryFilterGenerator.CreateFilter(planElement.FilterCondition, this.m_expressionGenerator, this.m_context.ErrorContext, dataIntersection.Id, this.m_context.CancellationToken);
			this.m_queryBuilder.AddGroupFilter(filterCondition, null);
			this.AddDataIntersectionLimit(planElement.Limit, planElement);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00019A58 File Offset: 0x00017C58
		private void AddDataIntersectionLimit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit, DataIntersectionPlanElement planElement)
		{
			DataIntersection dataIntersection = planElement.DataIntersection;
			if (limit != null && planElement.IsProjected)
			{
				QueryLimitGenerator.Generate(limit.Operator);
				List<DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(this.m_context.ExpressionTable);
				IEnumerable<GroupReference> enumerable = this.TranslateScopesToGroupReferences(groupScopesFromTargets);
				IList<QueryBuilder.SortDetail> sortDetailsForLimit = this.GetSortDetailsForLimit(limit, this.GetSortDetailsFromScopes(groupScopesFromTargets).ToList<QueryBuilder.SortDetail>());
				this.AddPostRegroupLimit(limit, enumerable, sortDetailsForLimit);
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00019ABC File Offset: 0x00017CBC
		internal override void Visit(ExpressionPlanElement planElement)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(planElement.ExpressionId, planElement.ExpressionContext);
			this.AddExpressionToQuery(planElement.ExpressionId, queryExpressionContext, planElement.SuppressJoinPredicate, planElement.ExpressionContext);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00019AFC File Offset: 0x00017CFC
		internal override void Visit(CalculationPlanElement planElement)
		{
			Calculation calculation = planElement.Calculation;
			List<KeyValuePair<ExpressionId, QueryExpressionContext>> list = this.m_expressionGenerator.TranslateCalculation(calculation);
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair = list[i];
				this.AddCalculationExpressionToQuery(planElement, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00019B4B File Offset: 0x00017D4B
		private ExpressionNode ExpressionNodeProcessor(ExpressionNode expressionNode)
		{
			if (expressionNode is DataSetFieldReferenceExpressionNode)
			{
				return new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, ((DataSetFieldReferenceExpressionNode)expressionNode).FieldName, null);
			}
			return expressionNode;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00019B70 File Offset: 0x00017D70
		private void AddCalculationExpressionToQuery(CalculationPlanElement planElement, ExpressionId expressionId, QueryExpressionContext queryExprContext)
		{
			Calculation calculation = planElement.Calculation;
			if (planElement.AsMeasureJoinPredicateOnly)
			{
				IJoinPredicate joinPredicate = JoinPredicates.CreateJoinPredicateForMeasureExpression(queryExprContext.QueryExpression);
				this.m_measureJoinPredicates.Add(joinPredicate);
				return;
			}
			this.AddExpressionToQuery(expressionId, queryExprContext, this.ShouldSuppressJoinPredicate(calculation, queryExprContext), new ExpressionContext(this.m_context.ErrorContext, calculation.ObjectType, calculation.Id, "Value"));
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00019BD8 File Offset: 0x00017DD8
		private bool ShouldSuppressJoinPredicate(Calculation calculation, QueryExpressionContext queryExprContext)
		{
			bool valueOrDefault = calculation.SuppressJoinPredicate.GetValueOrDefault<bool>();
			if (!queryExprContext.CalculateAsMeasure && valueOrDefault)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.SuppressJoinPredicateOnNonMeasure(EngineMessageSeverity.Warning, calculation.ObjectType, calculation.Id, "SuppressJoinPredicate"));
			}
			return valueOrDefault;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00019C28 File Offset: 0x00017E28
		private void AddExpressionToQuery(ExpressionId expressionId, QueryExpressionContext queryExprContext, bool suppressJoinPredicate, ExpressionContext expressionContext)
		{
			QueryExpression queryExpression = queryExprContext.QueryExpression;
			string text;
			if (queryExprContext.CalculateAsMeasure)
			{
				if (this.m_currentScopeShouldIncludeMeasuresAsDetails)
				{
					ReadOnlyCollection<GroupReference> groupReferences = this.m_activeGroups[this.m_activeGroups.Count - 1].GroupReferences;
					GroupReference groupReference = groupReferences[groupReferences.Count - 1];
					IEnumerable<QueryExpression> expressions = groupReference.Group.Keys.GetExpressions();
					this.m_queryBuilder.NamingContext.CreateOrReuseNameForDetail(expressions, queryExpression, null, expressionContext.ObjectId.Value);
					text = this.m_queryBuilder.AddOrReuseGroupDetail(groupReference.Group, queryExpression, true, false).Name;
				}
				else
				{
					this.m_queryBuilder.NamingContext.CreateOrReuseNameForMeasure(queryExpression, null, expressionContext.ObjectId.Value);
					text = this.m_queryBuilder.AddOrReuseMeasure(queryExpression, suppressJoinPredicate, null).Name;
				}
			}
			else
			{
				if (this.m_activeGroups.Count == 0)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.DetailWithoutGroup(EngineMessageSeverity.Error, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName));
					throw new QueryGenerationException("Detail outside of a group");
				}
				text = this.AddGroupDetailToQuery(queryExpression, expressionContext.ObjectId.Value, expressionId, expressionContext);
			}
			DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, text, null);
			this.m_expressionTable.SetNode(expressionId, dataSetFieldReferenceExpressionNode);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00019D78 File Offset: 0x00017F78
		private GroupReference FindInnermostCompatibleGroupReference(QueryExpression queryExpression, ExpressionId detailExprId, ExpressionContext exprContext)
		{
			GroupReference groupReference = null;
			bool flag = false;
			int num = this.m_activeGroups.Count - 1;
			while (num >= 0 && !flag)
			{
				ReadOnlyCollection<GroupReference> groupReferences = this.m_activeGroups[num].GroupReferences;
				flag = this.m_queryBuilder.TryFindInnermostCompatibleGroupReference(groupReferences, queryExpression, out groupReference);
				num--;
			}
			if (!flag)
			{
				this.RaiseInvalidDetailExpressionError(detailExprId, exprContext);
			}
			return groupReference;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00019DD4 File Offset: 0x00017FD4
		private GroupReference FindInnermostCompatibleGroupReference(IList<GroupReference> groupRefs, QueryExpression queryExpression, ExpressionId detailExprId, ExpressionContext exprContext)
		{
			GroupReference groupReference;
			if (!this.m_queryBuilder.TryFindInnermostCompatibleGroupReference(groupRefs, queryExpression, out groupReference))
			{
				this.RaiseInvalidDetailExpressionError(detailExprId, exprContext);
			}
			return groupReference;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00019DFC File Offset: 0x00017FFC
		private void RaiseInvalidDetailExpressionError(ExpressionId detailExprId, ExpressionContext exprContext)
		{
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidDetailFieldReference(EngineMessageSeverity.Error, exprContext.ObjectType, exprContext.ObjectId, exprContext.PropertyName, QueryGenerationDevErrors.GetFieldNameForDetailError(this.m_context.ExpressionTable, detailExprId)));
			throw new QueryGenerationException("Detail outside of a group");
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00019E4C File Offset: 0x0001804C
		private QueryBuilder.SortDetail BuildSortDetail(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection sortDirection, QueryExpressionContext queryExprContext, GroupReference targetGroupRef)
		{
			return new QueryBuilder.SortDetail(queryExprContext.QueryExpression, sortDirection, queryExprContext.CalculateInMeasureContext || queryExprContext.CalculateAsMeasure, false, targetGroupRef);
		}

		// Token: 0x04000345 RID: 837
		private const string IsAggregateSuffix = "IsAggregate";

		// Token: 0x04000346 RID: 838
		private readonly DataSetPlan m_dataSetPlan;

		// Token: 0x04000347 RID: 839
		private readonly QueryGenerationContext m_context;

		// Token: 0x04000348 RID: 840
		private readonly QueryBuilder m_queryBuilder;

		// Token: 0x04000349 RID: 841
		private readonly List<GroupAndSortingContext> m_activeGroups;

		// Token: 0x0400034A RID: 842
		private readonly List<KeyValuePair<IScope, List<SortKeyContext>>> m_activeSorting;

		// Token: 0x0400034B RID: 843
		private readonly List<QueryLimitConstraintContext> m_activeLimits;

		// Token: 0x0400034C RID: 844
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x0400034D RID: 845
		private readonly QueryExpressionGenerator m_expressionGenerator;

		// Token: 0x0400034E RID: 846
		private readonly IList<IJoinPredicate> m_measureJoinPredicates;

		// Token: 0x0400034F RID: 847
		private readonly WritableGeneratedQueryParameterMap m_queryParameterMap;

		// Token: 0x04000350 RID: 848
		private QuerySubtotalGenerator m_querySubtotalGenerator;

		// Token: 0x04000351 RID: 849
		private Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit m_postRegroupLimit;

		// Token: 0x04000352 RID: 850
		private bool m_currentScopeShouldIncludeMeasuresAsDetails;

		// Token: 0x04000353 RID: 851
		private List<KeyValuePair<SortKeyContext, ScalarValue>> m_startAtValues;
	}
}
