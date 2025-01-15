using System;

namespace MsolapWrapper
{
	// Token: 0x02000029 RID: 41
	public enum WrapperErrorCodes
	{
		// Token: 0x04000135 RID: 309
		MsolapWrapperError,
		// Token: 0x04000136 RID: 310
		QueryMemoryLimitExceeded,
		// Token: 0x04000137 RID: 311
		QueryTimeoutExceeded,
		// Token: 0x04000138 RID: 312
		OnPremiseServiceException,
		// Token: 0x04000139 RID: 313
		OperationCancelled,
		// Token: 0x0400013A RID: 314
		ExclusivePercentileOutOfRange,
		// Token: 0x0400013B RID: 315
		QueryUserError,
		// Token: 0x0400013C RID: 316
		QuerySystemError,
		// Token: 0x0400013D RID: 317
		QueryExternalError,
		// Token: 0x0400013E RID: 318
		InvalidDateTimeValue,
		// Token: 0x0400013F RID: 319
		ProxyModelChainLimitExceeded,
		// Token: 0x04000140 RID: 320
		InsecureLibraryLoadingPatchMissing
	}
}
