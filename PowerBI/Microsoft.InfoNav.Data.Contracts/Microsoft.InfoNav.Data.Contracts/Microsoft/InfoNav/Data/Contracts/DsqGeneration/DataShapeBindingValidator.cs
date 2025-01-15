using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.DsqGeneration
{
	// Token: 0x02000100 RID: 256
	public sealed class DataShapeBindingValidator
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0000DBA7 File Offset: 0x0000BDA7
		public DataShapeBindingValidator(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		public static bool IsValid(DataShapeBinding binding, string rootQueryName = null)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(binding, rootQueryName);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000DBC9 File Offset: 0x0000BDC9
		public static bool IsValid(DataShapeBindingAggregateContainer aggregateContainer)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(aggregateContainer);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000DBDB File Offset: 0x0000BDDB
		public static bool IsValid(AggregateScope aggregateScope)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(aggregateScope);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000DBED File Offset: 0x0000BDED
		public static bool IsValid(DataReduction reduction)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(reduction);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		public static bool IsValid(DataReductionScope scope)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(scope);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000DC11 File Offset: 0x0000BE11
		public static bool IsValid(DataReductionAlgorithm algorithm)
		{
			return new DataShapeBindingValidator(new ErrorTrackingOnlyErrorContext()).Validate(algorithm);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000DC24 File Offset: 0x0000BE24
		public bool Validate(DataShapeBinding binding, string rootQueryName)
		{
			if (binding.Primary == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.NullOrEmptyProperty(binding, "Primary"), new object[0]);
				return false;
			}
			if (!this.ValidateIfNonNull(binding.Primary) || !this.ValidateIfNonNull(binding.Secondary))
			{
				return false;
			}
			HashSet<int> hashSet = new HashSet<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			return this.TryAddAxisIndices(binding.Primary, hashSet, hashSet2) && this.TryAddAxisIndices(binding.Secondary, hashSet, hashSet2) && this.TryAddIndices(binding.Projections, hashSet) && this.Validate(binding.Aggregates, hashSet) && this.Validate(binding.Limits, binding.Primary, binding.Secondary) && this.ValidateSuppressedProjections(binding) && this.Validate(binding.DataReduction) && this.IsSuppressedJoinPredicatesByNameValid(binding.SuppressedJoinPredicatesByName) && this.IsHiddenProjectionsValid(binding.HiddenProjections, rootQueryName);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000DD24 File Offset: 0x0000BF24
		private bool ValidateIfNonNull(DataShapeBindingAxis axis)
		{
			if (axis == null)
			{
				return true;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			if (groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullOrEmptyProperty(axis, "groupings"), new object[0]);
				return false;
			}
			foreach (DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping in groupings)
			{
				if (!this.Validate(dataShapeBindingAxisGrouping))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		private bool Validate(DataShapeBindingAxisGrouping grouping)
		{
			IList<int> projections = grouping.Projections;
			if (projections.IsNullOrEmptyCollection<int>())
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullOrEmptyProperty(grouping, "projections"), new object[0]);
				return false;
			}
			IList<int> showItemsWithNoData = grouping.ShowItemsWithNoData;
			if (showItemsWithNoData != null)
			{
				foreach (int num in showItemsWithNoData)
				{
					if (!projections.Contains(num))
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.ShowItemsWithNoDataNotInProjections, new object[0]);
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000DE50 File Offset: 0x0000C050
		private bool TryAddAxisIndices(DataShapeBindingAxis axis, HashSet<int> projectionIndices, HashSet<int> groupByIndices)
		{
			if (axis == null)
			{
				return true;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			for (int i = 0; i < groupings.Count; i++)
			{
				if (!this.TryAddIndices(groupings[i].Projections, projectionIndices))
				{
					return false;
				}
			}
			for (int j = 0; j < groupings.Count; j++)
			{
				if (!this.TryAddIndices(groupings[j].GroupBy, groupByIndices))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		private bool TryAddIndices(IList<int> indices, HashSet<int> referencedIndices)
		{
			if (indices == null)
			{
				return true;
			}
			for (int i = 0; i < indices.Count; i++)
			{
				if (!referencedIndices.Add(indices[i]))
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.RepeatedIndicesProjectionsOrGroupBy, new object[0]);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000DF0C File Offset: 0x0000C10C
		private bool Validate(IList<DataShapeBindingAggregate> aggregates, ISet<int> referencedIndices)
		{
			if (aggregates.IsNullOrEmptyCollection<DataShapeBindingAggregate>())
			{
				return true;
			}
			foreach (DataShapeBindingAggregate dataShapeBindingAggregate in aggregates)
			{
				if (!this.Validate(dataShapeBindingAggregate, referencedIndices))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000DF68 File Offset: 0x0000C168
		private bool Validate(DataShapeBindingAggregate aggregate, ISet<int> referencedIndices)
		{
			if (!referencedIndices.Contains(aggregate.Select))
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.AggregateSelectNotInProjectionReferencedIndices, new object[0]);
				return false;
			}
			bool flag = aggregate.Aggregations.IsNullOrEmptyCollection<DataShapeBindingAggregateContainer>();
			if (!flag && !this.Validate(aggregate.Aggregations))
			{
				return false;
			}
			if (aggregate.Kind == DataShapeBindingAggregateKind.None && flag)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.AggregateKindNoneAggregationsNull, new object[0]);
				return false;
			}
			if (aggregate.Kind != DataShapeBindingAggregateKind.None && !flag)
			{
				for (int i = 0; i < aggregate.Aggregations.Count; i++)
				{
					if (aggregate.Aggregations[i].Max != null && aggregate.Kind.HasFlag(DataShapeBindingAggregateKind.Max))
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.AggregationsNotMatchAggregateKind, new object[0]);
						return false;
					}
					if (aggregate.Aggregations[i].Min != null && aggregate.Kind.HasFlag(DataShapeBindingAggregateKind.Min))
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.AggregationsNotMatchAggregateKind, new object[0]);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		private bool Validate(IList<DataShapeBindingAggregateContainer> aggregateContainerList)
		{
			for (int i = 0; i < aggregateContainerList.Count; i++)
			{
				if (aggregateContainerList[i] == null)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullAggregateContainer, new object[0]);
					return false;
				}
				if (!this.Validate(aggregateContainerList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		private bool Validate(DataShapeBindingAggregateContainer aggregateContainer)
		{
			int num = 0;
			DataShapeBindingPercentileAggregate percentile = aggregateContainer.Percentile;
			if (percentile != null)
			{
				num++;
				if (percentile.Exclusive)
				{
					if (percentile.K <= 0.0 || percentile.K >= 1.0)
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidAggregatePercentile, new object[0]);
						return false;
					}
				}
				else if (percentile.K < 0.0 || percentile.K > 1.0)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidAggregatePercentile, new object[0]);
					return false;
				}
			}
			if (aggregateContainer.Median != null)
			{
				num++;
			}
			if (aggregateContainer.Min != null)
			{
				num++;
			}
			if (aggregateContainer.Max != null)
			{
				num++;
			}
			if (aggregateContainer.Average != null)
			{
				num++;
			}
			if (num != 1)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidAggregationNumber, new object[0]);
				return false;
			}
			AggregateScope scope = aggregateContainer.Scope;
			return !(scope != null) || this.Validate(scope);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000E21C File Offset: 0x0000C41C
		private bool Validate(AggregateScope scope)
		{
			if (scope.PrimaryDepth < 0)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidAggregateScopeDepth, new object[0]);
				return false;
			}
			if (scope.SecondaryDepth != null)
			{
				int? secondaryDepth = scope.SecondaryDepth;
				int num = 0;
				if ((secondaryDepth.GetValueOrDefault() < num) & (secondaryDepth != null))
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidAggregateScopeDepth, new object[0]);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000E290 File Offset: 0x0000C490
		private bool Validate(IList<DataShapeBindingLimit> limits, DataShapeBindingAxis primaryAxis, DataShapeBindingAxis secondaryAxis)
		{
			if (limits != null)
			{
				HashSet<int> hashSet = new HashSet<int>();
				HashSet<int> hashSet2 = new HashSet<int>();
				foreach (DataShapeBindingLimit dataShapeBindingLimit in limits)
				{
					if (!this.Validate(dataShapeBindingLimit))
					{
						return false;
					}
					if (!this.HasValidLimitTargetGroupOrNull(dataShapeBindingLimit.Target.Primary, primaryAxis.Groupings.Count) || !this.HasValidLimitTargetGroupOrNull(dataShapeBindingLimit.Target.Secondary, (secondaryAxis != null) ? secondaryAxis.Groupings.Count : 0))
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidLimitTargetGroupIndex, new object[0]);
						return false;
					}
					if (!this.TryAddLimitTargetIndex(dataShapeBindingLimit.Target.Primary, hashSet) || !this.TryAddLimitTargetIndex(dataShapeBindingLimit.Target.Secondary, hashSet2))
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.RepeatedIndicesLimitTarget, new object[0]);
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		private bool Validate(DataShapeBindingLimit limit)
		{
			DataShapeBindingLimitTarget target = limit.Target;
			if ((target.Primary == null) ^ (target.Secondary != null))
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidLimitTarget, new object[0]);
				return false;
			}
			if (limit.Type == DataShapeBindingLimitType.First || limit.Type == DataShapeBindingLimitType.Last)
			{
				if (limit.Count != 0)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidLimitType, new object[0]);
					return false;
				}
				return true;
			}
			else
			{
				if (limit.Count <= 0)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidLimitCount, new object[0]);
					return false;
				}
				return true;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000E448 File Offset: 0x0000C648
		private bool Validate(DataReduction reduction)
		{
			return reduction == null || ((!(reduction.Primary != null) || this.Validate(reduction.Primary)) && (!(reduction.Secondary != null) || this.Validate(reduction.Secondary)) && (!(reduction.Intersection != null) || this.Validate(reduction.Intersection)) && (reduction.Scoped == null || this.Validate(reduction.Scoped)));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		private bool Validate(DataReductionAlgorithm algorithm)
		{
			int num = 0;
			if (algorithm.Top != null)
			{
				num++;
			}
			if (algorithm.Sample != null)
			{
				num++;
			}
			if (algorithm.Bottom != null)
			{
				num++;
			}
			if (algorithm.Window != null)
			{
				num++;
			}
			if (algorithm.BinnedLineSample != null)
			{
				num++;
			}
			if (algorithm.OverlappingPointsSample != null)
			{
				num++;
			}
			if (algorithm.TopNPerLevel != null)
			{
				num++;
			}
			if (num > 1)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidDataReductionAlgorithm, new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000E580 File Offset: 0x0000C780
		private bool Validate(IList<ScopedDataReduction> scopedDataReductionList)
		{
			foreach (ScopedDataReduction scopedDataReduction in scopedDataReductionList)
			{
				if (scopedDataReduction == null)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullScopedDataReduction, new object[0]);
					return false;
				}
				if (!this.Validate(scopedDataReduction))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		private bool Validate(ScopedDataReduction scopedDataReduction)
		{
			if (scopedDataReduction.Scope == null)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullOrEmptyProperty(scopedDataReduction, "Scope"), new object[0]);
				return false;
			}
			if (!this.Validate(scopedDataReduction.Scope))
			{
				return false;
			}
			if (scopedDataReduction.Algorithm == null)
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullOrEmptyProperty(scopedDataReduction, "Algorithm"), new object[0]);
				return false;
			}
			return this.Validate(scopedDataReduction.Algorithm);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000E67C File Offset: 0x0000C87C
		private bool Validate(DataReductionScope scope)
		{
			if (scope.Primary.IsNullOrEmptyCollection<int>() && scope.Secondary.IsNullOrEmptyCollection<int>())
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.NoGroupsConstrainedByDataReductionScope, new object[0]);
				return false;
			}
			if (!scope.Primary.IsNullOrEmptyCollection<int>() && !DataShapeBindingValidator.AreIndicesContiguous(scope.Primary))
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.IndicesNotContiguous, new object[0]);
				return false;
			}
			if (!scope.Secondary.IsNullOrEmptyCollection<int>() && !DataShapeBindingValidator.AreIndicesContiguous(scope.Secondary))
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.IndicesNotContiguous, new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000E720 File Offset: 0x0000C920
		private bool HasValidLimitTargetGroupOrNull(int? groupIndex, int groupCount)
		{
			return groupIndex == null || (groupIndex.Value >= 0 && groupIndex.Value < groupCount);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000E743 File Offset: 0x0000C943
		private bool TryAddLimitTargetIndex(int? groupIndex, HashSet<int> groupIndices)
		{
			return groupIndex == null || groupIndices.Add(groupIndex.Value);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000E764 File Offset: 0x0000C964
		private bool ValidateSuppressedProjections(DataShapeBinding binding)
		{
			List<int> list;
			List<int> list2;
			if (!this.AreSuppressedProjectionsValid(binding.Primary, true, out list) || !this.AreSuppressedProjectionsValid(binding.Secondary, false, out list2))
			{
				return false;
			}
			if (list.IsNullOrEmptyCollection<int>() || binding.Secondary == null)
			{
				return true;
			}
			foreach (DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping in binding.Secondary.Groupings)
			{
				if (dataShapeBindingAxisGrouping.SuppressedProjections.IsNullOrEmpty<int>() || !dataShapeBindingAxisGrouping.SuppressedProjections.ContainsAll(list))
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.SuppressedProjectionsOnPrimaryMissingFromSecondary, new object[0]);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000E828 File Offset: 0x0000CA28
		private bool AreSuppressedProjectionsValid(DataShapeBindingAxis axis, bool disallowSuppressedProjectionsOnAllGroupings, out List<int> allSuppressedProjections)
		{
			allSuppressedProjections = null;
			if (axis == null || axis.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return true;
			}
			if (disallowSuppressedProjectionsOnAllGroupings && !axis.Groupings[0].SuppressedProjections.IsNullOrEmpty<int>())
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.SuppressedProjectionsOnAllPrimaryGroupings, new object[] { axis.Groupings[0].SuppressedProjections.FirstOrDefault<int>() });
				return false;
			}
			for (int i = 0; i < axis.Groupings.Count; i++)
			{
				List<int> suppressedProjections = axis.Groupings[i].SuppressedProjections;
				if (!allSuppressedProjections.IsNullOrEmptyCollection<int>() && (suppressedProjections.IsNullOrEmptyCollection<int>() || !allSuppressedProjections.IsSubsetOf(suppressedProjections)))
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.SuppressedProjectionsOnOuterGroupings, new object[] { i });
					return false;
				}
				allSuppressedProjections = suppressedProjections;
			}
			return true;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000E909 File Offset: 0x0000CB09
		private bool IsSuppressedJoinPredicatesByNameValid(IList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByNameList)
		{
			return suppressedJoinPredicatesByNameList.IsNullOrEmptyCollection<DataShapeBindingSuppressedJoinPredicate>() || this.IsQueryReferenceContainerValid<DataShapeBindingSuppressedJoinPredicate>("SuppressedJoinPredicatesByName", suppressedJoinPredicatesByNameList, new Func<DataShapeBindingQueryReference, int, bool>(this.<IsSuppressedJoinPredicatesByNameValid>g__IsSuppressJoinPredicateByNameQueryReferenceValid|29_0));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000E930 File Offset: 0x0000CB30
		private bool IsHiddenProjectionsValid(IList<DataShapeBindingHiddenProjections> hiddenProjections, string rootQueryName)
		{
			DataShapeBindingValidator.<>c__DisplayClass30_0 CS$<>8__locals1 = new DataShapeBindingValidator.<>c__DisplayClass30_0();
			CS$<>8__locals1.rootQueryName = rootQueryName;
			CS$<>8__locals1.<>4__this = this;
			return hiddenProjections.IsNullOrEmptyCollection<DataShapeBindingHiddenProjections>() || this.IsQueryReferenceContainerValid<DataShapeBindingHiddenProjections>("HiddenProjections", hiddenProjections, new Func<DataShapeBindingQueryReference, int, bool>(CS$<>8__locals1.<IsHiddenProjectionsValid>g__IsHiddenProjectionQueryReferenceValid|0));
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000E974 File Offset: 0x0000CB74
		private bool IsQueryReferenceContainerValid<T>(string containerName, IList<T> queryReferenceContainers, Func<DataShapeBindingQueryReference, int, bool> isQueryReferenceValid) where T : IQueryReferenceContainer
		{
			bool flag = true;
			for (int i = 0; i < queryReferenceContainers.Count; i++)
			{
				T t = queryReferenceContainers[i];
				if (t == null)
				{
					this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullQueryReferenceContainer, new object[] { containerName, i });
					flag = false;
				}
				else
				{
					DataShapeBindingQueryReference queryReference = t.QueryReference;
					if (queryReference == null)
					{
						this._errorContext.RegisterError(DataShapeBindingValidationMessages.NullQueryReference, new object[] { containerName, i });
						flag = false;
					}
					else if (!isQueryReferenceValid(queryReference, i))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		private static bool AreIndicesContiguous(IList<int> indices)
		{
			int num = indices[0] - 1;
			foreach (int num2 in indices)
			{
				if (num2 < 0 || num2 != num + 1)
				{
					return false;
				}
				num = num2;
			}
			return true;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		[CompilerGenerated]
		private bool <IsSuppressedJoinPredicatesByNameValid>g__IsSuppressJoinPredicateByNameQueryReferenceValid|29_0(DataShapeBindingQueryReference queryReference, int queryReferenceIndex)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(queryReference.ExpressionName))
			{
				this._errorContext.RegisterError(DataShapeBindingValidationMessages.InvalidQueryReferenceExpressionName, new object[] { "SuppressedJoinPredicatesByName", queryReferenceIndex });
				flag = false;
			}
			return flag;
		}

		// Token: 0x040002E2 RID: 738
		private readonly IErrorContext _errorContext;
	}
}
