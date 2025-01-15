using System;
using System.ComponentModel;
using Microsoft.MachineLearning.TimeSeriesProcessing;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000037 RID: 55
	[ImmutableObject(true)]
	internal sealed class SSAFeatures
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x000064D8 File Offset: 0x000046D8
		internal SSAFeatures(SingleSpectrumAnalysisForecaster.SSAModelInfo ssaModeInfo)
		{
			AdaptiveSingularSpectrumSequenceModeler.ModelInfo info = ssaModeInfo.Model.Info;
			this.Rank = info.Rank;
			this.WindowSize = info.WindowSize;
			this.IsTrained = info.IsTrained;
			this.IsNaiveModelTrained = info.IsNaiveModelTrained;
			this.IsExponentialTrendPresent = info.IsExponentialTrendPresent;
			this.IsPolynomialTrendPresent = info.IsPolynomialTrendPresent;
			this.IsStabilized = info.IsStabilized;
			this.IsArtificialSeasonalityRemoved = info.IsArtificialSeasonalityRemoved;
			this.ExponentialTrendFactor = info.ExponentialTrendFactor;
		}

		// Token: 0x04000118 RID: 280
		internal readonly int Rank;

		// Token: 0x04000119 RID: 281
		internal readonly int WindowSize;

		// Token: 0x0400011A RID: 282
		internal readonly bool IsTrained;

		// Token: 0x0400011B RID: 283
		internal readonly bool IsNaiveModelTrained;

		// Token: 0x0400011C RID: 284
		internal readonly bool IsExponentialTrendPresent;

		// Token: 0x0400011D RID: 285
		internal readonly bool IsPolynomialTrendPresent;

		// Token: 0x0400011E RID: 286
		internal readonly bool IsStabilized;

		// Token: 0x0400011F RID: 287
		internal readonly bool IsArtificialSeasonalityRemoved;

		// Token: 0x04000120 RID: 288
		internal readonly double ExponentialTrendFactor;
	}
}
