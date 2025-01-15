using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003B RID: 59
	internal static class ScopedDataReductionGroupCoverageResolver
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00009B30 File Offset: 0x00007D30
		internal static void ResolveCoverage(List<IntermediateScopedReductionAlgorithm> scopedReductions, DataShapeGenerationErrorContext errorContext, IReadOnlyList<QueryMember> primaryMembers, IReadOnlyList<QueryMember> secondaryMembers)
		{
			IntermediateScopedReductionAlgorithm[] array;
			IntermediateScopedReductionAlgorithm[] array2;
			ScopedDataReductionGroupCoverageResolver.ResolveOverlap(scopedReductions, errorContext, primaryMembers.Count, secondaryMembers.Count, out array, out array2);
			ScopedDataReductionGroupCoverageResolver.FillGapsInAxis(scopedReductions, array, primaryMembers, delegate(IntermediateReductionScope scope, List<int> indices)
			{
				scope.Primary = indices;
			});
			ScopedDataReductionGroupCoverageResolver.FillGapsInAxis(scopedReductions, array2, secondaryMembers, delegate(IntermediateReductionScope scope, List<int> indices)
			{
				scope.Secondary = indices;
			});
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009BA4 File Offset: 0x00007DA4
		private static void ResolveOverlap(List<IntermediateScopedReductionAlgorithm> scopedReductions, DataShapeGenerationErrorContext errorContext, int primaryGroupCount, int secondaryGroupCount, out IntermediateScopedReductionAlgorithm[] reductionsByPrimaryIndex, out IntermediateScopedReductionAlgorithm[] reductionsBySecondaryIndex)
		{
			reductionsByPrimaryIndex = ScopedDataReductionGroupCoverageResolver.CreateReductionByGroupIndexMap(primaryGroupCount);
			reductionsBySecondaryIndex = ScopedDataReductionGroupCoverageResolver.CreateReductionByGroupIndexMap(secondaryGroupCount);
			for (int i = 0; i < scopedReductions.Count; i++)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = scopedReductions[i];
				ScopedDataReductionGroupCoverageResolver.ResolveOverlapOnAxis(errorContext, "primary", reductionsByPrimaryIndex, intermediateScopedReductionAlgorithm, intermediateScopedReductionAlgorithm.Scope.Primary);
				ScopedDataReductionGroupCoverageResolver.ResolveOverlapOnAxis(errorContext, "secondary", reductionsBySecondaryIndex, intermediateScopedReductionAlgorithm, intermediateScopedReductionAlgorithm.Scope.Secondary);
				if (intermediateScopedReductionAlgorithm.Scope.IsEmpty())
				{
					scopedReductions.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009C28 File Offset: 0x00007E28
		private static IntermediateScopedReductionAlgorithm[] CreateReductionByGroupIndexMap(int groupCount)
		{
			if (groupCount != 0)
			{
				return new IntermediateScopedReductionAlgorithm[groupCount];
			}
			return null;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009C38 File Offset: 0x00007E38
		private static void ResolveOverlapOnAxis(DataShapeGenerationErrorContext errorContext, string groupingAxis, IntermediateScopedReductionAlgorithm[] reductionsByGroupIndex, IntermediateScopedReductionAlgorithm reduction, List<int> scopeAxis)
		{
			if (scopeAxis == null)
			{
				return;
			}
			int? num = null;
			int? num2 = null;
			for (int i = 0; i < scopeAxis.Count; i++)
			{
				int num3 = scopeAxis[i];
				if (reductionsByGroupIndex[num3] == null)
				{
					reductionsByGroupIndex[num3] = reduction;
					num = new int?(num ?? i);
					num2 = new int?(i);
				}
				else
				{
					errorContext.Register(DataShapeGenerationMessages.OverlappingDataReductionScopes(EngineMessageSeverity.Warning, groupingAxis, num3));
					if (num != null)
					{
						break;
					}
				}
			}
			if (num != null)
			{
				int? num4 = num2;
				int num5 = scopeAxis.Count - 1;
				if ((num4.GetValueOrDefault() < num5) & (num4 != null))
				{
					int num6 = num2.Value + 1;
					scopeAxis.RemoveRange(num6, scopeAxis.Count - num6);
				}
				num4 = num;
				num5 = 0;
				if ((num4.GetValueOrDefault() > num5) & (num4 != null))
				{
					scopeAxis.RemoveRange(0, num.Value);
					return;
				}
			}
			else
			{
				scopeAxis.Clear();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009D38 File Offset: 0x00007F38
		private static void FillGapsInAxis(List<IntermediateScopedReductionAlgorithm> scopedReductions, IntermediateScopedReductionAlgorithm[] reductionsByGroupIndex, IReadOnlyList<QueryMember> queryMembers, Action<IntermediateReductionScope, List<int>> applyIndicesToScope)
		{
			if (reductionsByGroupIndex == null)
			{
				return;
			}
			List<int> list = null;
			int num = 0;
			while (num < reductionsByGroupIndex.Length && !queryMembers[num].IsContextOnly)
			{
				if (reductionsByGroupIndex[num] == null)
				{
					if (list == null)
					{
						list = new List<int> { num };
						IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = new IntermediateScopedReductionAlgorithm
						{
							Scope = new IntermediateReductionScope(),
							Algorithm = new IntermediateSimpleLimit
							{
								Kind = IntermediateSimpleLimitKind.Top
							}
						};
						applyIndicesToScope(intermediateScopedReductionAlgorithm.Scope, list);
						scopedReductions.Add(intermediateScopedReductionAlgorithm);
					}
					else
					{
						list.Add(num);
					}
				}
				else if (list != null)
				{
					list = null;
				}
				num++;
			}
		}
	}
}
