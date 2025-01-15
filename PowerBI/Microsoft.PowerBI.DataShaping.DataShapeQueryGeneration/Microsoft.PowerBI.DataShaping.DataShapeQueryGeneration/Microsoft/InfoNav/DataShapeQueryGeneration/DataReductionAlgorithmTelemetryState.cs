using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	internal sealed class DataReductionAlgorithmTelemetryState
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00008E9C File Offset: 0x0000709C
		private DataReductionAlgorithmTelemetryState()
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008EA4 File Offset: 0x000070A4
		internal DataReductionAlgorithmTelemetryState(IntermediateScopedReductionAlgorithm scopedAlgorithm)
			: this(scopedAlgorithm.Algorithm, scopedAlgorithm.TelemetryId)
		{
			this._scope = new DataReductionAlgorithmScopeTelemetry(scopedAlgorithm.Scope);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008ECC File Offset: 0x000070CC
		internal DataReductionAlgorithmTelemetryState(IntermediateReductionAlgorithm algorithm, int? telemetryId)
		{
			this._telemetryId = telemetryId;
			if (algorithm.Count != null)
			{
				Util.AddToLazyList<string>(ref this._arguments, Convert.ToString(algorithm.Count.Value, CultureInfo.InvariantCulture));
			}
			if (algorithm.WarningCount != null)
			{
				Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("WrnC:{0}", algorithm.WarningCount.Value));
			}
			IntermediateDataWindow intermediateDataWindow = algorithm as IntermediateDataWindow;
			if (intermediateDataWindow != null)
			{
				this._kind = "Window";
				if (intermediateDataWindow.RestartTokens != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, "HasRestartToken");
				}
				if (intermediateDataWindow.RestartMatchingBehavior != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, intermediateDataWindow.RestartMatchingBehavior.ToString());
				}
				return;
			}
			IntermediateBinnedLineSampleLimit intermediateBinnedLineSampleLimit = algorithm as IntermediateBinnedLineSampleLimit;
			if (intermediateBinnedLineSampleLimit != null)
			{
				this._kind = "BinnedLineSample";
				if (intermediateBinnedLineSampleLimit.MinPointsPerSeries != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("MinPPS:{0}", intermediateBinnedLineSampleLimit.MinPointsPerSeries.Value));
				}
				if (intermediateBinnedLineSampleLimit.MaxPointsPerSeries != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("MaxPPS:{0}", intermediateBinnedLineSampleLimit.MaxPointsPerSeries.Value));
				}
				if (intermediateBinnedLineSampleLimit.MaxDynamicSeriesCount != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("MaxDynSrs:{0}", intermediateBinnedLineSampleLimit.MaxDynamicSeriesCount.Value));
				}
				return;
			}
			IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = algorithm as IntermediateOverlappingPointsSampleLimit;
			if (intermediateOverlappingPointsSampleLimit != null)
			{
				this._kind = "OverlappingPointsSample";
				if (intermediateOverlappingPointsSampleLimit.X != null && intermediateOverlappingPointsSampleLimit.X.Transform != DataReductionPlotAxisTransform.None)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("XTrans:{0}", intermediateOverlappingPointsSampleLimit.X.Transform));
				}
				if (intermediateOverlappingPointsSampleLimit.Y != null && intermediateOverlappingPointsSampleLimit.Y.Transform != DataReductionPlotAxisTransform.None)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("YTrans:{0}", intermediateOverlappingPointsSampleLimit.Y.Transform));
				}
				return;
			}
			IntermediateTopNPerLevelSampleLimit intermediateTopNPerLevelSampleLimit = algorithm as IntermediateTopNPerLevelSampleLimit;
			if (intermediateTopNPerLevelSampleLimit != null)
			{
				this._kind = "TopNPerLevelSample";
				ResolvedDataReductionWindowExpansionState windowExpansionState = intermediateTopNPerLevelSampleLimit.WindowExpansionState;
				if (windowExpansionState != null && windowExpansionState.Levels != null)
				{
					Util.AddToLazyList<string>(ref this._arguments, StringUtil.FormatInvariant("NumberOfLevels:{0}", windowExpansionState.Levels.Count));
				}
				return;
			}
			IntermediateSimpleLimit intermediateSimpleLimit = (IntermediateSimpleLimit)algorithm;
			this._kind = intermediateSimpleLimit.Kind.ToString();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000917D File Offset: 0x0000737D
		internal string Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009185 File Offset: 0x00007385
		internal IReadOnlyList<string> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000918D File Offset: 0x0000738D
		internal DataReductionAlgorithmScopeTelemetry Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009195 File Offset: 0x00007395
		internal int? TelemetryId
		{
			get
			{
				return this._telemetryId;
			}
		}

		// Token: 0x040000B2 RID: 178
		[DataMember(Name = "Kind", EmitDefaultValue = false, Order = 10)]
		private readonly string _kind;

		// Token: 0x040000B3 RID: 179
		[DataMember(Name = "Args", EmitDefaultValue = false, Order = 20)]
		private readonly List<string> _arguments;

		// Token: 0x040000B4 RID: 180
		[DataMember(Name = "Scope", EmitDefaultValue = false, Order = 30)]
		private readonly DataReductionAlgorithmScopeTelemetry _scope;

		// Token: 0x040000B5 RID: 181
		[DataMember(Name = "I", EmitDefaultValue = false, Order = 40)]
		private readonly int? _telemetryId;
	}
}
