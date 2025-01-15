using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200012F RID: 303
	internal sealed class BatchGroupAndJoinQueryTableGenerator
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		private BatchGroupAndJoinQueryTableGenerator(BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator, PlanOperationGroupAndJoin join, IList<string> reservedColumnNames)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_expressionGenerator = expressionGenerator;
			this.m_join = join;
			this.m_reservedColumnNames = reservedColumnNames;
			this.m_columnMap = new WritableGeneratedColumnMap();
			FederatedEntityDataModel model = this.m_context.Model;
			this.m_builder = new GroupAndJoinTableBuilder((model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), true, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), this.m_declarations.ExpressionReferenceNameToTableMapping, this.ConvertJoinPredicateBehavior(join.PredicateBehavior), this.m_join.InUniqueMeasureNamesBlock ? this.m_reservedColumnNames : null);
			this.m_groupBuildersByKey = new Dictionary<ExpressionId, IQueryTableGroupBuilder>();
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002CDA1 File Offset: 0x0002AFA1
		public static GeneratedTable Generate(PlanOperationGroupAndJoin table, BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator, IList<string> reservedColumnNames, out BatchQueryConstraintTelemetry constraintTelemetry)
		{
			return new BatchGroupAndJoinQueryTableGenerator(context, declarations, expressionGenerator, table, reservedColumnNames).Generate(out constraintTelemetry);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		private GeneratedTable Generate(out BatchQueryConstraintTelemetry constraintTelemetry)
		{
			if (this.m_join.AllowEmptyGroups)
			{
				this.m_builder.SetAllowEmptyGroups();
			}
			this.AddGroupingTransformColumns(this.m_join.GroupingTransformColumns);
			this.AddMeasureTransformColumns(this.m_join.MeasureTransformColumns);
			this.AddGroups(this.m_join.PrimaryGroupingBucket);
			this.AddGroups(this.m_join.SecondaryGroupingBucket);
			this.AddContextTables();
			this.AddExistsFilters();
			this.AddCalculations(this.m_join.Calculations);
			this.AddAdditionalColumns(this.m_join.AdditionalColumns);
			this.AddAdditionalGroupingColumns(this.m_join.AdditionalGroupingColumns);
			bool flag;
			QueryTable queryTable = this.ToQueryTable(out flag, out constraintTelemetry);
			if (!this.m_join.SuppressUnconstrainedJoinCheck && flag)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidUnconstrainedJoin(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_context.DataShape.Id, "Id"));
			}
			return new GeneratedTable(queryTable, this.m_columnMap);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		private QueryTable ToQueryTable(out bool hasUnconstrainedJoin, out BatchQueryConstraintTelemetry constraintTelemetry)
		{
			QueryTable queryTable;
			try
			{
				queryTable = this.m_builder.ToQueryTable(out hasUnconstrainedJoin, out constraintTelemetry);
			}
			catch (QueryJoinException ex)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExistsFilter(EngineMessageSeverity.Error, ObjectType.ExistsFilterItem, this.m_context.DataShape.Id, "Exists", ex.Message));
				throw new QueryGenerationException("Error generating query. See ErrorContext for details.");
			}
			return queryTable;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002CF24 File Offset: 0x0002B124
		private void AddContextTables()
		{
			IEnumerable<global::System.ValueTuple<QueryTable, bool>> enumerable = BatchQueryGenerationUtils.GenerateContextTables(this.m_join.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext());
			this.m_builder.AddContextTables(enumerable);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002CF68 File Offset: 0x0002B168
		private void AddExistsFilters()
		{
			foreach (ExistsFilterItem existsFilterItem in this.m_join.ExistsFilters)
			{
				IQueryExpressionGenerator expressionGenerator = this.m_expressionGenerator;
				FederatedEntityDataModel model = this.m_context.Model;
				QueryExistsFilter queryExistsFilter = BatchQueryGenerationUtils.TranslateExistsFilter(existsFilterItem, expressionGenerator, (model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
				this.m_builder.AddExistsFilter(queryExistsFilter);
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002D004 File Offset: 0x0002B204
		private void AddGroups(IReadOnlyList<PlanGroupByMember> groupByMemberItems)
		{
			IQueryTableRollupBuilder queryTableRollupBuilder = null;
			IQueryTableGroupBuilder queryTableGroupBuilder = null;
			foreach (PlanGroupByMember planGroupByMember in groupByMemberItems)
			{
				IQueryTableGroupBuilder queryTableGroupBuilder2;
				if (planGroupByMember.RequiresRollupGroup)
				{
					if (queryTableRollupBuilder == null)
					{
						queryTableRollupBuilder = this.m_builder.AddRollup();
					}
					queryTableGroupBuilder = queryTableRollupBuilder.AddRollupGroup(planGroupByMember.SubtotalIndicatorColumnName);
					this.m_columnMap.Add(planGroupByMember.SubtotalIndicatorColumnName, queryTableGroupBuilder.SubtotalIndicatorColumn);
					queryTableGroupBuilder2 = queryTableGroupBuilder;
				}
				else if (queryTableGroupBuilder == null)
				{
					queryTableGroupBuilder2 = this.m_builder.AddGroup();
				}
				else
				{
					queryTableGroupBuilder2 = queryTableGroupBuilder;
				}
				this.AddGroup(planGroupByMember, queryTableGroupBuilder2);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002D0AC File Offset: 0x0002B2AC
		private void AddGroup(PlanGroupByMember groupByMemberItem, IQueryTableGroupBuilder groupBuilder)
		{
			DataMember member = groupByMemberItem.Member;
			Group group = member.Group;
			foreach (GroupKey groupKey in group.GroupKeys)
			{
				ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, groupKey.ObjectType, member.Id, "Value");
				this.AddGroupKey(groupBuilder, expressionContext, groupKey.Value.ExpressionId.Value);
			}
			this.AddGroupProcessSortKeys(groupByMemberItem, member, group);
			this.AddGroupContextTables(groupByMemberItem, groupBuilder);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002D158 File Offset: 0x0002B358
		private void AddGroupContextTables(PlanGroupByMember groupByMemberItem, IQueryTableGroupBuilder groupBuilder)
		{
			if (groupByMemberItem.ContextTables.IsNullOrEmpty<PlanOperation>())
			{
				return;
			}
			foreach (global::System.ValueTuple<QueryTable, bool> valueTuple in BatchQueryGenerationUtils.GenerateContextTables(groupByMemberItem.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext()))
			{
				groupBuilder.AddContextTable(valueTuple.Item1.Expression.NonVisual());
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002D1E0 File Offset: 0x0002B3E0
		private void AddGroupProcessSortKeys(PlanGroupByMember groupByMemberItem, DataMember member, Group group)
		{
			if (group.SortKeys == null)
			{
				return;
			}
			SortByMeasureInfoCollection sortByMeasureInfos = this.m_context.Annotations.DataMemberAnnotations.GetSortByMeasureInfos(member);
			for (int i = 0; i < group.SortKeys.Count; i++)
			{
				SortKey sortKey = group.SortKeys[i];
				bool flag = sortByMeasureInfos != null && sortByMeasureInfos.ContainsKey(sortKey);
				if (!groupByMemberItem.ExcludeMeasureSortKeys || !flag)
				{
					ScopeValueDefinition scopeValueDefinition = null;
					if (group.ScopeIdDefinition != null)
					{
						scopeValueDefinition = group.ScopeIdDefinition.Values[i];
					}
					ExpressionId? expressionId = null;
					ExpressionId expressionId2;
					if (sortByMeasureInfos == null || sortByMeasureInfos.IsAtMeasureScope || !flag)
					{
						expressionId2 = sortKey.Value.ExpressionId.Value;
						if (scopeValueDefinition != null)
						{
							expressionId = new ExpressionId?(scopeValueDefinition.Value.ExpressionId.Value);
						}
					}
					else
					{
						expressionId2 = this.m_context.SortByMeasureExpressions[sortKey].NewColumnExpression.ExpressionId.Value;
						expressionId = new ExpressionId?(expressionId2);
					}
					ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, sortKey.ObjectType, member.Id, "Value");
					QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(expressionId2, expressionContext);
					QueryTableColumn queryTableColumn = this.AddMeasureOrDetailToGroupAndJoinTable(expressionContext, expressionId2, member.Id.Value + "_Sort", queryExpressionContext, false);
					this.m_columnMap.Add(expressionId2, queryTableColumn);
					if (expressionId != null && expressionId2 != expressionId)
					{
						this.m_columnMap.Add(expressionId.Value, queryTableColumn);
					}
				}
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002D394 File Offset: 0x0002B594
		private QueryTableColumn AddGroupKey(IQueryTableGroupBuilder groupBuilder, ExpressionContext exprContext, ExpressionId exprId)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(exprId, exprContext);
			QueryTableColumn queryTableColumn = groupBuilder.AddGroupKey(queryExpressionContext.QueryExpression, exprContext.ObjectId.Value);
			this.m_columnMap.Add(exprId, queryTableColumn);
			this.m_groupBuildersByKey.Add(exprId, groupBuilder);
			return queryTableColumn;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		private void AddGroupingTransformColumns(IReadOnlyList<PlanGroupByDataTransformColumn> groupByItems)
		{
			if (groupByItems.Count == 0)
			{
				return;
			}
			IQueryTableGroupBuilder queryTableGroupBuilder = this.m_builder.AddGroup();
			foreach (PlanGroupByDataTransformColumn planGroupByDataTransformColumn in groupByItems)
			{
				DataTransformTableColumn column = planGroupByDataTransformColumn.Column;
				this.AddGroupKey(queryTableGroupBuilder, column.CreateValueExpressionContext(this.m_context.ErrorContext), column.Value.ExpressionId.Value);
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002D46C File Offset: 0x0002B66C
		private void AddMeasureTransformColumns(IReadOnlyList<PlanDataTransformColumnMeasure> measureColumns)
		{
			foreach (PlanDataTransformColumnMeasure planDataTransformColumnMeasure in measureColumns)
			{
				DataTransformTableColumn column = planDataTransformColumnMeasure.Column;
				ExpressionContext expressionContext = column.CreateValueExpressionContext(this.m_context.ErrorContext);
				ExpressionId value = column.Value.ExpressionId.Value;
				QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(value, expressionContext);
				QueryTableColumn queryTableColumn = this.AddMeasureOrDetailToGroupAndJoinTable(expressionContext, value, column.Id.Value, queryExpressionContext, false);
				this.m_columnMap.Add(value, queryTableColumn);
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002D510 File Offset: 0x0002B710
		private void AddCalculations(IReadOnlyList<Calculation> calculations)
		{
			foreach (Calculation calculation in calculations)
			{
				foreach (KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair in this.m_expressionGenerator.TranslateCalculation(calculation))
				{
					QueryExpressionContext value = keyValuePair.Value;
					QueryTableColumn queryTableColumn = this.AddMeasureOrDetailToGroupAndJoinTable(new ExpressionContext(this.m_context.ErrorContext, calculation.ObjectType, calculation.Id, "Value"), keyValuePair.Key, calculation.Id.Value, value, this.ShouldSuppressJoinPredicate(calculation, value));
					this.m_columnMap.Add(keyValuePair.Key, queryTableColumn);
				}
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		private bool ShouldSuppressJoinPredicate(Calculation calculation, QueryExpressionContext queryExprContext)
		{
			bool valueOrDefault = calculation.SuppressJoinPredicate.GetValueOrDefault<bool>();
			if (!queryExprContext.CalculateAsMeasure && valueOrDefault)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.SuppressJoinPredicateOnNonMeasure(EngineMessageSeverity.Warning, calculation.ObjectType, calculation.Id, "SuppressJoinPredicate"));
			}
			return valueOrDefault;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002D64C File Offset: 0x0002B84C
		private void AddAdditionalColumns(IReadOnlyList<PlanGroupAndJoinAdditionalColumn> columns)
		{
			foreach (PlanGroupAndJoinAdditionalColumn planGroupAndJoinAdditionalColumn in columns)
			{
				ExpressionId value = planGroupAndJoinAdditionalColumn.Expression.ExpressionId.Value;
				QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(value, planGroupAndJoinAdditionalColumn.ExpressionContext);
				QueryTableColumn queryTableColumn = this.AddMeasureOrDetailToGroupAndJoinTable(planGroupAndJoinAdditionalColumn.ExpressionContext, value, planGroupAndJoinAdditionalColumn.PlanName, queryExpressionContext, planGroupAndJoinAdditionalColumn.SuppressJoinPredicate);
				this.m_columnMap.Add(value, queryTableColumn);
				this.m_columnMap.Add(planGroupAndJoinAdditionalColumn.PlanName, queryTableColumn);
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		private void AddAdditionalGroupingColumns(IReadOnlyList<PlanGroupByGroupKey> groupByItems)
		{
			if (groupByItems.Count == 0)
			{
				return;
			}
			IQueryTableGroupBuilder queryTableGroupBuilder = this.m_builder.AddGroup();
			foreach (PlanGroupByGroupKey planGroupByGroupKey in groupByItems)
			{
				GroupKey groupKey = planGroupByGroupKey.GroupKey;
				ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, groupKey.ObjectType, planGroupByGroupKey.OwnerId, "GroupKey");
				this.AddGroupKey(queryTableGroupBuilder, expressionContext, groupKey.Value.ExpressionId.Value);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002D794 File Offset: 0x0002B994
		private QueryTableColumn AddMeasureOrDetailToGroupAndJoinTable(ExpressionContext exprContext, ExpressionId expressionId, string suggestedName, QueryExpressionContext queryExprContext, bool shouldSuppressJoinPredicate = false)
		{
			QueryExpression queryExpression = queryExprContext.QueryExpression;
			QueryTableColumn queryTableColumn;
			if (queryExprContext.CalculateAsMeasure)
			{
				queryTableColumn = this.m_builder.AddOrReuseColumn(queryExpression, suggestedName, shouldSuppressJoinPredicate);
				if (this.m_join.InUniqueMeasureNamesBlock)
				{
					this.m_reservedColumnNames.Add(queryTableColumn.Name);
				}
			}
			else
			{
				queryTableColumn = this.GetGroupBuilderForDetail(expressionId, exprContext).AddGroupDetail(queryExpression, suggestedName);
			}
			return queryTableColumn;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		private IQueryTableGroupBuilder GetGroupBuilderForDetail(ExpressionId detailExprId, ExpressionContext exprContext)
		{
			ExpressionId expressionId;
			if (!this.m_context.GroupDetailMapping.TryGetGroupKey(detailExprId, out expressionId))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidDetailFieldReference(EngineMessageSeverity.Error, exprContext.ObjectType, exprContext.ObjectId, exprContext.PropertyName, QueryGenerationDevErrors.GetFieldNameForDetailError(this.m_context.ExpressionTable, detailExprId)));
				throw new QueryGenerationException("Detail outside of a group");
			}
			IQueryTableGroupBuilder queryTableGroupBuilder;
			if (!this.m_groupBuildersByKey.TryGetValue(expressionId, out queryTableGroupBuilder))
			{
				Microsoft.DataShaping.Contract.RetailFail("Missing expected group builder for detail.");
			}
			return queryTableGroupBuilder;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002D878 File Offset: 0x0002BA78
		private JoinPredicateBehavior ConvertJoinPredicateBehavior(PlanGroupAndJoinPredicateBehavior groupAndJoinPredicateBehavior)
		{
			JoinPredicateBehavior joinPredicateBehavior = JoinPredicateBehavior.None;
			if (groupAndJoinPredicateBehavior.HasFlag(PlanGroupAndJoinPredicateBehavior.ExistsPredicates))
			{
				joinPredicateBehavior |= JoinPredicateBehavior.ExistsPredicates;
			}
			if (groupAndJoinPredicateBehavior.HasFlag(PlanGroupAndJoinPredicateBehavior.ApplyAutoPredicates))
			{
				joinPredicateBehavior |= JoinPredicateBehavior.AutoPredicates;
			}
			return joinPredicateBehavior;
		}

		// Token: 0x040005BD RID: 1469
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x040005BE RID: 1470
		private readonly GeneratedDeclarationCollection m_declarations;

		// Token: 0x040005BF RID: 1471
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x040005C0 RID: 1472
		private readonly PlanOperationGroupAndJoin m_join;

		// Token: 0x040005C1 RID: 1473
		private readonly GroupAndJoinTableBuilder m_builder;

		// Token: 0x040005C2 RID: 1474
		private readonly WritableGeneratedColumnMap m_columnMap;

		// Token: 0x040005C3 RID: 1475
		private readonly Dictionary<ExpressionId, IQueryTableGroupBuilder> m_groupBuildersByKey;

		// Token: 0x040005C4 RID: 1476
		private readonly IList<string> m_reservedColumnNames;
	}
}
