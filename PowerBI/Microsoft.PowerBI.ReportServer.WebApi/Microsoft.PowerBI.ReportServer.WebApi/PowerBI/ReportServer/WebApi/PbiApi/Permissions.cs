using System;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002D RID: 45
	[Flags]
	internal enum Permissions
	{
		// Token: 0x0400008E RID: 142
		None = 0,
		// Token: 0x0400008F RID: 143
		Read = 1,
		// Token: 0x04000090 RID: 144
		Write = 2,
		// Token: 0x04000091 RID: 145
		ReShare = 4,
		// Token: 0x04000092 RID: 146
		Explore = 8,
		// Token: 0x04000093 RID: 147
		CopyOnWrite = 16,
		// Token: 0x04000094 RID: 148
		WriteInReadOnlyGroup = 32,
		// Token: 0x04000095 RID: 149
		ReadWrite = 3,
		// Token: 0x04000096 RID: 150
		ReadWriteInReadOnlyGroup = 35,
		// Token: 0x04000097 RID: 151
		ReadReshare = 5,
		// Token: 0x04000098 RID: 152
		All = 7,
		// Token: 0x04000099 RID: 153
		ReadExplore = 9,
		// Token: 0x0400009A RID: 154
		ReadReshareExplore = 13,
		// Token: 0x0400009B RID: 155
		ReadWriteExplore = 11,
		// Token: 0x0400009C RID: 156
		ReadWriteReshareExplore = 15
	}
}
