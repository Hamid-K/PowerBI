using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A9 RID: 425
	internal sealed class ScopedAggregatesInputTableBuilder
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003CD58 File Offset: 0x0003AF58
		private ScopedAggregatesInputTableBuilder(DataShapeContext dataShapeContext, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, PlanOperationContext coreTable, bool respectInstanceFilters)
		{
			this.m_dataShapeContext = dataShapeContext;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_declarations = declarations;
			this.m_coreTable = coreTable;
			this.m_subtotalColumnFilterConditions = new Dictionary<DataMember, ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions>();
			this.m_respectInstanceFilters = respectInstanceFilters;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003CD90 File Offset: 0x0003AF90
		internal static IReadOnlyList<TableReference> PrepareScopedAggregateInputTables(DataShapeContext dataShapeContext, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, PlanOperationContext coreTable, IReadOnlyList<DataMember> dynamicMembers, bool respectInstanceFilters)
		{
			return new ScopedAggregatesInputTableBuilder(dataShapeContext, outputExpressionTable, declarations, coreTable, respectInstanceFilters).PrepareScopedAggregateInputTables(dynamicMembers);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		private IReadOnlyList<TableReference> PrepareScopedAggregateInputTables(IReadOnlyList<DataMember> dynamicMembers)
		{
			this.PrepareDataMembersSubtotalColumnFilterConditions(dynamicMembers);
			return this.CreateFilteredReferenceTables(dynamicMembers);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		private void PrepareDataMembersSubtotalColumnFilterConditions(IReadOnlyList<DataMember> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_dataShapeContext.Annotations.TryGetBatchSubtotalAnnotation(members[i], out batchSubtotalAnnotation))
				{
					Expression expression = FilterUtils.CreateColumnReferenceExpression(this.m_outputExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName);
					ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions subtotalColumnFilterConditions = new ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions
					{
						SubtotalColumnCondition = new UnaryFilterCondition
						{
							Expression = expression
						},
						NegatedSubtotalColumnCondition = new UnaryFilterCondition
						{
							Expression = expression,
							Not = true
						}
					};
					this.m_subtotalColumnFilterConditions.Add(members[i], subtotalColumnFilterConditions);
				}
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003CE4C File Offset: 0x0003B04C
		private List<TableReference> CreateFilteredReferenceTables(IReadOnlyList<DataMember> dynamicMembers)
		{
			List<TableReference> list = new List<TableReference>();
			if (this.m_subtotalColumnFilterConditions.IsNullOrEmpty<KeyValuePair<DataMember, ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions>>())
			{
				list.Add(new TableReference(this.m_coreTable, PlanNames.ScopedAggregateReferenceTable(this.m_dataShapeContext.DataShape.Id, this.m_respectInstanceFilters), this.m_declarations, RowResultSetType.Unrestricted));
				return list;
			}
			for (int i = 0; i < dynamicMembers.Count; i++)
			{
				TableReference tableReference = this.CreateFilteredReferenceTable(dynamicMembers, i);
				if (tableReference != null)
				{
					list.Add(tableReference);
				}
			}
			return list;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
		private TableReference CreateFilteredReferenceTable(IReadOnlyList<DataMember> members, int currentMemberIndex)
		{
			IReadOnlyList<SubtotalColumnFilteringMetadata> readOnlyList;
			FilterCondition filterCondition = this.CreateLevelFilterCondition(members, currentMemberIndex, out readOnlyList);
			PlanOperation planOperation = this.m_coreTable.Table.FilterBy(filterCondition);
			DataMember dataMember = members[currentMemberIndex];
			IReadOnlyList<Calculation> readOnlyList2 = this.m_dataShapeContext.ScopeTree.GetAllItemsInScope<Calculation>(dataMember.Id).ToReadOnlyList<Calculation>();
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(this.GetReferenceTableTotalMembers(readOnlyList, members, currentMemberIndex), this.m_respectInstanceFilters);
			return new TableReference(new PlanOperationContext(planOperation, this.m_dataShapeContext.ScopeTree.GetAllParentScopes(dataMember, members.First<DataMember>()).ToReadOnlyList<IScope>(), readOnlyList2, this.m_coreTable.ShowAll.TakeUntil(dataMember).ToReadOnlyList<DataMember>(), planOperationFilteringMetadata), PlanNames.ScopedAggregateReferenceTable(dataMember.Id, this.m_respectInstanceFilters), this.m_declarations, RowResultSetType.Unrestricted);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003CF84 File Offset: 0x0003B184
		private FilterCondition CreateLevelFilterCondition(IReadOnlyList<DataMember> members, int currentMemberIndex, out IReadOnlyList<SubtotalColumnFilteringMetadata> subtotalColumnFilteringMetadata)
		{
			List<FilterCondition> list = this.CreateSubtotalIndicatorColumnConditionsForGroup(members, currentMemberIndex, out subtotalColumnFilteringMetadata);
			if (list.Count == 1)
			{
				return list[0];
			}
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list
			};
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
		private List<FilterCondition> CreateSubtotalIndicatorColumnConditionsForGroup(IReadOnlyList<DataMember> members, int stopFilteringScopeMemberIndx, out IReadOnlyList<SubtotalColumnFilteringMetadata> subtotalColumnFilteringMetadata)
		{
			List<SubtotalColumnFilteringMetadata> list = new List<SubtotalColumnFilteringMetadata>(members.Count);
			List<FilterCondition> list2 = new List<FilterCondition>(members.Count);
			for (int i = 0; i < members.Count; i++)
			{
				ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions subtotalColumnFilterConditions;
				if (this.m_subtotalColumnFilterConditions.TryGetValue(members[i], out subtotalColumnFilterConditions))
				{
					bool subtotalIndicatorColumnFilteringValue = BatchDataSetPlanningUtils.GetSubtotalIndicatorColumnFilteringValue(i, stopFilteringScopeMemberIndx);
					list.Add(new SubtotalColumnFilteringMetadata(members[i], new bool?(subtotalIndicatorColumnFilteringValue)));
					list2.Add(subtotalIndicatorColumnFilteringValue ? subtotalColumnFilterConditions.SubtotalColumnCondition : subtotalColumnFilterConditions.NegatedSubtotalColumnCondition);
				}
			}
			subtotalColumnFilteringMetadata = list;
			return list2;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003D050 File Offset: 0x0003B250
		private IReadOnlyList<SubtotalColumnFilteringMetadata> GetReferenceTableTotalMembers(IReadOnlyList<SubtotalColumnFilteringMetadata> coreTableFilteredTotals, IReadOnlyList<DataMember> members, int currentMemberIndex)
		{
			int num = ((currentMemberIndex + 1 < members.Count) ? (currentMemberIndex + 1) : currentMemberIndex);
			List<SubtotalColumnFilteringMetadata> list = new List<SubtotalColumnFilteringMetadata>(num + 1);
			int i;
			Func<SubtotalColumnFilteringMetadata, bool> <>9__0;
			int j;
			for (i = 0; i <= num; i = j + 1)
			{
				Func<SubtotalColumnFilteringMetadata, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (SubtotalColumnFilteringMetadata ft) => ft.Member == members[i]);
				}
				SubtotalColumnFilteringMetadata subtotalColumnFilteringMetadata = coreTableFilteredTotals.FirstOrDefault(func);
				if (subtotalColumnFilteringMetadata != null)
				{
					list.Add(subtotalColumnFilteringMetadata);
				}
				j = i;
			}
			return list;
		}

		// Token: 0x0400070D RID: 1805
		private readonly DataShapeContext m_dataShapeContext;

		// Token: 0x0400070E RID: 1806
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400070F RID: 1807
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000710 RID: 1808
		private readonly PlanOperationContext m_coreTable;

		// Token: 0x04000711 RID: 1809
		private readonly bool m_respectInstanceFilters;

		// Token: 0x04000712 RID: 1810
		private readonly Dictionary<DataMember, ScopedAggregatesInputTableBuilder.SubtotalColumnFilterConditions> m_subtotalColumnFilterConditions;

		// Token: 0x02000300 RID: 768
		private class SubtotalColumnFilterConditions
		{
			// Token: 0x04000B17 RID: 2839
			internal FilterCondition SubtotalColumnCondition;

			// Token: 0x04000B18 RID: 2840
			internal FilterCondition NegatedSubtotalColumnCondition;
		}
	}
}
