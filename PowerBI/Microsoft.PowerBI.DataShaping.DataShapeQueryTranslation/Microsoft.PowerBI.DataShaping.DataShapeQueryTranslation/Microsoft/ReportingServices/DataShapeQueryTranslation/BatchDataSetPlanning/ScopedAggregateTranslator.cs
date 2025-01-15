using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001AA RID: 426
	internal sealed class ScopedAggregateTranslator
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x0003D0E1 File Offset: 0x0003B2E1
		private ScopedAggregateTranslator(DataShapeContext dataShapeContext, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, PlanOperationContext inputTable, IAggregatesPlanningContext plannerContext)
		{
			this.m_dataShapeContext = dataShapeContext;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_declarations = declarations;
			this.m_plannerContext = plannerContext;
			this.m_inputTable = inputTable;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003D10E File Offset: 0x0003B30E
		internal static PlanOperationContext GenerateScopedAggregates(DataShapeContext dataShapeContext, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, PlanOperationContext inputTable, IAggregatesPlanningContext plannerContext, IReadOnlyList<DataMember> dynamicMembers, IReadOnlyList<TableReference> refTables)
		{
			return new ScopedAggregateTranslator(dataShapeContext, outputExpressionTable, declarations, inputTable, plannerContext).ApplyScopedAggregatesOperations(dynamicMembers, refTables);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003D124 File Offset: 0x0003B324
		private PlanOperationContext ApplyScopedAggregatesOperations(IReadOnlyList<DataMember> dynamicMembers, IReadOnlyList<TableReference> refTables)
		{
			List<PlanOperationContext> list = this.CreateAggregateTables(dynamicMembers, refTables);
			if (list.IsNullOrEmpty<PlanOperationContext>())
			{
				return this.m_inputTable;
			}
			PlanOperationContext planOperationContext = this.JoinAggregateTablesWithInputTable(list);
			string text = PlanNames.PrimaryWithScopedAggregates(this.m_dataShapeContext.DataShape.Id);
			return planOperationContext.DeclareIfNotDeclared(text, this.m_declarations, false, null, false);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003D178 File Offset: 0x0003B378
		private List<PlanOperationContext> CreateAggregateTables(IReadOnlyList<DataMember> members, IReadOnlyList<TableReference> refTables)
		{
			if (refTables == null)
			{
				return null;
			}
			List<PlanOperationContext> list = null;
			for (int i = 0; i < members.Count; i++)
			{
				DataMember dataMember = members[i];
				if (dataMember != this.m_dataShapeContext.InnermostScopeExcludingContextOnly)
				{
					List<Calculation> list2 = (from c in this.m_dataShapeContext.ScopeTree.GetAllItemsInScope<Calculation>(dataMember.Id)
						where this.m_dataShapeContext.Annotations.IsAggregate(c)
						select c).ToList<Calculation>();
					if (list2.Count > 0)
					{
						PlanOperationContext planOperationContext = this.CreateAggregateTable(members, i, list2, refTables);
						if (planOperationContext != null)
						{
							planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.ScopedAggregatesTable(this.m_dataShapeContext.DataShape.Id, dataMember.Id), this.m_declarations, false, null, false);
							Util.AddToLazyList<PlanOperationContext>(ref list, planOperationContext);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003D238 File Offset: 0x0003B438
		private PlanOperationContext CreateAggregateTable(IReadOnlyList<DataMember> members, int memberIndx, IReadOnlyList<Calculation> aggCalculations, IReadOnlyList<TableReference> refTables)
		{
			PlanOperationContext planOperationContext = AggregatesTableBuilder.CreateAggregatesTable(this.m_plannerContext, this.m_dataShapeContext, this.m_declarations, refTables, Util.EmptyReadOnlyList<PlanOperation>(), this.m_outputExpressionTable, new RowScopesMetadata(this.m_dataShapeContext.RowScopes), aggCalculations, members[memberIndx], this.m_plannerContext.TelemetryInfo);
			return this.AddSubtotalColumns(planOperationContext, members, memberIndx);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003D298 File Offset: 0x0003B498
		private PlanOperationContext AddSubtotalColumns(PlanOperationContext aggregateTable, IReadOnlyList<DataMember> members, int stopFilteringScopeMemberIndx)
		{
			List<PlanProjectItem> list = null;
			List<SubtotalColumnFilteringMetadata> list2 = null;
			for (int i = 0; i < members.Count; i++)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_dataShapeContext.Annotations.TryGetBatchSubtotalAnnotation(members[i], out batchSubtotalAnnotation))
				{
					if (list == null && list2 == null)
					{
						list = new List<PlanProjectItem>(members.Count + 1);
						list2 = new List<SubtotalColumnFilteringMetadata>(members.Count);
					}
					bool subtotalIndicatorColumnFilteringValue = BatchDataSetPlanningUtils.GetSubtotalIndicatorColumnFilteringValue(i, stopFilteringScopeMemberIndx);
					list2.Add(new SubtotalColumnFilteringMetadata(members[i], new bool?(subtotalIndicatorColumnFilteringValue)));
					PlanNewColumnProjectItem subtotalIndicatorColumnProjectItem = batchSubtotalAnnotation.SubtotalIndicatorColumnName.GetSubtotalIndicatorColumnProjectItem(subtotalIndicatorColumnFilteringValue ? LiteralExpressionNode.True : LiteralExpressionNode.False, this.m_plannerContext.ErrorContext);
					list.Add(subtotalIndicatorColumnProjectItem);
				}
			}
			if (list.IsNullOrEmpty<PlanProjectItem>())
			{
				return aggregateTable;
			}
			list.Add(PlanPreserveAllColumnsProjectItem.Instance);
			PlanOperation planOperation = aggregateTable.Table;
			planOperation = planOperation.Project(list, false);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(list2, aggregateTable.RespectsInstanceFilters);
			return aggregateTable.ReplaceTable(planOperation, null, planOperationFilteringMetadata, null);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003D394 File Offset: 0x0003B594
		private PlanOperationContext JoinAggregateTablesWithInputTable(List<PlanOperationContext> tables)
		{
			PlanOperationContext planOperationContext = this.m_inputTable.LeftOuterJoin(tables[0], this.m_plannerContext.ScopeTree);
			for (int i = 1; i < tables.Count; i++)
			{
				planOperationContext = planOperationContext.LeftOuterJoin(tables[i], this.m_plannerContext.ScopeTree);
			}
			return planOperationContext;
		}

		// Token: 0x04000713 RID: 1811
		private readonly DataShapeContext m_dataShapeContext;

		// Token: 0x04000714 RID: 1812
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000715 RID: 1813
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000716 RID: 1814
		private readonly IAggregatesPlanningContext m_plannerContext;

		// Token: 0x04000717 RID: 1815
		private readonly PlanOperationContext m_inputTable;
	}
}
