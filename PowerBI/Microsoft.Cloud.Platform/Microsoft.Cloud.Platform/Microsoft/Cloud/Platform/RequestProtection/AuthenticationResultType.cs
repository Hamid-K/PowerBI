using System;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200045E RID: 1118
	public enum AuthenticationResultType
	{
		// Token: 0x04000C37 RID: 3127
		FailAndStop,
		// Token: 0x04000C38 RID: 3128
		FailAndContinue,
		// Token: 0x04000C39 RID: 3129
		Success,
		// Token: 0x04000C3A RID: 3130
		StopAndRedirect
	}
}
