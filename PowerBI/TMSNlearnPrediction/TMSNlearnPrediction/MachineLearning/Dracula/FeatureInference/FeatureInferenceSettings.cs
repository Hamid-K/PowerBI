using System;

namespace Microsoft.MachineLearning.Dracula.FeatureInference
{
	// Token: 0x0200041D RID: 1053
	public struct FeatureInferenceSettings
	{
		// Token: 0x060015EF RID: 5615 RVA: 0x0007FED2 File Offset: 0x0007E0D2
		public FeatureInferenceSettings(int labelCardinality, float significanceThreshold)
		{
			this.LabelCardinality = labelCardinality;
			this.SignificanceThreshold = significanceThreshold;
		}

		// Token: 0x04000D75 RID: 3445
		public readonly int LabelCardinality;

		// Token: 0x04000D76 RID: 3446
		public readonly float SignificanceThreshold;
	}
}
