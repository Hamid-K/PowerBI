using System;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x02000006 RID: 6
	public enum ForecastErrorType
	{
		// Token: 0x0400002F RID: 47
		NoError,
		// Token: 0x04000030 RID: 48
		DeprecatedTooManyBelowStepValues,
		// Token: 0x04000031 RID: 49
		DetectedSeasonalityTooBig,
		// Token: 0x04000032 RID: 50
		InputSeasonalityTooBig,
		// Token: 0x04000033 RID: 51
		DataIsTooSmall,
		// Token: 0x04000034 RID: 52
		UserTargetDatesError,
		// Token: 0x04000035 RID: 53
		InvalidRangeArguments,
		// Token: 0x04000036 RID: 54
		InvalidValueArguments,
		// Token: 0x04000037 RID: 55
		TooManyMissingValues,
		// Token: 0x04000038 RID: 56
		InputDataContainsErrors,
		// Token: 0x04000039 RID: 57
		NoDominantStepDetected,
		// Token: 0x0400003A RID: 58
		OutOfMemory,
		// Token: 0x0400003B RID: 59
		Unexpected,
		// Token: 0x0400003C RID: 60
		NextTimePointDistanceTooSmall,
		// Token: 0x0400003D RID: 61
		AnchorSequenceNotFound,
		// Token: 0x0400003E RID: 62
		NoAggregationAllowed,
		// Token: 0x0400003F RID: 63
		DupAggrPolicyNotImplemented,
		// Token: 0x04000040 RID: 64
		FoundDistanceIsNotNegative,
		// Token: 0x04000041 RID: 65
		NextStepTooClose,
		// Token: 0x04000042 RID: 66
		NoBusinessStep,
		// Token: 0x04000043 RID: 67
		NumericalStepNotAssigned,
		// Token: 0x04000044 RID: 68
		InputTimeStampsNotSorted,
		// Token: 0x04000045 RID: 69
		DestinationArrayTooSmall,
		// Token: 0x04000046 RID: 70
		PopulatingHistogramFailed,
		// Token: 0x04000047 RID: 71
		BestBinNotFound,
		// Token: 0x04000048 RID: 72
		SelectedBinOutOfRange,
		// Token: 0x04000049 RID: 73
		XAxisIsNull,
		// Token: 0x0400004A RID: 74
		InvalidSeasonality,
		// Token: 0x0400004B RID: 75
		InvalidDataType
	}
}
