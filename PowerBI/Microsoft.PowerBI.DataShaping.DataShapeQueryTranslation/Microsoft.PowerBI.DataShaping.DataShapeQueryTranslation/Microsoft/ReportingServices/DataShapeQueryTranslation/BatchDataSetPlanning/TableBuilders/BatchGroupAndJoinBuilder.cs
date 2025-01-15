using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BD RID: 445
	internal sealed class BatchGroupAndJoinBuilder
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
		internal BatchGroupAndJoinBuilder(bool allowEmptyGroups = false, bool suppressUnconstrainedJoinCheck = true)
		{
			this.m_allowEmptyGroups = allowEmptyGroups;
			this.m_suppressUnconstrainedJoinCheck = suppressUnconstrainedJoinCheck;
			this.m_primaryGroupingBucket = new List<PlanGroupByMember>();
			this.m_secondaryGroupingBucket = new List<PlanGroupByMember>();
			this.m_calculations = new List<Calculation>();
			this.m_groupingTransformColumns = new List<PlanGroupByDataTransformColumn>();
			this.m_measureTransformColumns = new List<PlanDataTransformColumnMeasure>();
			this.m_contextTables = new List<PlanOperation>();
			this.m_attributeFilters = new List<FilterCondition>();
			this.m_existsFilters = new List<ExistsFilterItem>();
			this.m_additionalColumns = new List<PlanGroupAndJoinAdditionalColumn>();
			this.m_additionalGroupingColumns = new List<PlanGroupByGroupKey>();
			this.m_namingContext = new NamingContext(null);
			this.m_predicateBehavior = PlanGroupAndJoinPredicateBehavior.ApplyAutoPredicates;
			this.m_innermostScope = null;
			this.m_inUniqueMeasureNamesBlock = false;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003F298 File Offset: 0x0003D498
		public NamingContext NamingContext
		{
			get
			{
				return this.m_namingContext;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0003F2A8 File Offset: 0x0003D4A8
		public PlanGroupAndJoinPredicateBehavior PredicateBehavior
		{
			get
			{
				return this.m_predicateBehavior;
			}
			set
			{
				this.m_predicateBehavior = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0003F2B1 File Offset: 0x0003D4B1
		public bool AllowEmptyGroups
		{
			get
			{
				return this.m_allowEmptyGroups;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003F2B9 File Offset: 0x0003D4B9
		public bool SuppressUnconstrainedJoinCheck
		{
			get
			{
				return this.m_suppressUnconstrainedJoinCheck;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0003F2C1 File Offset: 0x0003D4C1
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0003F2C9 File Offset: 0x0003D4C9
		public bool InUniqueMeasureNamesBlock
		{
			get
			{
				return this.m_inUniqueMeasureNamesBlock;
			}
			set
			{
				this.m_inUniqueMeasureNamesBlock = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003F2D2 File Offset: 0x0003D4D2
		public bool HasSubtotal
		{
			get
			{
				return this.Totals.Any<DataMember>();
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003F2DF File Offset: 0x0003D4DF
		internal bool HasContextTables
		{
			get
			{
				return this.m_contextTables.Count > 0;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003F2EF File Offset: 0x0003D4EF
		internal IReadOnlyList<PlanOperation> ContextTables
		{
			get
			{
				return this.m_contextTables;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003F2F7 File Offset: 0x0003D4F7
		internal IReadOnlyList<FilterCondition> AttributeFilters
		{
			get
			{
				return this.m_attributeFilters;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		internal IReadOnlyList<ExistsFilterItem> ExistsFilters
		{
			get
			{
				return this.m_existsFilters;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003F307 File Offset: 0x0003D507
		internal IEnumerable<PlanGroupByMember> GroupByMembers
		{
			get
			{
				return this.m_primaryGroupingBucket.Concat(this.m_secondaryGroupingBucket);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003F31C File Offset: 0x0003D51C
		internal IEnumerable<DataMember> Totals
		{
			get
			{
				return from m in this.GroupByMembers
					where m.RequiresRollupGroup
					select m.Member;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0003F377 File Offset: 0x0003D577
		internal IReadOnlyList<PlanGroupByMember> PrimaryGroupingBucket
		{
			get
			{
				return this.m_primaryGroupingBucket;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003F37F File Offset: 0x0003D57F
		internal IReadOnlyList<PlanGroupByMember> SecondaryGroupingBucket
		{
			get
			{
				return this.m_secondaryGroupingBucket;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0003F387 File Offset: 0x0003D587
		internal IReadOnlyList<Calculation> Calculations
		{
			get
			{
				return this.m_calculations;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003F38F File Offset: 0x0003D58F
		internal IReadOnlyList<PlanGroupByDataTransformColumn> GroupingTransformColumns
		{
			get
			{
				return this.m_groupingTransformColumns;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003F397 File Offset: 0x0003D597
		internal IReadOnlyList<PlanDataTransformColumnMeasure> MeasureTransformColumns
		{
			get
			{
				return this.m_measureTransformColumns;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003F39F File Offset: 0x0003D59F
		internal IReadOnlyList<PlanGroupAndJoinAdditionalColumn> AdditionalColumns
		{
			get
			{
				return this.m_additionalColumns;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003F3A7 File Offset: 0x0003D5A7
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0003F3AF File Offset: 0x0003D5AF
		internal IScope InnermostScope
		{
			get
			{
				return this.m_innermostScope;
			}
			set
			{
				this.m_innermostScope = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
		internal IReadOnlyList<Calculation> CalculationsAsJoinPredicates
		{
			get
			{
				return this.m_calculationsAsJoinPredicates;
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003F3C0 File Offset: 0x0003D5C0
		public void AddCalculation(Calculation calculation, bool isJoinPredicate)
		{
			this.m_calculations.Add(calculation);
			if (isJoinPredicate)
			{
				Util.AddToLazyList<Calculation>(ref this.m_calculationsAsJoinPredicates, calculation);
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003F3DD File Offset: 0x0003D5DD
		public void AddCalculations(IEnumerable<Calculation> calculations)
		{
			this.m_calculations.AddRange(calculations);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003F3EB File Offset: 0x0003D5EB
		public void AddToPrimaryGroupingBucket(PlanGroupByMember groupByItem)
		{
			this.m_primaryGroupingBucket.Add(groupByItem);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003F3F9 File Offset: 0x0003D5F9
		public void AddToSecondaryGroupingBucket(PlanGroupByMember groupByItem)
		{
			this.m_secondaryGroupingBucket.Add(groupByItem);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003F407 File Offset: 0x0003D607
		public void AddToPrimaryGroupingBucket(IEnumerable<PlanGroupByMember> groupByItems)
		{
			this.m_primaryGroupingBucket.AddRange(groupByItems);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003F415 File Offset: 0x0003D615
		public void AddToSecondaryGroupingBucket(IEnumerable<PlanGroupByMember> groupByItems)
		{
			this.m_secondaryGroupingBucket.AddRange(groupByItems);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003F423 File Offset: 0x0003D623
		public void AddGroupingTransformColumn(PlanGroupByDataTransformColumn column)
		{
			this.m_groupingTransformColumns.Add(column);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003F431 File Offset: 0x0003D631
		public void AddGroupingTransformColumns(IEnumerable<PlanGroupByDataTransformColumn> columns)
		{
			this.m_groupingTransformColumns.AddRange(columns);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003F43F File Offset: 0x0003D63F
		public void AddMeasureTransformColumn(PlanDataTransformColumnMeasure column)
		{
			this.m_measureTransformColumns.Add(column);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003F44D File Offset: 0x0003D64D
		public void AddMeasureTransformColumns(IEnumerable<PlanDataTransformColumnMeasure> columns)
		{
			this.m_measureTransformColumns.AddRange(columns);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003F45B File Offset: 0x0003D65B
		public void AddAttributeFilters(IEnumerable<FilterCondition> attributeFilters)
		{
			this.m_attributeFilters.AddRange(attributeFilters);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003F46C File Offset: 0x0003D66C
		public void AddContextTables(IEnumerable<PlanOperation> contextTables)
		{
			foreach (PlanOperation planOperation in contextTables)
			{
				this.AddContextTable(planOperation);
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		public void AddContextTable(PlanOperation contextTable)
		{
			using (List<PlanOperation>.Enumerator enumerator = this.m_contextTables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == contextTable)
					{
						return;
					}
				}
			}
			this.m_contextTables.Add(contextTable);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003F510 File Offset: 0x0003D710
		public void AddExistsFilters(IEnumerable<ExistsFilterItem> existsFilters)
		{
			this.m_existsFilters.AddRange(existsFilters);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003F51E File Offset: 0x0003D71E
		public void AddExistsFilters(ExistsFilterCondition existsCondition)
		{
			if (existsCondition == null)
			{
				return;
			}
			this.AddExistsFilters(existsCondition.Items);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003F530 File Offset: 0x0003D730
		public void AddAdditionalColumn(string planName, Expression expression, bool suppressJoinPredicate, ExpressionContext expressionContext)
		{
			this.m_additionalColumns.Add(new PlanGroupAndJoinAdditionalColumn(planName, expression, suppressJoinPredicate, expressionContext));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003F547 File Offset: 0x0003D747
		public void AddAdditionalColumns(IEnumerable<PlanGroupAndJoinAdditionalColumn> columns)
		{
			this.m_additionalColumns.AddRange(columns);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003F555 File Offset: 0x0003D755
		public void AddAdditionalGroupingColumn(PlanGroupByGroupKey additionalGrouping)
		{
			this.m_additionalGroupingColumns.Add(additionalGrouping);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003F563 File Offset: 0x0003D763
		public void AddAdditionalGroupingColumns(IEnumerable<PlanGroupByGroupKey> columns)
		{
			this.m_additionalGroupingColumns.AddRange(columns);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003F574 File Offset: 0x0003D774
		public bool HasMeasures(BatchDataSetPlannerContext plannerContext)
		{
			if (this.m_measureTransformColumns.Count > 0)
			{
				return true;
			}
			if (this.Calculations.Any((Calculation calc) => plannerContext.Annotations.IsMeasure(calc)))
			{
				return true;
			}
			WritableExpressionTable outputExpressionTable = plannerContext.OutputExpressionTable;
			IEnumerable<Expression> enumerable = this.m_additionalColumns.Select((PlanGroupAndJoinAdditionalColumn c) => c.Expression);
			ExpressionTable expressionTable = outputExpressionTable;
			Func<ExpressionNode, bool> func;
			if ((func = BatchGroupAndJoinBuilder.<>O.<0>__IsMeasure) == null)
			{
				func = (BatchGroupAndJoinBuilder.<>O.<0>__IsMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsMeasure));
			}
			if (BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(enumerable, expressionTable, func))
			{
				return true;
			}
			ExpressionTable expressionTable2 = outputExpressionTable;
			Func<ExpressionNode, bool> func2;
			if ((func2 = BatchGroupAndJoinBuilder.<>O.<0>__IsMeasure) == null)
			{
				func2 = (BatchGroupAndJoinBuilder.<>O.<0>__IsMeasure = new Func<ExpressionNode, bool>(MeasureAnalyzer.IsMeasure));
			}
			return this.HasExpressionFeatureInMember(expressionTable2, func2);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003F638 File Offset: 0x0003D838
		public bool HasExpressionFeature(ExpressionTable expressionTable, CalculationExpressionMap calculationMap, Func<ExpressionNode, bool> hasExpressionFeature)
		{
			if (BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(this.m_measureTransformColumns.Select((PlanDataTransformColumnMeasure c) => c.Column.Value), expressionTable, hasExpressionFeature))
			{
				return true;
			}
			return BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(this.m_additionalColumns.Select((PlanGroupAndJoinAdditionalColumn c) => c.Expression), expressionTable, hasExpressionFeature) || this.HasExpressionFeatureInMember(expressionTable, hasExpressionFeature) || BatchDataSetPlanningUtils.HasExpressionFeatureInCalculation(expressionTable, calculationMap, this.m_calculations, hasExpressionFeature);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003F6CC File Offset: 0x0003D8CC
		private bool HasExpressionFeatureInMember(ExpressionTable expressionTable, Func<ExpressionNode, bool> hasExpressionFeature)
		{
			foreach (PlanGroupByMember planGroupByMember in this.GroupByMembers)
			{
				Group group = planGroupByMember.Member.Group;
				if (group.SortKeys != null)
				{
					if (BatchDataSetPlanningUtils.HasExpressionFeatureInExpressions(group.SortKeys.Select((SortKey k) => k.Value), expressionTable, hasExpressionFeature))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003F760 File Offset: 0x0003D960
		public BatchGroupAndJoinBuilder Clone(IEnumerable<Calculation> excludedCalculations = null)
		{
			IEnumerable<Calculation> enumerable = this.Calculations;
			if (excludedCalculations != null)
			{
				enumerable = this.Calculations.Except(excludedCalculations);
			}
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = new BatchGroupAndJoinBuilder(this.AllowEmptyGroups, this.SuppressUnconstrainedJoinCheck);
			batchGroupAndJoinBuilder.AddToPrimaryGroupingBucket(this.PrimaryGroupingBucket);
			batchGroupAndJoinBuilder.AddToSecondaryGroupingBucket(this.SecondaryGroupingBucket);
			batchGroupAndJoinBuilder.AddCalculations(enumerable);
			batchGroupAndJoinBuilder.AddGroupingTransformColumns(this.GroupingTransformColumns);
			batchGroupAndJoinBuilder.AddMeasureTransformColumns(this.MeasureTransformColumns);
			batchGroupAndJoinBuilder.AddContextTables(this.ContextTables);
			batchGroupAndJoinBuilder.AddAttributeFilters(this.AttributeFilters);
			batchGroupAndJoinBuilder.AddExistsFilters(this.ExistsFilters);
			batchGroupAndJoinBuilder.AddAdditionalColumns(this.AdditionalColumns);
			batchGroupAndJoinBuilder.AddAdditionalGroupingColumns(this.m_additionalGroupingColumns);
			batchGroupAndJoinBuilder.PredicateBehavior = this.PredicateBehavior;
			batchGroupAndJoinBuilder.InnermostScope = this.InnermostScope;
			batchGroupAndJoinBuilder.InUniqueMeasureNamesBlock = this.InUniqueMeasureNamesBlock;
			return batchGroupAndJoinBuilder;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003F82C File Offset: 0x0003DA2C
		public void DisableSortByMeasureKeysAndSubtotals(IReadOnlyList<Calculation> subtotalCalculations)
		{
			this.DisableSortByMeasureKeysAndSubtotalsForGroups(this.m_primaryGroupingBucket);
			this.DisableSortByMeasureKeysAndSubtotalsForGroups(this.m_secondaryGroupingBucket);
			this.RemoveCalculations(subtotalCalculations);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003F850 File Offset: 0x0003DA50
		private void RemoveCalculations(IReadOnlyList<Calculation> subtotalCalculations)
		{
			if (subtotalCalculations.IsNullOrEmpty<Calculation>())
			{
				return;
			}
			this.m_calculations.RemoveAll((Calculation c) => subtotalCalculations.Contains(c));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0003F890 File Offset: 0x0003DA90
		private void DisableSortByMeasureKeysAndSubtotalsForGroups(IList<PlanGroupByMember> groupByMemberItems)
		{
			for (int i = 0; i < groupByMemberItems.Count; i++)
			{
				groupByMemberItems[i] = groupByMemberItems[i].DisableSortByMeasureKeysAndSubtotals();
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003F8C4 File Offset: 0x0003DAC4
		public override bool Equals(object other)
		{
			bool flag;
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder;
			if (CompareUtil.CheckReferenceAndTypeEquality<BatchGroupAndJoinBuilder, object>(this, other, out flag, out batchGroupAndJoinBuilder))
			{
				return flag;
			}
			return this.GroupByMembers.SetEquals(batchGroupAndJoinBuilder.GroupByMembers) && this.Calculations.SetEquals(batchGroupAndJoinBuilder.Calculations) && this.ContextTables.SetEquals(batchGroupAndJoinBuilder.ContextTables) && this.AttributeFilters.SetEquals(batchGroupAndJoinBuilder.AttributeFilters) && this.GroupingTransformColumns.SetEquals(batchGroupAndJoinBuilder.GroupingTransformColumns) && this.MeasureTransformColumns.SetEquals(batchGroupAndJoinBuilder.MeasureTransformColumns) && this.ExistsFilters.SetEquals(batchGroupAndJoinBuilder.ExistsFilters) && this.m_additionalColumns.SetEquals(batchGroupAndJoinBuilder.m_additionalColumns) && this.m_additionalGroupingColumns.SetEquals(batchGroupAndJoinBuilder.m_additionalGroupingColumns) && this.AllowEmptyGroups == batchGroupAndJoinBuilder.AllowEmptyGroups && this.SuppressUnconstrainedJoinCheck == batchGroupAndJoinBuilder.SuppressUnconstrainedJoinCheck && this.InUniqueMeasureNamesBlock == batchGroupAndJoinBuilder.InUniqueMeasureNamesBlock && this.PredicateBehavior == batchGroupAndJoinBuilder.PredicateBehavior && this.InnermostScope == batchGroupAndJoinBuilder.InnermostScope;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003F9E5 File Offset: 0x0003DBE5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003F9F0 File Offset: 0x0003DBF0
		public PlanOperationGroupAndJoin ToPlanOperation(string telemetryName = null)
		{
			return new PlanOperationGroupAndJoin(this.m_primaryGroupingBucket, this.m_secondaryGroupingBucket, this.m_calculations, this.m_groupingTransformColumns, this.m_measureTransformColumns, this.m_contextTables, this.m_existsFilters, this.m_additionalColumns, this.m_additionalGroupingColumns, this.m_predicateBehavior, this.m_allowEmptyGroups, this.m_suppressUnconstrainedJoinCheck, this.m_inUniqueMeasureNamesBlock, telemetryName);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003FA54 File Offset: 0x0003DC54
		public PlanOperationContext ToPlanOperationContext(ScopeTree scopeTree, PlanOperation operation = null, string telemetryName = null)
		{
			PlanOperation planOperation = operation ?? this.ToPlanOperation(telemetryName);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(this.Totals.ToReadOnlyList<DataMember>().ToTotalsMetadata(), false);
			return new PlanOperationContext(planOperation, this.InnermostScope.GetAllParentScopesWithoutTopDataShapes(scopeTree).ToReadOnlyList<IScope>(), this.Calculations, Util.EmptyReadOnlyCollection<DataMember>(), planOperationFilteringMetadata);
		}

		// Token: 0x04000758 RID: 1880
		private readonly List<PlanGroupByMember> m_primaryGroupingBucket;

		// Token: 0x04000759 RID: 1881
		private readonly List<PlanGroupByMember> m_secondaryGroupingBucket;

		// Token: 0x0400075A RID: 1882
		private readonly List<Calculation> m_calculations;

		// Token: 0x0400075B RID: 1883
		private readonly List<PlanGroupByDataTransformColumn> m_groupingTransformColumns;

		// Token: 0x0400075C RID: 1884
		private readonly List<PlanDataTransformColumnMeasure> m_measureTransformColumns;

		// Token: 0x0400075D RID: 1885
		private readonly List<PlanOperation> m_contextTables;

		// Token: 0x0400075E RID: 1886
		private readonly List<FilterCondition> m_attributeFilters;

		// Token: 0x0400075F RID: 1887
		private readonly List<ExistsFilterItem> m_existsFilters;

		// Token: 0x04000760 RID: 1888
		private readonly List<PlanGroupAndJoinAdditionalColumn> m_additionalColumns;

		// Token: 0x04000761 RID: 1889
		private readonly List<PlanGroupByGroupKey> m_additionalGroupingColumns;

		// Token: 0x04000762 RID: 1890
		private readonly NamingContext m_namingContext;

		// Token: 0x04000763 RID: 1891
		private readonly bool m_allowEmptyGroups;

		// Token: 0x04000764 RID: 1892
		private readonly bool m_suppressUnconstrainedJoinCheck;

		// Token: 0x04000765 RID: 1893
		private PlanGroupAndJoinPredicateBehavior m_predicateBehavior;

		// Token: 0x04000766 RID: 1894
		private IScope m_innermostScope;

		// Token: 0x04000767 RID: 1895
		private bool m_inUniqueMeasureNamesBlock;

		// Token: 0x04000768 RID: 1896
		private List<Calculation> m_calculationsAsJoinPredicates;

		// Token: 0x02000306 RID: 774
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B25 RID: 2853
			public static Func<ExpressionNode, bool> <0>__IsMeasure;
		}
	}
}
