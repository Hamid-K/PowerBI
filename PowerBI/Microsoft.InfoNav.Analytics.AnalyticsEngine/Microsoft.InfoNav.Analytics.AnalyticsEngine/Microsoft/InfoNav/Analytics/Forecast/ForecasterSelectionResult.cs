using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x0200002E RID: 46
	internal sealed class ForecasterSelectionResult
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00005415 File Offset: 0x00003615
		internal ForecasterSelectionResult(Algorithm algorithm, int? season = null, int? holdOutSize = null, SingleSpectrumAnalysisForecaster.SSAModelInfo ssaModelInfo = null, double? etsValidationError = null, double? ssaValidationError = null)
		{
			this.Algorithm = algorithm;
			this.SuggestedSeason = season;
			this.HoldOutSize = holdOutSize;
			this.SSAModelInfo = ssaModelInfo;
			this.ETSValidationError = etsValidationError;
			this.SSAValidationError = ssaValidationError;
		}

		// Token: 0x040000EA RID: 234
		internal static readonly ForecasterSelectionResult Default = new ForecasterSelectionResult(Algorithm.Auto, null, null, null, null, null);

		// Token: 0x040000EB RID: 235
		internal readonly Algorithm Algorithm;

		// Token: 0x040000EC RID: 236
		internal readonly int? SuggestedSeason;

		// Token: 0x040000ED RID: 237
		internal readonly int? HoldOutSize;

		// Token: 0x040000EE RID: 238
		internal readonly double? ETSValidationError;

		// Token: 0x040000EF RID: 239
		internal readonly double? SSAValidationError;

		// Token: 0x040000F0 RID: 240
		internal readonly SingleSpectrumAnalysisForecaster.SSAModelInfo SSAModelInfo;
	}
}
