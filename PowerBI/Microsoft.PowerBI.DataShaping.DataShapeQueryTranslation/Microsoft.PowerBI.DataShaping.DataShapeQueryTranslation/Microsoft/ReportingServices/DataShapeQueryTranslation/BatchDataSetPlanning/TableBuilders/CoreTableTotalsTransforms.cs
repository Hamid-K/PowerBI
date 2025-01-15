using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C3 RID: 451
	internal sealed class CoreTableTotalsTransforms
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00040589 File Offset: 0x0003E789
		internal CoreTableTotalsTransforms(CoreTableTotalsTransforms.Context context)
		{
			this.m_context = context;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00040598 File Offset: 0x0003E798
		internal static PlanOperationContext RemoveAllTotalRows(PlanOperationContext table, CoreTableTotalsTransforms.Context context)
		{
			IReadOnlyList<SubtotalColumnFilteringMetadata> readOnlyList;
			FilterCondition filterCondition = CoreTableTotalsTransforms.CreateRemoveTotalRowsCondition(table.Totals, context, true, out readOnlyList);
			if (filterCondition == null)
			{
				return table;
			}
			PlanOperation planOperation = table.Table.FilterBy(filterCondition);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(readOnlyList, false);
			return table.ReplaceTable(planOperation, null, planOperationFilteringMetadata, null);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000405DC File Offset: 0x0003E7DC
		private static FilterCondition CreateRemoveTotalRowsCondition(IReadOnlyList<DataMember> dynamics, CoreTableTotalsTransforms.Context context, bool removeAll, out IReadOnlyList<SubtotalColumnFilteringMetadata> subtotalColumnFilteringMetadata)
		{
			List<FilterCondition> list = null;
			List<SubtotalColumnFilteringMetadata> list2 = new List<SubtotalColumnFilteringMetadata>(dynamics.Count);
			foreach (DataMember dataMember in dynamics)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (context.Annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && (removeAll || batchSubtotalAnnotation.Usage.IsIncludeInOutput()))
				{
					BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(context.ExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName, false);
					Util.AddToLazyList<FilterCondition>(ref list, binaryFilterCondition);
					list2.Add(new SubtotalColumnFilteringMetadata(dataMember, new bool?(false)));
				}
			}
			subtotalColumnFilteringMetadata = list2;
			if (list == null)
			{
				return null;
			}
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

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000406B0 File Offset: 0x0003E8B0
		internal static PlanOperationContext RemoveUnneededSortByMeasureTotals(PlanOperationContext coreTable, CoreTableTotalsTransforms.Context context)
		{
			return new CoreTableTotalsTransforms(context).RemoveUnneededSortByMeasureTotals(coreTable);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000406C0 File Offset: 0x0003E8C0
		private PlanOperationContext RemoveUnneededSortByMeasureTotals(PlanOperationContext coreTable)
		{
			if (!this.m_context.HasSortByMeasure)
			{
				return coreTable;
			}
			PlanOperationContext planOperationContext = this.RemoveUnneededSortByMeasureTotalRowsFromTable(coreTable);
			return this.RemoveUnneededSortByMeasureColumns(planOperationContext);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000406EC File Offset: 0x0003E8EC
		private PlanOperationContext RemoveUnneededSortByMeasureColumns(PlanOperationContext input)
		{
			IReadOnlyList<DataMember> allGroups = input.GetAllGroups();
			bool flag = true;
			List<PlanProjectItem> list = this.ProjectGroups(allGroups, true, out flag);
			if (flag)
			{
				return input;
			}
			foreach (Calculation calculation in input.Calculations)
			{
				list.Add(calculation.ToPreserveColumnProjectItem());
			}
			PlanOperation planOperation = input.Table.Project(list, false);
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00040778 File Offset: 0x0003E978
		private PlanOperationContext RemoveUnneededSortByMeasureTotalRowsFromTable(PlanOperationContext input)
		{
			IReadOnlyList<SubtotalColumnFilteringMetadata> readOnlyList;
			FilterCondition filterCondition = this.RemoveUnneededSortByMeasureTotalRowsCondition(input.GetAllGroups(), out readOnlyList);
			if (filterCondition == null)
			{
				return input;
			}
			PlanOperation planOperation = input.Table.FilterBy(filterCondition);
			PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(readOnlyList, false);
			return input.ReplaceTable(planOperation, null, planOperationFilteringMetadata, null);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000407B8 File Offset: 0x0003E9B8
		internal static FilterCondition CreateUnneededSortByMeasureTotalRowsCondition(IScope inputRowScope, CoreTableTotalsTransforms.Context context)
		{
			CoreTableTotalsTransforms coreTableTotalsTransforms = new CoreTableTotalsTransforms(context);
			List<DataMember> list = context.ScopeTree.GetAllParentScopes(inputRowScope).OfType<DataMember>().ToList<DataMember>();
			IReadOnlyList<SubtotalColumnFilteringMetadata> readOnlyList;
			return coreTableTotalsTransforms.RemoveUnneededSortByMeasureTotalRowsCondition(list, out readOnlyList);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000407EC File Offset: 0x0003E9EC
		internal static PlanOperation FilterSortByMeasureTotalsFromTable(CoreTableTotalsTransforms.Context context, PlanOperation table, IReadOnlyList<DataMember> dynamics, bool isPrimary)
		{
			CoreTableTotalsTransforms coreTableTotalsTransforms = new CoreTableTotalsTransforms(context);
			List<DataMember> list = null;
			FilterCondition filterCondition = coreTableTotalsTransforms.RemoveUnneededSortByMeasureTotalRowsCondition(dynamics, isPrimary, ref list);
			if (filterCondition == null)
			{
				return table;
			}
			return table.FilterBy(filterCondition);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00040818 File Offset: 0x0003EA18
		private FilterCondition RemoveUnneededSortByMeasureTotalRowsCondition(IReadOnlyList<DataMember> dynamics, out IReadOnlyList<SubtotalColumnFilteringMetadata> remainingTotalsFilteringMetadata)
		{
			remainingTotalsFilteringMetadata = null;
			List<DataMember> list = null;
			FilterCondition filterCondition = this.RemoveUnneededSortByMeasureTotalRowsCondition(dynamics, true, ref list);
			FilterCondition filterCondition2 = this.RemoveUnneededSortByMeasureTotalRowsCondition(dynamics, false, ref list);
			if (!list.IsNullOrEmpty<DataMember>())
			{
				List<SubtotalColumnFilteringMetadata> list2 = new List<SubtotalColumnFilteringMetadata>(list.Count);
				foreach (DataMember dataMember in list)
				{
					list2.Add(new SubtotalColumnFilteringMetadata(dataMember, new bool?(true)));
				}
				remainingTotalsFilteringMetadata = list2.ToReadOnlyList<SubtotalColumnFilteringMetadata>();
			}
			if (filterCondition == null)
			{
				return filterCondition2;
			}
			if (filterCondition2 == null)
			{
				return filterCondition;
			}
			List<FilterCondition> list3 = new List<FilterCondition>();
			CoreTableTotalsTransforms.FlattenAllFilterCondition(list3, filterCondition);
			CoreTableTotalsTransforms.FlattenAllFilterCondition(list3, filterCondition2);
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list3
			};
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000408E4 File Offset: 0x0003EAE4
		private static void FlattenAllFilterCondition(List<FilterCondition> filterConditions, FilterCondition condition)
		{
			CompoundFilterCondition compoundFilterCondition = condition as CompoundFilterCondition;
			if (compoundFilterCondition != null && compoundFilterCondition.Operator.Value == CompoundFilterOperator.All)
			{
				filterConditions.AddRange(compoundFilterCondition.Conditions);
				return;
			}
			filterConditions.Add(condition);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0004091C File Offset: 0x0003EB1C
		private FilterCondition RemoveUnneededSortByMeasureTotalRowsCondition(IReadOnlyList<DataMember> allDynamicMembers, bool primary, ref List<DataMember> remainingTotals)
		{
			DataMemberAnnotations dataMemberAnnotations = this.m_context.Annotations.DataMemberAnnotations;
			IList<DataMember> list = allDynamicMembers.Where((DataMember d) => dataMemberAnnotations.IsPrimaryMember(d) == primary).Evaluate<DataMember>();
			int count = list.Count;
			bool[] array = new bool[count];
			BatchSubtotalAnnotation[] array2 = new BatchSubtotalAnnotation[count];
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				DataMember dataMember = list[i];
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_context.Annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation))
				{
					array2[i] = batchSubtotalAnnotation;
					if (batchSubtotalAnnotation.Usage.IsIncludeInOutput())
					{
						array[i] = true;
						if (num < 0)
						{
							num = i;
						}
						Util.AddToLazyList<DataMember>(ref remainingTotals, dataMember);
					}
					else
					{
						array[i] = false;
					}
				}
				else
				{
					array[i] = false;
					array2[i] = null;
				}
			}
			int num2 = array.Length - 1;
			while (num2 >= 0 && (array2[num2] == null || array[num2]))
			{
				num2--;
			}
			if (num2 < 0)
			{
				return null;
			}
			if (num < 0)
			{
				num = num2 + 1;
			}
			List<FilterCondition> list2 = new List<FilterCondition>();
			for (int j = 0; j < num; j++)
			{
				if (array2[j] != null)
				{
					list2.Add(FilterUtils.CreateBooleanColumnFilterCondition(this.m_context.ExpressionTable, array2[j].SubtotalIndicatorColumnName, false));
				}
			}
			if (num2 > num)
			{
				List<FilterCondition> list3 = new List<FilterCondition>();
				for (int k = num; k < num2; k++)
				{
					if (array2[k] != null && array[k])
					{
						BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(this.m_context.ExpressionTable, array2[k].SubtotalIndicatorColumnName, true);
						list3.Add(binaryFilterCondition);
					}
				}
				BinaryFilterCondition binaryFilterCondition2 = FilterUtils.CreateBooleanColumnFilterCondition(this.m_context.ExpressionTable, array2[num2].SubtotalIndicatorColumnName, false);
				list3.Add(binaryFilterCondition2);
				if (list3.Count == 1)
				{
					list2.Add(list3[0]);
				}
				else if (list3.Count > 1)
				{
					CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
					{
						Operator = CompoundFilterOperator.Any,
						Conditions = list3
					};
					list2.Add(compoundFilterCondition);
				}
			}
			if (list2.Count == 0)
			{
				return null;
			}
			if (list2.Count == 1)
			{
				return list2[0];
			}
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list2
			};
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00040B5C File Offset: 0x0003ED5C
		private List<PlanProjectItem> ProjectGroups(IEnumerable<DataMember> dynamicMembers, bool includeSortKeysAtMeasureScope, out bool isIdentityProjection)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			isIdentityProjection = true;
			foreach (DataMember dataMember in dynamicMembers)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				list.AddRange(dataMember.ToGroupProjectItems(this.m_context.Annotations, SubtotalUsage.Output, includeSortKeysAtMeasureScope, out batchSubtotalAnnotation));
				if (batchSubtotalAnnotation != null && !batchSubtotalAnnotation.Usage.IsIncludeInOutput())
				{
					isIdentityProjection = false;
				}
			}
			return list;
		}

		// Token: 0x04000774 RID: 1908
		private readonly CoreTableTotalsTransforms.Context m_context;

		// Token: 0x0200030C RID: 780
		internal sealed class Context
		{
			// Token: 0x06001724 RID: 5924 RVA: 0x00052684 File Offset: 0x00050884
			internal Context(DataShapeAnnotations annotations, WritableExpressionTable expressionTable, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, ScopeTree scopeTree)
			{
				this.m_annotations = annotations;
				this.m_expressionTable = expressionTable;
				this.m_sortByMeasureExpressionMappings = sortByMeasureExpressionMappings;
				this.m_scopeTree = scopeTree;
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06001725 RID: 5925 RVA: 0x000526A9 File Offset: 0x000508A9
			internal DataShapeAnnotations Annotations
			{
				get
				{
					return this.m_annotations;
				}
			}

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x06001726 RID: 5926 RVA: 0x000526B1 File Offset: 0x000508B1
			internal WritableExpressionTable ExpressionTable
			{
				get
				{
					return this.m_expressionTable;
				}
			}

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x06001727 RID: 5927 RVA: 0x000526B9 File Offset: 0x000508B9
			internal bool HasSortByMeasure
			{
				get
				{
					return this.m_sortByMeasureExpressionMappings.Count > 0;
				}
			}

			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x06001728 RID: 5928 RVA: 0x000526C9 File Offset: 0x000508C9
			internal ScopeTree ScopeTree
			{
				get
				{
					return this.m_scopeTree;
				}
			}

			// Token: 0x04000B32 RID: 2866
			private readonly DataShapeAnnotations m_annotations;

			// Token: 0x04000B33 RID: 2867
			private readonly WritableExpressionTable m_expressionTable;

			// Token: 0x04000B34 RID: 2868
			private readonly BatchSortByMeasureExpressionMappings m_sortByMeasureExpressionMappings;

			// Token: 0x04000B35 RID: 2869
			private readonly ScopeTree m_scopeTree;
		}
	}
}
