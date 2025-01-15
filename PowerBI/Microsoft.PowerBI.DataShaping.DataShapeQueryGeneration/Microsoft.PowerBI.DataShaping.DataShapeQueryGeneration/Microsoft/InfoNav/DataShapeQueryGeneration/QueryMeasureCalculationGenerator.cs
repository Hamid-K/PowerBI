using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B5 RID: 181
	internal sealed class QueryMeasureCalculationGenerator
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00018DDC File Offset: 0x00016FDC
		private QueryMeasureCalculationGenerator()
		{
			this._groupingMeasuresIndexDict = new Dictionary<DataShapeBindingAxisGrouping, List<int>>();
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00018DF0 File Offset: 0x00016FF0
		internal static QueryMeasureCalculationGenerator Parse(DataShapeBinding binding)
		{
			QueryMeasureCalculationGenerator queryMeasureCalculationGenerator = new QueryMeasureCalculationGenerator();
			List<int> list = new List<int>();
			queryMeasureCalculationGenerator.ParseBindingAxis(binding.Secondary, ref list);
			queryMeasureCalculationGenerator.ParseBindingAxis(binding.Primary, ref list);
			return queryMeasureCalculationGenerator;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00018E24 File Offset: 0x00017024
		internal bool ShouldGroupProjectMeasure(DataShapeBindingAxisGrouping grouping, int selectIndex)
		{
			List<int> list;
			return this._groupingMeasuresIndexDict.TryGetValue(grouping, out list) && list.Contains(selectIndex);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00018E4C File Offset: 0x0001704C
		internal bool IsGroupMeasureCalc(int selectIndex)
		{
			using (Dictionary<DataShapeBindingAxisGrouping, List<int>>.ValueCollection.Enumerator enumerator = this._groupingMeasuresIndexDict.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Contains(selectIndex))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00018EAC File Offset: 0x000170AC
		private void ParseBindingAxis(DataShapeBindingAxis axis, ref List<int> suppressed)
		{
			if (axis == null)
			{
				return;
			}
			for (int i = axis.Groupings.Count - 1; i >= 0; i--)
			{
				DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping = axis.Groupings[i];
				List<int> currentGroupSuppressed = axis.Groupings[i].SuppressedProjections;
				if (!currentGroupSuppressed.IsNullOrEmpty<int>())
				{
					foreach (int num in currentGroupSuppressed)
					{
						if (!suppressed.Contains(num))
						{
							suppressed.Add(num);
						}
					}
				}
				if (!suppressed.IsNullOrEmpty<int>())
				{
					List<int> list = suppressed.FindAll((int selectIndex) => currentGroupSuppressed == null || !currentGroupSuppressed.Contains(selectIndex));
					if (list.IsNotEmpty<int>())
					{
						this._groupingMeasuresIndexDict.Add(dataShapeBindingAxisGrouping, list);
						if (currentGroupSuppressed == null)
						{
							suppressed.Clear();
						}
						else
						{
							suppressed = new List<int>(currentGroupSuppressed);
						}
					}
				}
			}
		}

		// Token: 0x0400037E RID: 894
		private readonly Dictionary<DataShapeBindingAxisGrouping, List<int>> _groupingMeasuresIndexDict;
	}
}
