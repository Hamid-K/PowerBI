using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200016F RID: 367
	internal static class BatchDataSetPlanningSegmentationUtils
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x000366BC File Offset: 0x000348BC
		public static List<DataMember> GetStartAtMembers(IEnumerable<DataMember> primaryDynamics, BatchSubtotalAnnotations subtotalAnnotations)
		{
			List<DataMember> list = new List<DataMember>();
			foreach (DataMember dataMember in primaryDynamics)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				IList<IIdentifiable> list2;
				if (subtotalAnnotations.TryGetSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && subtotalAnnotations.TryGetSubtotalAnnotationSources(batchSubtotalAnnotation, out list2))
				{
					foreach (IIdentifiable identifiable in list2)
					{
						DataMember dataMember2 = identifiable as DataMember;
						if ((dataMember2 != null && dataMember2.SubtotalStartPosition.GetValueOrDefault<bool>()) || dataMember.HasStartPosition())
						{
							list.Add(dataMember2);
						}
					}
				}
				if (dataMember.HasStartPosition())
				{
					list.Add(dataMember);
				}
			}
			return list;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003678C File Offset: 0x0003498C
		public static bool NeedsSegmentationTopN(int? segmentSize, bool hasStartPosition, Limit primaryHierarchyLimit)
		{
			if (segmentSize == null)
			{
				return false;
			}
			if (hasStartPosition)
			{
				return true;
			}
			if (primaryHierarchyLimit == null)
			{
				return true;
			}
			TopLimitOperator topLimitOperator = primaryHierarchyLimit.Operator as TopLimitOperator;
			return topLimitOperator == null || segmentSize.Value < topLimitOperator.GetPaddedCount();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000367D0 File Offset: 0x000349D0
		public static int? DetermineEffectiveSegmentSize(Candidate<int> requestedPrimaryLeafCount, IReadOnlyList<DataMember> primaryDynamics, bool hasStartPosition, BatchSubtotalAnnotations subtotalAnnotations)
		{
			int? num = BatchDataSetPlanningSegmentationUtils.DetermineEffectiveSegmentSizeWithoutTotals(requestedPrimaryLeafCount);
			if (num != null)
			{
				int num2 = num.Value;
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (!hasStartPosition && subtotalAnnotations.TryGetSubtotalAnnotation(primaryDynamics[0], out batchSubtotalAnnotation) && batchSubtotalAnnotation.SortDirection == SortDirection.Descending)
				{
					num2++;
				}
				return new int?(num2);
			}
			return null;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00036828 File Offset: 0x00034A28
		public static int? DetermineEffectiveSegmentSizeWithoutTotals(Candidate<int> requestedPrimaryLeafCount)
		{
			if (requestedPrimaryLeafCount.IsSpecified<int>())
			{
				return new int?(requestedPrimaryLeafCount.Value + 1);
			}
			return null;
		}
	}
}
