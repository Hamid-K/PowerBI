using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Validation
{
	// Token: 0x020000E6 RID: 230
	internal class DataReductionValidator
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001F133 File Offset: 0x0001D333
		internal DataReductionValidator(IFeatureSwitchProvider featureSwitchProvider, DataShapeGenerationErrorContext errorContext)
		{
			this._featureSwitchProvider = featureSwitchProvider;
			this._errorContext = errorContext;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001F14C File Offset: 0x0001D34C
		internal void Validate(DataShapeBinding binding)
		{
			if (binding.DataReduction == null)
			{
				return;
			}
			if (binding.DataReduction.Primary != null && (binding.DataReduction.Primary.BinnedLineSample != null || binding.DataReduction.Primary.OverlappingPointsSample != null || binding.DataReduction.Primary.TopNPerLevel != null))
			{
				string sampleAlgorithmName = this.GetSampleAlgorithmName(binding.DataReduction.Primary);
				this.ValidateBinnedLineSample(binding, binding.DataReduction.Primary.BinnedLineSample);
				this.ValidateOverlappingPointsSample(binding.DataReduction.Primary.OverlappingPointsSample, new DataShapeBindingAxis[] { binding.Primary });
				this.ValidateTopNPerLevelSample(binding.DataReduction.Primary.TopNPerLevel, binding.Primary);
				if ((binding.DataReduction.Secondary != null || binding.DataReduction.Intersection != null) && binding.DataReduction.Scoped.IsNullOrEmptyCollection<ScopedDataReduction>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInPrimary(sampleAlgorithmName)));
				}
				this.CheckNoSecondaryAxis(binding, sampleAlgorithmName);
			}
			if (binding.DataReduction.Secondary != null && (binding.DataReduction.Secondary.BinnedLineSample != null || binding.DataReduction.Secondary.OverlappingPointsSample != null || binding.DataReduction.Secondary.TopNPerLevel != null))
			{
				string sampleAlgorithmName2 = this.GetSampleAlgorithmName(binding.DataReduction.Secondary);
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInSecondary(sampleAlgorithmName2)));
			}
			DataReductionAlgorithm secondary = binding.DataReduction.Secondary;
			if (((secondary != null) ? secondary.Window : null) != null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInSecondary("DataWindow")));
			}
			if (binding.DataReduction.Intersection != null && (binding.DataReduction.Intersection.BinnedLineSample != null || binding.DataReduction.Intersection.OverlappingPointsSample != null))
			{
				string sampleAlgorithmName3 = this.GetSampleAlgorithmName(binding.DataReduction.Intersection);
				this.ValidateBinnedLineSample(binding, binding.DataReduction.Intersection.BinnedLineSample);
				this.ValidateOverlappingPointsSample(binding.DataReduction.Intersection.OverlappingPointsSample, new DataShapeBindingAxis[] { binding.Primary, binding.Secondary });
				if ((binding.DataReduction.Primary != null || binding.DataReduction.Secondary != null) && binding.DataReduction.Scoped.IsNullOrEmptyCollection<ScopedDataReduction>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInIntersection(sampleAlgorithmName3)));
				}
				this.CheckSecondaryAxisProjectionsExist(binding, sampleAlgorithmName3);
			}
			if (binding.DataReduction.Intersection != null && binding.DataReduction.Intersection.TopNPerLevel != null && binding.DataReduction.Scoped.IsNullOrEmptyCollection<ScopedDataReduction>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInIntersection("TopNPerLevel")));
			}
			this.ValidateScoped(binding);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		private string GetSampleAlgorithmName(DataReductionAlgorithm algorithm)
		{
			if (algorithm.Sample != null)
			{
				return "Sampling";
			}
			if (algorithm.BinnedLineSample != null)
			{
				return "BinnedLineSampling";
			}
			if (algorithm.OverlappingPointsSample != null)
			{
				return "OverlappingPointsSampling";
			}
			if (algorithm.TopNPerLevel != null)
			{
				return "TopNPerLevel";
			}
			return "Unknown";
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001F502 File Offset: 0x0001D702
		private void CheckNoSecondaryAxis(DataShapeBinding binding, string algorithName)
		{
			if (binding.Secondary != null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInPrimaryWithSecondaryAxis(algorithName)));
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001F52C File Offset: 0x0001D72C
		private void CheckSecondaryAxisProjectionsExist(DataShapeBinding binding, string algorithName)
		{
			if (binding.Secondary != null && !binding.Secondary.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				bool flag = false;
				using (IEnumerator<DataShapeBindingAxisGrouping> enumerator = binding.Secondary.Groupings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Projections.IsNullOrEmptyCollection<int>())
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidDataReductionInIntersectionWithNoSecondarySeries(algorithName)));
				}
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001F5C4 File Offset: 0x0001D7C4
		private void ValidateBinnedLineSample(DataShapeBinding binding, DataReductionBinnedLineSampleLimit limit)
		{
			if (limit == null)
			{
				return;
			}
			if (binding.Primary != null && binding.Primary.Groupings != null && binding.Primary.Groupings.Count == 1 && binding.Primary.Groupings[0].Projections.IsNullOrEmptyCollection<int>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedDataShapeBinding(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidBinnedLineSampleDataReductionWithNoPrimaryProjections()));
			}
			if (limit.PrimaryScalarKey != null && !DataReductionValidator.IsValidPrimaryScalarKeyIndex(binding, limit.PrimaryScalarKey.Value))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidPrimaryScalarKeyIndex(EngineMessageSeverity.Error, limit.PrimaryScalarKey.Value));
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001F684 File Offset: 0x0001D884
		private void ValidateOverlappingPointsSample(DataReductionOverlappingPointsSampleLimit limit, params DataShapeBindingAxis[] axes)
		{
			if (limit == null)
			{
				return;
			}
			if (axes.IsNullOrEmpty<DataShapeBindingAxis>())
			{
				return;
			}
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < axes.Length; i++)
			{
				bool flag;
				SemanticQueryDataShapeCommandValidator.AddIndices(axes[i], hashSet, out flag, false);
			}
			this.ValidatePlotAxisBindingIndex(limit.X, hashSet);
			this.ValidatePlotAxisBindingIndex(limit.Y, hashSet);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		private void ValidatePlotAxisBindingIndex(DataReductionPlotAxisBinding plotAxisBinding, HashSet<int> referencedIndices)
		{
			if (plotAxisBinding != null && !referencedIndices.Contains(plotAxisBinding.Index))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidPlotAxisBindingIndex(EngineMessageSeverity.Error, plotAxisBinding.Index));
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001F710 File Offset: 0x0001D910
		private static bool IsValidPrimaryScalarKeyIndex(DataShapeBinding binding, int primaryScalarKeyIndex)
		{
			HashSet<int> hashSet = new HashSet<int>();
			return !(binding.Primary == null) && binding.Primary.Groupings != null && SemanticQueryDataShapeCommandValidator.TryAddIndices(binding.Primary, hashSet) && hashSet.Contains(primaryScalarKeyIndex);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001F758 File Offset: 0x0001D958
		private void ValidateTopNPerLevelSample(DataReductionTopNPerLevelSampleLimit topNPerLevelLimit, DataShapeBindingAxis bindingAxis)
		{
			if (topNPerLevelLimit == null)
			{
				return;
			}
			DataReductionWindowExpansionStateValidator.Validate(this._errorContext, topNPerLevelLimit.WindowExpansion, bindingAxis);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001F778 File Offset: 0x0001D978
		private void ValidateScoped(DataShapeBinding binding)
		{
			IList<ScopedDataReduction> scoped = binding.DataReduction.Scoped;
			if (scoped.IsNullOrEmptyCollection<ScopedDataReduction>())
			{
				return;
			}
			if (scoped.Count > 10)
			{
				this._errorContext.Register(DataShapeGenerationMessages.TooManyScopedDataReductions(EngineMessageSeverity.Error, scoped.Count, 10));
				return;
			}
			if (binding.DataReduction.Intersection != null)
			{
				this._errorContext.Register(DataShapeGenerationMessages.ScopedReductionWithIntersectionReduction(EngineMessageSeverity.Error));
			}
			foreach (ScopedDataReduction scopedDataReduction in scoped)
			{
				DataReductionScope scope = scopedDataReduction.Scope;
				if (!scope.Primary.IsNullOrEmptyCollection<int>() && !scope.Secondary.IsNullOrEmptyCollection<int>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.ScopedDataReductionIntersectionScope(EngineMessageSeverity.Error));
				}
				DataReductionAlgorithm algorithm = scopedDataReduction.Algorithm;
				this.ValidateScopeForAllowedAlgorithms(algorithm, binding, scopedDataReduction);
				this.ValidateRestrictedAlgorithms(algorithm, scope, binding);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001F864 File Offset: 0x0001DA64
		private void ValidateScopeForAllowedAlgorithms(DataReductionAlgorithm algorithm, DataShapeBinding binding, ScopedDataReduction scopedDataReduction)
		{
			if (algorithm.Top != null || algorithm.Sample != null || algorithm.Bottom != null || algorithm.Window != null)
			{
				this.ValidateScopeIsSubsetOfGroupings(binding, scopedDataReduction);
				this.ValidateScopedDataWindow(algorithm.Window, binding, scopedDataReduction);
				return;
			}
			if (algorithm.BinnedLineSample != null || algorithm.OverlappingPointsSample != null || algorithm.TopNPerLevel != null)
			{
				this.ValidateScopeIsFullQuery(binding, scopedDataReduction);
				return;
			}
			this._errorContext.Register(DataShapeGenerationMessages.UnexpectedReductionAlgorithmType(EngineMessageSeverity.Error));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001F904 File Offset: 0x0001DB04
		private void ValidateScopedDataWindow(DataReductionDataWindow window, DataShapeBinding binding, ScopedDataReduction scopedDataReduction)
		{
			if (window == null)
			{
				return;
			}
			if (!window.RestartTokens.IsNullOrEmptyCollection<IList<string>>())
			{
				IList<int> primary = scopedDataReduction.Scope.Primary;
				DataShapeBindingAxis primary2 = binding.Primary;
				if (!this.DoesScopeCoverFullAxis(primary, (primary2 != null) ? primary2.Groupings : null))
				{
					RestartMatchingBehavior? restartMatchingBehavior = window.RestartMatchingBehavior;
					RestartMatchingBehavior restartMatchingBehavior2 = RestartMatchingBehavior.IsAfter;
					if (!((restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2) & (restartMatchingBehavior != null)))
					{
						this._errorContext.Register(DataShapeGenerationMessages.MissingIsAfterWithPrimaySubsetCovered(EngineMessageSeverity.Error));
						return;
					}
				}
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001F97C File Offset: 0x0001DB7C
		private void ValidateRestrictedAlgorithms(DataReductionAlgorithm algorithm, DataReductionScope scope, DataShapeBinding binding)
		{
			if (algorithm.Window != null)
			{
				if (!scope.Secondary.IsNullOrEmptyCollection<int>() || scope.Primary.IsNullOrEmptyCollection<int>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidWindowScope(EngineMessageSeverity.Error));
					return;
				}
				for (int i = 0; i < scope.Primary.Count; i++)
				{
					if (scope.Primary[i] != i)
					{
						this._errorContext.Register(DataShapeGenerationMessages.InvalidWindowScope(EngineMessageSeverity.Error));
						return;
					}
				}
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001F9FC File Offset: 0x0001DBFC
		private void ValidateScopeIsSubsetOfGroupings(DataShapeBinding binding, ScopedDataReduction scopedDataReduction)
		{
			DataReductionScope scope = scopedDataReduction.Scope;
			IList<int> primary = scope.Primary;
			DataShapeBindingAxis primary2 = binding.Primary;
			this.EnsureValidDataReductionScopeIndices(primary, (primary2 != null) ? primary2.Groupings : null);
			IList<int> secondary = scope.Secondary;
			DataShapeBindingAxis secondary2 = binding.Secondary;
			this.EnsureValidDataReductionScopeIndices(secondary, (secondary2 != null) ? secondary2.Groupings : null);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001FA4C File Offset: 0x0001DC4C
		private void EnsureValidDataReductionScopeIndices(IList<int> scope, IList<DataShapeBindingAxisGrouping> axisGroupings)
		{
			if (!scope.IsNullOrEmptyCollection<int>())
			{
				int num = ((axisGroupings != null) ? axisGroupings.Count : 0);
				if (scope[scope.Count - 1] > num - 1)
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidScopedDataReductionIndices(EngineMessageSeverity.Error));
					return;
				}
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001FA94 File Offset: 0x0001DC94
		private void ValidateScopeIsFullQuery(DataShapeBinding binding, ScopedDataReduction scopedDataReduction)
		{
			DataReductionScope scope = scopedDataReduction.Scope;
			IList<int> primary = scope.Primary;
			DataShapeBindingAxis primary2 = binding.Primary;
			this.EnsureDataReductionScopeIsFullAxis(primary, (primary2 != null) ? primary2.Groupings : null);
			IList<int> secondary = scope.Secondary;
			DataShapeBindingAxis secondary2 = binding.Secondary;
			this.EnsureDataReductionScopeIsFullAxis(secondary, (secondary2 != null) ? secondary2.Groupings : null);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		private void EnsureDataReductionScopeIsFullAxis(IList<int> scope, IList<DataShapeBindingAxisGrouping> axisGrouping)
		{
			if (!this.DoesScopeCoverFullAxis(scope, axisGrouping))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidScopedDataReductionAlgorithm(EngineMessageSeverity.Error));
				return;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001FB04 File Offset: 0x0001DD04
		private bool DoesScopeCoverFullAxis(IList<int> scope, IList<DataShapeBindingAxisGrouping> axisGrouping)
		{
			int num = ((scope != null) ? scope.Count : 0);
			int num2 = ((axisGrouping != null) ? axisGrouping.Count : 0);
			return num == num2 && (num == 0 || scope[num - 1] == num2 - 1);
		}

		// Token: 0x04000423 RID: 1059
		private const int ScopedDataReductionMax = 10;

		// Token: 0x04000424 RID: 1060
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x04000425 RID: 1061
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
