using System;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CD RID: 205
	[Obsolete("Use custom properties to report exception handling layer")]
	public enum ExceptionHandledAt
	{
		// Token: 0x040002C6 RID: 710
		Unhandled,
		// Token: 0x040002C7 RID: 711
		UserCode,
		// Token: 0x040002C8 RID: 712
		Platform
	}
}
