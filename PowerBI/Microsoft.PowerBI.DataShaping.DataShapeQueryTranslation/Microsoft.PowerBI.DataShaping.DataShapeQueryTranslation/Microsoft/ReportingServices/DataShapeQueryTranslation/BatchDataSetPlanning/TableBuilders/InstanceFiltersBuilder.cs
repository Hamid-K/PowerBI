using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D5 RID: 469
	internal sealed class InstanceFiltersBuilder
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x00043C18 File Offset: 0x00041E18
		internal InstanceFiltersBuilder(WritableExpressionTable expressionTable, TranslationErrorContext errorContext, HashSet<FilterCondition> instanceFiltersRequiringPostProcessing)
		{
			this.m_expressionTable = expressionTable;
			this.m_errorContext = errorContext;
			this.m_instanceFiltersRequiringPostFiltering = instanceFiltersRequiringPostProcessing;
			this.m_filterModelReferences = new List<FilterExpressionInfo>();
			this.m_groupingLevels = new List<InstanceFiltersBuilder.InstanceGroupingLevel>();
			this.m_groupKeyExpressionIds = new List<ExpressionId>();
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00043C58 File Offset: 0x00041E58
		public static FilterCondition BuildInstanceFilters(IReadOnlyList<DataMember> groupByMembers, WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataShapeAnnotations annotations, HashSet<FilterCondition> instanceFiltersRequiringPostFiltering, out List<PlanProjectItem> projections)
		{
			InstanceFiltersBuilder instanceFiltersBuilder = new InstanceFiltersBuilder(expressionTable, errorContext, instanceFiltersRequiringPostFiltering);
			for (int i = 0; i < groupByMembers.Count; i++)
			{
				DataMember dataMember = groupByMembers[i];
				if (!dataMember.ContextOnly)
				{
					string text = null;
					BatchSubtotalAnnotation batchSubtotalAnnotation;
					if (annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput())
					{
						text = batchSubtotalAnnotation.SubtotalIndicatorColumnName;
					}
					instanceFiltersBuilder.AddMember(dataMember, text);
				}
			}
			return instanceFiltersBuilder.BuildFilterWithProjectionMapping(out projections);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00043CC4 File Offset: 0x00041EC4
		internal void AddMember(DataMember member, string subtotalIndicatorColumnName)
		{
			InstanceFiltersBuilder.InstanceGroupingLevel instanceGroupingLevel = new InstanceFiltersBuilder.InstanceGroupingLevel();
			this.m_groupingLevels.Add(instanceGroupingLevel);
			this.AddInstanceFilter(member.InstanceFilters, member.Id, instanceGroupingLevel);
			this.AddSubtotalColumn(subtotalIndicatorColumnName, instanceGroupingLevel);
			this.TrackGroupKeyIdentities(member);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00043D08 File Offset: 0x00041F08
		private void TrackGroupKeyIdentities(DataMember member)
		{
			foreach (GroupKey groupKey in member.Group.GroupKeys)
			{
				this.m_groupKeyExpressionIds.Add(groupKey.Value.ExpressionId.Value);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00043D78 File Offset: 0x00041F78
		private void AddInstanceFilter(IReadOnlyList<FilterCondition> filters, Identifier ownerId, InstanceFiltersBuilder.InstanceGroupingLevel level)
		{
			if (filters.IsNullOrEmpty<FilterCondition>())
			{
				return;
			}
			level.InstanceFilters = this.PreserveInstanceFiltersForPostProcessing(filters);
			foreach (FilterCondition filterCondition in level.InstanceFilters)
			{
				List<FilterExpressionInfo> list = FilterExpressionCollector.CollectNonMeasureModelExpressions(filterCondition, ownerId, this.m_expressionTable, this.m_errorContext);
				this.m_filterModelReferences.AddRange(list);
				this.m_hasFilters = true;
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00043DFC File Offset: 0x00041FFC
		private IReadOnlyList<FilterCondition> PreserveInstanceFiltersForPostProcessing(IReadOnlyList<FilterCondition> filters)
		{
			if (this.m_instanceFiltersRequiringPostFiltering == null)
			{
				return filters;
			}
			return filters.Where((FilterCondition f) => this.m_instanceFiltersRequiringPostFiltering.Contains(f)).ToList<FilterCondition>();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00043E20 File Offset: 0x00042020
		private void AddSubtotalColumn(string column, InstanceFiltersBuilder.InstanceGroupingLevel level)
		{
			if (column == null)
			{
				return;
			}
			this.m_hasSubtotals = true;
			Expression expression = FilterUtils.CreateColumnReferenceExpression(this.m_expressionTable, column);
			level.SubtotalColumnCondition = new UnaryFilterCondition
			{
				Expression = expression
			};
			level.NegatedSubtotalColumnCondition = new UnaryFilterCondition
			{
				Expression = expression,
				Not = true
			};
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00043E78 File Offset: 0x00042078
		internal FilterCondition BuildFilterWithProjectionMapping(out List<PlanProjectItem> projectItems)
		{
			projectItems = null;
			if (!this.m_hasFilters)
			{
				return null;
			}
			CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
			{
				Operator = (this.m_hasSubtotals ? CompoundFilterOperator.Any : CompoundFilterOperator.All),
				Conditions = new List<FilterCondition>()
			};
			FilterCondition filterCondition = this.CreateSubtotalCondition(0);
			if (filterCondition != null)
			{
				compoundFilterCondition.Conditions.Add(filterCondition);
			}
			for (int i = 0; i < this.m_groupingLevels.Count; i++)
			{
				filterCondition = this.CreateSubtotalCondition(i + 1);
				InstanceFiltersBuilder.InstanceGroupingLevel instanceGroupingLevel = this.m_groupingLevels[i];
				if (instanceGroupingLevel.InstanceFilters == null)
				{
					if (filterCondition != null)
					{
						compoundFilterCondition.Conditions.Add(filterCondition);
					}
				}
				else
				{
					FilterCondition filterCondition2 = InstanceFiltersBuilder.CreateSubtotalAndInstanceFiltersCondition(filterCondition, instanceGroupingLevel);
					compoundFilterCondition.Conditions.Add(filterCondition2);
				}
			}
			projectItems = this.BuildProjectItems();
			return compoundFilterCondition.UnwrapSingleOrSelf();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00043F3C File Offset: 0x0004213C
		private static FilterCondition CreateSubtotalAndInstanceFiltersCondition(FilterCondition subtotalMatchingCondition, InstanceFiltersBuilder.InstanceGroupingLevel currentLevel)
		{
			CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = new List<FilterCondition>()
			};
			if (subtotalMatchingCondition != null)
			{
				compoundFilterCondition.Conditions.Add(subtotalMatchingCondition);
			}
			compoundFilterCondition.Conditions.AddRange(currentLevel.InstanceFilters);
			return compoundFilterCondition.UnwrapSingleOrSelf();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00043F8C File Offset: 0x0004218C
		private FilterCondition CreateSubtotalCondition(int indexToStartTotalMatching)
		{
			if (!this.m_hasSubtotals)
			{
				return null;
			}
			CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = new List<FilterCondition>()
			};
			if (indexToStartTotalMatching > 0)
			{
				int num = indexToStartTotalMatching - 1;
				InstanceFiltersBuilder.InstanceGroupingLevel instanceGroupingLevel = this.m_groupingLevels[num];
				while (instanceGroupingLevel.SubtotalColumnCondition == null && num >= 0)
				{
					instanceGroupingLevel = this.m_groupingLevels[num--];
				}
				if (instanceGroupingLevel.SubtotalColumnCondition != null)
				{
					compoundFilterCondition.Conditions.Add(instanceGroupingLevel.NegatedSubtotalColumnCondition);
				}
			}
			if (indexToStartTotalMatching < this.m_groupingLevels.Count)
			{
				int num2 = indexToStartTotalMatching;
				InstanceFiltersBuilder.InstanceGroupingLevel instanceGroupingLevel2 = this.m_groupingLevels[num2];
				while (instanceGroupingLevel2.SubtotalColumnCondition == null && num2 < this.m_groupingLevels.Count)
				{
					instanceGroupingLevel2 = this.m_groupingLevels[num2++];
				}
				if (instanceGroupingLevel2.SubtotalColumnCondition != null)
				{
					compoundFilterCondition.Conditions.Add(instanceGroupingLevel2.SubtotalColumnCondition);
				}
			}
			return compoundFilterCondition.UnwrapSingleOrSelf();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00044078 File Offset: 0x00042278
		private List<PlanProjectItem> BuildProjectItems()
		{
			Dictionary<ExpressionId, List<ExpressionId>> dictionary = new Dictionary<ExpressionId, List<ExpressionId>>();
			foreach (FilterExpressionInfo filterExpressionInfo in this.m_filterModelReferences)
			{
				ExpressionNode node = this.m_expressionTable.GetNode(filterExpressionInfo.Expression);
				foreach (ExpressionId expressionId in this.m_groupKeyExpressionIds)
				{
					ExpressionNode node2 = this.m_expressionTable.GetNode(expressionId);
					if (node.Equals(node2))
					{
						dictionary.Add(expressionId, filterExpressionInfo.Expression.ExpressionId.Value);
						break;
					}
				}
			}
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			foreach (KeyValuePair<ExpressionId, List<ExpressionId>> keyValuePair in dictionary)
			{
				list.Add(new PlanMapColumnIdentityProjectItem(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x040007A1 RID: 1953
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x040007A2 RID: 1954
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040007A3 RID: 1955
		private readonly HashSet<FilterCondition> m_instanceFiltersRequiringPostFiltering;

		// Token: 0x040007A4 RID: 1956
		private List<FilterExpressionInfo> m_filterModelReferences;

		// Token: 0x040007A5 RID: 1957
		private List<ExpressionId> m_groupKeyExpressionIds;

		// Token: 0x040007A6 RID: 1958
		private List<InstanceFiltersBuilder.InstanceGroupingLevel> m_groupingLevels;

		// Token: 0x040007A7 RID: 1959
		private bool m_hasFilters;

		// Token: 0x040007A8 RID: 1960
		private bool m_hasSubtotals;

		// Token: 0x02000316 RID: 790
		private class InstanceGroupingLevel
		{
			// Token: 0x04000B53 RID: 2899
			internal IReadOnlyList<FilterCondition> InstanceFilters;

			// Token: 0x04000B54 RID: 2900
			internal FilterCondition SubtotalColumnCondition;

			// Token: 0x04000B55 RID: 2901
			internal FilterCondition NegatedSubtotalColumnCondition;
		}
	}
}
