using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000021 RID: 33
	[Flags]
	public enum EventManifestOptions
	{
		// Token: 0x0400008F RID: 143
		None = 0,
		// Token: 0x04000090 RID: 144
		Strict = 1,
		// Token: 0x04000091 RID: 145
		AllCultures = 2,
		// Token: 0x04000092 RID: 146
		OnlyIfNeededForRegistration = 4,
		// Token: 0x04000093 RID: 147
		AllowEventSourceOverride = 8
	}
}
