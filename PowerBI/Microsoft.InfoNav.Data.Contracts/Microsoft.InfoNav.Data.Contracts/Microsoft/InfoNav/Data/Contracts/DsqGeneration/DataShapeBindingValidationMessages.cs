using System;

namespace Microsoft.InfoNav.Data.Contracts.DsqGeneration
{
	// Token: 0x020000FF RID: 255
	internal static class DataShapeBindingValidationMessages
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0000DA87 File Offset: 0x0000BC87
		internal static string NullOrEmptyProperty(object parent, string property)
		{
			return StringUtil.FormatInvariant("The property {0} is null or empty on {1} ", property, parent.GetType().Name);
		}

		// Token: 0x040002C9 RID: 713
		internal static readonly string AggregateKindNoneAggregationsNull = "When DataShapeBindingAggregate.Kind is None, DataShapeBindingAggregate.Aggregations should not be null or empty.";

		// Token: 0x040002CA RID: 714
		internal static readonly string AggregateSelectNotInProjectionReferencedIndices = "ProjectionReferencedIndices does not contain DataShapeBindingAggregate.Select";

		// Token: 0x040002CB RID: 715
		internal static readonly string AggregationsNotMatchAggregateKind = "When DataShapeBindingAggregate.Kind is set, all aggregation in DataShapeBindingAggregate.Aggregations should match the kind.";

		// Token: 0x040002CC RID: 716
		internal static readonly string IndicesNotContiguous = "Indices are not contiguous in DataReductionScope.Primary or DataReductionScope.Secondary.";

		// Token: 0x040002CD RID: 717
		internal static readonly string InvalidAggregatePercentile = "Encountered invalid DataShapeBindingAggregateContainer.Percentile. When Exclusive is true K should be in (0, 1) and when Exclusive is false K should be in [0, 1].";

		// Token: 0x040002CE RID: 718
		internal static readonly string InvalidAggregateScopeDepth = "Absolute depth of the primary or secondary groups in AggregateScope should not be less than 0.";

		// Token: 0x040002CF RID: 719
		internal static readonly string InvalidAggregationNumber = "Exactly one aggregation on DataShapeBindingAggregateContainer should be set.";

		// Token: 0x040002D0 RID: 720
		internal static readonly string InvalidDataReductionAlgorithm = "DataReductionAlgorithm should not have more than one Algorithm.";

		// Token: 0x040002D1 RID: 721
		internal static readonly string InvalidLimitCount = "Limit.Count should be more than 0 when limit.Type is not First nor Last.";

		// Token: 0x040002D2 RID: 722
		internal static readonly string InvalidLimitTarget = "DataShapeBindingLimit.Target is not valid. Target.Primary and Target.Secondary should not be null or have value at the same time.";

		// Token: 0x040002D3 RID: 723
		internal static readonly string InvalidLimitTargetGroupIndex = "Invalid limit.Target group index.";

		// Token: 0x040002D4 RID: 724
		internal static readonly string InvalidLimitType = "When limit.Type is First or Last, limit.Count should be 0.";

		// Token: 0x040002D5 RID: 725
		internal static readonly string NoGroupsConstrainedByDataReductionScope = "No groups to be constrained by DataReductionScope. DataReductionScope.Primary and DataReductionScope.Secondary should not be both null nor empty.";

		// Token: 0x040002D6 RID: 726
		internal static readonly string NullAggregateContainer = "Null DataShapeBindingAggregateContainer found in DataShapeBindingAggregate.Aggregations.";

		// Token: 0x040002D7 RID: 727
		internal static readonly string NullScopedDataReduction = "Null scopedDataReduction found in DataReduction.Scoped.";

		// Token: 0x040002D8 RID: 728
		internal static readonly string RepeatedIndicesLimitTarget = "Repeated indices found in Limit.Target.Primary or Limit.Target.Secondary.";

		// Token: 0x040002D9 RID: 729
		internal static readonly string RepeatedIndicesProjectionsOrGroupBy = "Repeated indices found in GroupBy or Projections.";

		// Token: 0x040002DA RID: 730
		internal static readonly string ShowItemsWithNoDataNotInProjections = "All the items in ShowItemsWithNoData should be in Groupings.Projections.";

		// Token: 0x040002DB RID: 731
		internal static readonly string SuppressedProjectionsOnOuterGroupings = "If SuppressedProjection is used on an outer grouping, then all inner groupings need to also have it. Grouping at index {0} is missing expected SuppressedProjection.";

		// Token: 0x040002DC RID: 732
		internal static readonly string SuppressedProjectionsOnAllPrimaryGroupings = "Projection {0} is suppressed on all Primary groupings. A projection is not allowed to be suppressed on all primary groupings because we do not support computing it just for the secondary groups.";

		// Token: 0x040002DD RID: 733
		internal static readonly string SuppressedProjectionsOnPrimaryMissingFromSecondary = "If SuppressedProjection is used on a Primary grouping, then all Secondary groupings must also have it, as measures can't be computed at intersections other than the innermost.";

		// Token: 0x040002DE RID: 734
		internal static readonly string NullQueryReferenceContainer = "{0}[{1}] is null.";

		// Token: 0x040002DF RID: 735
		internal static readonly string NullQueryReference = "{0}[{1}].QueryReference is null.";

		// Token: 0x040002E0 RID: 736
		internal static readonly string NonNullQueryReferenceSourceName = "{0}[{1}].QueryReference.SourceName must be null. {0} must refer to the top-level query.";

		// Token: 0x040002E1 RID: 737
		internal static readonly string InvalidQueryReferenceExpressionName = "{0}[{1}].ExpressionName must not be null or empty.";
	}
}
