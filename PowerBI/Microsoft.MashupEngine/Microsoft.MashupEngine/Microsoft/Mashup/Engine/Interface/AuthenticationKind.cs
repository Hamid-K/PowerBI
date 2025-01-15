using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000005 RID: 5
	public enum AuthenticationKind
	{
		// Token: 0x0400001D RID: 29
		Implicit,
		// Token: 0x0400001E RID: 30
		UsernamePassword,
		// Token: 0x0400001F RID: 31
		Windows,
		// Token: 0x04000020 RID: 32
		WebApi,
		// Token: 0x04000021 RID: 33
		OAuth2,
		// Token: 0x04000022 RID: 34
		SapBasic,
		// Token: 0x04000023 RID: 35
		Exchange,
		// Token: 0x04000024 RID: 36
		Key,
		// Token: 0x04000025 RID: 37
		Parameterized
	}
}
