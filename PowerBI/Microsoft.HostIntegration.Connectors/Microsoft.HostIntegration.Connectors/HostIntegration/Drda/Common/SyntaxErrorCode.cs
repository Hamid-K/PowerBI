using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007A0 RID: 1952
	public enum SyntaxErrorCode
	{
		// Token: 0x04002926 RID: 10534
		DssLengthLessThan6 = 1,
		// Token: 0x04002927 RID: 10535
		DssLengthByteNumberMismatch,
		// Token: 0x04002928 RID: 10536
		C_ByteNotD0,
		// Token: 0x04002929 RID: 10537
		F_ByteNotSupported,
		// Token: 0x0400292A RID: 10538
		ObjectLengthLessThan4 = 7,
		// Token: 0x0400292B RID: 10539
		TooBig = 9,
		// Token: 0x0400292C RID: 10540
		ObjectLengthNotAllowed = 11,
		// Token: 0x0400292D RID: 10541
		WrongExtendedLength,
		// Token: 0x0400292E RID: 10542
		RequiredObjectNotFound = 14,
		// Token: 0x0400292F RID: 10543
		TooMany,
		// Token: 0x04002930 RID: 10544
		DuplicateObjectFound = 18,
		// Token: 0x04002931 RID: 10545
		InvalidCorrelator,
		// Token: 0x04002932 RID: 10546
		RequiredValueNotFound,
		// Token: 0x04002933 RID: 10547
		DssContLessOrEqual_2 = 22,
		// Token: 0x04002934 RID: 10548
		ChainOffSameNextCorrelator = 24,
		// Token: 0x04002935 RID: 10549
		ChainOffErrorContinue = 26,
		// Token: 0x04002936 RID: 10550
		InvalidCodePoint = 29
	}
}
