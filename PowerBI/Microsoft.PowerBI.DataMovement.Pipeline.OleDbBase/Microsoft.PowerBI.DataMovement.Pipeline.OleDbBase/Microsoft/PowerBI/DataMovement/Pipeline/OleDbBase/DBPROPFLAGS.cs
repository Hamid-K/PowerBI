using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000056 RID: 86
	[CLSCompliant(false)]
	public enum DBPROPFLAGS : uint
	{
		// Token: 0x040001DB RID: 475
		NOTSUPPORTED,
		// Token: 0x040001DC RID: 476
		COLUMN,
		// Token: 0x040001DD RID: 477
		DATASOURCE,
		// Token: 0x040001DE RID: 478
		DATASOURCECREATE = 4U,
		// Token: 0x040001DF RID: 479
		DATASOURCEINFO = 8U,
		// Token: 0x040001E0 RID: 480
		DBINIT = 16U,
		// Token: 0x040001E1 RID: 481
		INDEX = 32U,
		// Token: 0x040001E2 RID: 482
		ROWSET = 64U,
		// Token: 0x040001E3 RID: 483
		TABLE = 128U,
		// Token: 0x040001E4 RID: 484
		COLUMNOK = 256U,
		// Token: 0x040001E5 RID: 485
		READ = 512U,
		// Token: 0x040001E6 RID: 486
		WRITE = 1024U,
		// Token: 0x040001E7 RID: 487
		REQUIRED = 2048U,
		// Token: 0x040001E8 RID: 488
		SESSION = 4096U
	}
}
