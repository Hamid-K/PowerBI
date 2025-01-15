using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003E RID: 62
	internal static class DataShapeBindingExtensions
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00009EE0 File Offset: 0x000080E0
		internal static bool IsSuppressedJoinPredicate(this DataShapeBinding binding, string parentQueryName, int selectIndex, IReadOnlyList<ResolvedQuerySelect> selects, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName)
		{
			if (binding != null)
			{
				IList<int> suppressedJoinPredicates = binding.SuppressedJoinPredicates;
				bool? flag = ((suppressedJoinPredicates != null) ? new bool?(suppressedJoinPredicates.Contains(selectIndex)) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					return true;
				}
			}
			return DataShapeBindingExtensions.IsQueryReferenceContainerMatch<DataShapeBindingSuppressedJoinPredicate>(suppressedJoinPredicatesByName, parentQueryName, selects[selectIndex].Name);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009F3D File Offset: 0x0000813D
		internal static bool IsHiddenProjection(this DataShapeBinding binding, string parentQueryName, int selectIndex, IReadOnlyList<ResolvedQuerySelect> selects, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections)
		{
			return DataShapeBindingExtensions.IsQueryReferenceContainerMatch<DataShapeBindingHiddenProjections>(hiddenProjections, parentQueryName, selects[selectIndex].Name);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009F54 File Offset: 0x00008154
		internal static bool HasShowItemsWithNoData(this DataShapeBindingAxis axis)
		{
			if (axis == null)
			{
				return false;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			if (groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return false;
			}
			for (int i = 0; i < groupings.Count; i++)
			{
				if (!groupings[i].ShowItemsWithNoData.IsNullOrEmptyCollection<int>())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009FA4 File Offset: 0x000081A4
		internal static bool ShouldGenerateRestartIdentities(this DataShapeBinding binding)
		{
			DataReductionTopNPerLevelSampleLimit dataReductionTopNPerLevelSampleLimit;
			if (binding == null)
			{
				dataReductionTopNPerLevelSampleLimit = null;
			}
			else
			{
				DataReduction dataReduction = binding.DataReduction;
				if (dataReduction == null)
				{
					dataReductionTopNPerLevelSampleLimit = null;
				}
				else
				{
					DataReductionAlgorithm primary = dataReduction.Primary;
					dataReductionTopNPerLevelSampleLimit = ((primary != null) ? primary.TopNPerLevel : null);
				}
			}
			return dataReductionTopNPerLevelSampleLimit != null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009FD0 File Offset: 0x000081D0
		internal static bool ShouldTrackNonMeasureSortKeysForReferencing(this DataShapeBindingAxis axis, int groupingIndex)
		{
			if (axis.Synchronization.IsNullOrEmpty<DataShapeBindingAxisSynchronizedGroupingBlock>())
			{
				return false;
			}
			using (IEnumerator<DataShapeBindingAxisSynchronizedGroupingBlock> enumerator = axis.Synchronization.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Groupings.Contains(groupingIndex))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A038 File Offset: 0x00008238
		private static bool IsQueryReferenceContainerMatch<TQueryReferenceContainer>(IReadOnlyList<TQueryReferenceContainer> queryReferenceContainers, string parentQueryName, string selectName) where TQueryReferenceContainer : IQueryReferenceContainer
		{
			return !queryReferenceContainers.IsNullOrEmpty<TQueryReferenceContainer>() && !string.IsNullOrEmpty(selectName) && queryReferenceContainers.Any((TQueryReferenceContainer queryReferenceContainer) => QueryNameComparer.Instance.Equals(queryReferenceContainer.QueryReference.SourceName, parentQueryName) && QueryNameComparer.Instance.Equals(queryReferenceContainer.QueryReference.ExpressionName, selectName));
		}
	}
}
