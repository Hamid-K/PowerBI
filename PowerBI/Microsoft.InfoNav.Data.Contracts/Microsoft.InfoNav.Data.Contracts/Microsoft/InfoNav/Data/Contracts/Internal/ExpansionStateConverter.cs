using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001CC RID: 460
	internal sealed class ExpansionStateConverter
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00017C59 File Offset: 0x00015E59
		private ExpansionStateConverter(DataShapeBindingAxisExpansionState expansion)
		{
			this._expansion = expansion;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00017C68 File Offset: 0x00015E68
		internal static List<List<FilterDefinition>> ConvertToInstanceFilters(DataShapeBindingAxisExpansionState expansion, int groupingLevelCount)
		{
			if (expansion == null)
			{
				return null;
			}
			return new ExpansionStateConverter(expansion).ConvertToInstanceFilters(groupingLevelCount);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00017C81 File Offset: 0x00015E81
		internal List<List<FilterDefinition>> ConvertToInstanceFilters(int groupingLevelCount)
		{
			this.PrepareTupleLists();
			this.TraverseNode(this._expansion.Instances, -1, new List<QueryExpressionContainer>());
			return ExpansionStateConverter.PropagateParentFiltersToChildren(this.BuildFiltersPerLevel(), groupingLevelCount);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00017CAC File Offset: 0x00015EAC
		private static List<List<FilterDefinition>> PropagateParentFiltersToChildren(FilterDefinition[] filtersPerLevel, int groupingLevelCount)
		{
			int num = filtersPerLevel.Length;
			int num2 = groupingLevelCount - 1;
			List<List<FilterDefinition>> list = new List<List<FilterDefinition>>(num2);
			for (int i = 0; i < num2; i++)
			{
				FilterDefinition filterDefinition = ((i < num) ? filtersPerLevel[i] : null);
				List<FilterDefinition> list2 = new List<FilterDefinition>(i + 1);
				if (i >= 1 && list[i - 1] != null)
				{
					list2.AddRange(list[i - 1]);
				}
				if (filterDefinition != null)
				{
					list2.Add(filterDefinition);
				}
				if (list2.IsEmpty<FilterDefinition>())
				{
					list.Add(null);
				}
				else
				{
					list.Add(list2);
				}
			}
			return list;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00017D38 File Offset: 0x00015F38
		private void TraverseNode(DataShapeBindingAxisExpansionInstance node, int levelIdx, List<QueryExpressionContainer> currentPathFromRoot)
		{
			bool flag = !node.Values.IsNullOrEmptyCollection<QueryExpressionContainer>() && levelIdx >= 0;
			if (flag)
			{
				DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel = this._expansion.Levels[levelIdx];
				foreach (QueryExpressionContainer queryExpressionContainer in node.Values)
				{
					currentPathFromRoot.Add(queryExpressionContainer);
				}
				List<QueryExpressionContainer> list = new List<QueryExpressionContainer>(currentPathFromRoot);
				if (this._tupleListsPerLevel != null)
				{
					if (this._tupleListsPerLevel[levelIdx] == null)
					{
						this._tupleListsPerLevel[levelIdx] = new List<List<QueryExpressionContainer>>();
					}
					this._tupleListsPerLevel[levelIdx].Add(list);
				}
				if (this._tuplePathsEndingAtLevel != null && node.Children.IsNullOrEmptyCollection<DataShapeBindingAxisExpansionInstance>())
				{
					if (this._tuplePathsEndingAtLevel[levelIdx] == null)
					{
						this._tuplePathsEndingAtLevel[levelIdx] = new List<List<QueryExpressionContainer>>();
					}
					this._tuplePathsEndingAtLevel[levelIdx].Add(list);
				}
			}
			if (!node.Children.IsNullOrEmptyCollection<DataShapeBindingAxisExpansionInstance>())
			{
				foreach (DataShapeBindingAxisExpansionInstance dataShapeBindingAxisExpansionInstance in node.Children)
				{
					this.TraverseNode(dataShapeBindingAxisExpansionInstance, levelIdx + 1, currentPathFromRoot);
				}
			}
			if (flag)
			{
				foreach (QueryExpressionContainer queryExpressionContainer2 in node.Values)
				{
					currentPathFromRoot.RemoveAt(currentPathFromRoot.Count - 1);
				}
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00017EC0 File Offset: 0x000160C0
		private FilterDefinition[] BuildFiltersPerLevel()
		{
			FilterDefinition[] array = new FilterDefinition[this._expansion.Levels.Count];
			for (int i = 0; i < this._expansion.Levels.Count; i++)
			{
				array[i] = this.BuildFilterPerLevel(i);
			}
			return array;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00017F0C File Offset: 0x0001610C
		private FilterDefinition BuildFilterPerLevel(int levelIdx)
		{
			DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel = this._expansion.Levels[levelIdx];
			FilterDefinitionBuilder<ExpansionStateConverter> filterDefinitionBuilder = new FilterDefinitionBuilder<ExpansionStateConverter>(this);
			if (dataShapeBindingAxisExpansionLevel.Default == ExpansionDefaultState.Collapsed)
			{
				List<List<QueryExpressionContainer>> list = this._tupleListsPerLevel[levelIdx];
				List<QueryExpressionContainer> list2 = this.BuildExpressions(levelIdx, filterDefinitionBuilder);
				filterDefinitionBuilder.WithWhere(list2.In(list, new QueryEqualitySemanticsKind?(QueryEqualitySemanticsKind.Identity)));
			}
			if (dataShapeBindingAxisExpansionLevel.Default == ExpansionDefaultState.Expanded)
			{
				List<List<QueryExpressionContainer>> list3 = this._tuplePathsEndingAtLevel[levelIdx];
				if (list3 == null)
				{
					return null;
				}
				List<QueryExpressionContainer> list4 = this.BuildExpressions(levelIdx, filterDefinitionBuilder);
				filterDefinitionBuilder.WithWhere(list4.In(list3, new QueryEqualitySemanticsKind?(QueryEqualitySemanticsKind.Identity)).Not());
			}
			return filterDefinitionBuilder.Build();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00017FAC File Offset: 0x000161AC
		private List<QueryExpressionContainer> BuildExpressions(int levelIdx, FilterDefinitionBuilder<ExpansionStateConverter> instanceFilterBuilder)
		{
			List<EntitySource> from = this._expansion.From;
			List<QueryExpressionContainer> list = new List<QueryExpressionContainer>();
			for (int i = 0; i <= levelIdx; i++)
			{
				DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel = this._expansion.Levels[i];
				instanceFilterBuilder.WithFrom(from);
				list.AddRange(dataShapeBindingAxisExpansionLevel.Expressions);
			}
			return list;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00018000 File Offset: 0x00016200
		private void PrepareTupleLists()
		{
			IList<DataShapeBindingAxisExpansionLevel> levels = this._expansion.Levels;
			this._tuplePathsEndingAtLevel = null;
			this._tupleListsPerLevel = null;
			foreach (DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel in levels)
			{
				if (dataShapeBindingAxisExpansionLevel.Default == ExpansionDefaultState.Collapsed && this._tupleListsPerLevel == null)
				{
					this._tupleListsPerLevel = new List<List<QueryExpressionContainer>>[levels.Count];
				}
				if (dataShapeBindingAxisExpansionLevel.Default == ExpansionDefaultState.Expanded && this._tuplePathsEndingAtLevel == null)
				{
					this._tuplePathsEndingAtLevel = new List<List<QueryExpressionContainer>>[levels.Count];
				}
				if (this._tupleListsPerLevel != null && this._tuplePathsEndingAtLevel != null)
				{
					break;
				}
			}
		}

		// Token: 0x04000678 RID: 1656
		private readonly DataShapeBindingAxisExpansionState _expansion;

		// Token: 0x04000679 RID: 1657
		private List<List<QueryExpressionContainer>>[] _tuplePathsEndingAtLevel;

		// Token: 0x0400067A RID: 1658
		private List<List<QueryExpressionContainer>>[] _tupleListsPerLevel;
	}
}
