using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001DC RID: 476
	internal sealed class ScopedTablesBuilder
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00044515 File Offset: 0x00042715
		internal ScopedTablesBuilder()
			: this(new BatchGroupAndJoinBuilder(false, true))
		{
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00044524 File Offset: 0x00042724
		internal ScopedTablesBuilder(bool allowEmptyGroups, bool suppressUnconstrainedJoinCheck)
			: this(new BatchGroupAndJoinBuilder(allowEmptyGroups, suppressUnconstrainedJoinCheck))
		{
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00044533 File Offset: 0x00042733
		private ScopedTablesBuilder(BatchGroupAndJoinBuilder tableBuilder)
		{
			this.m_sealingMeasures = new List<Calculation>();
			this.m_totalCalcs = new List<Calculation>();
			this.m_tableBuilders = new Stack<BatchGroupAndJoinBuilder>();
			this.m_tableBuilders.Push(tableBuilder);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00044568 File Offset: 0x00042768
		internal IEnumerable<BatchGroupAndJoinBuilder> TableBuilders
		{
			get
			{
				return this.m_tableBuilders;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00044570 File Offset: 0x00042770
		internal BatchGroupAndJoinBuilder InnermostScopedTable
		{
			get
			{
				return this.m_tableBuilders.Peek();
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0004457D File Offset: 0x0004277D
		private bool IsInnermostScopeSealed
		{
			get
			{
				return this.m_sealingMeasures.Count > 0;
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004458D File Offset: 0x0004278D
		private static bool IsSealingMeasure(Calculation calc, DataShapeContext dsContext, DataShapeAnnotations annotations)
		{
			return annotations.IsMeasure(calc) && !dsContext.DataShapeAggregatesAndProjections.Contains(calc) && !annotations.CanBeHandledByProcessing(calc);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000445B6 File Offset: 0x000427B6
		internal void AddGroupingTransformColumn(PlanGroupByDataTransformColumn groupByItem)
		{
			this.InnermostScopedTable.AddGroupingTransformColumn(groupByItem);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000445C4 File Offset: 0x000427C4
		internal void AddMeasureTransformColumn(PlanDataTransformColumnMeasure column)
		{
			this.InnermostScopedTable.AddMeasureTransformColumn(column);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000445D2 File Offset: 0x000427D2
		internal void AddToPrimaryGroupingBucket(PlanGroupByMember groupByItem)
		{
			if (this.IsInnermostScopeSealed)
			{
				this.PushUnsealedTableToStack();
			}
			this.InnermostScopedTable.AddToPrimaryGroupingBucket(groupByItem);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000445EE File Offset: 0x000427EE
		internal void AddToSecondaryGroupingBucket(PlanGroupByMember groupByItem)
		{
			if (this.IsInnermostScopeSealed)
			{
				this.PushUnsealedTableToStack();
			}
			this.InnermostScopedTable.AddToSecondaryGroupingBucket(groupByItem);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004460C File Offset: 0x0004280C
		internal void AddCalculation(IScope currentInnermostScope, Calculation calculation, DataShapeContext dsContext, BatchDataSetPlannerContext plannerContext, bool isJoinPredicate)
		{
			if (plannerContext.Annotations.IsSubtotal(calculation))
			{
				this.m_totalCalcs.Add(calculation);
			}
			bool flag = ScopedTablesBuilder.IsSealingMeasure(calculation, dsContext, plannerContext.Annotations);
			if (this.IsInnermostScopeSealed && flag && !plannerContext.ScopeTree.AreSameScope(this.InnermostScopedTable.InnermostScope, currentInnermostScope))
			{
				this.PushUnsealedTableToStack();
			}
			this.InnermostScopedTable.AddCalculation(calculation, isJoinPredicate);
			if (flag)
			{
				this.SealInnermostScopeToNewGroups(calculation);
				this.InnermostScopedTable.InnermostScope = currentInnermostScope;
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00044691 File Offset: 0x00042891
		private void SealInnermostScopeToNewGroups(Calculation calculation)
		{
			this.m_sealingMeasures.Add(calculation);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000446A0 File Offset: 0x000428A0
		private void PushUnsealedTableToStack()
		{
			BatchGroupAndJoinBuilder innermostScopedTable = this.InnermostScopedTable;
			this.m_tableBuilders.Push(innermostScopedTable.Clone(this.m_sealingMeasures));
			this.InnermostScopedTable.InnermostScope = null;
			this.m_sealingMeasures.Clear();
			innermostScopedTable.DisableSortByMeasureKeysAndSubtotals(this.m_totalCalcs);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000446F0 File Offset: 0x000428F0
		internal void AddContextTables(IReadOnlyList<PlanOperation> contextTables)
		{
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in this.m_tableBuilders)
			{
				batchGroupAndJoinBuilder.AddContextTables(contextTables);
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00044744 File Offset: 0x00042944
		internal void AddContextTable(PlanOperation contextTable)
		{
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in this.m_tableBuilders)
			{
				batchGroupAndJoinBuilder.AddContextTable(contextTable);
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00044798 File Offset: 0x00042998
		internal void AddAttributeFilters(IEnumerable<FilterCondition> filters)
		{
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in this.m_tableBuilders)
			{
				batchGroupAndJoinBuilder.AddAttributeFilters(filters);
			}
		}

		// Token: 0x040007BD RID: 1981
		private readonly Stack<BatchGroupAndJoinBuilder> m_tableBuilders;

		// Token: 0x040007BE RID: 1982
		private readonly List<Calculation> m_sealingMeasures;

		// Token: 0x040007BF RID: 1983
		private readonly List<Calculation> m_totalCalcs;
	}
}
