using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200017F RID: 383
	public enum UiRequiredExceptionClassification
	{
		// Token: 0x040006DB RID: 1755
		None,
		// Token: 0x040006DC RID: 1756
		MessageOnly,
		// Token: 0x040006DD RID: 1757
		BasicAction,
		// Token: 0x040006DE RID: 1758
		AdditionalAction,
		// Token: 0x040006DF RID: 1759
		ConsentRequired,
		// Token: 0x040006E0 RID: 1760
		UserPasswordExpired,
		// Token: 0x040006E1 RID: 1761
		PromptNeverFailed,
		// Token: 0x040006E2 RID: 1762
		AcquireTokenSilentFailed
	}
}
