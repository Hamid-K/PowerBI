using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum LinkOptions
	{
		// Token: 0x0400008B RID: 139
		None = 0,
		// Token: 0x0400008C RID: 140
		ExportFirstModule = 1,
		// Token: 0x0400008D RID: 141
		IgnoreUnresolvedImports = 2,
		// Token: 0x0400008E RID: 142
		ExplicitEnvironment = 4
	}
}
