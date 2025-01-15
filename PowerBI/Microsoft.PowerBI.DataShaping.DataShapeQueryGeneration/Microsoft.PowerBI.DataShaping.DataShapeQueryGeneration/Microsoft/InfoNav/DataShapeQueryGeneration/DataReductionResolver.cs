using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001F RID: 31
	internal sealed class DataReductionResolver
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000060F4 File Offset: 0x000042F4
		private DataReductionResolver(DataShapeGenerationInternalContext context, DataReductionConfiguration config, QueryProjections projections, ResolvedQueryDefinition query, DataShapeBinding binding, DataReductionTelemetry telemetry)
		{
			this._context = context;
			this._config = config;
			this._projections = projections;
			this._query = query;
			this._binding = binding;
			this._telemetry = telemetry;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000612C File Offset: 0x0000432C
		internal static bool TryResolveDataReduction(DataShapeGenerationInternalContext context, DataReductionConfiguration config, IntermediateDataReduction reduction, QueryProjections projections, ResolvedQueryDefinition query, DataShapeBinding binding, SparklineDataStatistics sparklineStatistics, DataReductionTelemetry telemetry)
		{
			DataReductionResolver dataReductionResolver = new DataReductionResolver(context, config, projections, query, binding, telemetry);
			telemetry.CaptureInitialState(reduction, projections.PrimaryMembers.Count, projections.SecondaryMembers.Count);
			bool flag = dataReductionResolver.TryResolve(reduction);
			telemetry.CaptureFinalState(reduction, projections.PrimaryMembers.Count, projections.SecondaryMembers.Count);
			if (flag)
			{
				DataReductionResolver.AdjustWarningCounts(reduction);
				if (!DataReductionResolver.TryValidateSparklineCount(context, reduction, sparklineStatistics))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000061A4 File Offset: 0x000043A4
		[Conditional("DEBUG")]
		private static void AssertFullySpecified(IntermediateDataReduction reduction)
		{
			IntermediateReductionAlgorithm primary = reduction.Primary;
			IntermediateReductionAlgorithm secondary = reduction.Secondary;
			IntermediateReductionAlgorithm intersection = reduction.Intersection;
			if (reduction.Scoped != null)
			{
				foreach (IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm in reduction.Scoped)
				{
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006210 File Offset: 0x00004410
		private static bool TryValidateSparklineCount(DataShapeGenerationInternalContext context, IntermediateDataReduction reduction, SparklineDataStatistics sparklineStatistics)
		{
			if (sparklineStatistics.SparklinesTotalPointsCount == 0)
			{
				return true;
			}
			int num = 1;
			IntermediateReductionAlgorithm intersection = reduction.Intersection;
			int? num2 = ((intersection != null) ? intersection.Count : null);
			IntermediateReductionAlgorithm primary = reduction.Primary;
			int? num3 = ((primary != null) ? primary.Count : null);
			IntermediateReductionAlgorithm secondary = reduction.Secondary;
			int? num4 = DataReductionMath.MultiplyOptional(num3, (secondary != null) ? secondary.Count : null);
			if (num2 != null && num4 != null)
			{
				num *= Math.Min(num2.Value, num4.Value);
			}
			else
			{
				num = DataReductionMath.MultiplyOptional(num, num2);
				num = DataReductionMath.MultiplyOptional(num, num4);
			}
			if (reduction.Scoped != null)
			{
				foreach (IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm in reduction.Scoped)
				{
					num = DataReductionMath.MultiplyOptional(num, intermediateScopedReductionAlgorithm.Algorithm.Count);
				}
			}
			if (num * sparklineStatistics.SparklinesTotalPointsCount > 760000)
			{
				context.ErrorContext.Register(DataShapeGenerationMessages.UnsupportedSparklineNumberOfPoints(EngineMessageSeverity.Error, 760000));
				return false;
			}
			return true;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000633C File Offset: 0x0000453C
		private static void AdjustWarningCounts(IntermediateDataReduction reduction)
		{
			DataReductionResolver.AdjustWarningCount(reduction.Primary);
			DataReductionResolver.AdjustWarningCount(reduction.Secondary);
			DataReductionResolver.AdjustWarningCount(reduction.Intersection);
			DataReductionResolver.AdjustWarningCount(reduction.Scoped);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000636C File Offset: 0x0000456C
		private static void AdjustWarningCount(IntermediateReductionAlgorithm limit)
		{
			int? num = ((limit != null) ? limit.WarningCount : null);
			int? num2 = ((limit != null) ? limit.Count : null);
			if ((num.GetValueOrDefault() > num2.GetValueOrDefault()) & ((num != null) & (num2 != null)))
			{
				limit.WarningCount = limit.Count;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000063D4 File Offset: 0x000045D4
		private static void AdjustWarningCount(List<IntermediateScopedReductionAlgorithm> scopedReductions)
		{
			if (scopedReductions != null)
			{
				foreach (IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm in scopedReductions)
				{
					DataReductionResolver.AdjustWarningCount(intermediateScopedReductionAlgorithm.Algorithm);
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006428 File Offset: 0x00004628
		private bool TryResolve(IntermediateDataReduction reduction)
		{
			if (this._projections.PrimaryMembers.Count == 0 && this._projections.SecondaryMembers.Count == 0)
			{
				this.ResolveNoGroups(reduction);
				return true;
			}
			if (!reduction.Scoped.IsNullOrEmpty<IntermediateScopedReductionAlgorithm>())
			{
				this.ResolveScoped(reduction);
				return true;
			}
			if (this._projections.PrimaryMembers.Count > 0 && this._projections.SecondaryMembers.Count == 0)
			{
				this.ResolvePrimaryOnly(reduction);
				return true;
			}
			this.ResolvePrimarySecondaryAndIntersection(reduction);
			return true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000064B0 File Offset: 0x000046B0
		private void ResolveNoGroups(IntermediateDataReduction reduction)
		{
			if (reduction.Primary != null || reduction.Secondary != null || reduction.Intersection != null || reduction.Scoped != null)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.IgnoredDataReductionAlgorithm(EngineMessageSeverity.Warning, "primary, secondary, intersection and scoped"));
				reduction.Primary = null;
				reduction.Secondary = null;
				reduction.Intersection = null;
				reduction.Scoped = null;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006514 File Offset: 0x00004714
		private void ResolveScoped(IntermediateDataReduction reduction)
		{
			StructuralToScopedDataReductionConverter.ConvertToScoped(reduction, this._projections.PrimaryMembers.Count, this._projections.SecondaryMembers.Count);
			ScopedDataReductionGroupCoverageResolver.ResolveCoverage(reduction.Scoped, this._context.ErrorContext, this._projections.PrimaryMembers, this._projections.SecondaryMembers);
			this.FallbackAdvancedAlgorithms(reduction.Scoped);
			List<IntermediateScopedReductionAlgorithm> scoped = reduction.Scoped;
			global::System.ValueTuple<long, int, IList<DataReductionResolver.ScopedDataReductionResolutionState>, IList<DataReductionResolver.ScopedDataReductionResolutionState>> valueTuple = this.ComputeScopedReductionState(scoped);
			long num = valueTuple.Item1;
			int num2 = valueTuple.Item2;
			IList<DataReductionResolver.ScopedDataReductionResolutionState> item = valueTuple.Item3;
			IList<DataReductionResolver.ScopedDataReductionResolutionState> item2 = valueTuple.Item4;
			DataVolumeLevel activeDataVolumeConfigLevel = this.GetActiveDataVolumeConfigLevel(reduction, scoped, num2);
			this._telemetry.MaxIntersections = new int?(activeDataVolumeConfigLevel.MaxIntersectionCount);
			if (num > (long)activeDataVolumeConfigLevel.MaxIntersectionCount)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedReductionAlgorithmsExceedsMaxIntersections(EngineMessageSeverity.Warning, num));
				DataReductionResolver.ResetAllAlgorithmCounts(item);
				DataReductionResolver.ResetAllAlgorithmCounts(item2);
				num = 1L;
				num2 = 0;
			}
			if (scoped.Count == num2)
			{
				return;
			}
			DataReductionResolver.AssignUnspecifiedCounts(num, item, item2, activeDataVolumeConfigLevel);
			IntermediateDynamicLimits intermediateDynamicLimits = this.BuildScopedDynamicLimits(item, item2, activeDataVolumeConfigLevel);
			if (intermediateDynamicLimits != null)
			{
				reduction.DynamicLimits = intermediateDynamicLimits;
				this._telemetry.Dynamic = true;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000663C File Offset: 0x0000483C
		private static void ResetAllAlgorithmCounts(IList<DataReductionResolver.ScopedDataReductionResolutionState> reductions)
		{
			if (reductions == null)
			{
				return;
			}
			for (int i = 0; i < reductions.Count; i++)
			{
				DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState = reductions[i];
				scopedDataReductionResolutionState.ScopedReduction.Algorithm.Count = null;
				scopedDataReductionResolutionState.WasCountSpecified = false;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006684 File Offset: 0x00004884
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "RequestedCapacity", "NumSpecified", "PrimaryReductions", "SecondaryReductions" })]
		private global::System.ValueTuple<long, int, IList<DataReductionResolver.ScopedDataReductionResolutionState>, IList<DataReductionResolver.ScopedDataReductionResolutionState>> ComputeScopedReductionState(IReadOnlyList<IntermediateScopedReductionAlgorithm> reductions)
		{
			long num = 1L;
			int num2 = 0;
			SortedList<int, DataReductionResolver.ScopedDataReductionResolutionState> sortedList = null;
			SortedList<int, DataReductionResolver.ScopedDataReductionResolutionState> sortedList2 = null;
			for (int i = 0; i < reductions.Count; i++)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = reductions[i];
				int? count = intermediateScopedReductionAlgorithm.Algorithm.Count;
				if (count != null)
				{
					num2++;
					num *= (long)count.Value;
				}
				DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState = new DataReductionResolver.ScopedDataReductionResolutionState(intermediateScopedReductionAlgorithm, i, count != null);
				IntermediateReductionScope scope = intermediateScopedReductionAlgorithm.Scope;
				if (scope.Primary.IsNullOrEmpty<int>())
				{
					Util.AddToLazySortedList<int, DataReductionResolver.ScopedDataReductionResolutionState>(ref sortedList2, scope.Secondary[0], scopedDataReductionResolutionState);
				}
				else if (scope.Secondary.IsNullOrEmpty<int>())
				{
					Util.AddToLazySortedList<int, DataReductionResolver.ScopedDataReductionResolutionState>(ref sortedList, scope.Primary[0], scopedDataReductionResolutionState);
				}
				else
				{
					Contract.RetailFail("Scoped data reduction is not allowed to target intersections.");
				}
			}
			return new global::System.ValueTuple<long, int, IList<DataReductionResolver.ScopedDataReductionResolutionState>, IList<DataReductionResolver.ScopedDataReductionResolutionState>>(num, num2, (sortedList != null) ? sortedList.Values : null, (sortedList2 != null) ? sortedList2.Values : null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006770 File Offset: 0x00004970
		private DataVolumeLevel GetActiveDataVolumeConfigLevel(IntermediateDataReduction reduction, List<IntermediateScopedReductionAlgorithm> reductions, int numSpecified)
		{
			DataVolumeLevel dataVolumeLevel;
			if (numSpecified == reductions.Count)
			{
				dataVolumeLevel = this._config.MaxLevel;
			}
			else
			{
				if (reduction.DataVolume == null)
				{
					reduction.DataVolume = new int?(this._config.DefaultDataVolume);
				}
				dataVolumeLevel = this.GetConfigReductionLevelOrDefault(reduction);
			}
			return dataVolumeLevel;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000067C4 File Offset: 0x000049C4
		private static void AssignUnspecifiedCounts(long requestedCapacity, IList<DataReductionResolver.ScopedDataReductionResolutionState> primaryReductions, IList<DataReductionResolver.ScopedDataReductionResolutionState> secondaryReductions, DataVolumeLevel configLevel)
		{
			int num = DataReductionMath.DivideAndThresholdRound(configLevel.MaxIntersectionCount, (int)requestedCapacity);
			global::System.ValueTuple<int, int> valueTuple = DataReductionResolver.DetermineUnspecifiedOuterReductionsState(primaryReductions);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			global::System.ValueTuple<int, int> valueTuple2 = DataReductionResolver.DetermineUnspecifiedOuterReductionsState(secondaryReductions);
			int item3 = valueTuple2.Item1;
			int item4 = valueTuple2.Item2;
			if (item > 0 || item3 > 0)
			{
				int num2 = Math.Max(item2, 1) * Math.Max(item4, 1);
				int num3 = DataReductionMath.DivideAndThresholdRound(configLevel.OuterScopedReductionCount, num2);
				num3 = Math.Min(num3, num);
				int num4 = item + item3;
				double num5 = DataReductionMath.NthRoot(num3, num4);
				int num6 = 0;
				if (item > 0)
				{
					DataReductionResolver.FillUnspecifiedOuterReductionCount(primaryReductions, num5, ref num6);
				}
				if (item3 > 0)
				{
					DataReductionResolver.FillUnspecifiedOuterReductionCount(secondaryReductions, num5, ref num6);
				}
				num = DataReductionMath.DivideAndThresholdRound(num, num3);
			}
			DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState = ((primaryReductions != null) ? primaryReductions[primaryReductions.Count - 1] : null);
			IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = ((scopedDataReductionResolutionState != null) ? scopedDataReductionResolutionState.ScopedReduction : null);
			DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState2 = ((secondaryReductions != null) ? secondaryReductions[secondaryReductions.Count - 1] : null);
			IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm2 = ((scopedDataReductionResolutionState2 != null) ? scopedDataReductionResolutionState2.ScopedReduction : null);
			bool flag = intermediateScopedReductionAlgorithm == null || intermediateScopedReductionAlgorithm.Algorithm.Count != null;
			bool flag2 = intermediateScopedReductionAlgorithm2 == null || intermediateScopedReductionAlgorithm2.Algorithm.Count != null;
			bool flag3 = ((primaryReductions != null) ? primaryReductions.Count : 0) > 1;
			bool flag4 = ((secondaryReductions != null) ? secondaryReductions.Count : 0) > 1;
			bool flag5 = flag3 || flag4;
			if (!flag && !flag2)
			{
				int num7 = Math.Min(num, configLevel.InnerScopedReductionCount);
				double num8 = (double)configLevel.PrimaryCount / (double)configLevel.SecondaryCount;
				int num9 = DataReductionMath.ThresholdRound(Math.Sqrt((double)num7 / num8));
				int num10 = DataReductionMath.DivideAndThresholdRound(num7, num9);
				intermediateScopedReductionAlgorithm.Algorithm.Count = new int?(num10);
				intermediateScopedReductionAlgorithm2.Algorithm.Count = new int?(num9);
				return;
			}
			if (!flag)
			{
				int num11 = DataReductionResolver.DetermineOneUnspecifiedInnerReductionCount(configLevel, num, intermediateScopedReductionAlgorithm2, flag5);
				intermediateScopedReductionAlgorithm.Algorithm.Count = new int?(num11);
				return;
			}
			if (!flag2)
			{
				int num12 = DataReductionResolver.DetermineOneUnspecifiedInnerReductionCount(configLevel, num, intermediateScopedReductionAlgorithm, flag5);
				intermediateScopedReductionAlgorithm2.Algorithm.Count = new int?(num12);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000069C8 File Offset: 0x00004BC8
		private static int DetermineOneUnspecifiedInnerReductionCount(DataVolumeLevel configLevel, int remainingCapacity, IntermediateScopedReductionAlgorithm specifiedInnerAlgorithm, bool hasOuterReduction)
		{
			int num;
			if (specifiedInnerAlgorithm == null)
			{
				num = Math.Min(remainingCapacity, configLevel.InnerScopedReductionCount);
			}
			else if (specifiedInnerAlgorithm.Algorithm.Count.Value >= configLevel.InnerScopedReductionCount)
			{
				num = remainingCapacity;
			}
			else
			{
				num = Math.Max(DataReductionMath.DivideAndThresholdRound(configLevel.InnerScopedReductionCount, specifiedInnerAlgorithm.Algorithm.Count.Value), 1);
				num = Math.Min(num, remainingCapacity);
			}
			return num;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006A34 File Offset: 0x00004C34
		private IntermediateDynamicLimits BuildScopedDynamicLimits(IList<DataReductionResolver.ScopedDataReductionResolutionState> primaryReductions, IList<DataReductionResolver.ScopedDataReductionResolutionState> secondaryReductions, DataVolumeLevel configLevel)
		{
			int num = ((primaryReductions != null) ? primaryReductions.Count : 0);
			int num2 = ((secondaryReductions != null) ? secondaryReductions.Count : 0);
			if (num + num2 < 2 || !this._context.UseDynamicLimits)
			{
				return null;
			}
			List<IntermediateDynamicLimitBlock> list = new List<IntermediateDynamicLimitBlock>(2);
			IntermediateDynamicLimitBlock intermediateDynamicLimitBlock = null;
			int num3 = -1;
			bool flag = false;
			if (num > 1 || num2 > 1)
			{
				intermediateDynamicLimitBlock = DataReductionResolver.BuildOuterScopedDynamicLimitsBlock(primaryReductions, secondaryReductions, configLevel, out num3, out flag);
				list.Add(intermediateDynamicLimitBlock);
			}
			int num4;
			bool flag2;
			IntermediateDynamicLimitBlock intermediateDynamicLimitBlock2 = DataReductionResolver.BuildInnerScopedDynamicLimitsBlock(primaryReductions, secondaryReductions, configLevel, out num4, out flag2);
			list.Add(intermediateDynamicLimitBlock2);
			DataReductionResolver.AssignDynamicLimitBlockCounts(configLevel, intermediateDynamicLimitBlock, num3, flag, intermediateDynamicLimitBlock2, num4, flag2);
			return new IntermediateDynamicLimits
			{
				TargetIntersectionCount = configLevel.MaxIntersectionCount,
				Blocks = list
			};
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006ADC File Offset: 0x00004CDC
		private static void AssignDynamicLimitBlockCounts(DataVolumeLevel configLevel, IntermediateDynamicLimitBlock outerBlock, int outerBlockMandatoryConstraintCapacity, bool outerBlockHasOnlyMandatoryConstraints, IntermediateDynamicLimitBlock innerBlock, int innerBlockMandatoryConstraintCapacity, bool innerBlockHasOnlyMandatoryConstraints)
		{
			if (outerBlock != null)
			{
				int num;
				if (outerBlockHasOnlyMandatoryConstraints)
				{
					num = outerBlockMandatoryConstraintCapacity;
				}
				else
				{
					num = Math.Max(outerBlockMandatoryConstraintCapacity, configLevel.OuterScopedReductionCount);
				}
				outerBlock.Count = new IntermediateDynamicLimitRange
				{
					Min = 1,
					Max = num
				};
			}
			int? num2;
			if (innerBlockHasOnlyMandatoryConstraints)
			{
				num2 = new int?(innerBlockMandatoryConstraintCapacity);
			}
			else if (innerBlockMandatoryConstraintCapacity < configLevel.InnerScopedReductionCount)
			{
				num2 = new int?(configLevel.InnerScopedReductionCount);
			}
			else
			{
				num2 = null;
			}
			if (num2 != null)
			{
				innerBlock.Count = new IntermediateDynamicLimitRange
				{
					Min = 1,
					Max = num2.Value
				};
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006B74 File Offset: 0x00004D74
		private static IntermediateDynamicLimitEvenDistributionBlock BuildOuterScopedDynamicLimitsBlock(IList<DataReductionResolver.ScopedDataReductionResolutionState> primaryReductions, IList<DataReductionResolver.ScopedDataReductionResolutionState> secondaryReductions, DataVolumeLevel configLevel, out int mandatoryConstraintCapacity, out bool hasOnlyMandatoryConstraints)
		{
			int num2;
			if (primaryReductions == null)
			{
				int? num = ((secondaryReductions != null) ? new int?(secondaryReductions.Count) : null);
				num2 = ((num != null) ? new int?(num.GetValueOrDefault()) : null).GetValueOrDefault();
			}
			else
			{
				num2 = primaryReductions.Count;
			}
			List<IntermediateDynamicLimit> list = new List<IntermediateDynamicLimit>(num2);
			int outerScopedReductionCount = configLevel.OuterScopedReductionCount;
			mandatoryConstraintCapacity = 1;
			hasOnlyMandatoryConstraints = true;
			DataReductionResolver.AddOuterDynamicLimits(primaryReductions, outerScopedReductionCount, list, ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
			DataReductionResolver.AddOuterDynamicLimits(secondaryReductions, outerScopedReductionCount, list, ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
			return new IntermediateDynamicLimitEvenDistributionBlock
			{
				Limits = list
			};
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006C04 File Offset: 0x00004E04
		private static IntermediateDynamicLimitBlock BuildInnerScopedDynamicLimitsBlock(IList<DataReductionResolver.ScopedDataReductionResolutionState> primaryReductions, IList<DataReductionResolver.ScopedDataReductionResolutionState> secondaryReductions, DataVolumeLevel configLevel, out int mandatoryConstraintCapacity, out bool hasOnlyMandatoryConstraints)
		{
			mandatoryConstraintCapacity = 1;
			hasOnlyMandatoryConstraints = true;
			IntermediateDynamicLimitBlock intermediateDynamicLimitBlock;
			if (primaryReductions != null && secondaryReductions != null)
			{
				DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState = primaryReductions[primaryReductions.Count - 1];
				DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState2 = secondaryReductions[secondaryReductions.Count - 1];
				IntermediateDynamicLimit intermediateDynamicLimit = DataReductionResolver.CreateDynamicLimitForScopedReduction(scopedDataReductionResolutionState, new int?(configLevel.PrimaryCount), ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
				IntermediateDynamicLimit intermediateDynamicLimit2 = DataReductionResolver.CreateDynamicLimitForScopedReduction(scopedDataReductionResolutionState2, new int?(configLevel.SecondaryCount), ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
				intermediateDynamicLimitBlock = new IntermediateDynamicLimitPrimarySecondaryBlock
				{
					Primary = intermediateDynamicLimit,
					Secondary = intermediateDynamicLimit2
				};
			}
			else
			{
				IntermediateDynamicLimit intermediateDynamicLimit3 = DataReductionResolver.CreateDynamicLimitForScopedReduction(primaryReductions[primaryReductions.Count - 1], null, ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
				intermediateDynamicLimitBlock = new IntermediateDynamicLimitEvenDistributionBlock
				{
					Limits = new List<IntermediateDynamicLimit>(1) { intermediateDynamicLimit3 }
				};
			}
			return intermediateDynamicLimitBlock;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006CB8 File Offset: 0x00004EB8
		private static void AddOuterDynamicLimits(IList<DataReductionResolver.ScopedDataReductionResolutionState> reductions, int countForUnspecifiedReductions, List<IntermediateDynamicLimit> dynamicLimits, ref int mandatoryConstraintCapacity, ref bool hasOnlyMandatoryConstraints)
		{
			if (reductions == null)
			{
				return;
			}
			for (int i = 0; i < reductions.Count - 1; i++)
			{
				IntermediateDynamicLimit intermediateDynamicLimit = DataReductionResolver.CreateDynamicLimitForScopedReduction(reductions[i], new int?(countForUnspecifiedReductions), ref mandatoryConstraintCapacity, ref hasOnlyMandatoryConstraints);
				dynamicLimits.Add(intermediateDynamicLimit);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006CFC File Offset: 0x00004EFC
		private static IntermediateDynamicLimit CreateDynamicLimitForScopedReduction(DataReductionResolver.ScopedDataReductionResolutionState reductionState, int? countForUnspecifiedReductions, ref int mandatoryConstraintCapacity, ref bool hasOnlyMandatoryConstraints)
		{
			IntermediateDynamicLimitRange intermediateDynamicLimitRange;
			if (reductionState.WasCountSpecified)
			{
				int value = reductionState.ScopedReduction.Algorithm.Count.Value;
				intermediateDynamicLimitRange = new IntermediateDynamicLimitRange
				{
					Min = Math.Min(10, value),
					Max = value,
					IsMandatoryConstraint = true
				};
				mandatoryConstraintCapacity *= value;
			}
			else
			{
				int num = countForUnspecifiedReductions ?? reductionState.ScopedReduction.Algorithm.Count.Value;
				intermediateDynamicLimitRange = new IntermediateDynamicLimitRange
				{
					Min = Math.Min(10, num),
					Max = num
				};
				hasOnlyMandatoryConstraints = false;
			}
			return new IntermediateDynamicLimit
			{
				ScopedReductionIndex = reductionState.IndexInScoped,
				Count = intermediateDynamicLimitRange
			};
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "numUnspecified", "usedCapacity" })]
		private static global::System.ValueTuple<int, int> DetermineUnspecifiedOuterReductionsState(IList<DataReductionResolver.ScopedDataReductionResolutionState> reductions)
		{
			if (reductions == null)
			{
				return new global::System.ValueTuple<int, int>(0, 0);
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < reductions.Count - 1; i++)
			{
				DataReductionResolver.ScopedDataReductionResolutionState scopedDataReductionResolutionState = reductions[i];
				IntermediateScopedReductionAlgorithm scopedReduction = scopedDataReductionResolutionState.ScopedReduction;
				if (scopedDataReductionResolutionState.WasCountSpecified)
				{
					if (num2 == 0)
					{
						num2 = scopedReduction.Algorithm.Count.Value;
					}
					else
					{
						num2 *= scopedReduction.Algorithm.Count.Value;
					}
				}
				else
				{
					num++;
				}
			}
			return new global::System.ValueTuple<int, int>(num, num2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006E3C File Offset: 0x0000503C
		private static void FillUnspecifiedOuterReductionCount(IList<DataReductionResolver.ScopedDataReductionResolutionState> reductions, double countPerReduction, ref int numUpdated)
		{
			if (reductions == null)
			{
				return;
			}
			for (int i = 0; i < reductions.Count - 1; i++)
			{
				IntermediateScopedReductionAlgorithm scopedReduction = reductions[i].ScopedReduction;
				if (scopedReduction.Algorithm.Count == null)
				{
					bool flag = numUpdated == 0;
					scopedReduction.Algorithm.Count = new int?((int)(flag ? Math.Ceiling(countPerReduction) : ((double)Math.Max(DataReductionMath.ThresholdRound(countPerReduction), 1))));
					numUpdated++;
				}
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006EB8 File Offset: 0x000050B8
		private void FallbackAdvancedAlgorithms(List<IntermediateScopedReductionAlgorithm> scopedReductions)
		{
			for (int i = 0; i < scopedReductions.Count; i++)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = scopedReductions[i];
				if (intermediateScopedReductionAlgorithm.Algorithm is IntermediateBinnedLineSampleLimit || intermediateScopedReductionAlgorithm.Algorithm is IntermediateOverlappingPointsSampleLimit)
				{
					IntermediateReductionScope scope = intermediateScopedReductionAlgorithm.Scope;
					if (!scope.Primary.IsNullOrEmpty<int>() && !scope.Secondary.IsNullOrEmpty<int>())
					{
						IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm2 = new IntermediateScopedReductionAlgorithm
						{
							Algorithm = new IntermediateSimpleLimit
							{
								Kind = IntermediateSimpleLimitKind.Top
							},
							Scope = new IntermediateReductionScope
							{
								Secondary = scope.Secondary
							}
						};
						scopedReductions.Insert(i + 1, intermediateScopedReductionAlgorithm2);
						i++;
						intermediateScopedReductionAlgorithm.Algorithm = new IntermediateSimpleLimit
						{
							Kind = IntermediateSimpleLimitKind.Sample
						};
						intermediateScopedReductionAlgorithm.Scope.Secondary = null;
					}
					else
					{
						intermediateScopedReductionAlgorithm.Algorithm = this.ConvertToSample(intermediateScopedReductionAlgorithm.Algorithm);
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006F94 File Offset: 0x00005194
		private void ResolvePrimaryOnly(IntermediateDataReduction reduction)
		{
			if (reduction.Secondary != null || reduction.Intersection != null)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.IgnoredDataReductionAlgorithm(EngineMessageSeverity.Warning, "secondary and intersection"));
				reduction.Secondary = null;
				reduction.Intersection = null;
			}
			if (this.ShouldFallbackToSample(reduction.Primary))
			{
				reduction.Primary = this.ConvertToSample(reduction.Primary);
			}
			this.ResolvePrimaryOnlyCore(reduction);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007001 File Offset: 0x00005201
		private IntermediateSimpleLimit ConvertToSample(IntermediateReductionAlgorithm reductionAlgorithm)
		{
			return new IntermediateSimpleLimit
			{
				Kind = IntermediateSimpleLimitKind.Sample,
				Count = reductionAlgorithm.Count
			};
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000701C File Offset: 0x0000521C
		private void ResolvePrimaryOnlyCore(IntermediateDataReduction reduction)
		{
			DataVolumeLevel dataVolumeLevel;
			if (DataReductionResolver.IsReductionAlgorithmFullySpecified(reduction.Primary))
			{
				dataVolumeLevel = this._config.MaxLevel;
			}
			else
			{
				dataVolumeLevel = this.ResolvePrimaryOnlyDefaults(reduction);
			}
			this._telemetry.MaxIntersections = new int?(dataVolumeLevel.MaxIntersectionCount);
			IntermediateReductionAlgorithm primary = reduction.Primary;
			if (primary != null)
			{
				if (primary.Count != null && primary.Count.Value > dataVolumeLevel.MaxIntersectionCount)
				{
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, primary.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitToAllowedMaximum(dataVolumeLevel.MaxIntersectionCount)));
					primary.Count = new int?(dataVolumeLevel.MaxIntersectionCount);
				}
				IntermediateSimpleLimit intermediateSimpleLimit = primary as IntermediateSimpleLimit;
				if (intermediateSimpleLimit != null && intermediateSimpleLimit.Kind == IntermediateSimpleLimitKind.Sample)
				{
					DataReductionAxisTelemetry orCreatePrimary = this._telemetry.GetOrCreatePrimary();
					int num;
					if (DataReductionResolver.TryGetDistinctValueCountFromStats(this._projections.PrimaryMembers, orCreatePrimary, false, out num) && num <= intermediateSimpleLimit.Count.Value)
					{
						intermediateSimpleLimit.DisablePreserveKeyPoints = true;
						orCreatePrimary.AddFlag("NoKeyPtSmpl");
					}
				}
				this.ResolveAdvancedAlgorithm(primary, dataVolumeLevel);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000713C File Offset: 0x0000533C
		private DataVolumeLevel ResolvePrimaryOnlyDefaults(IntermediateDataReduction reduction)
		{
			if (reduction.DataVolume == null)
			{
				reduction.DataVolume = new int?(this._config.DefaultDataVolume);
			}
			DataVolumeLevel configReductionLevelOrDefault = this.GetConfigReductionLevelOrDefault(reduction);
			if (reduction.Primary == null)
			{
				reduction.Primary = new IntermediateSimpleLimit
				{
					Kind = IntermediateSimpleLimitKind.Top
				};
			}
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Primary as IntermediateSimpleLimit;
			if (intermediateSimpleLimit != null && intermediateSimpleLimit.Count == null)
			{
				intermediateSimpleLimit.Count = new int?(configReductionLevelOrDefault.MaxIntersectionCount);
			}
			IntermediateDataWindow intermediateDataWindow = reduction.Primary as IntermediateDataWindow;
			if (intermediateDataWindow != null && intermediateDataWindow.Count == null)
			{
				intermediateDataWindow.Count = new int?(configReductionLevelOrDefault.WindowSize);
			}
			return configReductionLevelOrDefault;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000071F2 File Offset: 0x000053F2
		private void ResolvePrimarySecondaryAndIntersection(IntermediateDataReduction reduction)
		{
			if (this.IsAdvancedAlgorithmIntersectionLimit(reduction.Intersection) && this.TryResolveAdvancedAlgorithmIntersectionLimit(reduction))
			{
				return;
			}
			this.ResolvePrimaryAndSecondary(reduction);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007214 File Offset: 0x00005414
		private bool TryResolveAdvancedAlgorithmIntersectionLimit(IntermediateDataReduction reduction)
		{
			IntermediateReductionAlgorithm intersection = reduction.Intersection;
			if (intersection == null)
			{
				return false;
			}
			if (this.ShouldFallbackToSample(intersection))
			{
				reduction.Primary = new IntermediateSimpleLimit
				{
					Kind = IntermediateSimpleLimitKind.Sample
				};
				reduction.Secondary = new IntermediateSimpleLimit
				{
					Kind = IntermediateSimpleLimitKind.Top
				};
				reduction.Intersection = null;
				return false;
			}
			DataVolumeLevel dataVolumeLevel;
			if (this.IsIntersectionFullySpecified(reduction))
			{
				dataVolumeLevel = this._config.MaxLevel;
			}
			else
			{
				dataVolumeLevel = this.GetConfigReductionLevelOrDefault(reduction);
			}
			this._telemetry.MaxIntersections = new int?(dataVolumeLevel.MaxIntersectionCount);
			if (intersection.Count != null && intersection.Count.Value > dataVolumeLevel.MaxIntersectionCount)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, intersection.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitToAllowedMaximum(dataVolumeLevel.MaxIntersectionCount)));
				intersection.Count = new int?(dataVolumeLevel.MaxIntersectionCount);
			}
			this.ResolveAdvancedAlgorithm(reduction.Intersection, dataVolumeLevel);
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000730C File Offset: 0x0000550C
		private bool IsAdvancedAlgorithmIntersectionLimit(IntermediateReductionAlgorithm limit)
		{
			bool flag = limit is IntermediateBinnedLineSampleLimit;
			IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = limit as IntermediateOverlappingPointsSampleLimit;
			return flag || intermediateOverlappingPointsSampleLimit != null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007330 File Offset: 0x00005530
		private void ResolveAdvancedAlgorithm(IntermediateReductionAlgorithm limit, DataVolumeLevel configLevel)
		{
			IntermediateBinnedLineSampleLimit intermediateBinnedLineSampleLimit = limit as IntermediateBinnedLineSampleLimit;
			IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = limit as IntermediateOverlappingPointsSampleLimit;
			IntermediateTopNPerLevelSampleLimit intermediateTopNPerLevelSampleLimit = limit as IntermediateTopNPerLevelSampleLimit;
			if (intermediateBinnedLineSampleLimit != null)
			{
				this.ResolveBinnedLineSampleLimit(intermediateBinnedLineSampleLimit, configLevel);
				return;
			}
			if (intermediateOverlappingPointsSampleLimit != null)
			{
				this.ResolveOverlappingPointsSampleLimit(intermediateOverlappingPointsSampleLimit, configLevel);
				return;
			}
			if (intermediateTopNPerLevelSampleLimit != null)
			{
				this.ResolveTopNPerLevelSampleLimit(intermediateTopNPerLevelSampleLimit, configLevel);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007378 File Offset: 0x00005578
		private bool ShouldFallbackToSample(IntermediateReductionAlgorithm reductionAlgorithm)
		{
			IntermediateBinnedLineSampleLimit intermediateBinnedLineSampleLimit = reductionAlgorithm as IntermediateBinnedLineSampleLimit;
			IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = reductionAlgorithm as IntermediateOverlappingPointsSampleLimit;
			if (intermediateBinnedLineSampleLimit != null)
			{
				return this.ShouldFallbackBinnedLineSampleLimit(intermediateBinnedLineSampleLimit);
			}
			return intermediateOverlappingPointsSampleLimit != null && this.ShouldFallbackOverlappingPointsSampleLimit(intermediateOverlappingPointsSampleLimit);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000073AC File Offset: 0x000055AC
		private void ResolveBinnedLineSampleLimit(IntermediateBinnedLineSampleLimit limit, DataVolumeLevel configLevel)
		{
			limit.MaxPointsPerSeries = new int?(configLevel.MaxPointsPerSeries);
			int maxIntersectionCount = configLevel.MaxIntersectionCount;
			if (limit.Count == null)
			{
				limit.Count = new int?(maxIntersectionCount);
			}
			if (limit.MinPointsPerSeries != null)
			{
				if (limit.MinPointsPerSeries.Value > maxIntersectionCount)
				{
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedMinPointsPerSeriesExceedsMaxIntersections(EngineMessageSeverity.Warning, limit.MinPointsPerSeries.Value, DataShapeGenerationMessagePhrases.ReduceLimitToDefault(configLevel.MinPointsPerSeries)));
					limit.MinPointsPerSeries = new int?(configLevel.MinPointsPerSeries);
				}
			}
			else
			{
				limit.MinPointsPerSeries = new int?(configLevel.MinPointsPerSeries);
			}
			if (limit.MaxDynamicSeriesCount != null)
			{
				if (limit.MaxDynamicSeriesCount.Value > configLevel.MaxDynamicSeriesCount)
				{
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedMaxDynamicSeriesExceedsMaxAllowedDynamicSeries(EngineMessageSeverity.Warning, limit.MaxDynamicSeriesCount.Value, DataShapeGenerationMessagePhrases.ReduceLimitToDefault(configLevel.MaxDynamicSeriesCount)));
					limit.MaxDynamicSeriesCount = new int?(configLevel.MaxDynamicSeriesCount);
				}
			}
			else
			{
				limit.MaxDynamicSeriesCount = new int?(configLevel.MaxDynamicSeriesCount);
			}
			limit.Measures = this.GetMeasuresEligibleForBinnedLineSample(this._projections.Measures);
			int count = limit.Measures.Count;
			if (this._projections.PrimaryMembers.Count > 0 && this._projections.SecondaryMembers.Count == 0)
			{
				int num = count;
				int? num2 = num * limit.MinPointsPerSeries;
				int num3 = maxIntersectionCount;
				if ((num2.GetValueOrDefault() > num3) & (num2 != null))
				{
					limit.MinPointsPerSeries = new int?(maxIntersectionCount / num);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000757C File Offset: 0x0000577C
		private void ResolveOverlappingPointsSampleLimit(IntermediateOverlappingPointsSampleLimit limit, DataVolumeLevel configLevel)
		{
			int maxIntersectionCount = configLevel.MaxIntersectionCount;
			if (limit.Count != null)
			{
				if (limit.Count.Value > maxIntersectionCount)
				{
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, limit.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitToDefault(maxIntersectionCount)));
					limit.Count = new int?(maxIntersectionCount);
					return;
				}
			}
			else
			{
				limit.Count = new int?(maxIntersectionCount);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000075F4 File Offset: 0x000057F4
		private void ResolveTopNPerLevelSampleLimit(IntermediateTopNPerLevelSampleLimit limit, DataVolumeLevel configLevel)
		{
			int maxIntersectionCount = configLevel.MaxIntersectionCount;
			if (limit.Count != null)
			{
				if (limit.Count.Value > maxIntersectionCount)
				{
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, limit.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitToDefault(maxIntersectionCount)));
					limit.Count = new int?(maxIntersectionCount);
				}
			}
			else
			{
				limit.Count = new int?(maxIntersectionCount);
			}
			if (limit.WindowExpansionState != null)
			{
				ResolvedDataReductionWindowExpansionState windowExpansionState = limit.WindowExpansionState;
				if (((windowExpansionState != null) ? windowExpansionState.Levels : null) != null)
				{
					int count = limit.WindowExpansionState.Levels.Count;
					if (count > 0 && limit.Count.Value * count > maxIntersectionCount)
					{
						int num = maxIntersectionCount / count;
						this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedPerLevelLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, limit.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitTo(num)));
						limit.Count = new int?(num);
					}
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000076EC File Offset: 0x000058EC
		private bool ShouldFallbackBinnedLineSampleLimit(IntermediateBinnedLineSampleLimit limit)
		{
			List<string> list = null;
			if (!this._context.FederatedConceptualSchema.GetCapabilities().SupportsBinnedLineSample)
			{
				Util.AddToLazyList<string>(ref list, "ModelSupport");
			}
			int count = this.GetMeasuresEligibleForBinnedLineSample(this._projections.Measures).Count;
			if (count == 0)
			{
				Util.AddToLazyList<string>(ref list, "NoMeasures");
			}
			if (this._binding.Primary.HasShowItemsWithNoData() || this._binding.Secondary.HasShowItemsWithNoData())
			{
				Util.AddToLazyList<string>(ref list, "ShowAll");
			}
			if (this._binding.Primary.Groupings.Count != 1)
			{
				Util.AddToLazyList<string>(ref list, "MultiGroup");
			}
			DataShapeBindingAxis secondary = this._binding.Secondary;
			if (secondary != null && !secondary.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>() && secondary.Groupings.Count > 1)
			{
				Util.AddToLazyList<string>(ref list, "MultiGroup");
			}
			if (this._projections.HasSortByMeasure())
			{
				Util.AddToLazyList<string>(ref list, "SortByMeasure");
			}
			if (limit.PrimaryScalarKey == null && this._projections.PrimaryMembers.Count == 1)
			{
				QueryGroup group = this._projections.PrimaryMembers[0].Group;
				if (group.Keys.Count > 1)
				{
					Util.AddToLazyList<string>(ref list, "MultipleAxisKeys");
				}
				if (group.SortKeys.Count > 1)
				{
					Util.AddToLazyList<string>(ref list, "MultipleAxisKeys");
				}
				int num;
				if (group.HasBinnableOrderByColumn(out num))
				{
					Util.AddToLazyList<string>(ref list, "MultipleAxisKeysWithBinnableOrderBy");
				}
				if (num == 0)
				{
					Util.AddToLazyList<string>(ref list, "NonBinKeys");
				}
			}
			this._telemetry.EligibleMeasureCount = new int?(count);
			if (list.IsNullOrEmptyCollection<string>())
			{
				return false;
			}
			this._telemetry.FallbackReasons = list;
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000078AC File Offset: 0x00005AAC
		private bool ShouldFallbackOverlappingPointsSampleLimit(IntermediateOverlappingPointsSampleLimit limit)
		{
			List<string> list = null;
			if (!this._context.FederatedConceptualSchema.GetCapabilities().SupportsOverlappingPointsSample)
			{
				Util.AddToLazyList<string>(ref list, "ModelSupport");
			}
			if (limit.X == null && limit.Y == null)
			{
				Util.AddToLazyList<string>(ref list, "PlotAxisBindingsMissing");
			}
			if (this._binding.Primary.HasShowItemsWithNoData() || this._binding.Secondary.HasShowItemsWithNoData())
			{
				Util.AddToLazyList<string>(ref list, "ShowAll");
			}
			if (this._binding.Primary.HasSubtotals() || this._binding.Secondary.HasSubtotals())
			{
				Util.AddToLazyList<string>(ref list, "TotalsPresent");
			}
			if (this._projections.HasSortByMeasure())
			{
				Util.AddToLazyList<string>(ref list, "SortByMeasure");
			}
			if ((limit.X != null && !this.IsReferenceToBinnableProjection(limit.X.Index)) || (limit.Y != null && !this.IsReferenceToBinnableProjection(limit.Y.Index)))
			{
				Util.AddToLazyList<string>(ref list, "NonNumericAxis");
			}
			if (list.IsNullOrEmptyCollection<string>())
			{
				return false;
			}
			this._telemetry.FallbackReasons = list;
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000079CC File Offset: 0x00005BCC
		private bool IsReferenceToBinnableProjection(int selectIndex)
		{
			return this.IsReferenceToBinnableMeasure(selectIndex) || this.IsReferenceToBinnableColumn(selectIndex);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000079E0 File Offset: 0x00005BE0
		private bool IsReferenceToBinnableColumn(int selectIndex)
		{
			IReadOnlyList<QueryGroupValue> readOnlyList = this._projections.FindGroupValuesBySelectIndex(selectIndex);
			if (readOnlyList.IsNullOrEmpty<QueryGroupValue>())
			{
				return false;
			}
			using (IEnumerator<QueryGroupValue> enumerator = readOnlyList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsScalar.GetValueOrDefault())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A50 File Offset: 0x00005C50
		private bool IsReferenceToBinnableMeasure(int selectIndex)
		{
			IReadOnlyList<ProjectedDsqExpression> readOnlyList = this._projections.FindMeasuresBySelectIndex(selectIndex);
			if (readOnlyList.IsNullOrEmpty<ProjectedDsqExpression>())
			{
				return false;
			}
			using (IEnumerator<ProjectedDsqExpression> enumerator = readOnlyList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsScalar.GetValueOrDefault())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007AC0 File Offset: 0x00005CC0
		private IReadOnlyList<ProjectedDsqExpression> GetMeasuresEligibleForBinnedLineSample(IReadOnlyList<ProjectedDsqExpression> measures)
		{
			return measures.Where((ProjectedDsqExpression measure) => !measure.SuppressJoinPredicate && measure.Value.DsqExpression.Kind != ExpressionNodeKind.VisualCalculation && !measure.IsContextOnly).ToList<ProjectedDsqExpression>();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007AEC File Offset: 0x00005CEC
		private void ResolvePrimaryAndSecondary(IntermediateDataReduction reduction)
		{
			bool flag = this.ArePrimaryAndSecondaryFullySpecified(reduction);
			DataVolumeLevel dataVolumeLevel;
			if (flag)
			{
				dataVolumeLevel = this._config.MaxLevel;
			}
			else
			{
				dataVolumeLevel = this.ResolvePrimaryAndSecondaryDefaults(reduction);
			}
			bool flag2 = reduction.Intersection != null;
			bool flag3 = this.IsIntersectionLimitSupported(reduction);
			if (flag2 && !flag3)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.IgnoredDataReductionAlgorithm(EngineMessageSeverity.Warning, "intersection"));
				reduction.Intersection = null;
			}
			this._telemetry.MaxIntersections = new int?(dataVolumeLevel.MaxIntersectionCount);
			this._telemetry.MaxPotentialIntersections = new int?(dataVolumeLevel.MaxPotentialIntersectionCount);
			this.ResolvePrimaryAndSecondary(reduction, dataVolumeLevel, flag, flag3);
			if (flag3)
			{
				this.ResolveIntersection(reduction, dataVolumeLevel);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007B94 File Offset: 0x00005D94
		private bool IsIntersectionLimitSupported(IntermediateDataReduction reduction)
		{
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Primary as IntermediateSimpleLimit;
			if (intermediateSimpleLimit == null)
			{
				return false;
			}
			IntermediateSimpleLimit intermediateSimpleLimit2 = reduction.Secondary as IntermediateSimpleLimit;
			if (intermediateSimpleLimit2 == null)
			{
				return false;
			}
			if (intermediateSimpleLimit.Kind != IntermediateSimpleLimitKind.Top || intermediateSimpleLimit2.Kind != IntermediateSimpleLimitKind.Top)
			{
				return false;
			}
			bool flag = this._projections.PrimaryMembers != null && this._projections.PrimaryMembers.Count == 1;
			bool flag2 = this._projections.SecondaryMembers != null && this._projections.SecondaryMembers.Count == 1;
			if (!flag || !flag2)
			{
				return false;
			}
			if (reduction.Intersection == null)
			{
				return true;
			}
			IntermediateSimpleLimit intermediateSimpleLimit3 = reduction.Intersection as IntermediateSimpleLimit;
			return intermediateSimpleLimit3 != null && intermediateSimpleLimit3.Kind == intermediateSimpleLimit.Kind;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007C50 File Offset: 0x00005E50
		private void ResolvePrimaryAndSecondary(IntermediateDataReduction reduction, DataVolumeLevel configLevel, bool areFullySpecified, bool intersectionLimitSupported)
		{
			int maxEffectiveIntersectionCount = configLevel.GetMaxEffectiveIntersectionCount(intersectionLimitSupported);
			if (areFullySpecified && this.ResolvePrimaryAndSecondaryFullySpecified(reduction, maxEffectiveIntersectionCount))
			{
				return;
			}
			IntermediateDataWindow intermediateDataWindow = reduction.Primary as IntermediateDataWindow;
			if (intermediateDataWindow != null)
			{
				this.ResolvePrimaryAndSecondaryWithWindow(reduction, configLevel, intermediateDataWindow, maxEffectiveIntersectionCount);
				return;
			}
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Primary as IntermediateSimpleLimit;
			IntermediateSimpleLimit intermediateSimpleLimit2 = reduction.Secondary as IntermediateSimpleLimit;
			this.AssignDisablePreserveKeyPoints(this._projections.PrimaryMembers, intermediateSimpleLimit, this._telemetry.GetOrCreatePrimary());
			this.AssignDisablePreserveKeyPoints(this._projections.SecondaryMembers, intermediateSimpleLimit2, this._telemetry.GetOrCreateSecondary());
			if (intermediateSimpleLimit.Count != null && intermediateSimpleLimit2.Count == null)
			{
				if (intermediateSimpleLimit.Count.Value > maxEffectiveIntersectionCount)
				{
					int value = intermediateSimpleLimit.Count.Value;
					intermediateSimpleLimit.Count = null;
					this.ResolvePrimaryAndSecondaryNoCounts(configLevel, reduction, intermediateSimpleLimit, intermediateSimpleLimit2, intersectionLimitSupported, maxEffectiveIntersectionCount);
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, value, DataShapeGenerationMessagePhrases.ReduceLimitTo(intermediateSimpleLimit.Count.Value)));
					return;
				}
				intermediateSimpleLimit2.Count = new int?(Math.Max(1, DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, intermediateSimpleLimit.Count.Value)));
				return;
			}
			else
			{
				if (intermediateSimpleLimit.Count != null || intermediateSimpleLimit2.Count == null)
				{
					this.ResolvePrimaryAndSecondaryNoCounts(configLevel, reduction, intermediateSimpleLimit, intermediateSimpleLimit2, intersectionLimitSupported, maxEffectiveIntersectionCount);
					return;
				}
				if (intermediateSimpleLimit2.Count.Value > maxEffectiveIntersectionCount)
				{
					int value2 = intermediateSimpleLimit2.Count.Value;
					intermediateSimpleLimit2.Count = null;
					this.ResolvePrimaryAndSecondaryNoCounts(configLevel, reduction, intermediateSimpleLimit, intermediateSimpleLimit2, intersectionLimitSupported, maxEffectiveIntersectionCount);
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, value2, DataShapeGenerationMessagePhrases.ReduceLimitTo(intermediateSimpleLimit2.Count.Value)));
					return;
				}
				intermediateSimpleLimit.Count = new int?(Math.Max(1, DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, intermediateSimpleLimit2.Count.Value)));
				return;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007E68 File Offset: 0x00006068
		private bool ResolvePrimaryAndSecondaryFullySpecified(IntermediateDataReduction reduction, int maxEffectiveIntersectionCount)
		{
			IntermediateDataWindow intermediateDataWindow = reduction.Primary as IntermediateDataWindow;
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Primary as IntermediateSimpleLimit;
			IntermediateSimpleLimit intermediateSimpleLimit2 = reduction.Secondary as IntermediateSimpleLimit;
			long num = (long)((intermediateDataWindow != null) ? intermediateDataWindow.Count.Value : intermediateSimpleLimit.Count.Value);
			long num2 = (long)intermediateSimpleLimit2.Count.Value;
			if (num * num2 <= (long)maxEffectiveIntersectionCount)
			{
				return true;
			}
			this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedReductionAlgorithmsExceedsMaxIntersections(EngineMessageSeverity.Warning, num * num2));
			if (intermediateDataWindow != null)
			{
				intermediateDataWindow.Count = null;
			}
			if (intermediateSimpleLimit != null)
			{
				intermediateSimpleLimit.Count = null;
			}
			intermediateSimpleLimit2.Count = null;
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007F2C File Offset: 0x0000612C
		private DataVolumeLevel ResolvePrimaryAndSecondaryDefaults(IntermediateDataReduction reduction)
		{
			if (reduction.DataVolume == null)
			{
				reduction.DataVolume = new int?(this._config.DefaultDataVolume);
			}
			DataVolumeLevel configReductionLevelOrDefault = this.GetConfigReductionLevelOrDefault(reduction);
			if (reduction.Primary == null && this._projections.PrimaryMembers.Count > 0)
			{
				reduction.Primary = new IntermediateSimpleLimit
				{
					Kind = IntermediateSimpleLimitKind.Top
				};
			}
			if (reduction.Secondary == null && this._projections.SecondaryMembers.Count > 0)
			{
				reduction.Secondary = new IntermediateSimpleLimit
				{
					Kind = IntermediateSimpleLimitKind.Top
				};
			}
			return configReductionLevelOrDefault;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007FC0 File Offset: 0x000061C0
		private void ResolvePrimaryAndSecondaryNoCounts(DataVolumeLevel configLevel, IntermediateDataReduction reduction, IntermediateSimpleLimit primaryLimit, IntermediateSimpleLimit secondaryLimit, bool intersectionLimitSupported, int maxEffectiveIntersectionCount)
		{
			if (DataReductionResolver.ShouldUseNoStatisticsConfig(this._projections))
			{
				this.ResolvePrimaryAndSecondaryNoStats(configLevel, primaryLimit, secondaryLimit, intersectionLimitSupported, maxEffectiveIntersectionCount);
			}
			else
			{
				this.ResolvePrimaryAndSecondaryStats(configLevel, primaryLimit, secondaryLimit, maxEffectiveIntersectionCount);
			}
			this.AdjustPrimaryAndSecondaryForFilters(primaryLimit, secondaryLimit, maxEffectiveIntersectionCount);
			if (this._context.UseDynamicLimits)
			{
				this._telemetry.Dynamic = true;
				reduction.DynamicLimits = new IntermediateDynamicLimits
				{
					TargetIntersectionCount = configLevel.MaxIntersectionCount,
					Primary = new IntermediateDynamicLimitRange
					{
						Min = Math.Min(10, configLevel.PrimaryCount),
						Max = configLevel.PrimaryCount
					},
					Secondary = new IntermediateDynamicLimitRange
					{
						Min = Math.Min(10, configLevel.SecondaryCount),
						Max = configLevel.SecondaryCount
					}
				};
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008088 File Offset: 0x00006288
		private void ResolvePrimaryAndSecondaryNoStats(DataVolumeLevel configLevel, IntermediateSimpleLimit primaryLimit, IntermediateSimpleLimit secondaryLimit, bool intersectionLimitSupported, int maxEffectiveIntersectionCount)
		{
			DataReductionAxisTelemetry orCreatePrimary = this._telemetry.GetOrCreatePrimary();
			DataReductionAxisTelemetry orCreateSecondary = this._telemetry.GetOrCreateSecondary();
			primaryLimit.Count = new int?(configLevel.PrimaryCountNoStats);
			orCreatePrimary.AddFlag("NoStats");
			secondaryLimit.Count = new int?(configLevel.SecondaryCountNoStats);
			orCreateSecondary.AddFlag("NoStats");
			if (intersectionLimitSupported && primaryLimit.Count.Value * secondaryLimit.Count.Value <= maxEffectiveIntersectionCount)
			{
				DataReductionMath.ApplyLinearScaling(primaryLimit, secondaryLimit, maxEffectiveIntersectionCount);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008114 File Offset: 0x00006314
		private void ResolvePrimaryAndSecondaryStats(DataVolumeLevel configLevel, IntermediateSimpleLimit primaryLimit, IntermediateSimpleLimit secondaryLimit, int maxEffectiveIntersectionCount)
		{
			DataReductionAxisTelemetry orCreatePrimary = this._telemetry.GetOrCreatePrimary();
			DataReductionAxisTelemetry orCreateSecondary = this._telemetry.GetOrCreateSecondary();
			int distinctValueCountFromStatsOrDefault = DataReductionResolver.GetDistinctValueCountFromStatsOrDefault(this._projections.PrimaryMembers, configLevel.PrimaryCount, orCreatePrimary);
			int distinctValueCountFromStatsOrDefault2 = DataReductionResolver.GetDistinctValueCountFromStatsOrDefault(this._projections.SecondaryMembers, configLevel.SecondaryCount, orCreateSecondary);
			if ((long)distinctValueCountFromStatsOrDefault * (long)distinctValueCountFromStatsOrDefault2 <= (long)maxEffectiveIntersectionCount)
			{
				primaryLimit.Count = new int?(distinctValueCountFromStatsOrDefault);
				secondaryLimit.Count = new int?(distinctValueCountFromStatsOrDefault2);
				DataReductionMath.ApplyLinearScaling(primaryLimit, secondaryLimit, maxEffectiveIntersectionCount);
				return;
			}
			if (distinctValueCountFromStatsOrDefault > configLevel.PrimaryCount && distinctValueCountFromStatsOrDefault2 > configLevel.SecondaryCount)
			{
				secondaryLimit.Count = new int?(configLevel.SecondaryCount);
				primaryLimit.Count = new int?(DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, secondaryLimit.Count.Value));
				DataReductionResolver.RecalculateCountsIfBelowMinimum(distinctValueCountFromStatsOrDefault, configLevel.PrimaryCount, maxEffectiveIntersectionCount, secondaryLimit, primaryLimit);
				return;
			}
			if (distinctValueCountFromStatsOrDefault2 > configLevel.SecondaryCount)
			{
				primaryLimit.Count = new int?(distinctValueCountFromStatsOrDefault);
				secondaryLimit.Count = new int?(DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, primaryLimit.Count.Value));
				DataReductionResolver.RecalculateCountsIfBelowMinimum(distinctValueCountFromStatsOrDefault2, configLevel.SecondaryCount, maxEffectiveIntersectionCount, primaryLimit, secondaryLimit);
				return;
			}
			secondaryLimit.Count = new int?(distinctValueCountFromStatsOrDefault2);
			primaryLimit.Count = new int?(DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, secondaryLimit.Count.Value));
			DataReductionResolver.RecalculateCountsIfBelowMinimum(distinctValueCountFromStatsOrDefault, configLevel.PrimaryCount, maxEffectiveIntersectionCount, secondaryLimit, primaryLimit);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008270 File Offset: 0x00006470
		private void AdjustPrimaryAndSecondaryForFilters(IntermediateSimpleLimit primaryLimit, IntermediateSimpleLimit secondaryLimit, int maxEffectiveIntersectionCount)
		{
			DataReductionAxisTelemetry orCreatePrimary = this._telemetry.GetOrCreatePrimary();
			DataReductionAxisTelemetry orCreateSecondary = this._telemetry.GetOrCreateSecondary();
			int num;
			bool flag = this.TryGetDistinctValueCountFromFilters(this._projections.PrimaryMembers, orCreatePrimary, out num);
			int num2;
			if (this.TryGetDistinctValueCountFromFilters(this._projections.SecondaryMembers, orCreateSecondary, out num2) && num2 < secondaryLimit.Count.Value)
			{
				secondaryLimit.Count = new int?(num2);
				primaryLimit.Count = new int?(DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, secondaryLimit.Count.Value));
				orCreateSecondary.AddFlag("Filter");
				return;
			}
			if (flag && num < primaryLimit.Count.Value)
			{
				primaryLimit.Count = new int?(num);
				secondaryLimit.Count = new int?(DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, primaryLimit.Count.Value));
				orCreatePrimary.AddFlag("Filter");
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008358 File Offset: 0x00006558
		private static void RecalculateCountsIfBelowMinimum(int caclulatedLimitCountStatistic, int calculatedLimitConfigLevel, int maxEffectiveIntersectionCount, IntermediateSimpleLimit acceptedLimit, IntermediateSimpleLimit calculatedLimit)
		{
			int? num = calculatedLimit.Count;
			int num2 = 10;
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				num = calculatedLimit.Count;
				if ((num.GetValueOrDefault() < caclulatedLimitCountStatistic) & (num != null))
				{
					num = calculatedLimit.Count;
					if ((num.GetValueOrDefault() < calculatedLimitConfigLevel) & (num != null))
					{
						int num3 = Math.Min(10, calculatedLimitConfigLevel);
						int num4 = DataReductionMath.DivideAndCeiling(maxEffectiveIntersectionCount, num3);
						if (num4 >= 10)
						{
							calculatedLimit.Count = new int?(num3);
							acceptedLimit.Count = new int?(num4);
						}
					}
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000083F0 File Offset: 0x000065F0
		private void ResolveIntersection(IntermediateDataReduction reduction, DataVolumeLevel configLevel)
		{
			if (configLevel.MaxPotentialIntersectionCount <= configLevel.MaxIntersectionCount)
			{
				reduction.Intersection = null;
				return;
			}
			if (this.IsIntersectionFullySpecified(reduction) && this.ResolveIntersectionFullySpecified(reduction, configLevel))
			{
				return;
			}
			IntermediateSimpleLimit intermediateSimpleLimit = (IntermediateSimpleLimit)reduction.Primary;
			IntermediateSimpleLimit intermediateSimpleLimit2 = (IntermediateSimpleLimit)reduction.Secondary;
			long num = (long)intermediateSimpleLimit.Count.Value;
			long num2 = (long)intermediateSimpleLimit2.Count.Value;
			if (num * num2 <= (long)configLevel.MaxIntersectionCount)
			{
				return;
			}
			if (reduction.Intersection == null)
			{
				reduction.Intersection = new IntermediateSimpleLimit
				{
					Kind = intermediateSimpleLimit.Kind
				};
				if (reduction.DynamicLimits != null)
				{
					reduction.DynamicLimits.SuppressIntersectionLimit = true;
				}
			}
			(reduction.Intersection as IntermediateSimpleLimit).Count = new int?(configLevel.MaxIntersectionCount);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000084B8 File Offset: 0x000066B8
		private bool ResolveIntersectionFullySpecified(IntermediateDataReduction reduction, DataVolumeLevel configLevel)
		{
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Intersection as IntermediateSimpleLimit;
			if (intermediateSimpleLimit.Count.Value > configLevel.MaxIntersectionCount)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedIntersectionReductionAlgorithmExceedsMaxIntersections(EngineMessageSeverity.Warning, (long)intermediateSimpleLimit.Count.Value));
				intermediateSimpleLimit.Count = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008520 File Offset: 0x00006720
		private void AssignDisablePreserveKeyPoints(IReadOnlyList<QueryMember> members, IntermediateSimpleLimit limit, DataReductionAxisTelemetry telemetry)
		{
			int num;
			if (!DataReductionResolver.TryGetDistinctValueCountFromStats(members, telemetry, false, out num))
			{
				return;
			}
			limit.DisablePreserveKeyPoints = limit.Kind == IntermediateSimpleLimitKind.Sample && members.Count == 1 && members[0].Group.HasAllGroupKeysFromSameEntity() && num <= 10;
			if (limit.DisablePreserveKeyPoints)
			{
				telemetry.AddFlag("NoKeyPtSmpl");
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008584 File Offset: 0x00006784
		private void ResolvePrimaryAndSecondaryWithWindow(IntermediateDataReduction reduction, DataVolumeLevel configLevel, IntermediateDataWindow window, int maxIntersectionCount)
		{
			if (window.Count == null)
			{
				window.Count = new int?(configLevel.WindowSize);
			}
			if (window.Count.Value > maxIntersectionCount)
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedDataWindowSizeExceedsMaxIntersections(EngineMessageSeverity.Warning, window.Count.Value, DataShapeGenerationMessagePhrases.ReduceDataWindowToDefault(configLevel.WindowSize)));
				window.Count = new int?(configLevel.WindowSize);
			}
			IntermediateSimpleLimit intermediateSimpleLimit = reduction.Secondary as IntermediateSimpleLimit;
			if (intermediateSimpleLimit.Count != null)
			{
				if (window.Count.Value * intermediateSimpleLimit.Count.Value > maxIntersectionCount)
				{
					int num = Math.Max(1, DataReductionMath.DivideAndCeiling(maxIntersectionCount, window.Count.Value));
					this._context.ErrorContext.Register(DataShapeGenerationMessages.SpecifiedLimitExceedsMaxIntersections(EngineMessageSeverity.Warning, intermediateSimpleLimit.Count.Value, DataShapeGenerationMessagePhrases.ReduceLimitTo(num)));
					intermediateSimpleLimit.Count = new int?(num);
					return;
				}
			}
			else
			{
				intermediateSimpleLimit.Count = new int?(Math.Max(1, DataReductionMath.DivideAndCeiling(maxIntersectionCount, window.Count.Value)));
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000086BC File Offset: 0x000068BC
		private DataVolumeLevel GetConfigReductionLevelOrDefault(IntermediateDataReduction reduction)
		{
			DataVolumeLevel dataVolumeLevel;
			if (!this._config.TryGetLevel(reduction.DataVolume.Value, out dataVolumeLevel))
			{
				this._context.ErrorContext.Register(DataShapeGenerationMessages.NonexistentDataVolumeLevel(EngineMessageSeverity.Warning, reduction.DataVolume.Value));
				reduction.DataVolume = new int?(this._config.DefaultDataVolume);
				this._config.TryGetLevel(this._config.DefaultDataVolume, out dataVolumeLevel);
			}
			return dataVolumeLevel;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000873C File Offset: 0x0000693C
		private bool ArePrimaryAndSecondaryFullySpecified(IntermediateDataReduction reduction)
		{
			return (this._projections.PrimaryMembers.Count == 0 || DataReductionResolver.IsReductionAlgorithmFullySpecified(reduction.Primary)) && (this._projections.SecondaryMembers.Count == 0 || DataReductionResolver.IsReductionAlgorithmFullySpecified(reduction.Secondary));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008789 File Offset: 0x00006989
		private bool IsIntersectionFullySpecified(IntermediateDataReduction reduction)
		{
			return DataReductionResolver.IsReductionAlgorithmFullySpecified(reduction.Intersection);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00008796 File Offset: 0x00006996
		private static bool IsReductionAlgorithmFullySpecified(IntermediateReductionAlgorithm algorithm)
		{
			return algorithm != null && algorithm.IsFullySpecified();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000087A4 File Offset: 0x000069A4
		private static int GetDistinctValueCountFromStatsOrDefault(IReadOnlyList<QueryMember> members, int defaultCount, DataReductionAxisTelemetry algorithmTelemetry)
		{
			int num;
			if (!DataReductionResolver.TryGetDistinctValueCountFromStats(members, algorithmTelemetry, true, out num))
			{
				num = defaultCount;
			}
			return num;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000087C0 File Offset: 0x000069C0
		private bool TryGetDistinctValueCountFromFilters(IReadOnlyList<QueryMember> members, DataReductionAxisTelemetry algorithmTelemetry, out int count)
		{
			if (members.Count != 1)
			{
				count = 0;
				return false;
			}
			if (this._filterState == null)
			{
				this._filterState = DiscreteFilterAnalyzer.Analyze(this._query.Where);
			}
			if (!DataReductionResolver.TryGetDistinctCountForGroup(members[0], new DataReductionResolver.TryGetDistinctCountForField(this._filterState.TryGetDiscreteCount), out count))
			{
				count = 0;
				return false;
			}
			if (count == 0)
			{
				return false;
			}
			algorithmTelemetry.FilterDistinctCount = count;
			return true;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008830 File Offset: 0x00006A30
		public static bool ShouldUseNoStatisticsConfig(QueryProjections projections)
		{
			return (!projections.PrimaryMembers.IsNullOrEmpty<QueryMember>() || !projections.SecondaryMembers.IsNullOrEmpty<QueryMember>()) && (!DataReductionResolver.HaveStatistics(projections.PrimaryMembers) || (projections.SecondaryMembers.Count > 0 && !DataReductionResolver.HaveStatistics(projections.SecondaryMembers)));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008888 File Offset: 0x00006A88
		public static bool HaveStatistics(IReadOnlyList<QueryMember> members)
		{
			foreach (QueryMember queryMember in members)
			{
				DataReductionResolver.TryGetDistinctCountForField tryGetDistinctCountForField;
				if ((tryGetDistinctCountForField = DataReductionResolver.<>O.<0>__TryGetDistinctCountStats) == null)
				{
					tryGetDistinctCountForField = (DataReductionResolver.<>O.<0>__TryGetDistinctCountStats = new DataReductionResolver.TryGetDistinctCountForField(DataReductionResolver.TryGetDistinctCountStats));
				}
				int num;
				if (!DataReductionResolver.TryGetDistinctCountForGroup(queryMember, tryGetDistinctCountForField, out num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000088F4 File Offset: 0x00006AF4
		private static bool TryGetDistinctValueCountFromStats(IReadOnlyList<QueryMember> members, DataReductionAxisTelemetry algorithmTelemetry, bool reportTelemetry, out int count)
		{
			QueryGroup group = members[0].Group;
			if (members.Count == 1)
			{
				QueryMember queryMember = members[0];
				DataReductionResolver.TryGetDistinctCountForField tryGetDistinctCountForField;
				if ((tryGetDistinctCountForField = DataReductionResolver.<>O.<0>__TryGetDistinctCountStats) == null)
				{
					tryGetDistinctCountForField = (DataReductionResolver.<>O.<0>__TryGetDistinctCountStats = new DataReductionResolver.TryGetDistinctCountForField(DataReductionResolver.TryGetDistinctCountStats));
				}
				if (DataReductionResolver.TryGetDistinctCountForGroup(queryMember, tryGetDistinctCountForField, out count))
				{
					if (reportTelemetry)
					{
						algorithmTelemetry.StatsDistinctCount = count;
					}
					return true;
				}
			}
			if (reportTelemetry)
			{
				if (members.Count == 1 && group.Keys.Count == 1)
				{
					algorithmTelemetry.AddFlag("NoStats");
				}
				else
				{
					algorithmTelemetry.AddFlag("NoStatsMultiKey");
				}
			}
			count = -1;
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008984 File Offset: 0x00006B84
		private static bool TryGetDistinctCountStats(IConceptualProperty prop, out int count)
		{
			IConceptualColumn conceptualColumn = prop as IConceptualColumn;
			ConceptualColumnStatistics conceptualColumnStatistics;
			if (conceptualColumn != null && conceptualColumn.HasStatistics(out conceptualColumnStatistics) && conceptualColumnStatistics.DistinctValueCount > 0)
			{
				count = conceptualColumnStatistics.DistinctValueCount;
				return true;
			}
			count = -1;
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000089BC File Offset: 0x00006BBC
		private static bool TryGetDistinctCountForGroup(QueryMember member, DataReductionResolver.TryGetDistinctCountForField tryGetFieldCount, out int count)
		{
			count = int.MinValue;
			if (member.Group == null || member.Group.Keys.IsNullOrEmpty<QueryGroupKey>())
			{
				return false;
			}
			IConceptualEntity conceptualEntity = null;
			foreach (QueryGroupKey queryGroupKey in member.Group.Keys)
			{
				if (queryGroupKey.IsIdentityKey)
				{
					IConceptualColumn field = queryGroupKey.Field;
					if (field == null)
					{
						return false;
					}
					if (conceptualEntity == null)
					{
						conceptualEntity = field.Entity;
					}
					else if (!conceptualEntity.Equals(field.Entity))
					{
						return false;
					}
					int num;
					if (!tryGetFieldCount(field, out num))
					{
						return false;
					}
					count = Math.Max(count, num);
				}
			}
			return count > 0;
		}

		// Token: 0x04000099 RID: 153
		internal const int MinCalculatedLimit = 10;

		// Token: 0x0400009A RID: 154
		private readonly DataShapeGenerationInternalContext _context;

		// Token: 0x0400009B RID: 155
		private readonly DataReductionConfiguration _config;

		// Token: 0x0400009C RID: 156
		private readonly QueryProjections _projections;

		// Token: 0x0400009D RID: 157
		private readonly ResolvedQueryDefinition _query;

		// Token: 0x0400009E RID: 158
		private readonly DataShapeBinding _binding;

		// Token: 0x0400009F RID: 159
		private readonly DataReductionTelemetry _telemetry;

		// Token: 0x040000A0 RID: 160
		private DiscreteFilterState _filterState;

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x0600093A RID: 2362
		private delegate bool TryGetDistinctCountForField(IConceptualProperty prop, out int count);

		// Token: 0x02000121 RID: 289
		private sealed class ScopedDataReductionResolutionState
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x00025380 File Offset: 0x00023580
			internal ScopedDataReductionResolutionState(IntermediateScopedReductionAlgorithm scopedReduction, int indexInScoped, bool wasCountSpecified)
			{
				this.ScopedReduction = scopedReduction;
				this.IndexInScoped = indexInScoped;
				this.WasCountSpecified = wasCountSpecified;
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002539D File Offset: 0x0002359D
			public IntermediateScopedReductionAlgorithm ScopedReduction { get; }

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600093F RID: 2367 RVA: 0x000253A5 File Offset: 0x000235A5
			public int IndexInScoped { get; }

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000940 RID: 2368 RVA: 0x000253AD File Offset: 0x000235AD
			// (set) Token: 0x06000941 RID: 2369 RVA: 0x000253B5 File Offset: 0x000235B5
			public bool WasCountSpecified { get; set; }
		}

		// Token: 0x02000122 RID: 290
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004C2 RID: 1218
			public static DataReductionResolver.TryGetDistinctCountForField <0>__TryGetDistinctCountStats;
		}
	}
}
