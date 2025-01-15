using System;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000656 RID: 1622
	[Flags]
	public enum TraceFlags
	{
		// Token: 0x04001F47 RID: 8007
		None = 0,
		// Token: 0x04001F48 RID: 8008
		Fatal = 1,
		// Token: 0x04001F49 RID: 8009
		Error = 2,
		// Token: 0x04001F4A RID: 8010
		Warning = 4,
		// Token: 0x04001F4B RID: 8011
		Information = 8,
		// Token: 0x04001F4C RID: 8012
		Verbose = 16,
		// Token: 0x04001F4D RID: 8013
		Debug = 32,
		// Token: 0x04001F4E RID: 8014
		Data = 64,
		// Token: 0x04001F4F RID: 8015
		All = 127
	}
}
