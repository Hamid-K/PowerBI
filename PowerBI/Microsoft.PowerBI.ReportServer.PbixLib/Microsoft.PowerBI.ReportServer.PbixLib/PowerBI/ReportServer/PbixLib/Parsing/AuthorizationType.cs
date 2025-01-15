using System;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x02000008 RID: 8
	public enum AuthorizationType
	{
		// Token: 0x04000058 RID: 88
		Unknown,
		// Token: 0x04000059 RID: 89
		Anonymous,
		// Token: 0x0400005A RID: 90
		Integrated,
		// Token: 0x0400005B RID: 91
		Windows,
		// Token: 0x0400005C RID: 92
		UsernamePassword,
		// Token: 0x0400005D RID: 93
		Key,
		// Token: 0x0400005E RID: 94
		Impersonate,
		// Token: 0x0400005F RID: 95
		TrustedConnection
	}
}
