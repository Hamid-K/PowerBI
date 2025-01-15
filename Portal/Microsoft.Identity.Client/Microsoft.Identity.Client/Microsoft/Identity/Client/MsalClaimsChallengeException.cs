using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200016A RID: 362
	public class MsalClaimsChallengeException : MsalUiRequiredException
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x0003DB0B File Offset: 0x0003BD0B
		public MsalClaimsChallengeException(string errorCode, string errorMessage)
			: base(errorCode, errorMessage)
		{
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0003DB15 File Offset: 0x0003BD15
		public MsalClaimsChallengeException(string errorCode, string errorMessage, Exception innerException)
			: this(errorCode, errorMessage, innerException, UiRequiredExceptionClassification.None)
		{
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003DB21 File Offset: 0x0003BD21
		public MsalClaimsChallengeException(string errorCode, string errorMessage, Exception innerException, UiRequiredExceptionClassification classification)
			: base(errorCode, errorMessage, innerException, classification)
		{
		}
	}
}
