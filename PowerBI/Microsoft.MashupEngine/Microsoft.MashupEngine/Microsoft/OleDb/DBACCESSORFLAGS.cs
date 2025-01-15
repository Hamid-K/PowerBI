using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001ECB RID: 7883
	public enum DBACCESSORFLAGS : uint
	{
		// Token: 0x0400639C RID: 25500
		INVALID,
		// Token: 0x0400639D RID: 25501
		PASSBYREF,
		// Token: 0x0400639E RID: 25502
		ROWDATA,
		// Token: 0x0400639F RID: 25503
		PARAMETERDATA = 4U,
		// Token: 0x040063A0 RID: 25504
		OPTIMIZED = 8U,
		// Token: 0x040063A1 RID: 25505
		INHERITED = 16U
	}
}
