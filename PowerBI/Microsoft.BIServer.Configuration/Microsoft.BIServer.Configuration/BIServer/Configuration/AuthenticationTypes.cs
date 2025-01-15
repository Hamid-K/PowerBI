using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000013 RID: 19
	[Flags]
	public enum AuthenticationTypes
	{
		// Token: 0x04000088 RID: 136
		None = 0,
		// Token: 0x04000089 RID: 137
		RSWindowsNegotiate = 1,
		// Token: 0x0400008A RID: 138
		RSWindowsKerberos = 2,
		// Token: 0x0400008B RID: 139
		RSWindowsNTLM = 4,
		// Token: 0x0400008C RID: 140
		RSWindowsBasic = 8,
		// Token: 0x0400008D RID: 141
		Custom = 16,
		// Token: 0x0400008E RID: 142
		RSForms = 32,
		// Token: 0x0400008F RID: 143
		OAuth = 64
	}
}
