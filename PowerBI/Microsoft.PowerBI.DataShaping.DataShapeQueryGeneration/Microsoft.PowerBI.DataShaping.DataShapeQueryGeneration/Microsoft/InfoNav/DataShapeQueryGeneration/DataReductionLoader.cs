using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001D RID: 29
	internal sealed class DataReductionLoader
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal static bool TryLoadFromDataReduction(DataShapeGenerationErrorContext errorContext, ResolvedDataReduction dataReduction, out IntermediateDataReduction dsqReduction)
		{
			dsqReduction = new IntermediateDataReduction();
			dsqReduction.DataVolume = dataReduction.DataVolume;
			if (dataReduction.Primary != null)
			{
				IntermediateReductionAlgorithm intermediateReductionAlgorithm;
				if (!DataReductionLoader.TryLoadFromDataReductionAlgorithm(errorContext, dataReduction.Primary, out intermediateReductionAlgorithm))
				{
					errorContext.Register(DataShapeGenerationMessages.CouldNotInterpretAlgorithm(EngineMessageSeverity.Error, "primary"));
					return false;
				}
				dsqReduction.Primary = intermediateReductionAlgorithm;
			}
			if (dataReduction.Secondary != null)
			{
				IntermediateReductionAlgorithm intermediateReductionAlgorithm2;
				if (!DataReductionLoader.TryLoadFromDataReductionAlgorithm(errorContext, dataReduction.Secondary, out intermediateReductionAlgorithm2))
				{
					errorContext.Register(DataShapeGenerationMessages.CouldNotInterpretAlgorithm(EngineMessageSeverity.Error, "secondary"));
					return false;
				}
				dsqReduction.Secondary = intermediateReductionAlgorithm2;
			}
			if (dataReduction.Intersection != null)
			{
				IntermediateReductionAlgorithm intermediateReductionAlgorithm3;
				if (!DataReductionLoader.TryLoadFromDataReductionAlgorithm(errorContext, dataReduction.Intersection, out intermediateReductionAlgorithm3))
				{
					errorContext.Register(DataShapeGenerationMessages.CouldNotInterpretAlgorithm(EngineMessageSeverity.Error, "intersection"));
					return false;
				}
				dsqReduction.Intersection = intermediateReductionAlgorithm3;
			}
			if (dataReduction.Scoped != null)
			{
				List<IntermediateScopedReductionAlgorithm> list;
				if (!DataReductionLoader.TryLoadScopedReductions(errorContext, dataReduction.Scoped, out list))
				{
					errorContext.Register(DataShapeGenerationMessages.CouldNotInterpretAlgorithm(EngineMessageSeverity.Error, "DataReduction.Scoped"));
					return false;
				}
				dsqReduction.Scoped = list;
			}
			return true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private static bool TryLoadScopedReductions(DataShapeGenerationErrorContext errorContext, IReadOnlyList<ResolvedScopedDataReduction> resolvedScopedReductions, out List<IntermediateScopedReductionAlgorithm> intermediateScopedReductions)
		{
			intermediateScopedReductions = new List<IntermediateScopedReductionAlgorithm>(resolvedScopedReductions.Count);
			foreach (ResolvedScopedDataReduction resolvedScopedDataReduction in resolvedScopedReductions)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm;
				if (!DataReductionLoader.TryLoadScopedReduction(errorContext, resolvedScopedDataReduction, out intermediateScopedReductionAlgorithm))
				{
					intermediateScopedReductions = null;
					return false;
				}
				intermediateScopedReductions.Add(intermediateScopedReductionAlgorithm);
			}
			return true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005D14 File Offset: 0x00003F14
		private static bool TryLoadScopedReduction(DataShapeGenerationErrorContext errorContext, ResolvedScopedDataReduction scopedReduction, out IntermediateScopedReductionAlgorithm intermediateReduction)
		{
			IntermediateReductionAlgorithm intermediateReductionAlgorithm;
			if (!DataReductionLoader.TryLoadFromDataReductionAlgorithm(errorContext, scopedReduction.Algorithm, out intermediateReductionAlgorithm))
			{
				intermediateReduction = null;
				return false;
			}
			intermediateReduction = new IntermediateScopedReductionAlgorithm
			{
				Algorithm = intermediateReductionAlgorithm,
				Scope = new IntermediateReductionScope
				{
					Primary = DataReductionLoader.ResolveScopeList(scopedReduction.Scope.Primary),
					Secondary = DataReductionLoader.ResolveScopeList(scopedReduction.Scope.Secondary)
				}
			};
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005D7C File Offset: 0x00003F7C
		private static List<int> ResolveScopeList(IReadOnlyList<int> scopes)
		{
			if (scopes.IsNullOrEmpty<int>())
			{
				return null;
			}
			return new List<int>(scopes);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D90 File Offset: 0x00003F90
		private static bool TryLoadFromDataReductionAlgorithm(DataShapeGenerationErrorContext errorContext, ResolvedDataReductionLimit algorithm, out IntermediateReductionAlgorithm dsqAlgorithm)
		{
			ResolvedDataReductionTopLimit resolvedDataReductionTopLimit = algorithm as ResolvedDataReductionTopLimit;
			if (resolvedDataReductionTopLimit != null)
			{
				dsqAlgorithm = new IntermediateSimpleLimit
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionTopLimit.Count),
					Kind = IntermediateSimpleLimitKind.Top
				};
				return true;
			}
			ResolvedDataReductionSampleLimit resolvedDataReductionSampleLimit = algorithm as ResolvedDataReductionSampleLimit;
			if (resolvedDataReductionSampleLimit != null)
			{
				dsqAlgorithm = new IntermediateSimpleLimit
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionSampleLimit.Count),
					Kind = IntermediateSimpleLimitKind.Sample
				};
				return true;
			}
			ResolvedDataReductionBottomLimit resolvedDataReductionBottomLimit = algorithm as ResolvedDataReductionBottomLimit;
			if (resolvedDataReductionBottomLimit != null)
			{
				dsqAlgorithm = new IntermediateSimpleLimit
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionBottomLimit.Count),
					Kind = IntermediateSimpleLimitKind.Bottom
				};
				return true;
			}
			ResolvedDataReductionDataWindow resolvedDataReductionDataWindow = algorithm as ResolvedDataReductionDataWindow;
			if (resolvedDataReductionDataWindow != null)
			{
				RestartMatchingBehavior? restartMatchingBehavior = resolvedDataReductionDataWindow.RestartMatchingBehavior;
				if (restartMatchingBehavior == null && !resolvedDataReductionDataWindow.RestartTokens.IsNullOrEmpty<IReadOnlyList<PrimitiveValue>>())
				{
					restartMatchingBehavior = new RestartMatchingBehavior?(RestartMatchingBehavior.IsOnOrAfter);
				}
				dsqAlgorithm = new IntermediateDataWindow
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionDataWindow.Count),
					IncludeRestartToken = true,
					RestartTokens = resolvedDataReductionDataWindow.RestartTokens,
					RestartMatchingBehavior = restartMatchingBehavior
				};
				return true;
			}
			ResolvedDataReductionBinnedLineSampleLimit resolvedDataReductionBinnedLineSampleLimit = algorithm as ResolvedDataReductionBinnedLineSampleLimit;
			if (resolvedDataReductionBinnedLineSampleLimit != null)
			{
				dsqAlgorithm = new IntermediateBinnedLineSampleLimit
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionBinnedLineSampleLimit.Count),
					MinPointsPerSeries = resolvedDataReductionBinnedLineSampleLimit.MinPointsPerSeries,
					MaxDynamicSeriesCount = resolvedDataReductionBinnedLineSampleLimit.MaxDynamicSeriesCount,
					PrimaryScalarKey = resolvedDataReductionBinnedLineSampleLimit.PrimaryScalarKey,
					WarningCount = resolvedDataReductionBinnedLineSampleLimit.WarningCount
				};
				return true;
			}
			ResolvedDataReductionOverlappingPointsSampleLimit resolvedDataReductionOverlappingPointsSampleLimit = algorithm as ResolvedDataReductionOverlappingPointsSampleLimit;
			if (resolvedDataReductionOverlappingPointsSampleLimit != null)
			{
				dsqAlgorithm = new IntermediateOverlappingPointsSampleLimit
				{
					Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionOverlappingPointsSampleLimit.Count),
					X = DataReductionLoader.CreateIntermediatePlotAxisBinding(resolvedDataReductionOverlappingPointsSampleLimit.X),
					Y = DataReductionLoader.CreateIntermediatePlotAxisBinding(resolvedDataReductionOverlappingPointsSampleLimit.Y)
				};
				return true;
			}
			ResolvedDataReductionTopNPerLevelSampleLimit resolvedDataReductionTopNPerLevelSampleLimit = algorithm as ResolvedDataReductionTopNPerLevelSampleLimit;
			if (resolvedDataReductionTopNPerLevelSampleLimit == null)
			{
				errorContext.Register(DataShapeGenerationMessages.UnexpectedReductionAlgorithmType(EngineMessageSeverity.Warning));
				dsqAlgorithm = null;
				return false;
			}
			dsqAlgorithm = new IntermediateTopNPerLevelSampleLimit
			{
				Count = DataReductionLoader.GetValidAlgorithmCount(errorContext, resolvedDataReductionTopNPerLevelSampleLimit.Count),
				WindowExpansionState = resolvedDataReductionTopNPerLevelSampleLimit.WindowExpansion
			};
			return true;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005F8C File Offset: 0x0000418C
		private static int? GetValidAlgorithmCount(DataShapeGenerationErrorContext errorContext, int? count)
		{
			if (count != null)
			{
				int? num = count;
				int num2 = 0;
				if (!((num.GetValueOrDefault() > num2) & (num != null)))
				{
					errorContext.Register(DataShapeGenerationMessages.InvalidDataReductionAlgorithmCount(EngineMessageSeverity.Warning, count.Value));
					return null;
				}
			}
			return count;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005FD8 File Offset: 0x000041D8
		private static IntermediatePlotAxisBinding CreateIntermediatePlotAxisBinding(ResolvedDataReductionPlotAxisBinding inBinding)
		{
			if (inBinding == null)
			{
				return null;
			}
			return new IntermediatePlotAxisBinding
			{
				Index = inBinding.Index,
				Transform = inBinding.Transform
			};
		}
	}
}
