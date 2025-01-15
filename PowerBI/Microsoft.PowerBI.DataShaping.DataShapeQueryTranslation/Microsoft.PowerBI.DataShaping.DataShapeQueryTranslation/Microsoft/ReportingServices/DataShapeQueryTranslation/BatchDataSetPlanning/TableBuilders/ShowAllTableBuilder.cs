using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001DE RID: 478
	internal static class ShowAllTableBuilder
	{
		// Token: 0x06001094 RID: 4244 RVA: 0x00044C2C File Offset: 0x00042E2C
		public static PlanOperationContext Build(IShowAllTableBuilderContext context, IReadOnlyList<PlanOperation> contextTables, PlanDeclarationCollection declarations, PlanOperationContext inputTable, IReadOnlyList<DataMember> dynamicMembers, IReadOnlyList<DataMember> showAllMembers, BatchDataSetPlannerJoinPredicates joinPredicates, string declarationName, IReadOnlyList<FilterCondition> attributeFilters, ContextTableManager attributeFilterManager, InstanceFiltersContext instanceFiltersContext, bool allowBlankRow, SubtotalUsage subtotalKind)
		{
			if (showAllMembers.IsNullOrEmpty<DataMember>())
			{
				return inputTable;
			}
			QueryStageForInstanceFilters queryStageForInstanceFilters = instanceFiltersContext.QueryStageForInstanceFilters;
			bool flag = ShowAllTableBuilder.CanUseAddMissingItems(context, dynamicMembers);
			bool flag2 = queryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllRollupContextTables || queryStageForInstanceFilters == QueryStageForInstanceFilters.PostCoreTableAndInShowAllRollupContextTables;
			bool flag3 = queryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllPostFilter || queryStageForInstanceFilters == QueryStageForInstanceFilters.PostCoreTableAndInShowAllPostFilter;
			bool flag4 = flag && flag2;
			bool flag5 = (!flag && flag2) || flag3;
			IEnumerable<PlanGroupByMember> enumerable = dynamicMembers.ToGroupByItems(context.Annotations, subtotalKind, false, true, flag4 ? instanceFiltersContext.InstanceFilterDeclarations : null);
			PlanOperationContext planOperationContext;
			if (flag)
			{
				planOperationContext = ShowAllTableBuilder.BuildUsingAddMissingItems(context.Annotations, context.OutputExpressionTable, context.Schema, contextTables, declarations, inputTable, showAllMembers, enumerable, attributeFilters, attributeFilterManager, context.FeatureSwitches);
			}
			else
			{
				planOperationContext = ShowAllTableBuilder.BuildUsingCompatPattern(context.Annotations, context.ErrorContext, context.ScopeTree, contextTables, declarations, inputTable, dynamicMembers, enumerable, joinPredicates, declarationName, allowBlankRow, subtotalKind);
			}
			if (flag5 || (flag4 && !instanceFiltersContext.InstanceFiltersRequiringPostFiltering.IsNullOrEmpty<FilterCondition>()))
			{
				planOperationContext = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(context.OutputExpressionTable, context.ErrorContext, context.Annotations, planOperationContext, dynamicMembers, instanceFiltersContext.InstanceFiltersRequiringPostFiltering);
			}
			return planOperationContext;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00044D2C File Offset: 0x00042F2C
		private static bool CanUseAddMissingItems(IShowAllTableBuilderContext context, IReadOnlyList<DataMember> dynamicMembers)
		{
			bool flag = false;
			bool flag2 = true;
			bool flag3 = true;
			HashSet<IConceptualEntity> hashSet = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			for (int i = 0; i < dynamicMembers.Count; i++)
			{
				DataMember dataMember = dynamicMembers[i];
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (!context.Annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation))
				{
					flag2 = false;
				}
				int count = hashSet.Count;
				bool flag4;
				bool flag5;
				if (!DataShapeQueryUtils.TryAddGroupKeyEntities(dataMember.Group.GroupKeys, hashSet, context.OutputExpressionTable, out flag4, out flag5))
				{
					Microsoft.DataShaping.Contract.RetailFail("Group keys should have entity references");
				}
				flag = hashSet.Count - count > 1;
				flag3 = flag3 && flag5;
			}
			return !DataShapeQueryUtils.HasDirectlyRelatedGroupKeys(hashSet, context.Schema.GetDefaultSchema()) || (!flag && flag2 && flag3);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00044DDC File Offset: 0x00042FDC
		private static PlanOperationContext BuildUsingAddMissingItems(DataShapeAnnotations annotations, ExpressionTable expressionTable, IFederatedConceptualSchema schema, IReadOnlyList<PlanOperation> contextTables, PlanDeclarationCollection declarations, PlanOperationContext table, IReadOnlyList<DataMember> showAllMembers, IEnumerable<PlanGroupByMember> groupByMembers, IReadOnlyList<FilterCondition> attributeFilters, ContextTableManager attributeFilterManager, IFeatureSwitchProvider featureSwitchProvider)
		{
			IReadOnlyList<PlanOperation> readOnlyList = ShowAllTableBuilder.FinalizeContextTables(annotations, expressionTable, schema, contextTables, declarations, attributeFilters, showAllMembers, attributeFilterManager, featureSwitchProvider);
			IEnumerable<PlanGroupByMember> enumerable = showAllMembers.ToGroupByItems(annotations, SubtotalUsage.Output, false, true, null);
			PlanOperation planOperation = table.Table.AddMissingItems(groupByMembers, enumerable, readOnlyList);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(table.Totals.ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, table.RowScopes, table.Calculations, showAllMembers, planOperationFilteringMetadata);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00044E44 File Offset: 0x00043044
		private static IReadOnlyList<PlanOperation> FinalizeContextTables(DataShapeAnnotations annotations, ExpressionTable expressionTable, IFederatedConceptualSchema schema, IReadOnlyList<PlanOperation> contextTables, PlanDeclarationCollection declarations, IReadOnlyList<FilterCondition> attributeFilters, IReadOnlyList<DataMember> showAllMembers, ContextTableManager attributeFilterManager, IFeatureSwitchProvider featureSwitchProvider)
		{
			if (attributeFilters.IsNullOrEmpty<FilterCondition>())
			{
				return contextTables;
			}
			Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> dictionary = ShowAllTableBuilder.ExtractGroupKeyEntities(expressionTable, showAllMembers);
			HashSet<IConceptualEntity> filterEntities = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			DataShapeQueryUtils.FilterExpressionCallbackVisitor filterExpressionCallbackVisitor = new DataShapeQueryUtils.FilterExpressionCallbackVisitor(delegate(Expression expr)
			{
				IConceptualEntity conceptualEntity;
				if (expr.TryExtractEntityFromProperty(expressionTable, out conceptualEntity))
				{
					filterEntities.Add(conceptualEntity);
				}
			}, null);
			Dictionary<IConceptualEntity, int> dictionary2 = new Dictionary<IConceptualEntity, int>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			List<BatchGroupAndJoinBuilder> list = new List<BatchGroupAndJoinBuilder>(dictionary.Count);
			NamingContext namingContext = new NamingContext(null);
			ShowAllTableBuilder.AddFilterConditionsToAdditionalContextTables(annotations, expressionTable, schema, attributeFilterManager, declarations, namingContext, dictionary, filterEntities, filterExpressionCallbackVisitor, dictionary2, list, attributeFilters, false, featureSwitchProvider);
			if (dictionary2.Count == 0)
			{
				return contextTables;
			}
			List<PlanOperation> list2 = new List<PlanOperation>(contextTables.Count + dictionary2.Count);
			list2.AddRange(contextTables);
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in list)
			{
				list2.Add(batchGroupAndJoinBuilder.ToPlanOperation(null));
			}
			return list2;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00044F54 File Offset: 0x00043154
		private static void AddFilterConditionsToAdditionalContextTables(DataShapeAnnotations annotations, ExpressionTable expressionTable, IFederatedConceptualSchema schema, ContextTableManager attributeFilterManager, PlanDeclarationCollection declarations, NamingContext namingContext, Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> showAllGroupKeyEntities, HashSet<IConceptualEntity> filterEntities, DataShapeQueryUtils.FilterExpressionCallbackVisitor expressionEntityAnalyzer, Dictionary<IConceptualEntity, int> builderIndexPerGroupEntity, List<BatchGroupAndJoinBuilder> builders, IReadOnlyList<FilterCondition> filterConditions, bool isNestedFilter, IFeatureSwitchProvider featureSwitchProvider)
		{
			foreach (FilterCondition filterCondition in filterConditions)
			{
				CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
				if (compoundFilterCondition != null && compoundFilterCondition.ConditionsHaveIndependentContext() && compoundFilterCondition.Conditions.Count > 1)
				{
					ShowAllTableBuilder.AddFilterConditionsToAdditionalContextTables(annotations, expressionTable, schema, attributeFilterManager, declarations, namingContext, showAllGroupKeyEntities, filterEntities, expressionEntityAnalyzer, builderIndexPerGroupEntity, builders, compoundFilterCondition.Conditions, true, featureSwitchProvider);
				}
				else
				{
					ApplyFilterCondition applyFilterCondition = filterCondition as ApplyFilterCondition;
					if (applyFilterCondition != null)
					{
						DataShape applyFilterDataShape = applyFilterCondition.GetApplyFilterDataShape(expressionTable);
						bool flag;
						DataShapeQueryUtils.TryCollectEntities(applyFilterDataShape.PrimaryHierarchy.GetAllDynamicMembers(), expressionTable, filterEntities, out flag);
						DataShapeQueryUtils.TryCollectEntities(applyFilterDataShape.SecondaryHierarchy.GetAllDynamicMembers(), expressionTable, filterEntities, out flag);
					}
					expressionEntityAnalyzer.VisitFilter(filterCondition);
					ShowAllTableBuilder.BuildContextTablesForGroupKeys(schema, declarations, namingContext, showAllGroupKeyEntities, filterEntities, builderIndexPerGroupEntity, builders, attributeFilterManager, filterCondition, isNestedFilter);
					filterEntities.Clear();
				}
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00045040 File Offset: 0x00043240
		private static void BuildContextTablesForGroupKeys(IFederatedConceptualSchema schema, PlanDeclarationCollection declarations, NamingContext namingContext, Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> showAllGroupKeyEntities, HashSet<IConceptualEntity> filterEntities, Dictionary<IConceptualEntity, int> buildersPerGroupEntity, List<BatchGroupAndJoinBuilder> builders, ContextTableManager attributeFilterManager, FilterCondition condition, bool isNestedFilter)
		{
			foreach (KeyValuePair<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> keyValuePair in showAllGroupKeyEntities)
			{
				foreach (IConceptualEntity conceptualEntity in filterEntities)
				{
					IConceptualEntity key = keyValuePair.Key;
					if (!conceptualEntity.Equals(key) && DataShapeQueryUtils.WillCrossFilter(conceptualEntity, key.GetBaseModelEntity(), schema.GetDefaultSchema(), true))
					{
						IList<Tuple<GroupKey, Identifier>> value = keyValuePair.Value;
						int num;
						BatchGroupAndJoinBuilder batchGroupAndJoinBuilder;
						if (!buildersPerGroupEntity.TryGetValue(key, out num))
						{
							batchGroupAndJoinBuilder = new BatchGroupAndJoinBuilder(false, true);
							builders.Add(batchGroupAndJoinBuilder);
							buildersPerGroupEntity.Add(key, num);
							using (IEnumerator<Tuple<GroupKey, Identifier>> enumerator3 = value.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									Tuple<GroupKey, Identifier> tuple = enumerator3.Current;
									batchGroupAndJoinBuilder.AddAdditionalGroupingColumn(new PlanGroupByGroupKey(tuple.Item1, tuple.Item2));
								}
								goto IL_00DB;
							}
							goto IL_00D0;
						}
						goto IL_00D0;
						IL_00DB:
						PlanOperation planOperation;
						if (!attributeFilterManager.TryGetContextTableForFilterCondition(condition, out planOperation))
						{
							planOperation = condition.CreateFilterContextTable().DeclareIfNotDeclared(namingContext.GenerateUniqueName(PlanNames.FilterTable(conceptualEntity.EdmName, null)), declarations, true, isNestedFilter, null, false);
						}
						batchGroupAndJoinBuilder.AddContextTable(planOperation);
						continue;
						IL_00D0:
						batchGroupAndJoinBuilder = builders[num];
						goto IL_00DB;
					}
				}
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000451F8 File Offset: 0x000433F8
		private static Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> ExtractGroupKeyEntities(ExpressionTable expressionTable, IReadOnlyList<DataMember> showAllMembers)
		{
			Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>> dictionary = new Dictionary<IConceptualEntity, IList<Tuple<GroupKey, Identifier>>>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			foreach (DataMember dataMember in showAllMembers)
			{
				foreach (GroupKey groupKey in dataMember.Group.GroupKeys)
				{
					IConceptualEntity conceptualEntity;
					if (!groupKey.Value.TryExtractEntityFromProperty(expressionTable, out conceptualEntity))
					{
						Microsoft.DataShaping.Contract.RetailFail("Group keys should have entity references");
					}
					Tuple<GroupKey, Identifier> tuple = new Tuple<GroupKey, Identifier>(groupKey, dataMember.Id);
					if (conceptualEntity != null)
					{
						IList<Tuple<GroupKey, Identifier>> list;
						if (!dictionary.TryGetValue(conceptualEntity, out list))
						{
							list = new List<Tuple<GroupKey, Identifier>>();
							list.Add(tuple);
							dictionary.Add(conceptualEntity, list);
						}
						else
						{
							list.Add(tuple);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000452E8 File Offset: 0x000434E8
		private static PlanOperationContext BuildUsingCompatPattern(DataShapeAnnotations annotations, TranslationErrorContext errorContext, ScopeTree scopeTree, IReadOnlyList<PlanOperation> contextTables, PlanDeclarationCollection declarations, PlanOperationContext inputTable, IReadOnlyList<DataMember> dynamicMembers, IEnumerable<PlanGroupByMember> groupByMembers, BatchDataSetPlannerJoinPredicates joinPredicates, string declarationName, bool allowBlankRow, SubtotalUsage subtotalKind)
		{
			PlanOperation planOperation = groupByMembers.AddMissingItemsCompatPattern(joinPredicates.Calculations, contextTables, allowBlankRow);
			PlanOperation planOperation2 = ShowAllTableBuilder.BuildShowAllOverInput(annotations, errorContext, declarations, dynamicMembers, inputTable, planOperation, subtotalKind, declarationName).GroupBy(groupByMembers);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(inputTable.Totals.ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation2, scopeTree.GetSpanningScopes(dynamicMembers).ToReadOnlyList<IScope>(), Microsoft.DataShaping.Util.EmptyReadOnlyCollection<Calculation>(), dynamicMembers, planOperationFilteringMetadata).LeftOuterJoin(inputTable, scopeTree);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00045358 File Offset: 0x00043558
		private static PlanOperation BuildShowAllOverInput(DataShapeAnnotations annotations, TranslationErrorContext errorContext, PlanDeclarationCollection declarations, IReadOnlyList<DataMember> dynamicMembers, PlanOperationContext inputTable, PlanOperation showAll, SubtotalUsage subtotalKind, string declarationName)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			List<PlanProjectItem> list2 = new List<PlanProjectItem>();
			List<PlanProjectItem> list3 = new List<PlanProjectItem>();
			bool flag = false;
			foreach (DataMember dataMember in dynamicMembers)
			{
				PlanProjectItem planProjectItem = dataMember.ToGroupProjectItemsGroupOnly(annotations, false);
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				PlanProjectItem planProjectItem2 = dataMember.ToGroupProjectItemsTotalOnly(annotations, subtotalKind, out batchSubtotalAnnotation);
				if (planProjectItem2 != null)
				{
					list3.Add(planProjectItem2);
					PlanNewColumnProjectItem subtotalIndicatorColumnProjectItem = batchSubtotalAnnotation.SubtotalIndicatorColumnName.GetSubtotalIndicatorColumnProjectItem(LiteralExpressionNode.False, errorContext);
					list.Add(subtotalIndicatorColumnProjectItem);
					flag = true;
				}
				list.Add(planProjectItem);
				list2.Add(planProjectItem);
			}
			if (flag)
			{
				showAll = showAll.Project(list, true);
			}
			List<PlanProjectItem> list4 = list2.Concat(list3).ToList<PlanProjectItem>();
			showAll = showAll.Project(list4, true);
			showAll = showAll.DeclareIfNotDeclared(PlanNames.ShowAllCompat(declarationName), declarations, false, false, null, false);
			return inputTable.Table.Project(list4, true).DeclareIfNotDeclared(PlanNames.Reordered(declarationName), declarations, false, false, null, false).Union(showAll);
		}
	}
}
