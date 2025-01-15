using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200005A RID: 90
	[CLSCompliant(false)]
	public enum DBCOLUMNFLAGS : uint
	{
		// Token: 0x040001FF RID: 511
		NONE,
		// Token: 0x04000200 RID: 512
		ISBOOKMARK,
		// Token: 0x04000201 RID: 513
		MAYDEFER,
		// Token: 0x04000202 RID: 514
		WRITE = 4U,
		// Token: 0x04000203 RID: 515
		WRITEUNKNOWN = 8U,
		// Token: 0x04000204 RID: 516
		ISFIXEDLENGTH = 16U,
		// Token: 0x04000205 RID: 517
		ISNULLABLE = 32U,
		// Token: 0x04000206 RID: 518
		MAYBENULL = 64U,
		// Token: 0x04000207 RID: 519
		ISLONG = 128U,
		// Token: 0x04000208 RID: 520
		ISROWID = 256U,
		// Token: 0x04000209 RID: 521
		ISROWVER = 512U,
		// Token: 0x0400020A RID: 522
		CACHEDEFERRED = 4096U
	}
}
