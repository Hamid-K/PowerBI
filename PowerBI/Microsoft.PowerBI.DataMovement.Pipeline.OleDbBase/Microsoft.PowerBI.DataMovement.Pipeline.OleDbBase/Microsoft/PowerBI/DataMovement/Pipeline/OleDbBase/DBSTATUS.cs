using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000063 RID: 99
	[CLSCompliant(false)]
	public enum DBSTATUS : uint
	{
		// Token: 0x0400027B RID: 635
		S_OK,
		// Token: 0x0400027C RID: 636
		E_BADACCESSOR,
		// Token: 0x0400027D RID: 637
		E_CANTCONVERTVALUE,
		// Token: 0x0400027E RID: 638
		S_ISNULL,
		// Token: 0x0400027F RID: 639
		S_TRUNCATED,
		// Token: 0x04000280 RID: 640
		E_SIGNMISMATCH,
		// Token: 0x04000281 RID: 641
		E_DATAOVERFLOW,
		// Token: 0x04000282 RID: 642
		E_CANTCREATE,
		// Token: 0x04000283 RID: 643
		E_UNAVAILABLE,
		// Token: 0x04000284 RID: 644
		E_PERMISSIONDENIED,
		// Token: 0x04000285 RID: 645
		E_INTEGRITYVIOLATION,
		// Token: 0x04000286 RID: 646
		E_SCHEMAVIOLATION,
		// Token: 0x04000287 RID: 647
		E_BADSTATUS,
		// Token: 0x04000288 RID: 648
		S_DEFAULT
	}
}
