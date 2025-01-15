using System;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000023 RID: 35
	[Flags]
	public enum AccessRight
	{
		// Token: 0x040000A0 RID: 160
		None = 0,
		// Token: 0x040000A1 RID: 161
		Read = 1,
		// Token: 0x040000A2 RID: 162
		Write = 2,
		// Token: 0x040000A3 RID: 163
		ReadWrite = 3,
		// Token: 0x040000A4 RID: 164
		Impersonate = 8,
		// Token: 0x040000A5 RID: 165
		ReadImpersonate = 9
	}
}
