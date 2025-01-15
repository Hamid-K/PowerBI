using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x0200002B RID: 43
	internal static class ForecastConstants
	{
		// Token: 0x040000B3 RID: 179
		internal const string XInputRoleName = "X";

		// Token: 0x040000B4 RID: 180
		internal const string YInputRoleName = "Y";

		// Token: 0x040000B5 RID: 181
		internal const string ForecastColumnName = "Forecast";

		// Token: 0x040000B6 RID: 182
		internal const string ForecastRoleName = "Forecast";

		// Token: 0x040000B7 RID: 183
		internal const string LowerBoundColumnName = "LowerBound";

		// Token: 0x040000B8 RID: 184
		internal const string LowerBoundRoleName = "LowerBound";

		// Token: 0x040000B9 RID: 185
		internal const string UpperBoundColumnName = "UpperBound";

		// Token: 0x040000BA RID: 186
		internal const string UpperBoundRoleName = "UpperBound";

		// Token: 0x040000BB RID: 187
		internal const string ForecastUnit = "Unit";

		// Token: 0x040000BC RID: 188
		internal const string ForecastLength = "ForecastLength";

		// Token: 0x040000BD RID: 189
		internal const string IgnoreLast = "IgnoreLast";

		// Token: 0x040000BE RID: 190
		internal const string MaxSeasonality = "MaxSeasonality";

		// Token: 0x040000BF RID: 191
		internal const string ConfidenceLevel = "ConfidenceLevel";

		// Token: 0x040000C0 RID: 192
		internal const string DataStepUnit = "DataStepUnit";

		// Token: 0x040000C1 RID: 193
		internal const string DataStepSize = "DataStepSize";

		// Token: 0x040000C2 RID: 194
		internal const string ForecastAlgorithm = "ForecastAlgorithm";

		// Token: 0x040000C3 RID: 195
		internal const string ConsumeLength = "ConsumeLength";

		// Token: 0x040000C4 RID: 196
		internal const int ETSMinimumTrainSize = 2;

		// Token: 0x040000C5 RID: 197
		internal const int SSAMinimumTrainSize = 6;

		// Token: 0x040000C6 RID: 198
		internal const int MaximumTrainSize = 10000;

		// Token: 0x040000C7 RID: 199
		internal const int MaximumWindowSize = 145;

		// Token: 0x040000C8 RID: 200
		internal const int MinimumWindowSize = 2;

		// Token: 0x040000C9 RID: 201
		internal const float DefaultConfidence = 0.95f;

		// Token: 0x040000CA RID: 202
		internal const int MaxForecastLength = 5000;

		// Token: 0x040000CB RID: 203
		internal const int IgnoreAnchorSequenceNotFoundThreshold = 1500;

		// Token: 0x040000CC RID: 204
		internal const int MaximumTrainingSizeToRunForecastSelector = 2500;

		// Token: 0x040000CD RID: 205
		internal const double HoldoutPercentage = 0.2;

		// Token: 0x040000CE RID: 206
		internal const int MinimumHoldoutSize = 4;

		// Token: 0x040000CF RID: 207
		internal const int MaximumHoldoutSize = 12;

		// Token: 0x040000D0 RID: 208
		internal const double MinimumSMAPEThreshold = 1E-05;

		// Token: 0x040000D1 RID: 209
		internal const int NumberOfForwardStepsToComputeTrainingError = 10;

		// Token: 0x040000D2 RID: 210
		internal const double PreProcessorMemoryRatio = 1.4;

		// Token: 0x040000D3 RID: 211
		internal const int TriggerSSASeasonLimit = 32;

		// Token: 0x040000D4 RID: 212
		internal const int StepsAheaderForComputingTrainingError = 20;

		// Token: 0x040000D5 RID: 213
		internal const double ForecasterSelectionThresholdETS = 1.1;

		// Token: 0x040000D6 RID: 214
		internal const double ForecasterSelectionThresholdSSA = 20.0;

		// Token: 0x040000D7 RID: 215
		internal const double MinimumDifferenceinX = 1E-10;

		// Token: 0x040000D8 RID: 216
		internal const double MinimumRelativeDifferenceinX = 1E-08;

		// Token: 0x040000D9 RID: 217
		internal const double BoundTolerance = 1E-05;
	}
}
