using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000015 RID: 21
	[Flags]
	public enum EventSourceSettings
	{
		// Token: 0x0400004F RID: 79
		Default = 0,
		// Token: 0x04000050 RID: 80
		ThrowOnEventWriteErrors = 1,
		// Token: 0x04000051 RID: 81
		EtwManifestEventFormat = 4,
		// Token: 0x04000052 RID: 82
		EtwSelfDescribingEventFormat = 8
	}
}
